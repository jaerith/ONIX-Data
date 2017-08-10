using System;
using System.IO;
using System.Text;

namespace OnixData.Extensions
{
    public static class OnixParserExtensions
    {
        #region CONSTANTS

        private const int CONST_MSG_REFERENCE_LENGTH = 2048;
        private const int CONST_BLOCK_COUNT_SIZE     = 50000000;

        private const string CONST_FILENAME_SKIP_REPLACE_MARKER = "SKIPREPLACECHARENC";

        #endregion

        static public bool DebugFlag = true;

        /// <summary>
        /// 
        /// Since the .NET XML parser has very limited support for incorporating DTD/XSD/ENT/ELT files, this method will 
        /// perform the work on behalf of ELT utilization (i.e., by translating any ONIX shorthand codes that map to Unicode encodings).
        /// In particular, this method places on a focus on ISO Latin encodings.
        /// 
        /// <param name="PerformInMemory">Indicates whether or not the encoding replacements are performed entirely in memory.</param>
        /// <param name="ShouldReplaceTechEncodings">Indicates whether or not tech encodings should be replaced.</param>
        /// <returns>N/A</returns>
        /// </summary>
        public static void ReplaceIsoLatinEncodings(this FileInfo ParserFileInfo, bool PerformInMemory, bool ShouldReplaceTechEncodings = true)
        {
            if ((ParserFileInfo != null) && ParserFileInfo.Exists && !ParserFileInfo.FullName.Contains(CONST_FILENAME_SKIP_REPLACE_MARKER))
            {
                // Only perform in-memory replacement if flag is set and if the file is less than 250 MB
                if (PerformInMemory && (ParserFileInfo.Length < 250000000))
                {
                    StringBuilder AllFileText = new StringBuilder(File.ReadAllText(ParserFileInfo.FullName));

                    AllFileText.ReplaceIsoLatinEncodings(ShouldReplaceTechEncodings);

                    File.WriteAllText(ParserFileInfo.FullName, AllFileText.ToString());
                }
                else
                {
                    string sTmpFile = ParserFileInfo.FullName + ".tmp";

                    FileInfo fTmpFile = new FileInfo(sTmpFile);
                    if (!fTmpFile.Exists)
                    {
                        using (StreamReader OnixFileReader = new StreamReader(ParserFileInfo.FullName))
                        {
                            using (StreamWriter OnixFileWriter = new StreamWriter(sTmpFile))
                            {
                                int nLineCount = 0;
                                StringBuilder TempLineBuilder = new StringBuilder(CONST_BLOCK_COUNT_SIZE);

                                for (nLineCount = 0; !OnixFileReader.EndOfStream; ++nLineCount)
                                {
                                    TempLineBuilder.Append(OnixFileReader.ReadLine());
                                    TempLineBuilder.Append("\n");

                                    if (TempLineBuilder.Length >= CONST_BLOCK_COUNT_SIZE)
                                    {
                                        TempLineBuilder.ReplaceIsoLatinEncodings(ShouldReplaceTechEncodings);

                                        // NOTE: This section will remove any problematic control characters which are not allowed within XML
                                        string sControlCharDomain = "[\x00-\x08\x0B\x0C\x0E-\x1F]";

                                        string sFilteredContents =
                                             System.Text.RegularExpressions.Regex.Replace(TempLineBuilder.ToString(),
                                                                                          sControlCharDomain,
                                                                                          "",
                                                                                          System.Text.RegularExpressions.RegexOptions.Compiled);

                                        // Finally, we write the block
                                        OnixFileWriter.WriteLine(sFilteredContents);
                                        TempLineBuilder.Clear();

                                        if (DebugFlag)
                                        {
                                            System.Console.WriteLine("\tProcessed encodings for (" + nLineCount + ") lines so far..." + DateTime.Now);
                                            System.Console.Out.Flush();
                                        }
                                    }
                                }

                                // Process any remaining characters
                                TempLineBuilder.ReplaceIsoLatinEncodings(ShouldReplaceTechEncodings);

                                OnixFileWriter.WriteLine(TempLineBuilder.ToString());
                                TempLineBuilder.Clear();

                                if (DebugFlag)
                                {
                                    System.Console.WriteLine("\tProcessed encodings for (" + nLineCount + ") lines so far..." + DateTime.Now);
                                    System.Console.Out.Flush();
                                }
                            }

                        } // outer using
                    }

                    File.Delete(ParserFileInfo.FullName);

                    File.Move(sTmpFile, ParserFileInfo.FullName);
                }
            }
        }

        /// <summary>
        /// 
        /// Since the .NET XML parser has very limited support for incorporating DTD/XSD/ENT/ELT files, this method will 
        /// perform the work on behalf of ELT utilization (i.e., by translating any ONIX shorthand codes that map to Unicode encodings).
        /// In particular, this method places on a focus on ISO Latin encodings.
        /// 
        /// <param name="PerformInMemory">Indicates whether or not the encoding replacements are performed entirely in memory.</param>
        /// <param name="ShouldReplaceTechEncodings">Indicates whether or not tech encodings should be replaced.</param>
        /// <returns>N/A</returns>
        /// </summary>
        public static void ReplaceIsoLatinEncodings(this StringBuilder ParserFileContent, bool ShouldReplaceTechEncodings = true)
        {
            if (ParserFileContent.Length > 0)
            {
                System.Console.WriteLine("Content is of length [" + ParserFileContent.Length + "]...");
                System.Console.Out.Flush();

                ReplaceIsoLatinEncodings(ParserFileContent);

                // From the "iso-tech.ent" file
                if (ShouldReplaceTechEncodings)
                    ReplaceTechEncodings(ParserFileContent);
            }
        }

