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

using OnixData.Legacy;
using OnixData.Extensions;

namespace OnixData
{
    /// <summary>
    /// 
    /// This class serves as a way to parse files of the ONIX 2.1 standard (and earlier).
    /// It is intended to be used when parsing very large ONIX files, in the attempt to balance 
    /// performance with memory use.
    ///     
    /// </summary>
    public class OnixLegacyPlusParser : IDisposable, IEnumerable
    {
        #region CONSTANTS

        private const int    CONST_MSG_REFERENCE_LENGTH = 512;
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

        public bool ShouldApplyDefaults { get; set; }

        public OnixLegacyPlusParser(FileInfo LegacyOnixFilepath, 
                                        bool ExecuteValidation,
                                        bool PreprocessOnixFile = true)
        {
            if (!File.Exists(LegacyOnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + LegacyOnixFilepath + ") does not exist.");

            this.ParserFileInfo      = LegacyOnixFilepath;
            this.ParserRVWFlag       = true;
            this.ShouldApplyDefaults = true;
            this.PerformValidFlag    = ExecuteValidation;

            if (PreprocessOnixFile)
                LegacyOnixFilepath.ReplaceIsoLatinEncodingsMT(true);

            bool   ReferenceVersion = DetectVersionReference(LegacyOnixFilepath);
            string sOnixMsgTag      = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            this.ParserRefVerFlag = ReferenceVersion;
        }

        public OnixLegacyPlusParser(bool ExecuteValidation,
                                FileInfo LegacyOnixFilepath,
                                    bool ReferenceVersion,
                                    bool PreprocessOnixFile = true)
        {
            string sOnixMsgTag = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            if (!File.Exists(LegacyOnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + LegacyOnixFilepath + ") does not exist.");

            this.ParserRefVerFlag    = ReferenceVersion;
            this.ParserFileInfo      = LegacyOnixFilepath;
            this.ParserRVWFlag       = true;
            this.ShouldApplyDefaults = true;
            this.PerformValidFlag    = ExecuteValidation;

            if (PreprocessOnixFile)
                LegacyOnixFilepath.ReplaceIsoLatinEncodingsMT(true);
        }

        static public XmlReader CreateXmlReader(FileInfo LegacyOnixFilepath, bool ReportValidationWarnings, bool ExecutionValidation)
        {
            XmlReader OnixXmlReader = 
                new OnixXmlTextReader(LegacyOnixFilepath) { DtdProcessing = DtdProcessing.Ignore };

            return OnixXmlReader;
        }

        public OnixLegacyHeader MessageHeader
        {
            get
            {
                string sOnixHdrTag = 
                    this.ParserRefVerFlag ? CONST_ONIX_HEADER_REFERENCE_TAG : CONST_ONIX_HEADER_SHORT_TAG;

                OnixLegacyHeader LegacyHeader = new OnixLegacyHeader();

                using (XmlReader reader = CreateXmlReader(this.ParserFileInfo, false, true))
                {
                    reader.MoveToContent();
                    for (int nLineCount = 0; reader.Read(); ++nLineCount)
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == sOnixHdrTag))
                        {
                            // XElement xHeader = XElement.ReadFrom(reader) as XElement;

                            string sHeaderBody = reader.ReadOuterXml();

                            LegacyHeader =
                                new XmlSerializer(typeof(OnixLegacyHeader), new XmlRootAttribute(sOnixHdrTag))
                                .Deserialize(new StringReader(sHeaderBody)) as OnixLegacyHeader;

                            break;
                        }
                        else if (nLineCount > 100)
                            break;
                    }
                }

                return LegacyHeader;
            }
        }

        public void Dispose()
        { }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public OnixLegacyPlusEnumerator GetEnumerator()
        {
            return new OnixLegacyPlusEnumerator(this, this.ParserFileInfo);
        }

        #region Support Methods

        public bool DetectVersionReference(FileInfo LegacyOnixFileInfo)
        {
            bool bReferenceVersion = true;

            if (LegacyOnixFileInfo.Length < CONST_MSG_REFERENCE_LENGTH)
                throw new Exception("ERROR!  ONIX File is smaller than expected!");

            byte[] buffer = new byte[CONST_MSG_REFERENCE_LENGTH];
            using (FileStream fs = new FileStream(LegacyOnixFileInfo.FullName, FileMode.Open, FileAccess.Read))
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

    public class OnixLegacyPlusEnumerator : IDisposable, IEnumerator
    {
        private OnixLegacyPlusParser OnixParser = null;
        private XmlReader            OnixReader = null;
        
        private string             ProductXmlTag     = null;
        private OnixLegacyHeader   OnixHeader        = null;
        private OnixLegacyProduct  CurrentRecord     = null;
        private XmlSerializer      ProductSerializer = null;

        public OnixLegacyPlusEnumerator(OnixLegacyPlusParser ProvidedParser, FileInfo LegacyOnixFilepath) 
        {
            this.ProductXmlTag = ProvidedParser.ReferenceVersion ? "Product" : "product";

            this.OnixParser = ProvidedParser;
            this.OnixReader = OnixLegacyPlusParser.CreateXmlReader(LegacyOnixFilepath, false, ProvidedParser.PerformValidation);

            this.OnixReader.MoveToContent();

            ProductSerializer = new XmlSerializer(typeof(OnixLegacyProduct), new XmlRootAttribute(this.ProductXmlTag));
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

            if (this.OnixHeader == null)
                this.OnixHeader = OnixParser.MessageHeader;

            do
            {
                if ((this.OnixReader.NodeType == XmlNodeType.Element) && (this.OnixReader.Name == this.ProductXmlTag))
                {
                    // XElement product = XElement.ReadFrom(reader) as XElement;

                    sProductBody = this.OnixReader.ReadOuterXml();
                    break;
                }

            } while (this.OnixReader.Read());

            if (!String.IsNullOrEmpty(sProductBody))
            {
                try
                {
                    CurrentRecord =
                        this.ProductSerializer.Deserialize(new StringReader(sProductBody)) as OnixLegacyProduct;

                    if ((CurrentRecord != null) && OnixParser.ShouldApplyDefaults)
                        CurrentRecord.ApplyHeaderDefaults(this.OnixHeader);

                    bResult = true;
                }
                catch (Exception ex)
                {
                    CurrentRecord = new OnixLegacyProduct();

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
