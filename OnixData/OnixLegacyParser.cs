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
    /// <summary>
    /// 
    /// This class serves as a way to parse files of the ONIX 2.1 standard (and earlier).
    /// You can use this class to either:
    /// 
    /// a.) Deserialize and load the entire file into memory
    /// b.) Enumerate through the file record by record, loading each into memory one at a time
    ///     
    /// </summary>
    public class OnixLegacyParser : IDisposable, IEnumerable
    {
        #region CONSTANTS

        private const int    CONST_MSG_REFERENCE_LENGTH = 2048;

        private const string CONST_ONIX_MESSAGE_REFERENCE_TAG = "ONIXMessage";
        private const string CONST_ONIX_MESSAGE_SHORT_TAG     = "ONIXmessage";

        private const string CONST_ONIX_HEADER_REFERENCE_TAG = "Header";
        private const string CONST_ONIX_HEADER_SHORT_TAG     = "header";

        #endregion

        private bool              ParserRefVerFlag  = false;
        private bool              ParserRVWFlag     = false;
        private bool              PerformValidFlag  = false;
        private FileInfo          ParserFileInfo    = null;
        private XmlReader         LegacyOnixReader  = null;
        private OnixLegacyMessage LegacyOnixMessage = null;

        public bool PerformValidation
        {
            get { return this.PerformValidFlag; }
        }

        public bool ReferenceVersion
        {
            get { return this.ParserRefVerFlag; }
        }

        public OnixLegacyParser(FileInfo LegacyOnixFilepath, 
                                    bool ExecuteValidation,
                                    bool PreprocessOnixFile = true, 
                                    bool LoadEntireFileIntoMemory = false)
        {
            if (!File.Exists(LegacyOnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + LegacyOnixFilepath + ") does not exist.");

            this.ParserFileInfo   = LegacyOnixFilepath;
            this.ParserRVWFlag    = true;
            this.PerformValidFlag = ExecuteValidation;

            if (PreprocessOnixFile)
                ReplaceIsoLatinEncodings(true);

            this.LegacyOnixReader = CreateXmlReader(this.ParserFileInfo, this.ParserRVWFlag, this.PerformValidFlag);

            bool   ReferenceVersion = DetectVersionReference(LegacyOnixFilepath);
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

        public OnixLegacyParser(bool ExecuteValidation,
                            FileInfo LegacyOnixFilepath,
                                bool ReferenceVersion,
                                bool PreprocessOnixFile = true,
                                bool LoadEntireFileIntoMemory = false)
        {
            string sOnixMsgTag = ReferenceVersion ? CONST_ONIX_MESSAGE_REFERENCE_TAG : CONST_ONIX_MESSAGE_SHORT_TAG;

            if (!File.Exists(LegacyOnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + LegacyOnixFilepath + ") does not exist.");

            this.ParserRefVerFlag = ReferenceVersion;
            this.ParserFileInfo   = LegacyOnixFilepath;
            this.ParserRVWFlag    = true;
            this.PerformValidFlag = ExecuteValidation;

            if (PreprocessOnixFile)
                ReplaceIsoLatinEncodings(true);

            this.LegacyOnixReader = CreateXmlReader(this.ParserFileInfo, this.ParserRVWFlag, this.PerformValidFlag);

            if (LoadEntireFileIntoMemory)
            {
                this.LegacyOnixMessage =
                    new XmlSerializer(typeof(OnixLegacyMessage), new XmlRootAttribute(sOnixMsgTag)).Deserialize(this.LegacyOnixReader) as OnixLegacyMessage;
            }
            else
                this.LegacyOnixMessage = null;
        }

        /// <summary>
        /// 
        /// This method will prepare the XmlReader that we will use to read the ONIX XML file.
        /// 
        /// <param name="CurrOnixFilepath">The path to the ONIX file</param>
        /// <param name="ReportValidationWarnings">The indicator for whether we should report validation warnings to the caller</param>
        /// <param name="ExecutionValidation">The indicator for whether or not the ONIX file should be validated</param>
        /// <returns>The XmlReader that will be used to read the ONIX file</returns>
        /// </summary>
        static public XmlReader CreateXmlReader(FileInfo LegacyOnixFilepath, bool ReportValidationWarnings, bool ExecutionValidation)
        {
            bool          bUseXSD       = true;
            bool          bUseDTD       = false;
            StringBuilder InvalidErrMsg = new StringBuilder();
            XmlReader     OnixXmlReader = null;

            XmlReaderSettings settings = new XmlReaderSettings();

            settings.ValidationType = ValidationType.None;
            settings.DtdProcessing  = DtdProcessing.Ignore;

            if (ExecutionValidation)
            {
                settings.ConformanceLevel = ConformanceLevel.Document;

                if (bUseXSD)
                {
                    /*
                     * NOTE: XSD Validation does not appear to be working correctly yet,
					 *       especially in that it always qualifies all ONIX files as valid
                     * 
                    XmlSchemaSet schemas = new XmlSchemaSet();

                    string XsdFilepath = "";

                    if (ParserRefVerFlag)
                        XsdFilepath = @"C:\ONIX_XSD\2.1\ONIX_BookProduct_Release2.1_reference.xsd";
                    else
                        XsdFilepath = @"C:\ONIX_XSD\2.1\ONIX_BookProduct_Release2.1_short.xsd";

                    schemas.Add(null, XmlReader.Create(new StringReader(File.ReadAllText(XsdFilepath))));

                    settings.Schemas        = schemas;
                    settings.ValidationType = ValidationType.Schema;                

                    if (ReportValidationWarnings)
                        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                    else
                        settings.ValidationFlags &= ~(XmlSchemaValidationFlags.ReportValidationWarnings);

                    // settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                    */
                }

                /*
                 * NOTE: DTD Validation does not appear that it will ever work correctly on the .NET platform
				 *       , especially in that it never qualifies an ONIX file as valid
                 * 
                if (bUseDTD)
                {
                    settings.DtdProcessing  = DtdProcessing.Parse;
                    settings.ValidationType = ValidationType.DTD;

                    settings.ValidationFlags |= XmlSchemaValidationFlags.AllowXmlAttributes;

                    System.Xml.XmlResolver newXmlResolver = new System.Xml.XmlUrlResolver();
                    newXmlResolver.ResolveUri(new Uri("C:\\ONIX_DTD\\2.1\\short"), "onix-international.dtd");
                    settings.XmlResolver = newXmlResolver;

                    settings.ValidationEventHandler += new ValidationEventHandler(delegate(object sender, ValidationEventArgs args)
                    {
                        // InvalidErrMsg.AppendLine(args.Message);
                        throw new Exception(args.Message);
                    });
                }
                */
            }

            OnixXmlReader = XmlReader.Create(LegacyOnixFilepath.FullName, settings);

            return OnixXmlReader;
        }

        /// <summary>
        /// 
        /// This property will return the header of an ONIX file.  If the file has already been loaded 
        /// into memory, it will extract the header from the internal member reader.  If not, it will 
        /// open the file temporarily and extract it from there.
        /// 
        /// <returns>The header of the ONIX file</returns>
        /// </summary>
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
		
        /// <summary>
        /// 
        /// Since the .NET XML parser has very limited support for incorporating DTD/XSD/ENT/ELT files, this method will 
        /// perform the work on behalf of ELT utilization (i.e., by translating any ONIX shorthand codes that map to Unicode encodings).
        /// In particular, this method places on a focus on ISO Latin encodings.
        /// 
        /// <param name="PerformInMemory">Indicates whether or not the encoding replacements are performed entirely in memory.</param>
        /// <returns>N/A</returns>
        /// </summary>
        public void ReplaceIsoLatinEncodings(bool PerformInMemory)
        {
            if ((ParserFileInfo != null) && (ParserFileInfo.Exists))
            {
                if (PerformInMemory)
                {
                    StringBuilder AllFileText = new StringBuilder(File.ReadAllText(ParserFileInfo.FullName));

                    // From the "iso-lat1.ent" file
                    AllFileText.Replace("&aacute;", "&#x000E1;"); // <!--=small a, acute accent -->
                    AllFileText.Replace("&Aacute;", "&#x000C1;"); // <!--=capital A, acute accent -->
                    AllFileText.Replace("&acirc;",  "&#x000E2;"); // <!--=small a, circumflex accent -->
                    AllFileText.Replace("&Acirc;",  "&#x000C2;"); // <!--=capital A, circumflex accent -->
                    AllFileText.Replace("&aelig;",  "&#x000E6;"); // <!--=small ae diphthong (ligature) -->
                    AllFileText.Replace("&AElig;",  "&#x000C6;"); // <!--=capital AE diphthong (ligature) -->
                    AllFileText.Replace("&agrave;", "&#x000E0;"); // !--=small a, grave accent -->
                    AllFileText.Replace("&Agrave;", "&#x000C0;"); // <!--=capital A, grave accent -->
                    AllFileText.Replace("&aring;",  "&#x000E5;"); // <!--=small a, ring -->
                    AllFileText.Replace("&Aring;",  "&#x000C5;"); // <!--=capital A, ring -->
                    AllFileText.Replace("&atilde;", "&#x000E3;"); // <!--=small a, tilde -->
                    AllFileText.Replace("&Atilde;", "&#x000C3;"); // <!--=capital A, tilde -->
                    AllFileText.Replace("&auml;",   "&#x000E4;"); // <!--=small a, dieresis or umlaut mark -->
                    AllFileText.Replace("&Auml;",   "&#x000C4;"); // <!--=capital A, dieresis or umlaut mark -->
                    AllFileText.Replace("&ccedil;", "&#x000E7;"); // <!--=small c, cedilla -->
                    AllFileText.Replace("&Ccedil;", "&#x000C7;"); // <!--=capital C, cedilla -->
                    AllFileText.Replace("&eacute;", "&#x000E9;"); // <!--=small e, acute accent -->
                    AllFileText.Replace("&Eacute;", "&#x000C9;"); // <!--=capital E, acute accent -->
                    AllFileText.Replace("&ecirc;",  "&#x000EA;"); // <!--=small e, circumflex accent -->
                    AllFileText.Replace("&Ecirc;",  "&#x000CA;"); // <!--=capital E, circumflex accent -->
                    AllFileText.Replace("&egrave;", "&#x000E8;"); // <!--=small e, grave accent -->
                    AllFileText.Replace("&Egrave;", "&#x000C8;"); // <!--=capital E, grave accent -->
                    AllFileText.Replace("&eth;",    "&#x000F0;"); // <!--=small eth, Icelandic -->
                    AllFileText.Replace("&ETH;",    "&#x000D0;"); // <!--=capital Eth, Icelandic -->
                    AllFileText.Replace("&euml;",   "&#x000EB;"); // <!--=small e, dieresis or umlaut mark -->
                    AllFileText.Replace("&Euml;",   "&#x000CB;"); // <!--=capital E, dieresis or umlaut mark -->
                    AllFileText.Replace("&iacute;", "&#x000ED;"); // <!--=small i, acute accent -->
                    AllFileText.Replace("&Iacute;", "&#x000CD;"); // <!--=capital I, acute accent -->
                    AllFileText.Replace("&icirc;",  "&#x000EE;"); // <!--=small i, circumflex accent -->
                    AllFileText.Replace("&Icirc;",  "&#x000CE;"); // <!--=capital I, circumflex accent -->
                    AllFileText.Replace("&igrave;", "&#x000EC;"); // <!--=small i, grave accent -->
                    AllFileText.Replace("&Igrave;", "&#x000CC;"); // <!--=capital I, grave accent -->
                    AllFileText.Replace("&iuml;",   "&#x000EF;"); // <!--=small i, dieresis or umlaut mark -->
                    AllFileText.Replace("&Iuml;",   "&#x000CF;"); // <!--=capital I, dieresis or umlaut mark -->
                    AllFileText.Replace("&iacute;", "&#x000ED;"); // <!--=small i, acute accent -->
                    AllFileText.Replace("&Iacute;", "&#x000CD;"); // <!--=capital I, acute accent -->
                    AllFileText.Replace("&ntilde;", "&#x000F1;"); // <!--=small n, tilde -->
                    AllFileText.Replace("&Ntilde;", "&#x000D1;"); // <!--=capital N, tilde -->
                    AllFileText.Replace("&oacute;", "&#x000F3;"); // <!--=small o, acute accent -->
                    AllFileText.Replace("&Oacute;", "&#x000D3;"); // <!--=capital O, acute accent -->
                    AllFileText.Replace("&ocirc;",  "&#x000F4;"); // <!--=small o, circumflex accent -->
                    AllFileText.Replace("&Ocirc;",  "&#x000D4;"); // <!--=capital O, circumflex accent -->
                    AllFileText.Replace("&ograve;", "&#x000F2;"); // <!--=small o, grave accent -->
                    AllFileText.Replace("&Ograve;", "&#x000D2;"); // <!--=capital O, grave accent -->
                    AllFileText.Replace("&oslash;", "&#x000F8;"); // <!--latin small letter o with stroke -->
                    AllFileText.Replace("&Oslash;", "&#x000D8;"); // <!--=capital O, slash -->
                    AllFileText.Replace("&otilde;", "&#x000F5;"); // <!--=small o, tilde -->
                    AllFileText.Replace("&Otilde;", "&#x000D5;"); // <!--=capital O, tilde -->
                    AllFileText.Replace("&ouml;",   "&#x000F6;"); // <!--=small o, dieresis or umlaut mark -->
                    AllFileText.Replace("&Ouml;",   "&#x000D6;"); // <!--=capital O, dieresis or umlaut mark -->
                    AllFileText.Replace("&szlig;",  "&#x000DF;"); // <!--=small sharp s, German (sz ligature) -->
                    AllFileText.Replace("&thorn;",  "&#x000FE;"); // <!--=small thorn, Icelandic -->
                    AllFileText.Replace("&THORN;",  "&#x000DE;"); // <!--=capital THORN, Icelandic -->
                    AllFileText.Replace("&uacute;", "&#x000FA;"); // <!--=small u, acute accent -->
                    AllFileText.Replace("&Uacute;", "&#x000DA;"); // <!--=capital U, acute accent -->
                    AllFileText.Replace("&ucirc;",  "&#x000FB;"); // <!--=small u, circumflex accent -->
                    AllFileText.Replace("&Ucirc;",  "&#x000DB;"); // <!--=capital U, circumflex accent -->
                    AllFileText.Replace("&ugrave;", "&#x000F9;"); // <!--=small u, grave accent -->
                    AllFileText.Replace("&Ugrave;", "&#x000D9;"); // <!--=capital U, grave accent -->
                    AllFileText.Replace("&uuml;",   "&#x000FC;"); // <!--=small u, dieresis or umlaut mark -->
                    AllFileText.Replace("&Uuml;",   "&#x000DC;"); // <!--=capital U, dieresis or umlaut mark -->
                    AllFileText.Replace("&yacute;", "&#x000FD;"); // <!--=small y, acute accent -->
                    AllFileText.Replace("&Yacute;", "&#x000DD;"); // <!--=capital Y, acute accent -->
                    AllFileText.Replace("&yuml;",   "&#x000FF;"); // <!--=small y, dieresis or umlaut mark -->

                    // From the "iso-lat2.ent" file
                    AllFileText.Replace("&abreve;", "&#x00103;"); // <!--=small a, breve -->
                    AllFileText.Replace("&Abreve;", "&#x00102;"); // <!--=capital A, breve -->
                    AllFileText.Replace("&amacr;",  "&#x00101;"); // <!--=small a, macron -->
                    AllFileText.Replace("&Amacr;",  "&#x00100;"); // <!--=capital A, macron -->
                    AllFileText.Replace("&aogon;",  "&#x00105;"); // <!--=small a, ogonek -->
                    AllFileText.Replace("&Aogon;",  "&#x00104;"); // <!--=capital A, ogonek -->
                    AllFileText.Replace("&cacute;", "&#x00107;"); // <!--=small c, acute accent -->
                    AllFileText.Replace("&Cacute;", "&#x00106;"); // <!--=capital C, acute accent -->
                    AllFileText.Replace("&ccaron;", "&#x0010D;"); // <!--=small c, caron -->
                    AllFileText.Replace("&Ccaron;", "&#x0010C;"); // <!--=capital C, caron -->
                    AllFileText.Replace("&ccirc;",  "&#x00109;"); // <!--=small c, circumflex accent -->
                    AllFileText.Replace("&Ccirc;",  "&#x00108;"); // <!--=capital C, circumflex accent -->
                    AllFileText.Replace("&cdot;",   "&#x0010B;"); // <!--=small c, dot above -->
                    AllFileText.Replace("&Cdot;",   "&#x0010A;"); // <!--=capital C, dot above -->
                    AllFileText.Replace("&dcaron;", "&#x0010F;"); // <!--=small d, caron -->
                    AllFileText.Replace("&Dcaron;", "&#x0010E;"); // <!--=capital D, caron -->
                    AllFileText.Replace("&dstrok;", "&#x00111;"); // <!--=small d, stroke -->
                    AllFileText.Replace("&Dstrok;", "&#x00110;"); // <!--=capital D, stroke -->
                    AllFileText.Replace("&ecaron;", "&#x0011B;"); // <!--=small e, caron -->
                    AllFileText.Replace("&Ecaron;", "&#x0011A;"); // <!--=capital E, caron -->
                    AllFileText.Replace("&edot;",   "&#x00117;"); // <!--=small e, dot above -->
                    AllFileText.Replace("&Edot;",   "&#x00116;"); // <!--=capital E, dot above -->
                    AllFileText.Replace("&emacr;",  "&#x00113;"); // <!--=small e, macron -->
                    AllFileText.Replace("&Emacr;",  "&#x00112;"); // <!--=capital E, macron -->
                    AllFileText.Replace("&eng;",    "&#x0014B;"); // <!--=small eng, Lapp -->
                    AllFileText.Replace("&ENG;",    "&#x0014A;"); // <!--=capital ENG, Lapp -->
                    AllFileText.Replace("&eogon;",  "&#x00119;"); // <!--=small e, ogonek -->
                    AllFileText.Replace("&Eogon;",  "&#x00118;"); // <!--=capital E, ogonek -->
                    AllFileText.Replace("&gacute;", "&#x001F5;"); // <!--=small g, acute accent -->
                    AllFileText.Replace("&gbreve;", "&#x0011F;"); // <!--=small g, breve -->
                    AllFileText.Replace("&Gbreve;", "&#x0011E;"); // <!--=capital G, breve -->
                    AllFileText.Replace("&Gcedil;", "&#x00122;"); // <!--=capital G, cedilla -->
                    AllFileText.Replace("&gcirc;",  "&#x0011D;"); // <!--=small g, circumflex accent -->
                    AllFileText.Replace("&Gcirc;",  "&#x0011C;"); // <!--=capital G, circumflex accent -->
                    AllFileText.Replace("&gdot;",   "&#x00121;"); // <!--=small g, dot above -->
                    AllFileText.Replace("&Gdot;",   "&#x00120;"); // <!--=capital G, dot above -->
                    AllFileText.Replace("&hcirc;",  "&#x00125;"); // <!--=small h, circumflex accent -->
                    AllFileText.Replace("&Hcirc;",  "&#x00124;"); // <!--=capital H, circumflex accent -->
                    AllFileText.Replace("&hstrok;", "&#x00127;"); // <!--=small h, stroke -->
                    AllFileText.Replace("&Hstrok;", "&#x00126;"); // <!--=capital H, stroke -->
                    AllFileText.Replace("&Idot;",   "&#x00130;"); // <!--=capital I, dot above -->
                    AllFileText.Replace("&ijlig;",  "&#x00133;"); // <!--=small ij ligature -->
                    AllFileText.Replace("&IJlig;",  "&#x00132;"); // <!--=capital IJ ligature -->
                    AllFileText.Replace("&imacr;",  "&#x0012B;"); // <!--=small i, macron -->
                    AllFileText.Replace("&Imacr;",  "&#x0012A;"); // <!--=capital I, macron -->
                    AllFileText.Replace("&inodot;", "&#x00131;"); // <!--=small i without dot -->
                    AllFileText.Replace("&iogon;",  "&#x0012F;"); // <!--=small i, ogonek -->
                    AllFileText.Replace("&Iogon;",  "&#x0012E;"); // <!--=capital I, ogonek -->
                    AllFileText.Replace("&itilde;", "&#x00129;"); // <!--=small i, tilde -->
                    AllFileText.Replace("&Itilde;", "&#x00128;"); // <!--=capital I, tilde -->
                    AllFileText.Replace("&jcirc;",  "&#x00135;"); // <!--=small j, circumflex accent -->
                    AllFileText.Replace("&Jcirc;",  "&#x00134;"); // <!--=capital J, circumflex accent -->
                    AllFileText.Replace("&kcedil;", "&#x00137;"); // <!--=small k, cedilla -->
                    AllFileText.Replace("&Kcedil;", "&#x00136;"); // <!--=capital K, cedilla -->
                    AllFileText.Replace("&kgreen;", "&#x00138;"); // <!--=small k, Greenlandic -->
                    AllFileText.Replace("&lacute;", "&#x0013A;"); // <!--=small l, acute accent -->
                    AllFileText.Replace("&Lacute;", "&#x00139;"); // <!--=capital L, acute accent -->
                    AllFileText.Replace("&lcaron;", "&#x0013E;"); // <!--=small l, caron -->
                    AllFileText.Replace("&Lcaron;", "&#x0013D;"); // <!--=capital L, caron -->
                    AllFileText.Replace("&lcedil;", "&#x0013C;"); // <!--=small l, cedilla -->
                    AllFileText.Replace("&Lcedil;", "&#x0013B;"); // <!--=capital L, cedilla -->
                    AllFileText.Replace("&lmidot;", "&#x00140;"); // <!--=small l, middle dot -->
                    AllFileText.Replace("&Lmidot;", "&#x0013F;"); // <!--=capital L, middle dot -->
                    AllFileText.Replace("&lstrok;", "&#x00142;"); // <!--=small l, stroke -->
                    AllFileText.Replace("&Lstrok;", "&#x00141;"); // <!--=capital L, stroke -->
                    AllFileText.Replace("&nacute;", "&#x00144;"); // <!--=small n, acute accent -->
                    AllFileText.Replace("&Nacute;", "&#x00143;"); // <!--=capital N, acute accent -->
                    AllFileText.Replace("&napos;",  "&#x00149;"); // <!--=small n, apostrophe -->
                    AllFileText.Replace("&ncaron;", "&#x00148;"); // <!--=small n, caron -->
                    AllFileText.Replace("&Ncaron;", "&#x00147;"); // <!--=capital N, caron -->
                    AllFileText.Replace("&ncedil;", "&#x00146;"); // <!--=small n, cedilla -->
                    AllFileText.Replace("&Ncedil;", "&#x00145;"); // <!--=capital N, cedilla -->
                    AllFileText.Replace("&odblac;", "&#x00151;"); // <!--=small o, double acute accent -->
                    AllFileText.Replace("&Odblac;", "&#x00150;"); // <!--=capital O, double acute accent -->
                    AllFileText.Replace("&oelig;",  "&#x00153;"); // <!--=small oe ligature -->
                    AllFileText.Replace("&OElig;",  "&#x00152;"); // <!--=capital OE ligature -->
                    AllFileText.Replace("&omacr;",  "&#x0014D;"); // <!--=small o, macron -->
                    AllFileText.Replace("&Omacr;",  "&#x0014C;"); // <!--=capital O, macron -->
                    AllFileText.Replace("&racute;", "&#x00155;"); // <!--=small r, acute accent -->
                    AllFileText.Replace("&Racute;", "&#x00154;"); // <!--=capital R, acute accent -->
                    AllFileText.Replace("&rcaron;", "&#x00159;"); // <!--=small r, caron -->
                    AllFileText.Replace("&Rcaron;", "&#x00158;"); // <!--=capital R, caron -->
                    AllFileText.Replace("&rcedil;", "&#x00157;"); // <!--=small r, cedilla -->
                    AllFileText.Replace("&Rcedil;", "&#x00156;"); // <!--=capital R, cedilla -->
                    AllFileText.Replace("&sacute;", "&#x0015B;"); // <!--=small s, acute accent -->
                    AllFileText.Replace("&Sacute;", "&#x0015A;"); // <!--=capital S, acute accent -->
                    AllFileText.Replace("&scaron;", "&#x00161;"); // <!--=small s, caron -->
                    AllFileText.Replace("&Scaron;", "&#x00160;"); // <!--=capital S, caron -->
                    AllFileText.Replace("&scedil;", "&#x0015F;"); // <!--=small s, cedilla -->
                    AllFileText.Replace("&Scedil;", "&#x0015E;"); // <!--=capital S, cedilla -->
                    AllFileText.Replace("&scirc;",  "&#x0015D;"); // <!--=small s, circumflex accent -->
                    AllFileText.Replace("&Scirc;",  "&#x0015C;"); // <!--=capital S, circumflex accent -->
                    AllFileText.Replace("&tcaron;", "&#x00165;"); // <!--=small t, caron -->
                    AllFileText.Replace("&Tcaron;", "&#x00164;"); // <!--=capital T, caron -->
                    AllFileText.Replace("&tcedil;", "&#x00163;"); // <!--=small t, cedilla -->
                    AllFileText.Replace("&Tcedil;", "&#x00162;"); // <!--=capital T, cedilla -->
                    AllFileText.Replace("&tstrok;", "&#x00167;"); // <!--=small t, stroke -->
                    AllFileText.Replace("&Tstrok;", "&#x00166;"); // <!--=capital T, stroke -->
                    AllFileText.Replace("&ubreve;", "&#x0016D;"); // <!--=small u, breve -->
                    AllFileText.Replace("&Ubreve;", "&#x0016C;"); // <!--=capital U, breve -->
                    AllFileText.Replace("&udblac;", "&#x00171;"); // <!--=small u, double acute accent -->
                    AllFileText.Replace("&Udblac;", "&#x00170;"); // <!--=capital U, double acute accent -->
                    AllFileText.Replace("&umacr;",  "&#x0016B;"); // <!--=small u, macron -->
                    AllFileText.Replace("&Umacr;",  "&#x0016A;"); // <!--=capital U, macron -->
                    AllFileText.Replace("&uogon;",  "&#x00173;"); // <!--=small u, ogonek -->
                    AllFileText.Replace("&Uogon;",  "&#x00172;"); // <!--=capital U, ogonek -->
                    AllFileText.Replace("&uring;",  "&#x0016F;"); // <!--=small u, ring -->
                    AllFileText.Replace("&Uring;",  "&#x0016E;"); // <!--=capital U, ring -->
                    AllFileText.Replace("&utilde;", "&#x00169;"); // <!--=small u, tilde -->
                    AllFileText.Replace("&Utilde;", "&#x00168;"); // <!--=capital U, tilde -->
                    AllFileText.Replace("&wcirc;",  "&#x00175;"); // <!--=small w, circumflex accent -->
                    AllFileText.Replace("&Wcirc;",  "&#x00174;"); // <!--=capital W, circumflex accent -->
                    AllFileText.Replace("&ycirc;",  "&#x00177;"); // <!--=small y, circumflex accent -->
                    AllFileText.Replace("&Ycirc;",  "&#x00176;"); // <!--=capital Y, circumflex accent -->
                    AllFileText.Replace("&Yuml;",   "&#x00178;"); // <!--=capital Y, dieresis or umlaut mark -->
                    AllFileText.Replace("&zacute;", "&#x0017A;"); // <!--=small z, acute accent -->
                    AllFileText.Replace("&Zacute;", "&#x00179;"); // <!--=capital Z, acute accent -->
                    AllFileText.Replace("&zcaron;", "&#x0017E;"); // <!--=small z, caron -->
                    AllFileText.Replace("&Zcaron;", "&#x0017D;"); // <!--=capital Z, caron -->
                    AllFileText.Replace("&zdot;",   "&#x0017C;"); // <!--=small z, dot above -->
                    AllFileText.Replace("&Zdot;",   "&#x0017B;"); // <!--=capital Z, dot above -->

                    File.WriteAllText(ParserFileInfo.FullName, AllFileText.ToString());
                }
                else
                {
                    // NOTE: This section has yet to be implemented
                }
            }
        }
		
		/// <summary>
        /// 
        /// This method was intended to provide the functionality of validating a legacy ONIX file against its 
		/// respective DTD/XSD.  Unfortunately, though, the .NET platform does not seem to be compatible with 
		/// parsing and understanding a complex schema as specified in the ONIX standard.  So, it seems that this
		/// method will never come into fruition.
		/// 
        /// <returns>The Boolean that indicates whether or not the ONIX file is valid</returns>
        /// </summary>
        public bool ValidateFile()
        {
            bool ValidOnixFile = true;

            /*
             * NOTE: XSD Validation is proving to be consistently problematic
             * 
            try
            {
                XmlReaderSettings TempReaderSettings = new XmlReaderSettings() { DtdProcessing = DtdProcessing.Ignore };

                using (XmlReader TempXmlReader = XmlReader.Create(ParserFileInfo.FullName, TempReaderSettings))
                {
                    XDocument OnixDoc = XDocument.Load(TempXmlReader);

                    XmlSchemaSet schemas = new XmlSchemaSet();

                    string XsdFilepath = "";

                    if (ParserRefVerFlag)
                        XsdFilepath = @"C:\ONIX_XSD\2.1\ONIX_BookProduct_Release2.1_reference.xsd";
                    else
                        XsdFilepath = @"C:\ONIX_XSD\2.1\ONIX_BookProduct_Release2.1_short.xsd";

                    schemas.Add(null, XmlReader.Create(new StringReader(File.ReadAllText(XsdFilepath))));

                    schemas.Compile();

                    OnixDoc.Validate(schemas, (o, e) =>
                    {
                        Console.WriteLine("{0}", e.Message);
                        ValidOnixFile = false;
                    });
                }
            }
            catch (Exception ex)
            {
                ValidOnixFile = false;
            }
            */

            return ValidOnixFile;
        }

        #region Support Methods

        /// <summary>
        /// 
        /// This method will help determine whether the XML structure of an ONIX file belongs to the 'Reference' type 
        /// of ONIX (i.e., verbose tags) or the 'Short' type of ONIX (i.e., alphanumeric tags).
        /// 
        /// <param name="LegacyOnixFilepath">The path to the ONIX file</param>
        /// <returns>The Boolean that indicates whether or not the ONIX file belongs to the 'Reference' type</returns>
        /// </summary>
        public bool DetectVersionReference(FileInfo LegacyOnixFilepath)
        {
            bool bReferenceVersion = true;

            if (LegacyOnixFilepath.Length < CONST_MSG_REFERENCE_LENGTH)
                throw new Exception("ERROR!  ONIX File is smaller than expected!");

            byte[] buffer = new byte[CONST_MSG_REFERENCE_LENGTH];
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
            this.OnixReader = OnixLegacyParser.CreateXmlReader(LegacyOnixFilepath, false, ProvidedParser.PerformValidation);

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