        private static void ReplaceIsoLatinEncodings(StringBuilder LegacyOnixFileText)
        {
            // From the "iso-lat1.ent" file
            LegacyOnixFileText.Replace("&aacute;", "&#x000E1;"); // <!--=small a, acute accent -->
            LegacyOnixFileText.Replace("&Aacute;", "&#x000C1;"); // <!--=capital A, acute accent -->
            LegacyOnixFileText.Replace("&acirc;", "&#x000E2;"); // <!--=small a, circumflex accent -->
            LegacyOnixFileText.Replace("&Acirc;", "&#x000C2;"); // <!--=capital A, circumflex accent -->
            LegacyOnixFileText.Replace("&aelig;", "&#x000E6;"); // <!--=small ae diphthong (ligature) -->
            LegacyOnixFileText.Replace("&AElig;", "&#x000C6;"); // <!--=capital AE diphthong (ligature) -->
            LegacyOnixFileText.Replace("&agrave;", "&#x000E0;"); // !--=small a, grave accent -->
            LegacyOnixFileText.Replace("&Agrave;", "&#x000C0;"); // <!--=capital A, grave accent -->
            LegacyOnixFileText.Replace("&aring;", "&#x000E5;"); // <!--=small a, ring -->
            LegacyOnixFileText.Replace("&Aring;", "&#x000C5;"); // <!--=capital A, ring -->
            LegacyOnixFileText.Replace("&atilde;", "&#x000E3;"); // <!--=small a, tilde -->
            LegacyOnixFileText.Replace("&Atilde;", "&#x000C3;"); // <!--=capital A, tilde -->
            LegacyOnixFileText.Replace("&auml;", "&#x000E4;"); // <!--=small a, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&Auml;", "&#x000C4;"); // <!--=capital A, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&ccedil;", "&#x000E7;"); // <!--=small c, cedilla -->
            LegacyOnixFileText.Replace("&Ccedil;", "&#x000C7;"); // <!--=capital C, cedilla -->
            LegacyOnixFileText.Replace("&eacute;", "&#x000E9;"); // <!--=small e, acute accent -->
            LegacyOnixFileText.Replace("&Eacute;", "&#x000C9;"); // <!--=capital E, acute accent -->
            LegacyOnixFileText.Replace("&ecirc;", "&#x000EA;"); // <!--=small e, circumflex accent -->
            LegacyOnixFileText.Replace("&Ecirc;", "&#x000CA;"); // <!--=capital E, circumflex accent -->
            LegacyOnixFileText.Replace("&egrave;", "&#x000E8;"); // <!--=small e, grave accent -->
            LegacyOnixFileText.Replace("&Egrave;", "&#x000C8;"); // <!--=capital E, grave accent -->
            LegacyOnixFileText.Replace("&eth;", "&#x000F0;"); // <!--=small eth, Icelandic -->
            LegacyOnixFileText.Replace("&ETH;", "&#x000D0;"); // <!--=capital Eth, Icelandic -->
            LegacyOnixFileText.Replace("&euml;", "&#x000EB;"); // <!--=small e, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&Euml;", "&#x000CB;"); // <!--=capital E, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&iacute;", "&#x000ED;"); // <!--=small i, acute accent -->
            LegacyOnixFileText.Replace("&Iacute;", "&#x000CD;"); // <!--=capital I, acute accent -->
            LegacyOnixFileText.Replace("&icirc;", "&#x000EE;"); // <!--=small i, circumflex accent -->
            LegacyOnixFileText.Replace("&Icirc;", "&#x000CE;"); // <!--=capital I, circumflex accent -->
            LegacyOnixFileText.Replace("&igrave;", "&#x000EC;"); // <!--=small i, grave accent -->
            LegacyOnixFileText.Replace("&Igrave;", "&#x000CC;"); // <!--=capital I, grave accent -->
            LegacyOnixFileText.Replace("&iuml;", "&#x000EF;"); // <!--=small i, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&Iuml;", "&#x000CF;"); // <!--=capital I, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&iacute;", "&#x000ED;"); // <!--=small i, acute accent -->
            LegacyOnixFileText.Replace("&Iacute;", "&#x000CD;"); // <!--=capital I, acute accent -->
            LegacyOnixFileText.Replace("&ntilde;", "&#x000F1;"); // <!--=small n, tilde -->
            LegacyOnixFileText.Replace("&Ntilde;", "&#x000D1;"); // <!--=capital N, tilde -->
            LegacyOnixFileText.Replace("&oacute;", "&#x000F3;"); // <!--=small o, acute accent -->
            LegacyOnixFileText.Replace("&Oacute;", "&#x000D3;"); // <!--=capital O, acute accent -->
            LegacyOnixFileText.Replace("&ocirc;", "&#x000F4;"); // <!--=small o, circumflex accent -->
            LegacyOnixFileText.Replace("&Ocirc;", "&#x000D4;"); // <!--=capital O, circumflex accent -->
            LegacyOnixFileText.Replace("&ograve;", "&#x000F2;"); // <!--=small o, grave accent -->
            LegacyOnixFileText.Replace("&Ograve;", "&#x000D2;"); // <!--=capital O, grave accent -->
            LegacyOnixFileText.Replace("&oslash;", "&#x000F8;"); // <!--latin small letter o with stroke -->
            LegacyOnixFileText.Replace("&Oslash;", "&#x000D8;"); // <!--=capital O, slash -->
            LegacyOnixFileText.Replace("&otilde;", "&#x000F5;"); // <!--=small o, tilde -->
            LegacyOnixFileText.Replace("&Otilde;", "&#x000D5;"); // <!--=capital O, tilde -->
            LegacyOnixFileText.Replace("&ouml;", "&#x000F6;"); // <!--=small o, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&Ouml;", "&#x000D6;"); // <!--=capital O, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&szlig;", "&#x000DF;"); // <!--=small sharp s, German (sz ligature) -->
            LegacyOnixFileText.Replace("&thorn;", "&#x000FE;"); // <!--=small thorn, Icelandic -->
            LegacyOnixFileText.Replace("&THORN;", "&#x000DE;"); // <!--=capital THORN, Icelandic -->
            LegacyOnixFileText.Replace("&uacute;", "&#x000FA;"); // <!--=small u, acute accent -->
            LegacyOnixFileText.Replace("&Uacute;", "&#x000DA;"); // <!--=capital U, acute accent -->
            LegacyOnixFileText.Replace("&ucirc;", "&#x000FB;"); // <!--=small u, circumflex accent -->
            LegacyOnixFileText.Replace("&Ucirc;", "&#x000DB;"); // <!--=capital U, circumflex accent -->
            LegacyOnixFileText.Replace("&ugrave;", "&#x000F9;"); // <!--=small u, grave accent -->
            LegacyOnixFileText.Replace("&Ugrave;", "&#x000D9;"); // <!--=capital U, grave accent -->
            LegacyOnixFileText.Replace("&uuml;", "&#x000FC;"); // <!--=small u, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&Uuml;", "&#x000DC;"); // <!--=capital U, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&yacute;", "&#x000FD;"); // <!--=small y, acute accent -->
            LegacyOnixFileText.Replace("&Yacute;", "&#x000DD;"); // <!--=capital Y, acute accent -->
            LegacyOnixFileText.Replace("&yuml;", "&#x000FF;"); // <!--=small y, dieresis or umlaut mark -->

            // From the "iso-lat2.ent" file
            LegacyOnixFileText.Replace("&abreve;", "&#x00103;"); // <!--=small a, breve -->
            LegacyOnixFileText.Replace("&Abreve;", "&#x00102;"); // <!--=capital A, breve -->
            LegacyOnixFileText.Replace("&amacr;", "&#x00101;"); // <!--=small a, macron -->
            LegacyOnixFileText.Replace("&Amacr;", "&#x00100;"); // <!--=capital A, macron -->
            LegacyOnixFileText.Replace("&aogon;", "&#x00105;"); // <!--=small a, ogonek -->
            LegacyOnixFileText.Replace("&Aogon;", "&#x00104;"); // <!--=capital A, ogonek -->
            LegacyOnixFileText.Replace("&cacute;", "&#x00107;"); // <!--=small c, acute accent -->
            LegacyOnixFileText.Replace("&Cacute;", "&#x00106;"); // <!--=capital C, acute accent -->
            LegacyOnixFileText.Replace("&ccaron;", "&#x0010D;"); // <!--=small c, caron -->
            LegacyOnixFileText.Replace("&Ccaron;", "&#x0010C;"); // <!--=capital C, caron -->
            LegacyOnixFileText.Replace("&ccirc;", "&#x00109;"); // <!--=small c, circumflex accent -->
            LegacyOnixFileText.Replace("&Ccirc;", "&#x00108;"); // <!--=capital C, circumflex accent -->
            LegacyOnixFileText.Replace("&cdot;", "&#x0010B;"); // <!--=small c, dot above -->
            LegacyOnixFileText.Replace("&Cdot;", "&#x0010A;"); // <!--=capital C, dot above -->
            LegacyOnixFileText.Replace("&dcaron;", "&#x0010F;"); // <!--=small d, caron -->
            LegacyOnixFileText.Replace("&Dcaron;", "&#x0010E;"); // <!--=capital D, caron -->
            LegacyOnixFileText.Replace("&dstrok;", "&#x00111;"); // <!--=small d, stroke -->
            LegacyOnixFileText.Replace("&Dstrok;", "&#x00110;"); // <!--=capital D, stroke -->
            LegacyOnixFileText.Replace("&ecaron;", "&#x0011B;"); // <!--=small e, caron -->
            LegacyOnixFileText.Replace("&Ecaron;", "&#x0011A;"); // <!--=capital E, caron -->
            LegacyOnixFileText.Replace("&edot;", "&#x00117;"); // <!--=small e, dot above -->
            LegacyOnixFileText.Replace("&Edot;", "&#x00116;"); // <!--=capital E, dot above -->
            LegacyOnixFileText.Replace("&emacr;", "&#x00113;"); // <!--=small e, macron -->
            LegacyOnixFileText.Replace("&Emacr;", "&#x00112;"); // <!--=capital E, macron -->
            LegacyOnixFileText.Replace("&eng;", "&#x0014B;"); // <!--=small eng, Lapp -->
            LegacyOnixFileText.Replace("&ENG;", "&#x0014A;"); // <!--=capital ENG, Lapp -->
            LegacyOnixFileText.Replace("&eogon;", "&#x00119;"); // <!--=small e, ogonek -->
            LegacyOnixFileText.Replace("&Eogon;", "&#x00118;"); // <!--=capital E, ogonek -->
            LegacyOnixFileText.Replace("&gacute;", "&#x001F5;"); // <!--=small g, acute accent -->
            LegacyOnixFileText.Replace("&gbreve;", "&#x0011F;"); // <!--=small g, breve -->
            LegacyOnixFileText.Replace("&Gbreve;", "&#x0011E;"); // <!--=capital G, breve -->
            LegacyOnixFileText.Replace("&Gcedil;", "&#x00122;"); // <!--=capital G, cedilla -->
            LegacyOnixFileText.Replace("&gcirc;", "&#x0011D;"); // <!--=small g, circumflex accent -->
            LegacyOnixFileText.Replace("&Gcirc;", "&#x0011C;"); // <!--=capital G, circumflex accent -->
            LegacyOnixFileText.Replace("&gdot;", "&#x00121;"); // <!--=small g, dot above -->
            LegacyOnixFileText.Replace("&Gdot;", "&#x00120;"); // <!--=capital G, dot above -->
            LegacyOnixFileText.Replace("&hcirc;", "&#x00125;"); // <!--=small h, circumflex accent -->
            LegacyOnixFileText.Replace("&Hcirc;", "&#x00124;"); // <!--=capital H, circumflex accent -->
            LegacyOnixFileText.Replace("&hstrok;", "&#x00127;"); // <!--=small h, stroke -->
            LegacyOnixFileText.Replace("&Hstrok;", "&#x00126;"); // <!--=capital H, stroke -->
            LegacyOnixFileText.Replace("&Idot;", "&#x00130;"); // <!--=capital I, dot above -->
            LegacyOnixFileText.Replace("&ijlig;", "&#x00133;"); // <!--=small ij ligature -->
            LegacyOnixFileText.Replace("&IJlig;", "&#x00132;"); // <!--=capital IJ ligature -->
            LegacyOnixFileText.Replace("&imacr;", "&#x0012B;"); // <!--=small i, macron -->
            LegacyOnixFileText.Replace("&Imacr;", "&#x0012A;"); // <!--=capital I, macron -->
            LegacyOnixFileText.Replace("&inodot;", "&#x00131;"); // <!--=small i without dot -->
            LegacyOnixFileText.Replace("&iogon;", "&#x0012F;"); // <!--=small i, ogonek -->
            LegacyOnixFileText.Replace("&Iogon;", "&#x0012E;"); // <!--=capital I, ogonek -->
            LegacyOnixFileText.Replace("&itilde;", "&#x00129;"); // <!--=small i, tilde -->
            LegacyOnixFileText.Replace("&Itilde;", "&#x00128;"); // <!--=capital I, tilde -->
            LegacyOnixFileText.Replace("&jcirc;", "&#x00135;"); // <!--=small j, circumflex accent -->
            LegacyOnixFileText.Replace("&Jcirc;", "&#x00134;"); // <!--=capital J, circumflex accent -->
            LegacyOnixFileText.Replace("&kcedil;", "&#x00137;"); // <!--=small k, cedilla -->
            LegacyOnixFileText.Replace("&Kcedil;", "&#x00136;"); // <!--=capital K, cedilla -->
            LegacyOnixFileText.Replace("&kgreen;", "&#x00138;"); // <!--=small k, Greenlandic -->
            LegacyOnixFileText.Replace("&lacute;", "&#x0013A;"); // <!--=small l, acute accent -->
            LegacyOnixFileText.Replace("&Lacute;", "&#x00139;"); // <!--=capital L, acute accent -->
            LegacyOnixFileText.Replace("&lcaron;", "&#x0013E;"); // <!--=small l, caron -->
            LegacyOnixFileText.Replace("&Lcaron;", "&#x0013D;"); // <!--=capital L, caron -->
            LegacyOnixFileText.Replace("&lcedil;", "&#x0013C;"); // <!--=small l, cedilla -->
            LegacyOnixFileText.Replace("&Lcedil;", "&#x0013B;"); // <!--=capital L, cedilla -->
            LegacyOnixFileText.Replace("&lmidot;", "&#x00140;"); // <!--=small l, middle dot -->
            LegacyOnixFileText.Replace("&Lmidot;", "&#x0013F;"); // <!--=capital L, middle dot -->
            LegacyOnixFileText.Replace("&lstrok;", "&#x00142;"); // <!--=small l, stroke -->
            LegacyOnixFileText.Replace("&Lstrok;", "&#x00141;"); // <!--=capital L, stroke -->
            LegacyOnixFileText.Replace("&nacute;", "&#x00144;"); // <!--=small n, acute accent -->
            LegacyOnixFileText.Replace("&Nacute;", "&#x00143;"); // <!--=capital N, acute accent -->
            LegacyOnixFileText.Replace("&napos;", "&#x00149;"); // <!--=small n, apostrophe -->
            LegacyOnixFileText.Replace("&ncaron;", "&#x00148;"); // <!--=small n, caron -->
            LegacyOnixFileText.Replace("&Ncaron;", "&#x00147;"); // <!--=capital N, caron -->
            LegacyOnixFileText.Replace("&ncedil;", "&#x00146;"); // <!--=small n, cedilla -->
            LegacyOnixFileText.Replace("&Ncedil;", "&#x00145;"); // <!--=capital N, cedilla -->
            LegacyOnixFileText.Replace("&odblac;", "&#x00151;"); // <!--=small o, double acute accent -->
            LegacyOnixFileText.Replace("&Odblac;", "&#x00150;"); // <!--=capital O, double acute accent -->
            LegacyOnixFileText.Replace("&oelig;", "&#x00153;"); // <!--=small oe ligature -->
            LegacyOnixFileText.Replace("&OElig;", "&#x00152;"); // <!--=capital OE ligature -->
            LegacyOnixFileText.Replace("&omacr;", "&#x0014D;"); // <!--=small o, macron -->
            LegacyOnixFileText.Replace("&Omacr;", "&#x0014C;"); // <!--=capital O, macron -->
            LegacyOnixFileText.Replace("&racute;", "&#x00155;"); // <!--=small r, acute accent -->
            LegacyOnixFileText.Replace("&Racute;", "&#x00154;"); // <!--=capital R, acute accent -->
            LegacyOnixFileText.Replace("&rcaron;", "&#x00159;"); // <!--=small r, caron -->
            LegacyOnixFileText.Replace("&Rcaron;", "&#x00158;"); // <!--=capital R, caron -->
            LegacyOnixFileText.Replace("&rcedil;", "&#x00157;"); // <!--=small r, cedilla -->
            LegacyOnixFileText.Replace("&Rcedil;", "&#x00156;"); // <!--=capital R, cedilla -->
            LegacyOnixFileText.Replace("&sacute;", "&#x0015B;"); // <!--=small s, acute accent -->
            LegacyOnixFileText.Replace("&Sacute;", "&#x0015A;"); // <!--=capital S, acute accent -->
            LegacyOnixFileText.Replace("&scaron;", "&#x00161;"); // <!--=small s, caron -->
            LegacyOnixFileText.Replace("&Scaron;", "&#x00160;"); // <!--=capital S, caron -->
            LegacyOnixFileText.Replace("&scedil;", "&#x0015F;"); // <!--=small s, cedilla -->
            LegacyOnixFileText.Replace("&Scedil;", "&#x0015E;"); // <!--=capital S, cedilla -->
            LegacyOnixFileText.Replace("&scirc;", "&#x0015D;"); // <!--=small s, circumflex accent -->
            LegacyOnixFileText.Replace("&Scirc;", "&#x0015C;"); // <!--=capital S, circumflex accent -->
            LegacyOnixFileText.Replace("&tcaron;", "&#x00165;"); // <!--=small t, caron -->
            LegacyOnixFileText.Replace("&Tcaron;", "&#x00164;"); // <!--=capital T, caron -->
            LegacyOnixFileText.Replace("&tcedil;", "&#x00163;"); // <!--=small t, cedilla -->
            LegacyOnixFileText.Replace("&Tcedil;", "&#x00162;"); // <!--=capital T, cedilla -->
            LegacyOnixFileText.Replace("&tstrok;", "&#x00167;"); // <!--=small t, stroke -->
            LegacyOnixFileText.Replace("&Tstrok;", "&#x00166;"); // <!--=capital T, stroke -->
            LegacyOnixFileText.Replace("&ubreve;", "&#x0016D;"); // <!--=small u, breve -->
            LegacyOnixFileText.Replace("&Ubreve;", "&#x0016C;"); // <!--=capital U, breve -->
            LegacyOnixFileText.Replace("&udblac;", "&#x00171;"); // <!--=small u, double acute accent -->
            LegacyOnixFileText.Replace("&Udblac;", "&#x00170;"); // <!--=capital U, double acute accent -->
            LegacyOnixFileText.Replace("&umacr;", "&#x0016B;"); // <!--=small u, macron -->
            LegacyOnixFileText.Replace("&Umacr;", "&#x0016A;"); // <!--=capital U, macron -->
            LegacyOnixFileText.Replace("&uogon;", "&#x00173;"); // <!--=small u, ogonek -->
            LegacyOnixFileText.Replace("&Uogon;", "&#x00172;"); // <!--=capital U, ogonek -->
            LegacyOnixFileText.Replace("&uring;", "&#x0016F;"); // <!--=small u, ring -->
            LegacyOnixFileText.Replace("&Uring;", "&#x0016E;"); // <!--=capital U, ring -->
            LegacyOnixFileText.Replace("&utilde;", "&#x00169;"); // <!--=small u, tilde -->
            LegacyOnixFileText.Replace("&Utilde;", "&#x00168;"); // <!--=capital U, tilde -->
            LegacyOnixFileText.Replace("&wcirc;", "&#x00175;"); // <!--=small w, circumflex accent -->
            LegacyOnixFileText.Replace("&Wcirc;", "&#x00174;"); // <!--=capital W, circumflex accent -->
            LegacyOnixFileText.Replace("&ycirc;", "&#x00177;"); // <!--=small y, circumflex accent -->
            LegacyOnixFileText.Replace("&Ycirc;", "&#x00176;"); // <!--=capital Y, circumflex accent -->
            LegacyOnixFileText.Replace("&Yuml;", "&#x00178;"); // <!--=capital Y, dieresis or umlaut mark -->
            LegacyOnixFileText.Replace("&zacute;", "&#x0017A;"); // <!--=small z, acute accent -->
            LegacyOnixFileText.Replace("&Zacute;", "&#x00179;"); // <!--=capital Z, acute accent -->
            LegacyOnixFileText.Replace("&zcaron;", "&#x0017E;"); // <!--=small z, caron -->
            LegacyOnixFileText.Replace("&Zcaron;", "&#x0017D;"); // <!--=capital Z, caron -->
            LegacyOnixFileText.Replace("&zdot;", "&#x0017C;"); // <!--=small z, dot above -->
            LegacyOnixFileText.Replace("&Zdot;", "&#x0017B;"); // <!--=capital Z, dot above -->
        }

