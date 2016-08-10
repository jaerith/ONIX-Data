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

namespace OnixData
{
    public class OnixLegacyParser : IDisposable, IEnumerable
    {
        #region CONSTANTS

        private const int    CONST_DTD_REFERENCE_LENGTH = 2048;

        private const string CONST_ONIX_MESSAGE_REFERENCE_TAG = "ONIXMessage";
        private const string CONST_ONIX_MESSAGE_SHORT_TAG     = "ONIXmessage";

        private const string CONST_ONIX_HEADER_REFERENCE_TAG = "Header";
        private const string CONST_ONIX_HEADER_SHORT_TAG     = "header";

        #endregion

        private bool              ParserRefVerFlag  = false;
        private bool              ParserRVWFlag     = false;
        private FileInfo          ParserFileInfo    = null;
        private XmlReader         LegacyOnixReader  = null;
        private OnixLegacyMessage LegacyOnixMessage = null;

        public bool ReferenceVersion
        {
            get { return this.ParserRefVerFlag; }
        }

        public OnixLegacyParser(FileInfo LegacyOnixFilepath, 
                                    bool ReportValidationWarnings, 
                                    bool LoadEntireFileIntoMemory = false)
        {
            if (!File.Exists(LegacyOnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + LegacyOnixFilepath + ") does not exist.");

            this.ParserFileInfo   = LegacyOnixFilepath;
            this.ParserRVWFlag    = ReportValidationWarnings;
            this.LegacyOnixReader = CreateXmlReader(this.ParserFileInfo, this.ParserRVWFlag);

            bool   ReferenceVersion = DetectDtdVersionReference(LegacyOnixFilepath);
            string sOnixMsgTag      = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            this.ParserRefVerFlag = ReferenceVersion;

            if (LoadEntireFileIntoMemory)
            {
                this.LegacyOnixMessage =
                    new XmlSerializer(typeof(OnixLegacyMessage), new XmlRootAttribute(sOnixMsgTag)).Deserialize(this.LegacyOnixReader) as OnixLegacyMessage;
            }
            else
                this.LegacyOnixMessage = null;
        }

        public OnixLegacyParser(FileInfo LegacyOnixFilepath,
                                    bool ReportValidationWarnings,
                                    bool ReferenceVersion,
                                    bool LoadEntireFileIntoMemory = false)
        {
            string sOnixMsgTag = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            if (!File.Exists(LegacyOnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + LegacyOnixFilepath + ") does not exist.");

            this.ParserRefVerFlag = ReferenceVersion;
            this.ParserFileInfo   = LegacyOnixFilepath;
            this.ParserRVWFlag    = ReportValidationWarnings;

            this.LegacyOnixReader = CreateXmlReader(this.ParserFileInfo, this.ParserRVWFlag);

            if (LoadEntireFileIntoMemory)
            {
                this.LegacyOnixMessage =
                    new XmlSerializer(typeof(OnixLegacyMessage), new XmlRootAttribute(sOnixMsgTag)).Deserialize(this.LegacyOnixReader) as OnixLegacyMessage;
            }
            else
                this.LegacyOnixMessage = null;
        }

        static public XmlReader CreateXmlReader(FileInfo LegacyOnixFilepath, bool ReportValidationWarnings)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel  = ConformanceLevel.Document;
            settings.ValidationType    = ValidationType.Schema;
            settings.DtdProcessing     = DtdProcessing.Parse;
            settings.ValidationFlags  |= XmlSchemaValidationFlags.ProcessInlineSchema;

            if (ReportValidationWarnings)
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            else
                settings.ValidationFlags &= ~(XmlSchemaValidationFlags.ReportValidationWarnings);

            return XmlReader.Create(LegacyOnixFilepath.FullName, settings);
        }

        public OnixLegacyHeader MessageHeader
        {
            get
            {
                string sOnixHdrTag = 
                    this.ParserRefVerFlag ? CONST_ONIX_HEADER_REFERENCE_TAG : CONST_ONIX_HEADER_SHORT_TAG;

                OnixLegacyHeader LegacyHeader = new OnixLegacyHeader();

                if (this.LegacyOnixMessage != null)
                    LegacyHeader = this.LegacyOnixMessage.Header;
                else if (this.LegacyOnixReader != null)
                {
                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.Load(this.LegacyOnixReader);

                    XmlNodeList HeaderList = XMLDoc.GetElementsByTagName(sOnixHdrTag);

                    if ((HeaderList != null) && (HeaderList.Count > 0))
                    {
                        XmlNode HeaderNode  = HeaderList.Item(0);
                        string  sHeaderBody = HeaderNode.OuterXml;

                        LegacyHeader = 
                            new XmlSerializer(typeof(OnixLegacyHeader), new XmlRootAttribute(sOnixHdrTag))
                            .Deserialize(new StringReader(sHeaderBody)) as OnixLegacyHeader;
                    }
                }

                return LegacyHeader;
            }
        }

        public OnixLegacyMessage Message
        {
            get
            {
                return LegacyOnixMessage;
            }
        }

        public void Dispose()
        {
            if (this.LegacyOnixReader != null)
                this.LegacyOnixReader.Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public OnixLegacyEnumerator GetEnumerator()
        {
            return new OnixLegacyEnumerator(this, this.ParserFileInfo);
        }

        #region Support Methods

        public bool DetectDtdVersionReference(FileInfo LegacyOnixFilepath)
        {
            bool bReferenceVersion = true;

            if (LegacyOnixFilepath.Length < CONST_DTD_REFERENCE_LENGTH)
                throw new Exception("ERROR!  ONIX File is smaller than expected!");

            byte[] buffer = new byte[CONST_DTD_REFERENCE_LENGTH];
            using (FileStream fs = new FileStream(LegacyOnixFilepath.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
            }

            string sRefMsgTag = "<" + CONST_ONIX_MESSAGE_REFERENCE_TAG + ">";
            string sFileHead  = Encoding.Default.GetString(buffer);
            if (sFileHead.Contains(sRefMsgTag))
                bReferenceVersion = true;
            else
                bReferenceVersion = false;

            return bReferenceVersion;
        }

        #endregion
    }

    public class OnixLegacyEnumerator : IDisposable, IEnumerator
    {
        private OnixLegacyParser OnixParser = null;
        private XmlReader        OnixReader = null;
        
        private int                CurrentIndex      = -1;
        private string             ProductXmlTag     = null;
        private XmlDocument        OnixDoc           = null;
        private XmlNodeList        ProductList       = null;
        private OnixLegacyProduct  CurrentRecord     = null;
        private XmlSerializer      ProductSerializer = null;

        public OnixLegacyEnumerator(OnixLegacyParser ProvidedParser, FileInfo LegacyOnixFilepath) 
        {
            this.ProductXmlTag = ProvidedParser.ReferenceVersion ? "Product" : "product";

            this.OnixParser = ProvidedParser;
            this.OnixReader = OnixLegacyParser.CreateXmlReader(LegacyOnixFilepath, false);

            ProductSerializer = new XmlSerializer(typeof(OnixLegacyProduct), new XmlRootAttribute(this.ProductXmlTag));
        }

        public void Dispose()
        {
            if (this.OnixReader != null)
                this.OnixReader.Close();
        }

        public bool MoveNext()
        {
            bool bResult = true;

            if (OnixDoc == null)
            {
                this.OnixDoc = new XmlDocument();
                this.OnixDoc.Load(this.OnixReader);

                this.ProductList = this.OnixDoc.GetElementsByTagName(this.ProductXmlTag);
            }

            if (++CurrentIndex < this.ProductList.Count)
            {
                string sInputXml = this.ProductList[CurrentIndex].OuterXml;

                try
                {
                    CurrentRecord =
                        this.ProductSerializer.Deserialize(new StringReader(sInputXml)) as OnixLegacyProduct;
                }
                catch (Exception ex)
                {
                    CurrentRecord = new OnixLegacyProduct();

                    CurrentRecord.SetParsingError(ex);
                    CurrentRecord.SetInputXml(sInputXml);
                }
            }
            else
                bResult = false;

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
