using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Schema;

using OnixData.Extensions;
using OnixData.Version3;
using OnixData.Version3.Header;

namespace OnixData
{
    public class OnixPlusParser : IDisposable, IEnumerable
    {
        #region CONSTANTS

        private const int    CONST_MSG_REFERENCE_LENGTH = 500;
        private const int    CONST_BLOCK_COUNT_SIZE     = 50000000;

        private const string CONST_ONIX_MESSAGE_REFERENCE_TAG = "ONIXMessage";
        private const string CONST_ONIX_MESSAGE_SHORT_TAG     = "ONIXmessage";

        private const string CONST_ONIX_HEADER_REFERENCE_TAG = "Header";
        private const string CONST_ONIX_HEADER_SHORT_TAG     = "header";

        #endregion

        private bool       ParserRefVerFlag  = false;
        private bool       ParserRVWFlag     = false;
        private bool       PerformValidFlag  = false;
        private FileInfo   ParserFileInfo    = null;

        public bool PerformValidation
        {
            get { return this.PerformValidFlag; }
        }

        public bool ReferenceVersion
        {
            get { return this.ParserRefVerFlag; }
        }

        public OnixPlusParser(FileInfo OnixFilepath, 
                                  bool ExecuteValidation,
                                  bool PreprocessOnixFile = true,
                                  bool FilterBadEncodings = false)
        {
            if (!File.Exists(OnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + OnixFilepath + ") does not exist.");

            this.ParserFileInfo   = OnixFilepath;
            this.ParserRVWFlag    = true;
            this.PerformValidFlag = ExecuteValidation;

            if (PreprocessOnixFile)
                OnixFilepath.ReplaceIsoLatinEncodingsMT(true, FilterBadEncodings);

            bool   ReferenceVersion = DetectVersionReference(OnixFilepath);
            string sOnixMsgTag      = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            this.ParserRefVerFlag = ReferenceVersion;
        }

        public OnixPlusParser(bool ExecuteValidation,
                          FileInfo OnixFilepath,
                              bool ReferenceVersion,
                              bool PreprocessOnixFile = true,
                              bool FilterBadEncodings = false)
        {
            string sOnixMsgTag = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            if (!File.Exists(OnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + OnixFilepath + ") does not exist.");

            this.ParserRefVerFlag = ReferenceVersion;
            this.ParserFileInfo   = OnixFilepath;
            this.ParserRVWFlag    = true;
            this.PerformValidFlag = ExecuteValidation;

            if (PreprocessOnixFile)
                OnixFilepath.ReplaceIsoLatinEncodingsMT(true, FilterBadEncodings);
        }

        static public XmlReader CreateXmlReader(FileInfo OnixFilepath, bool ReportValidationWarnings, bool ExecutionValidation)
        {
            XmlReader OnixXmlReader = null;

            OnixXmlReader = new OnixXmlTextReader(OnixFilepath) { DtdProcessing = DtdProcessing.Ignore };

            return OnixXmlReader;
        }

        public OnixHeader MessageHeader
        {
            get
            {
                string sOnixHdrTag = 
                    this.ParserRefVerFlag ? CONST_ONIX_HEADER_REFERENCE_TAG : CONST_ONIX_HEADER_SHORT_TAG;

                OnixHeader Header = new OnixHeader();

                using (XmlReader reader = CreateXmlReader(this.ParserFileInfo, false, true))
                {
                    reader.MoveToContent();
                    for (int nLineCount = 0; reader.Read(); ++nLineCount)
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == sOnixHdrTag))
                        {
                            string sHeaderBody = reader.ReadOuterXml();

                            Header =
                                new XmlSerializer(typeof(OnixHeader), new XmlRootAttribute(sOnixHdrTag))
                                .Deserialize(new StringReader(sHeaderBody)) as OnixHeader;

                            break;
                        }
                        else if (nLineCount > 100)
                            break;
                    }
                }

                return Header;
            }
        }

        public void Dispose()
        { }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public OnixPlusEnumerator GetEnumerator()
        {
            return new OnixPlusEnumerator(this, this.ParserFileInfo);
        }

        #region Support Methods

        public bool DetectVersionReference(FileInfo OnixFileInfo)
        {
            bool bReferenceVersion = true;

            if (OnixFileInfo.Length < CONST_MSG_REFERENCE_LENGTH)
                throw new Exception("ERROR!  ONIX File is smaller than expected!");

            byte[] buffer = new byte[CONST_MSG_REFERENCE_LENGTH];
            using (FileStream fs = new FileStream(OnixFileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
            }

            string sRefMsgTag = "<" + CONST_ONIX_MESSAGE_REFERENCE_TAG;
            string sFileHead  = Encoding.Default.GetString(buffer);
            if (sFileHead.Contains(sRefMsgTag))
                bReferenceVersion = true;
            else
                bReferenceVersion = false;

            return bReferenceVersion;
        }

        #endregion
    }

    public class OnixPlusEnumerator : IDisposable, IEnumerator
    {
        private OnixPlusParser OnixParser = null;
        private XmlReader      OnixReader = null;
        
        private string        ProductXmlTag     = null;
        private OnixProduct   CurrentRecord     = null;
        private XmlSerializer ProductSerializer = null;

        public OnixPlusEnumerator(OnixPlusParser ProvidedParser, FileInfo OnixFilepath) 
        {
            this.ProductXmlTag = ProvidedParser.ReferenceVersion ? "Product" : "product";

            this.OnixParser = ProvidedParser;
            this.OnixReader = OnixPlusParser.CreateXmlReader(OnixFilepath, false, ProvidedParser.PerformValidation);

            this.OnixReader.MoveToContent();

            ProductSerializer = new XmlSerializer(typeof(OnixProduct), new XmlRootAttribute(this.ProductXmlTag));
        }

        public void Dispose()
        {
            if (this.OnixReader != null)
            {
                this.OnixReader.Close();
                this.OnixReader = null;
            }
        }

        public bool MoveNext()
        {
            bool   bResult      = false;
            string sProductBody = null;

            while (this.OnixReader.Read())
            {
                if ((this.OnixReader.NodeType == XmlNodeType.Element) && (this.OnixReader.Name == this.ProductXmlTag))
                {
                    // XElement product = XElement.ReadFrom(reader) as XElement;

                    sProductBody = this.OnixReader.ReadOuterXml();
                    break;
                }
            }

            if (!String.IsNullOrEmpty(sProductBody))
            {
                try
                {
                    CurrentRecord =
                        this.ProductSerializer.Deserialize(new StringReader(sProductBody)) as OnixProduct;

                    bResult = true;
                }
                catch (Exception ex)
                {
                    CurrentRecord = new OnixProduct();

                    CurrentRecord.SetParsingError(ex);
                    CurrentRecord.SetInputXml(sProductBody);
                }
            }

            return bResult;
        }

        public void Reset()
        {
            return;
        }

        public object Current
        {
            get
            {
                return CurrentRecord;
            }
        }
    }
}