        private static void ReplaceTechEncodings(StringBuilder LegacyOnixFileText)
        {
            if ((LegacyOnixFileText != null) && (LegacyOnixFileText.Length > 0))
            {
                LegacyOnixFileText.Replace("&acd;", "&#x0223F;"); // <!--ac current -->
                LegacyOnixFileText.Replace("&aleph;", "&#x02135;"); // <!--/aleph aleph, Hebrew -->
                LegacyOnixFileText.Replace("&and;", "&#x02227;"); // <!--/wedge /land B: logical and -->
                LegacyOnixFileText.Replace("&And;", "&#x02A53;"); // <!--dbl logical and -->
                LegacyOnixFileText.Replace("&andand;", "&#x02A55;"); // <!--two logical and -->
                LegacyOnixFileText.Replace("&andd;", "&#x02A5C;"); // <!--and, horizontal dash -->
                LegacyOnixFileText.Replace("&andslope;", "&#x02A58;"); // <!--sloping large and -->
                LegacyOnixFileText.Replace("&andv;", "&#x02A5A;"); // <!--and with middle stem -->
                LegacyOnixFileText.Replace("&angrt;", "&#x0221F;"); // <!--right (90 degree) angle -->
                LegacyOnixFileText.Replace("&angsph;", "&#x02222;"); // <!--/sphericalangle angle-spherical -->
                LegacyOnixFileText.Replace("&angst;", "&#x0212B;"); // <!--Angstrom capital A, ring -->
                LegacyOnixFileText.Replace("&ap;", "&#x02248;"); // <!--/approx R: approximate -->
                LegacyOnixFileText.Replace("&apacir;", "&#x02A6F;"); // <!--approximate, circumflex accent -->
                LegacyOnixFileText.Replace("&awconint;", "&#x02233;"); // <!--contour integral, anti-clockwise -->
                LegacyOnixFileText.Replace("&awint;", "&#x02A11;"); // <!--anti clock-wise integration -->
                LegacyOnixFileText.Replace("&becaus;", "&#x02235;"); // <!--/because R: because -->
                LegacyOnixFileText.Replace("&bernou;", "&#x0212C;"); // <!--Bernoulli function (script capital B)  -->
                LegacyOnixFileText.Replace("&bne;", "&#x0003D;&#x020E5;"); // <!--reverse not equal -->
                LegacyOnixFileText.Replace("&bnequiv;", "&#x02261;&#x020E5;"); // <!--reverse not equivalent -->
                LegacyOnixFileText.Replace("&bnot;", "&#x02310;"); // <!--reverse not -->
                LegacyOnixFileText.Replace("&bNot;", "&#x02AED;"); // <!--reverse not with two horizontal strokes -->
                LegacyOnixFileText.Replace("&bottom;", "&#x022A5;"); // <!--/bot bottom -->
                LegacyOnixFileText.Replace("&cap;", "&#x02229;"); // <!--/cap B: intersection -->
                LegacyOnixFileText.Replace("&Cconint;", "&#x02230;"); // <!--triple contour integral operator -->
                LegacyOnixFileText.Replace("&cirfnint;", "&#x02A10;"); // <!--circulation function -->
                LegacyOnixFileText.Replace("&compfn;", "&#x02218;"); // <!--/circ B: composite function (small circle) -->
                LegacyOnixFileText.Replace("&cong;", "&#x02245;"); // <!--/cong R: congruent with -->
                LegacyOnixFileText.Replace("&conint;", "&#x0222E;"); // <!--/oint L: contour integral operator -->
                LegacyOnixFileText.Replace("&Conint;", "&#x0222F;"); // <!--double contour integral operator -->
                LegacyOnixFileText.Replace("&ctdot;", "&#x022EF;"); // <!--/cdots, three dots, centered -->
                LegacyOnixFileText.Replace("&cup;", "&#x0222A;"); // <!--/cup B: union or logical sum -->
                LegacyOnixFileText.Replace("&cwconint;", "&#x02232;"); // <!--contour integral, clockwise -->
                LegacyOnixFileText.Replace("&cwint;", "&#x02231;"); // <!--clockwise integral -->
                LegacyOnixFileText.Replace("&cylcty;", "&#x0232D;"); // <!--cylindricity -->
                LegacyOnixFileText.Replace("&disin;", "&#x022F2;"); // <!--set membership, long horizontal stroke -->
                LegacyOnixFileText.Replace("&Dot;", "&#x000A8;"); // <!--dieresis or umlaut mark -->
                LegacyOnixFileText.Replace("&DotDot;", "&#x020DC;"); // <!--four dots above -->
                LegacyOnixFileText.Replace("&dsol;", "&#x029F6;"); // <!--solidus, bar above -->
                LegacyOnixFileText.Replace("&dtdot;", "&#x022F1;"); // <!--/ddots, three dots, descending -->
                LegacyOnixFileText.Replace("&dwangle;", "&#x029A6;"); // <!--large downward pointing angle -->
                LegacyOnixFileText.Replace("&epar;", "&#x022D5;"); // <!--parallel, equal; equal or parallel -->
                LegacyOnixFileText.Replace("&eparsl;", "&#x029E3;"); // <!--parallel, slanted, equal; homothetically congruent to -->
                LegacyOnixFileText.Replace("&equiv;", "&#x02261;"); // <!--/equiv R: identical with -->
                LegacyOnixFileText.Replace("&eqvparsl;", "&#x029E5;"); // <!--equivalent, equal; congruent and parallel -->
                LegacyOnixFileText.Replace("&exist;", "&#x02203;"); // <!--/exists at least one exists -->
                LegacyOnixFileText.Replace("&fnof;", "&#x00192;"); // <!--function of (italic small f) -->
                LegacyOnixFileText.Replace("&forall;", "&#x02200;"); // <!--/forall for all -->
                LegacyOnixFileText.Replace("&fpartint;", "&#x02A0D;"); // <!--finite part integral -->
                LegacyOnixFileText.Replace("&ge;", "&#x02265;"); // <!--/geq /ge R: greater-than-or-equal -->
                LegacyOnixFileText.Replace("&hamilt;", "&#x0210B;"); // <!--Hamiltonian (script capital H)  -->
                LegacyOnixFileText.Replace("&iff;", "&#x021D4;"); // <!--/iff if and only if  -->
                LegacyOnixFileText.Replace("&iinfin;", "&#x029DC;"); // <!--infinity sign, incomplete -->
                LegacyOnixFileText.Replace("&imped;", "&#x1D543;"); // <!--impedance -->
                LegacyOnixFileText.Replace("&infin;", "&#x0221E;"); // <!--/infty infinity -->
                LegacyOnixFileText.Replace("&int;", "&#x0222B;"); // <!--/int L: integral operator -->
                LegacyOnixFileText.Replace("&Int;", "&#x0222C;"); // <!--double integral operator -->
                LegacyOnixFileText.Replace("&intlarhk;", "&#x02A17;"); // <!--integral, left arrow with hook -->
                LegacyOnixFileText.Replace("&isin;", "&#x02208;"); // <!--/in R: set membership  -->
                LegacyOnixFileText.Replace("&isindot;", "&#x022F5;"); // <!--set membership, dot above -->
                LegacyOnixFileText.Replace("&isinE;", "&#x022F9;"); // <!--set membership, two horizontal strokes -->
                LegacyOnixFileText.Replace("&isins;", "&#x022F4;"); // <!--set membership, vertical bar on horizontal stroke -->
                LegacyOnixFileText.Replace("&isinsv;", "&#x022F3;"); // <!--large set membership, vertical bar on horizontal stroke -->
                LegacyOnixFileText.Replace("&isinv;", "&#x02208;"); // <!--set membership, variant -->
                LegacyOnixFileText.Replace("&lagran;", "&#x02112;"); // <!--Lagrangian (script capital L)  -->
                LegacyOnixFileText.Replace("&lang;", "&#x02329;"); // <!--/langle O: left angle bracket -->
                LegacyOnixFileText.Replace("&Lang;", "&#x0300A;"); // <!--left angle bracket, double -->
                LegacyOnixFileText.Replace("&lArr;", "&#x021D0;"); // <!--/Leftarrow A: is implied by -->
                LegacyOnixFileText.Replace("&lbbrk;", "&#x03014;"); // <!--left broken bracket -->
                LegacyOnixFileText.Replace("&le;", "&#x02264;"); // <!--/leq /le R: less-than-or-equal -->
                LegacyOnixFileText.Replace("&loang;", "&#x0F558;"); // <!--left open angular bracket -->
                LegacyOnixFileText.Replace("&lobrk;", "&#x0301A;"); // <!--left open bracket -->
                LegacyOnixFileText.Replace("&lopar;", "&#x03018;"); // <!--left open parenthesis -->
                LegacyOnixFileText.Replace("&lowast;", "&#x02217;"); // <!--low asterisk -->
                LegacyOnixFileText.Replace("&minus;", "&#x02212;"); // <!--B: minus sign -->
                LegacyOnixFileText.Replace("&mnplus;", "&#x02213;"); // <!--/mp B: minus-or-plus sign -->
                LegacyOnixFileText.Replace("&nabla;", "&#x02207;"); // <!--/nabla del, Hamilton operator -->
                LegacyOnixFileText.Replace("&ne;", "&#x02260;"); // <!--/ne /neq R: not equal -->
                LegacyOnixFileText.Replace("&nedot;", "&#x02260;&#x0FE00;"); // <!--not equal, dot -->
                LegacyOnixFileText.Replace("&nhpar;", "&#x02AF2;"); // <!--not, horizontal, parallel -->
                LegacyOnixFileText.Replace("&ni;", "&#x0220B;"); // <!--/ni /owns R: contains -->
                LegacyOnixFileText.Replace("&nis;", "&#x022FC;"); // <!--contains, vertical bar on horizontal stroke -->
                LegacyOnixFileText.Replace("&nisd;", "&#x022FA;"); // <!--contains, long horizontal stroke -->
                LegacyOnixFileText.Replace("&niv;", "&#x0220B;"); // <!--contains, variant -->
                LegacyOnixFileText.Replace("&Not;", "&#x02AEC;"); // <!--not with two horizontal strokes -->
                LegacyOnixFileText.Replace("&notin;", "&#x02209;"); // <!--/notin N: negated set membership -->
                LegacyOnixFileText.Replace("&notindot;", "&#x022F6;&#x0FE00;"); // <!--negated set membership, dot above -->
                LegacyOnixFileText.Replace("&notinva;", "&#x02209;&#x00338;"); // <!--negated set membership, variant -->
                LegacyOnixFileText.Replace("&notinvb;", "&#x022F7;"); // <!--negated set membership, variant -->
                LegacyOnixFileText.Replace("&notinvc;", "&#x022F6;"); // <!--negated set membership, variant -->
                LegacyOnixFileText.Replace("&notni;", "&#x0220C;"); // <!--negated contains -->
                LegacyOnixFileText.Replace("&notniva;", "&#x0220C;"); // <!--negated contains, variant -->
                LegacyOnixFileText.Replace("&notnivb;", "&#x022FE;"); // <!--contains, variant -->
                LegacyOnixFileText.Replace("&notnivc;", "&#x022FD;"); // <!--contains, variant -->
                LegacyOnixFileText.Replace("&nparsl;", "&#x02225;&#x0FE00;&#x020E5;"); // <!--not parallel, slanted -->
                LegacyOnixFileText.Replace("&npart;", "&#x02202;&#x00338;"); // <!--not partial differential -->
                LegacyOnixFileText.Replace("&npolint;", "&#x02A14;"); // <!--line integration, not including the pole -->
                LegacyOnixFileText.Replace("&nvinfin;", "&#x029DE;"); // <!--not, vert, infinity -->
                LegacyOnixFileText.Replace("&olcross;", "&#x029BB;"); // <!--circle, cross -->
                LegacyOnixFileText.Replace("&or;", "&#x02228;"); // <!--/vee /lor B: logical or -->
                LegacyOnixFileText.Replace("&Or;", "&#x02A54;"); // <!--dbl logical or -->
                LegacyOnixFileText.Replace("&ord;", "&#x02A5D;"); // <!--or, horizontal dash -->
                LegacyOnixFileText.Replace("&order;", "&#x02134;"); // <!--order of (script small o)  -->
                LegacyOnixFileText.Replace("&oror;", "&#x02A56;"); // <!--two logical or -->
                LegacyOnixFileText.Replace("&orslope;", "&#x02A57;"); // <!--sloping large or -->
                LegacyOnixFileText.Replace("&orv;", "&#x02A5B;"); // <!--or with middle stem -->
                LegacyOnixFileText.Replace("&par;", "&#x02225;"); // <!--/parallel R: parallel -->

                LegacyOnixFileText.Replace("&parsl;", "&#x02225;&#x0FE00;"); // <!--parallel, slanted -->
                LegacyOnixFileText.Replace("&part;", "&#x02202;"); // <!--/partial partial differential -->
                LegacyOnixFileText.Replace("&permil;", "&#x02030;"); // <!--per thousand -->
                LegacyOnixFileText.Replace("&perp;", "&#x022A5;"); // <!--/perp R: perpendicular -->
                LegacyOnixFileText.Replace("&pertenk;", "&#x02031;"); // <!--per 10 thousand -->
                LegacyOnixFileText.Replace("&phmmat;", "&#x02133;"); // <!--physics M-matrix (script capital M)  -->
                LegacyOnixFileText.Replace("&pointint;", "&#x02A15;"); // <!--integral around a point operator -->
                LegacyOnixFileText.Replace("&prime;", "&#x02032;"); // <!--/prime prime or minute -->
                LegacyOnixFileText.Replace("&Prime;", "&#x02033;"); // <!--double prime or second -->
                LegacyOnixFileText.Replace("&profalar;", "&#x0232E;"); // <!--all-around profile -->
                LegacyOnixFileText.Replace("&profline;", "&#x02312;"); // <!--profile of a line -->
                LegacyOnixFileText.Replace("&profsurf;", "&#x02313;"); // <!--profile of a surface -->
                LegacyOnixFileText.Replace("&prop;", "&#x0221D;"); // <!--/propto R: is proportional to -->
                LegacyOnixFileText.Replace("&qint;", "&#x02A0C;"); // <!--/iiiint quadruple integral operator -->
                LegacyOnixFileText.Replace("&qprime;", "&#x02057;"); //<!--quadruple prime -->
                LegacyOnixFileText.Replace("&quatint;", "&#x02A16;"); //<!--quaternion integral operator -->
                LegacyOnixFileText.Replace("&radic;", "&#x0221A;"); //<!--/surd radical -->
                LegacyOnixFileText.Replace("&rang;", "&#x0232A;"); //<!--/rangle C: right angle bracket -->
                LegacyOnixFileText.Replace("&Rang;", "&#x0300B;"); //<!--right angle bracket, double -->
                LegacyOnixFileText.Replace("&rArr;", "&#x021D2;"); //<!--/Rightarrow A: implies -->
                LegacyOnixFileText.Replace("&rbbrk;", "&#x03015;"); //<!--right broken bracket -->
                LegacyOnixFileText.Replace("&roang;", "&#x0F559;"); //<!--right open angular bracket -->
                LegacyOnixFileText.Replace("&robrk;", "&#x0301B;"); //<!--right open bracket -->
                LegacyOnixFileText.Replace("&ropar;", "&#x03019;"); //<!--right open parenthesis -->
                LegacyOnixFileText.Replace("&rppolint;", "&#x02A12;"); //<!--line integration, rectangular path around pole -->
                LegacyOnixFileText.Replace("&scpolint;", "&#x02A13;"); //<!--line integration, semi-circular path around pole -->
                LegacyOnixFileText.Replace("&sim;", "&#x0223C;"); //<!--/sim R: similar -->
                LegacyOnixFileText.Replace("&simdot;", "&#x02A6A;"); //<!--similar, dot -->
                LegacyOnixFileText.Replace("&sime;", "&#x02243;"); //<!--/simeq R: similar, equals -->
                LegacyOnixFileText.Replace("&smeparsl;", "&#x029E4;"); //<!--similar, parallel, slanted, equal -->
                LegacyOnixFileText.Replace("&square;", "&#x025A1;"); //<!--/square, square -->
                LegacyOnixFileText.Replace("&squarf;", "&#x025AA;"); //<!--/blacksquare, square, filled  -->
                LegacyOnixFileText.Replace("&sub;", "&#x02282;"); //<!--/subset R: subset or is implied by -->
                LegacyOnixFileText.Replace("&sube;", "&#x02286;"); //<!--/subseteq R: subset, equals -->
                LegacyOnixFileText.Replace("&sup;", "&#x02283;"); //<!--/supset R: superset or implies -->
                LegacyOnixFileText.Replace("&supe;", "&#x02287;"); //<!--/supseteq R: superset, equals -->
                LegacyOnixFileText.Replace("&tdot;", "&#x020DB;"); //<!--three dots above -->
                LegacyOnixFileText.Replace("&there4;", "&#x02234;"); //<!--/therefore R: therefore -->
                LegacyOnixFileText.Replace("&tint;", "&#x0222D;"); //<!--/iiint triple integral operator -->
                LegacyOnixFileText.Replace("&top;", "&#x022A4;"); //<!--/top top -->
                LegacyOnixFileText.Replace("&topbot;", "&#x02336;"); //<!--top and bottom -->
                LegacyOnixFileText.Replace("&topcir;", "&#x02AF1;"); //<!--top, circle below -->
                LegacyOnixFileText.Replace("&tprime;", "&#x02034;"); //<!--triple prime -->
                LegacyOnixFileText.Replace("&utdot ;", "&#x022F0;"); //<!--three dots, ascending -->
                LegacyOnixFileText.Replace("&uwangle;", "&#x029A7;"); //<!--large upward pointing angle -->
                LegacyOnixFileText.Replace("&vangrt;", "&#x022BE;"); //<!--right angle, variant -->
                LegacyOnixFileText.Replace("&veeeq;", "&#x0225A;"); //<!--logical or, equals -->
                LegacyOnixFileText.Replace("&Verbar;", "&#x02016;"); //<!--/Vert dbl vertical bar -->
                LegacyOnixFileText.Replace("&wedgeq;", "&#x02259;"); //<!--/wedgeq R: corresponds to (wedge, equals) -->
                LegacyOnixFileText.Replace("&xnis;", "&#x022FB;"); // <!--large contains, vertical bar on horizontal stroke -->

                // From the "iso-num.ent" file
                LegacyOnixFileText.Replace("&amp;", "&#x00026;"); //<!--=ampersand -->^M
                LegacyOnixFileText.Replace("&apos;", "&#x00027;"); //<!--=apostrophe -->^M
                LegacyOnixFileText.Replace("&ast;", "&#x0002A;"); //<!--/ast B: =asterisk -->^M
                LegacyOnixFileText.Replace("&brvbar;", "&#x000A6;"); //<!--=broken (vertical) bar -->^M
                LegacyOnixFileText.Replace("&bsol;", "&#x0005C;"); //<!--/backslash =reverse solidus -->^M
                LegacyOnixFileText.Replace("&cent;", "&#x000A2;"); //<!--=cent sign -->^M
                LegacyOnixFileText.Replace("&colon;", "&#x0003A;"); //<!--/colon P: -->^M
                LegacyOnixFileText.Replace("&comma;", "&#x0002C;"); //<!--P: =comma -->^M
                LegacyOnixFileText.Replace("&commat;", "&#x00040;"); //<!--=commercial at -->^M
                LegacyOnixFileText.Replace("&copy;", "&#x000A9;"); //<!--=copyright sign -->^M
                LegacyOnixFileText.Replace("&curren;", "&#x000A4;"); //<!--=general currency sign -->^M
                LegacyOnixFileText.Replace("&darr;", "&#x02193;"); //<!--/downarrow A: =downward arrow -->^M
                LegacyOnixFileText.Replace("&deg;", "&#x000B0;"); //<!--=degree sign -->^M
                LegacyOnixFileText.Replace("&divide;", "&#x000F7;"); //<!--/div B: =divide sign -->^M
                LegacyOnixFileText.Replace("&dollar;", "&#x00024;"); //<!--=dollar sign -->^M
                LegacyOnixFileText.Replace("&equals;", "&#x0003D;"); //<!--=equals sign R: -->^M
                LegacyOnixFileText.Replace("&excl;", "&#x00021;"); //<!--=exclamation mark -->^M
                LegacyOnixFileText.Replace("&frac12;", "&#x000BD;"); //<!--=fraction one-half -->^M
                LegacyOnixFileText.Replace("&frac14;", "&#x000BC;"); //<!--=fraction one-quarter -->^M
                LegacyOnixFileText.Replace("&frac18;", "&#x0215B;"); //<!--=fraction one-eighth -->^M
                LegacyOnixFileText.Replace("&frac34;", "&#x000BE;"); //<!--=fraction three-quarters -->^M
                LegacyOnixFileText.Replace("&frac38;", "&#x0215C;"); //<!--=fraction three-eighths -->^M
                LegacyOnixFileText.Replace("&frac58;", "&#x0215D;"); //<!--=fraction five-eighths -->^M
                LegacyOnixFileText.Replace("&frac78;", "&#x0215E;"); //<!--=fraction seven-eighths -->^M
                LegacyOnixFileText.Replace("&gt;", "&#x0003E;"); //<!--=greater-than sign R: -->^M
                LegacyOnixFileText.Replace("&half;", "&#x000BD;"); //<!--=fraction one-half -->^M
                LegacyOnixFileText.Replace("&horbar;", "&#x02015;"); //<!--=horizontal bar -->^M
                LegacyOnixFileText.Replace("&hyphen;", "&#x02010;"); //<!--=hyphen -->^M
                LegacyOnixFileText.Replace("&iexcl;", "&#x000A1;"); //<!--=inverted exclamation mark -->^M
                LegacyOnixFileText.Replace("&iquest;", "&#x000BF;"); //<!--=inverted question mark -->^M
                LegacyOnixFileText.Replace("&laquo;", "&#x000AB;"); //<!--=angle quotation mark, left -->^M
                LegacyOnixFileText.Replace("&larr;", "&#x02190;"); //<!--/leftarrow /gets A: =leftward arrow -->^M
                LegacyOnixFileText.Replace("&lcub;", "&#x0007B;"); //<!--/lbrace O: =left curly bracket -->^M
                LegacyOnixFileText.Replace("&ldquo;", "&#x0201C;"); //<!--=double quotation mark, left -->^M
                LegacyOnixFileText.Replace("&lowbar;", "&#x0005F;"); //<!--=low line -->^M
                LegacyOnixFileText.Replace("&lpar;", "&#x00028;"); //<!--O: =left parenthesis -->^M
                LegacyOnixFileText.Replace("&lsqb;", "&#x0005B;"); //<!--/lbrack O: =left square bracket -->^M
                LegacyOnixFileText.Replace("&lsquo;", "&#x02018;"); //<!--=single quotation mark, left -->^M
                LegacyOnixFileText.Replace("&lt;", "&#x26;#x0003C;"); //<!--=less-than sign R: -->^M
                LegacyOnixFileText.Replace("&micro;", "&#x000B5;"); //<!--=micro sign -->^M
                LegacyOnixFileText.Replace("&middot;", "&#x000B7;"); //<!--/centerdot B: =middle dot -->^M
                LegacyOnixFileText.Replace("&nbsp;", "&#x000A0;"); //<!--=no break (required) space -->^M
                LegacyOnixFileText.Replace("&not;", "&#x000AC;"); //<!--/neg /lnot =not sign -->^M
                LegacyOnixFileText.Replace("&num;", "&#x00023;"); //<!--=number sign -->^M
                LegacyOnixFileText.Replace("&ohm;", "&#x02126;"); //<!--=ohm sign -->^M
                LegacyOnixFileText.Replace("&ordf;", "&#x000AA;"); //<!--=ordinal indicator, feminine -->^M
                LegacyOnixFileText.Replace("&ordm;", "&#x000BA;"); //<!--=ordinal indicator, masculine -->^M
                LegacyOnixFileText.Replace("&para;", "&#x000B6;"); //<!--=pilcrow (paragraph sign) -->^M
                LegacyOnixFileText.Replace("&percnt;", "&#x00025;"); //<!--=percent sign -->^M
                LegacyOnixFileText.Replace("&period;", "&#x0002E;"); //<!--=full stop, period -->^M
                LegacyOnixFileText.Replace("&plus;", "&#x0002B;"); //<!--=plus sign B: -->^M
                LegacyOnixFileText.Replace("&plusmn;", "&#x000B1;"); //<!--/pm B: =plus-or-minus sign -->^M
                LegacyOnixFileText.Replace("&pound;", "&#x000A3;"); //<!--=pound sign -->^M
                LegacyOnixFileText.Replace("&quest;", "&#x0003F;"); //<!--=question mark -->^M
                LegacyOnixFileText.Replace("&quot;", "&#x00022;"); //<!--=quotation mark -->^M
                LegacyOnixFileText.Replace("&raquo;", "&#x000BB;"); //<!--=angle quotation mark, right -->^M
                LegacyOnixFileText.Replace("&rarr;", "&#x02192;"); //<!--/rightarrow /to A: =rightward arrow -->^M
                LegacyOnixFileText.Replace("&rcub;", "&#x0007D;"); //<!--/rbrace C: =right curly bracket -->^M
                LegacyOnixFileText.Replace("&rdquo;", "&#x0201D;"); //<!--=double quotation mark, right -->^M
                LegacyOnixFileText.Replace("&reg;", "&#x000AE;"); //<!--/circledR =registered sign -->^M
                LegacyOnixFileText.Replace("&rpar;", "&#x00029;"); //<!--C: =right parenthesis -->^M
                LegacyOnixFileText.Replace("&rsqb;", "&#x0005D;"); //<!--/rbrack C: =right square bracket -->^M
                LegacyOnixFileText.Replace("&rsquo;", "&#x02019;"); //<!--=single quotation mark, right -->^M
                LegacyOnixFileText.Replace("&sect;", "&#x000A7;"); //<!--=section sign -->^M
                LegacyOnixFileText.Replace("&semi;", "&#x0003B;"); //<!--=semicolon P: -->^M
                LegacyOnixFileText.Replace("&shy;", "&#x000AD;"); //<!--=soft hyphen -->^M
                LegacyOnixFileText.Replace("&sol;", "&#x0002F;"); //<!--=solidus -->^M
                LegacyOnixFileText.Replace("&sung;", "&#x0266A;"); //<!--=music note (sung text sign) -->^M
                LegacyOnixFileText.Replace("&sup1;", "&#x000B9;"); //<!--=superscript one -->^M
                LegacyOnixFileText.Replace("&sup2;", "&#x000B2;"); //<!--=superscript two -->^M
                LegacyOnixFileText.Replace("&sup3;", "&#x000B3;"); //<!--=superscript three -->^M
                LegacyOnixFileText.Replace("&times;", "&#x000D7;"); //<!--/times B: =multiply sign -->^M
                LegacyOnixFileText.Replace("&trade;", "&#x02122;"); //<!--=trade mark sign -->^M
                LegacyOnixFileText.Replace("&uarr;", "&#x02191;"); //<!--/uparrow A: =upward arrow -->^M
                LegacyOnixFileText.Replace("&verbar;", "&#x0007C;"); //<!--/vert =vertical bar -->^M
                LegacyOnixFileText.Replace("&yen;", "&#x000A5;"); //<!--/yen =yen sign -->^M

                // From the "xhtml-special.ent" file
                LegacyOnixFileText.Replace("&ndash;", "&#8211;"); // <!-- en dash, U+2013 ISOpub -->
                LegacyOnixFileText.Replace("&mdash;", "&#8212;"); // <!-- em dash, U+2014 ISOpub -->
                LegacyOnixFileText.Replace("&lsquo;", "&#8216;"); // <!-- left single quotation mark, U+2018 ISOnum -->
                LegacyOnixFileText.Replace("&rsquo;", "&#8217;"); // <!-- right single quotation mark, U+2019 ISOnum -->
                LegacyOnixFileText.Replace("&sbquo;", "&#8218;"); // <!-- single low-9 quotation mark, U+201A NEW -->
                LegacyOnixFileText.Replace("&ldquo;", "&#8220;"); // <!-- left double quotation mark, U+201C ISOnum -->
                LegacyOnixFileText.Replace("&rdquo;", "&#8221;"); // <!-- right double quotation mark, U+201D ISOnum -->
                LegacyOnixFileText.Replace("&bdquo;", "&#8222;"); // <!-- double low-9 quotation mark, U+201E NEW -->
                LegacyOnixFileText.Replace("&dagger;", "&#8224;"); // <!-- dagger, U+2020 ISOpub -->
                LegacyOnixFileText.Replace("&Dagger;", "&#8225;"); // <!-- double dagger, U+2021 ISOpub -->
                LegacyOnixFileText.Replace("&permil;", "&#8240;"); // <!-- per mille sign, U+2030 ISOtech -->
                LegacyOnixFileText.Replace("&lsaquo;", "&#8249;"); // <!-- single left-pointing angle quotation mark, U+2039 ISO proposed -->
                LegacyOnixFileText.Replace("&rsaquo;", "&#8250;"); // <!-- single right-pointing angle quotation mark, U+203A ISO proposed -->
                LegacyOnixFileText.Replace("&euro;", "&#8364;"); // <!--  euro sign, U+20AC NEW -->
                LegacyOnixFileText.Replace("&ensp;", "&#8194;"); // <!-- en space, U+2002 ISOpub -->
                LegacyOnixFileText.Replace("&emsp;", "&#8195;"); // <!-- em space, U+2003 ISOpub -->
                LegacyOnixFileText.Replace("&thinsp;", "&#8201;"); // <!-- thin space, U+2009 ISOpub -->
                LegacyOnixFileText.Replace("&zwnj;", "&#8204;"); // <!-- zero width non-joiner -->
                LegacyOnixFileText.Replace("&zwj;", "&#8205;"); // <!-- zero width non-joiner -->
                LegacyOnixFileText.Replace("&zwj;", "&#8205;"); // <!-- zero width non-joiner -->
                LegacyOnixFileText.Replace("&lrm;", "&#8206;"); // <!-- left-to-right mark, U+200E NEW RFC 2070 -->
                LegacyOnixFileText.Replace("&rlm;", "&#8207;"); // <!-- right-to-left mark, U+200F NEW RFC 2070 -->

                // From the "iso-dia.ent" file
                LegacyOnixFileText.Replace("&acute;", "&#x000B4;"); // <!-- acute accent -->
                LegacyOnixFileText.Replace("&breve;", "&#x002D8;"); // <!--=breve -->
                LegacyOnixFileText.Replace("&caron;", "&#x002C7;"); // <!--=caron -->
                LegacyOnixFileText.Replace("&cedil;", "&#x000B8;"); // <!--=cedilla -->
                LegacyOnixFileText.Replace("&circ;", "&#x0005E;"); // <!--circumflex accent -->
                LegacyOnixFileText.Replace("&dblac;", "&#x002DD;"); // <!--=double acute accent -->
                LegacyOnixFileText.Replace("&die;", "&#x000A8;"); // <!--=dieresis -->
                LegacyOnixFileText.Replace("&dot;", "&#x002D9;"); // <!--=dot above -->
                LegacyOnixFileText.Replace("&grave;", "&#x00060;"); // <!--=grave accent -->
                LegacyOnixFileText.Replace("&macr;", "&#x000AF;"); // <!--=macron -->
                LegacyOnixFileText.Replace("&ogon;", "&#x002DB;"); // <!--=ogonek -->
                LegacyOnixFileText.Replace("&ring;", "&#x002DA;"); // <!--=ring -->
                LegacyOnixFileText.Replace("&tilde;", "&#x002DC;"); // <!--=tilde -->
                LegacyOnixFileText.Replace("&uml;", "&#x000A8;"); // <!--=umlaut mark -->

                // From the "xhtml-symbol.ent" file
                LegacyOnixFileText.Replace("&fnof;", "&#402;"); // <!-- latin small f with hook = function
                LegacyOnixFileText.Replace("&Alpha;", "&#913;"); // <!-- greek capital letter alpha, U+0391 -->
                LegacyOnixFileText.Replace("&Beta;", "&#914;"); // <!-- greek capital letter beta, U+0392 -->
                LegacyOnixFileText.Replace("&Gamma;", "&#915;"); // <!-- greek capital letter gamma, U+0393 ISOgrk3 -->
                LegacyOnixFileText.Replace("&Delta;", "&#916;"); // <!-- greek capital letter delta, U+0394 ISOgrk3 -->
                LegacyOnixFileText.Replace("&Epsilon;", "&#917;"); // <!-- greek capital letter epsilon, U+0395 -->
                LegacyOnixFileText.Replace("&Zeta;", "&#918;"); // <!-- greek capital letter zeta, U+0396 -->
                LegacyOnixFileText.Replace("&Eta;", "&#919;"); // <!-- greek capital letter eta, U+0397 -->
                LegacyOnixFileText.Replace("&Theta;", "&#920;"); // <!-- greek capital letter theta, U+0398 ISOgrk3 -->
                LegacyOnixFileText.Replace("&Iota;", "&#921;"); // <!-- greek capital letter iota, U+0399 -->
                LegacyOnixFileText.Replace("&Kappa;", "&#922;"); // <!-- greek capital letter kappa, U+039A -->
                LegacyOnixFileText.Replace("&Lambda;", "&#923;"); // <!-- greek capital letter lambda, U+039B ISOgrk3 -->
                LegacyOnixFileText.Replace("&Mu;", "&#924;"); // <!-- greek capital letter mu, U+039C -->
                LegacyOnixFileText.Replace("&Nu;", "&#925;"); // <!-- greek capital letter nu, U+039D -->
                LegacyOnixFileText.Replace("&Xi;", "&#926;"); // <!-- greek capital letter xi, U+039E ISOgrk3 -->
                LegacyOnixFileText.Replace("&Omicron;", "&#927;"); // <!-- greek capital letter omicron, U+039F -->
                LegacyOnixFileText.Replace("&Pi;", "&#928;"); // <!-- greek capital letter pi, U+03A0 ISOgrk3 -->
                LegacyOnixFileText.Replace("&Rho;", "&#929;"); // <!-- greek capital letter rho, U+03A1 -->
                // <!-- there is no Sigmaf, and no U+03A2 character either -->
                LegacyOnixFileText.Replace("&Sigma;", "&#931;"); // <!-- greek capital letter sigma, U+03A3 ISOgrk3 -->
                LegacyOnixFileText.Replace("&Tau;", "&#932;"); // <!-- greek capital letter tau, U+03A4 -->
                LegacyOnixFileText.Replace("&Upsilon;", "&#933;"); // <!-- greek capital letter upsilon, U+03A5 ISOgrk3 -->
                LegacyOnixFileText.Replace("&Phi;", "&#934;"); // <!-- greek capital letter phi, U+03A6 ISOgrk3 -->
                LegacyOnixFileText.Replace("&Chi;", "&#935;"); // <!-- greek capital letter chi, U+03A7 -->
                LegacyOnixFileText.Replace("&Psi;", "&#936;"); // <!-- greek capital letter psi, U+03A8 ISOgrk3 -->
                LegacyOnixFileText.Replace("&Omega;", "&#937;"); // <!-- greek capital letter omega, U+03A9 ISOgrk3 -->
                LegacyOnixFileText.Replace("&alpha;", "&#945;"); // <!-- greek small letter alpha, U+03B1 ISOgrk3 -->
                LegacyOnixFileText.Replace("&beta;", "&#946;"); // <!-- greek small letter beta, U+03B2 ISOgrk3 -->
                LegacyOnixFileText.Replace("&gamma;", "&#947;"); // <!-- greek small letter gamma, U+03B3 ISOgrk3 -->
                LegacyOnixFileText.Replace("&delta;", "&#948;"); // <!-- greek small letter delta, U+03B4 ISOgrk3 -->
                LegacyOnixFileText.Replace("&epsilon;", "&#949;"); // <!-- greek small letter epsilon, U+03B5 ISOgrk3 -->
                LegacyOnixFileText.Replace("&zeta;", "&#950;"); // <!-- greek small letter zeta, U+03B6 ISOgrk3 -->
                LegacyOnixFileText.Replace("&eta;", "&#951;"); // <!-- greek small letter eta, U+03B7 ISOgrk3 -->
                LegacyOnixFileText.Replace("&theta;", "&#952;"); // <!-- greek small letter theta, U+03B8 ISOgrk3 -->
                LegacyOnixFileText.Replace("&iota;", "&#953;"); // <!-- greek small letter iota, U+03B9 ISOgrk3 -->
                LegacyOnixFileText.Replace("&kappa;", "&#954;"); // <!-- greek small letter kappa, U+03BA ISOgrk3 -->
                LegacyOnixFileText.Replace("&lambda;", "&#955;"); // <!-- greek small letter lambda, U+03BB ISOgrk3 -->
                LegacyOnixFileText.Replace("&mu;", "&#956;"); // <!-- greek small letter mu, U+03BC ISOgrk3 -->
                LegacyOnixFileText.Replace("&nu;", "&#957;"); // <!-- greek small letter nu, U+03BD ISOgrk3 -->
                LegacyOnixFileText.Replace("&xi;", "&#958;"); // <!-- greek small letter xi, U+03BE ISOgrk3 -->
                LegacyOnixFileText.Replace("&omicron;", "&#959;"); // <!-- greek small letter omicron, U+03BF NEW -->
                LegacyOnixFileText.Replace("&pi;", "&#960;"); // <!-- greek small letter pi, U+03C0 ISOgrk3 -->
                LegacyOnixFileText.Replace("&rho;", "&#961;"); // <!-- greek small letter rho, U+03C1 ISOgrk3 -->
                LegacyOnixFileText.Replace("&sigmaf;", "&#962;"); // <!-- greek small letter final sigma, U+03C2 ISOgrk3 -->
                LegacyOnixFileText.Replace("&sigma;", "&#963;"); // <!-- greek small letter sigma, U+03C3 ISOgrk3 -->
                LegacyOnixFileText.Replace("&tau;", "&#964;"); // <!-- greek small letter tau, U+03C4 ISOgrk3 -->
                LegacyOnixFileText.Replace("&upsilon;", "&#965;"); // <!-- greek small letter upsilon, U+03C5 ISOgrk3 -->
                LegacyOnixFileText.Replace("&phi;", "&#966;"); // <!-- greek small letter phi, U+03C6 ISOgrk3 -->
                LegacyOnixFileText.Replace("&chi;", "&#967;"); // <!-- greek small letter chi, U+03C7 ISOgrk3 -->
                LegacyOnixFileText.Replace("&psi;", "&#968;"); // <!-- greek small letter psi, U+03C8 ISOgrk3 -->
                LegacyOnixFileText.Replace("&omega;", "&#969;"); // <!-- greek small letter omega, U+03C9 ISOgrk3 -->
                LegacyOnixFileText.Replace("&thetasym;", "&#977;"); // <!-- greek small letter theta symbol, U+03D1 NEW -->
                LegacyOnixFileText.Replace("&upsih;", "&#978;"); // <!-- greek upsilon with hook symbol, U+03D2 NEW -->
                LegacyOnixFileText.Replace("&piv;", "&#982;"); // <!-- greek pi symbol, U+03D6 ISOgrk3 -->

            }
        }
    }
}