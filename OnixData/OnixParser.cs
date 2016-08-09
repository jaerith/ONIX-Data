using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

using OnixData.Version3;
using OnixData.Version3.Header;

namespace OnixData
{
    public class OnixParser : IDisposable, IEnumerable
    {
        #region CONSTANTS

        private const string CONST_ONIX_MESSAGE_REFERENCE_TAG = "ONIXMessage";
        private const string CONST_ONIX_MESSAGE_SHORT_TAG     = "ONIXmessage";

        private const string CONST_ONIX_HEADER_REFERENCE_TAG = "Header";
        private const string CONST_ONIX_HEADER_SHORT_TAG     = "header";

        #endregion

        private bool        ParserRefVerFlag  = false;
        private bool        ParserRVWFlag     = false;
        private FileInfo    ParserFileInfo    = null;
        private XmlReader   CurrOnixReader    = null;
        private OnixMessage CurrOnixMessage   = null;

        public bool ReferenceVersion
        {
            get { return this.ParserRefVerFlag; }
        }

        public OnixParser(FileInfo OnixFilepath, 
                              bool ReportValidationWarnings, 
                              bool ReferenceVersion, 
                              bool LoadEntireFileIntoMemory = true)
        {
            string sOnixMsgTag = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            if (!File.Exists(OnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + OnixFilepath + ") does not exist.");

            this.ParserRefVerFlag = ReferenceVersion;
            this.ParserFileInfo   = OnixFilepath;
            this.ParserRVWFlag    = ReportValidationWarnings;

            this.CurrOnixReader = CreateXmlReader(this.ParserFileInfo, this.ParserRVWFlag);

            if (LoadEntireFileIntoMemory)
            {
                this.CurrOnixMessage =
                    new XmlSerializer(typeof(OnixMessage), new XmlRootAttribute(sOnixMsgTag)).Deserialize(this.CurrOnixReader) as OnixMessage;
            }
        }

        static public XmlReader CreateXmlReader(FileInfo CurrOnixFilepath, bool ReportValidationWarnings)
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

            return XmlReader.Create(CurrOnixFilepath.FullName, settings);
        }

        public OnixHeader MessageHeader
        {
            get
            {
                string sOnixHdrTag = 
                    this.ParserRefVerFlag ? CONST_ONIX_HEADER_REFERENCE_TAG : CONST_ONIX_HEADER_SHORT_TAG;

                OnixHeader OnixHeader = new OnixHeader();

                if (this.CurrOnixMessage != null)
                    OnixHeader = this.CurrOnixMessage.Header;
                else if (this.CurrOnixReader != null)
                {
                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.Load(this.CurrOnixReader);

                    XmlNodeList HeaderList = XMLDoc.GetElementsByTagName(sOnixHdrTag);

                    if ((HeaderList != null) && (HeaderList.Count > 0))
                    {
                        XmlNode HeaderNode  = HeaderList.Item(0);
                        string  sHeaderBody = HeaderNode.OuterXml;

                        OnixHeader = 
                            new XmlSerializer(typeof(OnixHeader), new XmlRootAttribute(sOnixHdrTag))
                            .Deserialize(new StringReader(sHeaderBody)) as OnixHeader;
                    }
                }

                return OnixHeader;
            }
        }

        public OnixMessage Message
        {
            get
            {
                return this.CurrOnixMessage;
            }
        }

        public void Dispose()
        {
            if (this.CurrOnixReader != null)
                this.CurrOnixReader.Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public OnixEnumerator GetEnumerator()
        {
            return new OnixEnumerator(this, this.ParserFileInfo);
        }
    }

    public class OnixEnumerator : IDisposable, IEnumerator
    {
        private OnixParser OnixParser = null;
        private XmlReader  OnixReader = null;
        
        private int           CurrentIndex      = -1;
        private string        ProductXmlTag     = null;
        private XmlDocument   OnixDoc           = null;
        private XmlNodeList   ProductList       = null;
        private OnixProduct   CurrentRecord     = null;
        private XmlSerializer ProductSerializer = null;

        public OnixEnumerator(OnixParser ProvidedParser, FileInfo OnixFilepath) 
        {
            this.ProductXmlTag = ProvidedParser.ReferenceVersion ? "Product" : "product";

            this.OnixParser = ProvidedParser;
            this.OnixReader = OnixParser.CreateXmlReader(OnixFilepath, false);

            ProductSerializer = new XmlSerializer(typeof(OnixProduct), new XmlRootAttribute(this.ProductXmlTag));
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
                try
                {
                    CurrentRecord =
                        this.ProductSerializer.Deserialize(new StringReader(this.ProductList[CurrentIndex].OuterXml)) as OnixProduct;
                }
                catch (Exception ex)
                {
                    CurrentRecord = new OnixProduct();

                    CurrentRecord.SetParsingError(ex);
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
