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
    /// <summary>
    /// 
    /// This class serves as a way to parse files of the ONIX 3.0 standard.
    /// You can use this class to either:
    /// 
    /// a.) Deserialize and load the entire file into memory
    /// b.) Enumerate through the file record by record, loading each into memory one at a time
    ///     
    /// </summary>
    public class OnixParser : IDisposable, IEnumerable
    {
        #region CONSTANTS

        private const int CONST_DTD_REFERENCE_LENGTH = 2048;

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
                              bool LoadEntireFileIntoMemory = false)
        {
            if (!File.Exists(OnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + OnixFilepath + ") does not exist.");

            this.ParserFileInfo = OnixFilepath;
            this.ParserRVWFlag  = ReportValidationWarnings;
            this.CurrOnixReader = CreateXmlReader(this.ParserFileInfo, this.ParserRVWFlag);

            bool   ReferenceVersion = DetectDtdVersionReference(OnixFilepath);
            string sOnixMsgTag      = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            this.ParserRefVerFlag = ReferenceVersion;

            if (LoadEntireFileIntoMemory)
            {
                this.CurrOnixMessage =
                    new XmlSerializer(typeof(OnixMessage), new XmlRootAttribute(sOnixMsgTag)).Deserialize(this.CurrOnixReader) as OnixMessage;
            }
            else
                this.CurrOnixMessage = null;
        }

        public OnixParser(bool ReportValidationWarnings,
                      FileInfo OnixFilepath,
                          bool ReferenceVersion,
                          bool LoadEntireFileIntoMemory = false)
        {
            string sOnixMsgTag = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            if (!File.Exists(OnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + OnixFilepath + ") does not exist.");

            this.ParserRefVerFlag = ReferenceVersion;
            this.ParserFileInfo = OnixFilepath;
            this.ParserRVWFlag = ReportValidationWarnings;

            this.CurrOnixReader = CreateXmlReader(this.ParserFileInfo, this.ParserRVWFlag);

            if (LoadEntireFileIntoMemory)
            {
                this.CurrOnixMessage =
                    new XmlSerializer(typeof(OnixMessage), new XmlRootAttribute(sOnixMsgTag)).Deserialize(this.CurrOnixReader) as OnixMessage;
            }
        }

        /// <summary>
        /// 
        /// This method will prepare the XmlReader that we will use to read the ONIX XML file.
        /// 
        /// <param name="CurrOnixFilepath">The path to the ONIX file</param>
        /// <param name="ReportValidationWarnings">The indicator for whether we should report validation warnings to the caller</param>
        /// <returns>The XmlReader that will be used to read the ONIX file</returns>
        /// </summary>
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

        /// <summary>
        /// 
        /// This property will return the header of an ONIX file.  If the file has already been loaded 
        /// into memory, it will extract the header from the internal member reader.  If not, it will 
        /// open the file temporarily and extract it from there.
        /// 
        /// <returns>The header of the ONIX file</returns>
        /// </summary>
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

        /// <summary>
        /// 
        /// This method will help determine whether the XML structure of an ONIX file belongs to the 'Reference' type 
        /// of ONIX (i.e., verbose tags) or the 'Short' type of ONIX (i.e., alphanumeric tags).
        /// 
        /// <param name="LegacyOnixFilepath">The path to the ONIX file</param>
        /// <returns>The Boolean that indicates whether or not the ONIX file belongs to the 'Reference' type</returns>
        /// </summary>
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

    }

    /// <summary>
    ///
    /// This class can be useful in the case that one wants to iterate through an ONIX file, even if it has a bad record due to:
	/// 
	/// a.) incorrect XML syntax
	/// b.) invalid text within a XML document (like certain hexadecimal Unicode)
	/// d.) improper tag placement
	/// d.) invalid data types
	/// 
    /// In that way, the user of the class can investigate each record on a case-by-case basis, and the file can be processed
	/// without a sole record preventing the rest of the file from being handled.
	/// 
    /// NOTE: It is still recommended that the files be validated through an alternate process before using this class.
	/// 
    /// </summary> 
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
                string sInputXml = this.ProductList[CurrentIndex].OuterXml;

                try
                {
                    CurrentRecord =
                        this.ProductSerializer.Deserialize(new StringReader(sInputXml)) as OnixProduct;
                }
                catch (Exception ex)
                {
                    CurrentRecord = new OnixProduct();

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
