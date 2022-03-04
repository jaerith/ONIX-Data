using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using OnixData.Legacy;
using OnixData.Version3;
using OnixData.Version3.Header;
using OnixData.Version3.Price;
using OnixData.Version3.Supply;

namespace OnixData.Extensions
{
    public static class OnixParserExtensions
    {
        #region CONSTANTS

        private const int  CONST_ONIX_READ_BUFFER_LEN = 2048;
        private const int  CONST_MSG_REFERENCE_LENGTH = 2048;
        private const int  CONST_BLOCK_COUNT_SIZE     = 50000000;
        private const long CONST_LARGE_FILE_MINIMUM   = 250000000;
        private const int  FILTER_THREAD_COUNT        = 3;

        private const string CONST_FILENAME_SKIP_REPLACE_MARKER = "SKIPREPLACECHARENC";

        private const string CONST_ONIX_V3_MSG_SHORT_AND_RELEASE = "ONIXmessage release=\"3.0\"";
        private const string CONST_ONIX_V3_MSG_REF_AND_RELEASE   = "ONIXMessage release=\"3.0\"";

        private const string CONST_ONIX_MSG_REF_TAG_START   = "<" + OnixParser.CONST_ONIX_MESSAGE_REFERENCE_TAG;
        private const string CONST_ONIX_MSG_SHORT_TAG_START = "<" + OnixParser.CONST_ONIX_MESSAGE_SHORT_TAG;

        public const string CONST_REMOVE_CTRL_CHARS_REG_EXPR = @"[\x00-\x08\x0B\x0C\x0E-\x1F]";

        #endregion

        static public bool DebugFlag          = true;
        static public bool FilterBadEncodings = true;

        /// <summary>
        /// 
        /// Since the deserialized ONIX product may not have certain values populated, we must look to the 
        /// deserialized message header (from the provided ONIX file) and check whether or not default values 
        /// are supplied for those fields.  If so, we will populate them here.
        /// 
        /// <param name="poOnixProduct">Refers to the current deserialized ONIX product being examined</param>
        /// <param name="poOnixHeader">Refers to the deserialized header that belongs to the ONIX file</param>
        /// <returns>N/A</returns>
        /// </summary>
        public static void ApplyHeaderDefaults(this OnixLegacyProduct poOnixProduct, OnixLegacyHeader poOnixHeader)
        {
            if (poOnixHeader != null)
            {
                foreach (OnixLegacySupplyDetail TmpSupplyDetail in poOnixProduct.OnixSupplyDetailList)
                {
                    if ((TmpSupplyDetail != null) && (TmpSupplyDetail.OnixPriceList != null))
                    {
                        foreach (OnixLegacyPrice TmpPrice in TmpSupplyDetail.OnixPriceList)
                        {
                            if (!String.IsNullOrEmpty(poOnixHeader.DefaultCurrencyCode) && String.IsNullOrEmpty(TmpPrice.CurrencyCode))
                                TmpPrice.CurrencyCode = poOnixHeader.DefaultCurrencyCode;

                            if (!String.IsNullOrEmpty(poOnixHeader.DefaultPriceTypeCode) && (TmpPrice.PriceTypeCode <= 0))
                            {
                                int nDefPriceTypeCd = -1;

                                try { nDefPriceTypeCd = Convert.ToInt32(poOnixHeader.DefaultPriceTypeCode); }
                                catch (Exception ex) { }

                                if (nDefPriceTypeCd > 0)
                                    TmpPrice.PriceTypeCode = nDefPriceTypeCd;
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(poOnixHeader.DefaultClassOfTrade))
                {
                    if ((poOnixProduct.USDValidPrice != null) &&
                        (String.IsNullOrEmpty(poOnixProduct.USDValidPrice.ClassOfTrade)))
                    {
                        poOnixProduct.USDValidPrice.ClassOfTrade = poOnixHeader.DefaultClassOfTrade;
                    }
                }

                if (!String.IsNullOrEmpty(poOnixHeader.DefaultLanguageOfText))
                {
                    if ((poOnixProduct.Language != null) && (String.IsNullOrEmpty(poOnixProduct.Language.LanguageCode)))
                        poOnixProduct.Language.LanguageCode = poOnixHeader.DefaultLanguageOfText;
                }

                if (poOnixProduct.OnixMeasureList != null)
                {
                    foreach (OnixLegacyMeasure TmpMeasure in poOnixProduct.OnixMeasureList)
                    {
                        if (TmpMeasure.MeasureTypeCode > 0)
                        {
                            if ((TmpMeasure.MeasureTypeCode == OnixLegacyMeasure.CONST_MEASURE_TYPE_HEIGHT) ||
                                (TmpMeasure.MeasureTypeCode == OnixLegacyMeasure.CONST_MEASURE_TYPE_THICK)  ||
                                (TmpMeasure.MeasureTypeCode == OnixLegacyMeasure.CONST_MEASURE_TYPE_WIDTH)  ||
                                (TmpMeasure.MeasureTypeCode == OnixLegacyMeasure.CONST_MEASURE_TYPE_DIAMTR))
                            {
                                if (!String.IsNullOrEmpty(poOnixHeader.DefaultLinearUnit) && String.IsNullOrEmpty(TmpMeasure.MeasureUnitCode))
                                    TmpMeasure.MeasureUnitCode = poOnixHeader.DefaultLinearUnit;
                            }
                            else if (TmpMeasure.MeasureTypeCode == OnixLegacyMeasure.CONST_MEASURE_TYPE_WEIGHT)
                            {
                                if (!String.IsNullOrEmpty(poOnixHeader.DefaultWeightUnit) && String.IsNullOrEmpty(TmpMeasure.MeasureUnitCode))
                                    TmpMeasure.MeasureUnitCode = poOnixHeader.DefaultWeightUnit;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// Since the deserialized ONIX product may not have certain values populated, we must look to the 
        /// deserialized message header (from the provided ONIX file) and check whether or not default values 
        /// are supplied for those fields.  If so, we will populate them here.
        /// 
        /// <param name="pOnixProduct">Refers to the current deserialized ONIX product being examined</param>
        /// <param name="pOnixHeader">Refers to the deserialized header that belongs to the ONIX file</param>
        /// <returns>N/A</returns>
        /// </summary>
        public static void ApplyHeaderDefaults(this OnixProduct pOnixProduct, OnixHeader pOnixHeader)
        {
            if (pOnixHeader != null)
            {
                if (!String.IsNullOrEmpty(pOnixHeader.DefaultCurrencyCode) || !String.IsNullOrEmpty(pOnixHeader.DefaultPriceType))
                {
                    foreach (OnixProductSupply TmpSupply in pOnixProduct.OnixProductSupplyList)
                    {
                        OnixSupplyDetail TmpSupplyDetail = TmpSupply.SupplyDetail;

                        if ((TmpSupplyDetail != null) && (TmpSupplyDetail.OnixPriceList != null))
                        {
                            foreach (OnixPrice TmpPrice in TmpSupplyDetail.OnixPriceList)
                            {
                                if (!String.IsNullOrEmpty(pOnixHeader.DefaultCurrencyCode) && String.IsNullOrEmpty(TmpPrice.CurrencyCode))
                                    TmpPrice.CurrencyCode = pOnixHeader.DefaultCurrencyCode;

                                if (!String.IsNullOrEmpty(pOnixHeader.DefaultPriceType) && (TmpPrice.PriceType <= 0))
                                {
                                    int nDefPriceTypeCd = -1;
                                    Int32.TryParse(pOnixHeader.DefaultPriceType, out nDefPriceTypeCd);

                                    if (nDefPriceTypeCd > 0)
                                        TmpPrice.PriceType = nDefPriceTypeCd;
                                }
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(pOnixHeader.DefaultLanguageOfText))
                {
                    if ((pOnixProduct.DescriptiveDetail != null) && (pOnixProduct.DescriptiveDetail.OnixLanguageList != null))
                    {
                        pOnixProduct.DescriptiveDetail.OnixLanguageList
                                                      .Where(x => String.IsNullOrEmpty(x.LanguageCode))
                                                      .ToList()
                                                      .ForEach(x => x.LanguageCode = pOnixHeader.DefaultLanguageOfText);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// Purges all control characters (and other bad encodings) from the ONIX file
        /// and performs needed preprocessing (i.e., ONIX-specific encodings).  Optionally, 
        /// since the ONIX parsers of this project do not use DTDs when parsing, some tags 
        /// containing XHTML can be wrapped with CDATA via this method, preventing them from 
        /// failing with these parsers.
        /// 
        /// NOTE: These are permanent alterations to the file, so this method should be used carefully.
        /// 
        /// <param name="ParserFileInfo">Indicates the file to be cleaned.</param>
        /// <param name="ShouldCDATAWrapXHTML">Indicates any commentary fields marked as XHTML should be wrapped with CDATA.</param>
        /// <returns>N/A</returns>
        /// </summary>
        public static void Clean(this FileInfo ParserFileInfo, bool ShouldCDATAWrapXHTML = false)
        {
            if ((ParserFileInfo != null) && ParserFileInfo.Exists && !ParserFileInfo.FullName.Contains(CONST_FILENAME_SKIP_REPLACE_MARKER))
            {
                // Only perform in-memory replacement if flag is set and if the file is less than 250 MB
                if (ParserFileInfo.Length < CONST_LARGE_FILE_MINIMUM)
                {
                    StringBuilder AllFileText = new StringBuilder(File.ReadAllText(ParserFileInfo.FullName));

                    AllFileText.ReplaceIsoLatinEncodings(true);

                    if (ShouldCDATAWrapXHTML)
                        AllFileText.WrapXHTMLTextWithCDATA();

                    var sAllFileText = AllFileText.ToString();

                    sAllFileText =
                         Regex.Replace(sAllFileText, CONST_REMOVE_CTRL_CHARS_REG_EXPR, "", System.Text.RegularExpressions.RegexOptions.Compiled);

                    File.WriteAllText(ParserFileInfo.FullName, sAllFileText);
                }
                else
                    throw new Exception("ERROR!  File is too large to be cleaned via this method.");
            }
        }

        /// <summary>
        /// 
        /// This callback will examine and then process any matches to our regular expression.  Basically, we will use this function
        /// to remove any unwanted encodings that are incomplete, causing our XML parser to fail.  For example, this function will 
        /// remove the following examples:
        /// 
        /// &#9996
        /// &#
        /// &#x000
        /// &#Xae
        /// 
        /// However, it will not touch and keep valid encodings/entities, like the following:
        /// 
        /// &#9996;
        /// &#xff40;
        /// &#Xae
        /// &gt;
        /// 
        /// <param name="Match">A string that fits the specs defined by a regular expression.</param>
        /// <returns>Either the string provided by 'm' (if it's considered valid) or a blank string (if it's invalid).</returns>
        /// </summary>
        static public string EncodingMatcher(Match m)
        {
            string sResult = "";

            try
            {
                if (m.Value == "&#")
                    sResult = "";
                else if (m.Value.StartsWith("&#") && (m.Value.Length > 2))
                {
                    int nEncodingVal = 0;
                    string sInsideEncoding = m.Value.Substring(2);

                    if (m.Value.EndsWith(";"))
                        sResult = m.Value;
                    else if (!Int32.TryParse(sInsideEncoding, out nEncodingVal))
                    {
                        int nIdx;
                        for (nIdx = 0; nIdx < sInsideEncoding.Length; ++nIdx)
                        {
                            char cTemp = sInsideEncoding[nIdx];
                            if (!(cTemp == 'x') && !Char.IsDigit(cTemp))
                                break;
                        }

                        if (nIdx == sInsideEncoding.Length)
                            sResult = m.Value;
                        else
                            sResult = m.Value.Substring(nIdx + 2);
                    }
                    else
                        sResult = "";
                }
                else if (m.Value.StartsWith("&"))
                    sResult = m.Value;
            }
            catch (Exception ex)
            {
                sResult = m.Value;
            }

            return sResult;
        }

        public static void FilterIncompleteEncodings(this StringBuilder FileContentBuilder)
        {
            var sAllFileText = FileContentBuilder.ToString();

            if (FilterBadEncodings)
                sAllFileText = Regex.Replace(sAllFileText, @"&#?x?[A-Za-z0-9]*;?", EncodingMatcher, RegexOptions.Compiled);

            FileContentBuilder.Clear();
            FileContentBuilder.Append(sAllFileText);
        }

        /// <summary>
        /// Returns the index of the start of the contents in a StringBuilder
        /// </summary>        
        /// <param name="value">The string to find</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="ignoreCase">if set to <c>true</c> it will ignore case</param>
        /// <returns></returns>
        public static int IndexOf(this StringBuilder sb, string value, int startIndex, bool ignoreCase = false, int maxSrchLen = 0)
        {
            int index;
            int length = value.Length;

            int maxSearchIndex = 0;

            if (maxSrchLen > 0)
                maxSearchIndex = startIndex + maxSrchLen;
            else
                maxSearchIndex = (sb.Length - length) + 1;

            if (maxSearchIndex > sb.Length)
                maxSearchIndex = (sb.Length - length) + 1;

            if (ignoreCase)
            {
                for (int i = startIndex; i < maxSearchIndex; ++i)
                {
                    if (Char.ToLower(sb[i]) == Char.ToLower(value[0]))
                    {
                        index = 1;
                        while ((index < length) && (Char.ToLower(sb[i + index]) == Char.ToLower(value[index])))
                            ++index;

                        if (index == length)
                            return i;
                    }
                }

                return -1;
            }

            for (int i = startIndex; i < maxSearchIndex; ++i)
            {
                if (sb[i] == value[0])
                {
                    index = 1;
                    while ((index < length) && (sb[i + index] == value[index]))
                        ++index;

                    if (index == length)
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Indicates whether or not the provided file is an ONIX file
        /// </summary>        
        /// <param name="poTargetFile">The file being examined</param>
        /// <returns>Boolean that indicates whether or not the file is an ONIX file</returns>
        public static bool IsOnixFile(this FileInfo poTargetFile)
        {
            string sFirstBlock = "";

            using (var stream = File.OpenRead(poTargetFile.FullName))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    char[] buffer = new char[CONST_ONIX_READ_BUFFER_LEN];

                    int n = reader.ReadBlock(buffer, 0, CONST_ONIX_READ_BUFFER_LEN);

                    char[] result = new char[n];
                    Array.Copy(buffer, result, n);

                    sFirstBlock = new string(result);
                }
            }

            return IsOnixFile(sFirstBlock);
        }

        /// <summary>
        /// Indicates whether or not the provided string is an ONIX body
        /// </summary>        
        /// <param name="psTargetFileContents">The string body being examined</param>
        /// <returns>Boolean that indicates whether or not the string body is an ONIX type</returns>
        public static bool IsOnixFile(this string psTargetFileContents)
        {
            string sTargetTag = "";

            if (psTargetFileContents.Contains(CONST_ONIX_MSG_SHORT_TAG_START))
                sTargetTag = CONST_ONIX_MSG_SHORT_TAG_START;
            else if (psTargetFileContents.Contains(CONST_ONIX_MSG_REF_TAG_START))
                sTargetTag = CONST_ONIX_MSG_REF_TAG_START;

            return (!String.IsNullOrEmpty(sTargetTag));
        }

        /// <summary>
        /// Indicates whether or not the provided file is an ONIX 3.0 file
        /// </summary>        
        /// <param name="poTargetFile">The file being examined</param>
        /// <returns>Boolean that indicates whether or not the file is an ONIX 3.0 file</returns>
        public static bool IsOnixVersion3File(this FileInfo poTargetFile)
        {            
            string sFirstBlock = "";

            using (var stream = File.OpenRead(poTargetFile.FullName))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    char[] buffer = new char[CONST_ONIX_READ_BUFFER_LEN];

                    int n = reader.ReadBlock(buffer, 0, CONST_ONIX_READ_BUFFER_LEN);

                    char[] result = new char[n];
                    Array.Copy(buffer, result, n);

                    sFirstBlock = new string(result);
                }
            }

            return IsOnixVersion3File(sFirstBlock);
        }

        /// <summary>
        /// Indicates whether or not the provided string is an ONIX 3.0 body
        /// </summary>        
        /// <param name="psTargetFileContents">The string body being examined</param>
        /// <returns>Boolean that indicates whether or not the string body is an ONIX 3.0 type</returns>
        public static bool IsOnixVersion3File(this string psTargetFileContents)
        {
            bool   bVersion3File = false;
            string sTargetTag    = "";

            if (psTargetFileContents.Contains(CONST_ONIX_MSG_SHORT_TAG_START))
                sTargetTag = CONST_ONIX_MSG_SHORT_TAG_START;
            else if (psTargetFileContents.Contains(CONST_ONIX_MSG_REF_TAG_START))
                sTargetTag = CONST_ONIX_MSG_REF_TAG_START;

            if (!String.IsNullOrEmpty(sTargetTag))
            {
                int nONIXMsgStartIdx = psTargetFileContents.IndexOf(sTargetTag);
                int nONIXMsgEndIdx   = psTargetFileContents.IndexOf(">", nONIXMsgStartIdx);

                string sONIXMsgTag = psTargetFileContents.Substring(nONIXMsgStartIdx, (nONIXMsgEndIdx - nONIXMsgStartIdx));
                if (sONIXMsgTag.Contains("3.0"))
                    bVersion3File = true;
            }

            return bVersion3File;
        }

        /// <summary>
        /// Removes all ONIX commentary bodies (which is useful for data providers who consistently
        /// send problematic garbage data in large text segments)
        /// </summary>        
        /// <returns></returns>
        public static void PurgeAllCommTags(this StringBuilder psText)
        {
            psText.PurgeTags("ContributorStatement");
            psText.PurgeTags("b049");

            psText.PurgeTags("BiographicalNote");
            psText.PurgeTags("b044");

            psText.PurgeTags("OtherText");
            psText.PurgeTags("othertext");
        }

        /// <summary>
        /// Removes the tag (and body) from the string
        /// </summary>        
        /// <param name="psCommTagName">The string to find</param>
        /// <returns></returns>
        public static void PurgeTags(this StringBuilder psText, string psCommTagName)
        {
            string sCommTagStart = "<" + psCommTagName + ">";
            string sCommTagEnd   = "</" + psCommTagName + ">";

            int nStartIdx = 0;
            int nEndIdx   = 0;
            while (nStartIdx < psText.Length)
            {
                nStartIdx = psText.IndexOf(sCommTagStart, nStartIdx);
                if (nStartIdx >= 0)
                {
                    nEndIdx = psText.IndexOf(sCommTagEnd, nStartIdx, false, 50000);
                    if (nEndIdx > 0)
                    {
                        int nCommBodyLen = ((nEndIdx - nStartIdx) + sCommTagEnd.Length);

                        psText.Remove(nStartIdx, nCommBodyLen);
                    }
                    else
                    {
                        nStartIdx += sCommTagStart.Length;
                    }
                }
                else
                {
                    break;
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
        public static void ReplaceIsoLatinEncodings(this FileInfo ParserFileInfo, bool PerformInMemory, bool ShouldReplaceTechEncodings = true)
        {
            if ((ParserFileInfo != null) && ParserFileInfo.Exists && !ParserFileInfo.FullName.Contains(CONST_FILENAME_SKIP_REPLACE_MARKER))
            {
                string sControlCharDomain = "[\x00-\x08\x0B\x0C\x0E-\x1F]";

                System.Console.WriteLine("File is of length [" + ParserFileInfo.Length + "]...");
                System.Console.Out.Flush();

                if (DebugFlag && FilterBadEncodings)
                {
                    System.Console.WriteLine("\nDEBUG: Flag set to filter out bad encodings...\n" + DateTime.Now);
                    System.Console.Out.Flush();
                }

                // Only perform in-memory replacement if flag is set and if the file is less than 250 MB
                if (PerformInMemory && (ParserFileInfo.Length < CONST_LARGE_FILE_MINIMUM))
                {
                    StringBuilder AllFileText = new StringBuilder(File.ReadAllText(ParserFileInfo.FullName));

                    AllFileText.ReplaceIsoLatinEncodings(ShouldReplaceTechEncodings);

                    var sAllFileText = AllFileText.ToString();

                    if (FilterBadEncodings)
                        sAllFileText = Regex.Replace(sAllFileText, @"&#?x?[A-Za-z0-9]*;?", EncodingMatcher, RegexOptions.Compiled);

                    sAllFileText = Regex.Replace(sAllFileText, sControlCharDomain, "", RegexOptions.Compiled);

                    File.WriteAllText(ParserFileInfo.FullName, sAllFileText);
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
                                    var sTempLine = OnixFileReader.ReadLine();

                                    if (FilterBadEncodings)
                                        sTempLine = Regex.Replace(sTempLine, @"&#?x?[A-Za-z0-9]*;?", EncodingMatcher, RegexOptions.Compiled);

                                    sTempLine = Regex.Replace(sTempLine, sControlCharDomain, "", RegexOptions.Compiled);

                                    TempLineBuilder.Append(sTempLine);
                                    TempLineBuilder.Append("\n");

                                    if (TempLineBuilder.Length >= CONST_BLOCK_COUNT_SIZE)
                                    {
                                        TempLineBuilder.ReplaceIsoLatinEncodings(ShouldReplaceTechEncodings);

                                        // NOTE: This section will remove any problematic control characters which are not allowed within XML
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
        /// NOTE: This method uses multithreading.
        /// 
        /// <param name="PerformInMemory">Indicates whether or not the encoding replacements are performed entirely in memory.</param>
        /// <param name="ShouldReplaceTechEncodings">Indicates whether or not tech encodings should be replaced.</param>
        /// <returns>N/A</returns>
        /// </summary>
        public static void ReplaceIsoLatinEncodingsMT(this FileInfo ParserFileInfo, bool PerformInMemory, bool ShouldReplaceTechEncodings = true)
        {
            if ((ParserFileInfo != null) && ParserFileInfo.Exists && !ParserFileInfo.FullName.Contains(CONST_FILENAME_SKIP_REPLACE_MARKER))
            {
                string sControlCharDomain = "[\x00-\x08\x0B\x0C\x0E-\x1F]";

                System.Console.WriteLine("\nReplaceIsoLatinEncodings in Multithread Mode \n");

                System.Console.WriteLine("File is of length [" + ParserFileInfo.Length + "]...");
                System.Console.Out.Flush();

                // Only perform in-memory replacement if flag is set and if the file is less than 250 MB
                if (PerformInMemory && (ParserFileInfo.Length < CONST_LARGE_FILE_MINIMUM))
                {
                    StringBuilder AllFileText = new StringBuilder(File.ReadAllText(ParserFileInfo.FullName));

                    AllFileText.ReplaceIsoLatinEncodings(ShouldReplaceTechEncodings);

                    var sAllFileText = AllFileText.ToString();

                    if (FilterBadEncodings)
                        sAllFileText = Regex.Replace(sAllFileText, @"&#?x?[A-Za-z0-9]*;?", EncodingMatcher, RegexOptions.Compiled);

                    sAllFileText = Regex.Replace(sAllFileText, sControlCharDomain, "", RegexOptions.Compiled);

                    File.WriteAllText(ParserFileInfo.FullName, sAllFileText);
                }
                else
                {
                    System.Console.WriteLine("\nReplaceIsoLatinEncodings in Multithread Mode - Threads Started \n");

                    int    nThreadCount = FILTER_THREAD_COUNT;
                    string sTmpFile     = ParserFileInfo.FullName + ".tmp";

                    if (DebugFlag && FilterBadEncodings)
                    {
                        System.Console.WriteLine("\nDEBUG: Flag set to filter out bad encodings...\n" + DateTime.Now);
                        System.Console.Out.Flush();
                    }

                    FileInfo fTmpFile = new FileInfo(sTmpFile);
                    if (!fTmpFile.Exists)
                    {
                        using (StreamReader OnixFileReader = new StreamReader(ParserFileInfo.FullName))
                        {
                            using (StreamWriter OnixFileWriter = new StreamWriter(sTmpFile))
                            {
                                int nLineCount  = 0;
                                int nThreadIdx  = 0;
                                int nBlockCount = 1;

                                StringBuilder[] BlockBuilders     = new StringBuilder[nThreadCount];
                                Thread[]        FilterTextThreads = new Thread[nThreadCount];

                                for (int nIdx = 0; nIdx < nThreadCount; ++nIdx)
                                    BlockBuilders[nIdx] = new StringBuilder(CONST_BLOCK_COUNT_SIZE);

                                for (nLineCount = 0; !OnixFileReader.EndOfStream; ++nLineCount)
                                {
                                    var sTempLine = OnixFileReader.ReadLine();

                                    if (FilterBadEncodings)
                                        sTempLine = Regex.Replace(sTempLine, @"&#?x?[A-Za-z0-9]*;?", EncodingMatcher, RegexOptions.Compiled);

                                    sTempLine = Regex.Replace(sTempLine, sControlCharDomain, "", RegexOptions.Compiled);

                                    BlockBuilders[nThreadIdx].Append(sTempLine);
                                    BlockBuilders[nThreadIdx].Append("\n");

                                    if (BlockBuilders[nThreadIdx].Length >= CONST_BLOCK_COUNT_SIZE)
                                    {
                                        StringBuilder TargetBlock = BlockBuilders[nThreadIdx];

                                        System.Console.WriteLine("\tReplaceIsoLatinEncodings in Multithread Mode - Starting thread [" + 
                                                                 nThreadIdx + "] and Content w/ Size(" + TargetBlock.Length + ")");
                                        System.Console.Out.Flush();

                                        FilterTextThreads[nThreadIdx] = new Thread(() => FilterTextThread(TargetBlock));
                                        FilterTextThreads[nThreadIdx].Start();

                                        nThreadIdx++;
                                    }

                                    // For large files (i.e., bigger than CONST_BLOCK_COUNT_SIZE x nThreadCount), we'll let these threads finish
                                    // before we continue on
                                    if (nThreadIdx == nThreadCount)
                                    {
                                        PersistTextBlocks(FilterTextThreads, BlockBuilders, OnixFileWriter, ref nBlockCount);
                                        nThreadIdx = 0;
                                    }
                                }

                                StringBuilder LastBlock = BlockBuilders[nThreadIdx];
                                // If there's any remaining text to filter, let's assign it to one additional thread
                                if (LastBlock.Length > 0)
                                {
                                    System.Console.WriteLine("\tReplaceIsoLatinEncodings in Multithread Mode - Starting thread [" + nThreadIdx + "]");
                                    System.Console.Out.Flush();

                                    FilterTextThreads[nThreadIdx] = new Thread(() => FilterTextThread(LastBlock));
                                    FilterTextThreads[nThreadIdx].Start();

                                    nThreadIdx++;
                                }

                                // Now let the remaining threads finish
                                PersistTextBlocks(FilterTextThreads, BlockBuilders, OnixFileWriter, ref nBlockCount);

                            } // using StreamWriter

                        } // using StreamReader
                    }

                    System.Console.WriteLine("\nReplaceIsoLatinEncodings in Multithread Mode - Threads Finished \n");

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
        /// <param name="poParserFileBlock">Text block whose content needs to be filtered (encoding replacement, etc.)</param>
        /// <returns>N/A</returns>
        /// </summary>
        public static void FilterTextThread(StringBuilder poParserFileBlock)
        {
            if (poParserFileBlock.Length > 0)
            {
                System.Console.WriteLine("Thread -> Content is of length [" + poParserFileBlock.Length + "]...");
                System.Console.Out.Flush();

                poParserFileBlock.ReplaceIsoLatinEncodings(true);

                string sCurrentBlock = poParserFileBlock.ToString();

                //
                // NOTE: This section is currently not needed
                //
                // string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
                // string ChangedFileContents = 
                //     System.Text.RegularExpressions.Regex.Replace((AllFileContents, r, "", System.Text.RegularExpressions.RegexOptions.Compiled);
                //

                if (DebugFlag)
                {
                    System.Console.WriteLine("\tThread -> Processed encodings for block of size (" + poParserFileBlock.Length + ")..." + DateTime.Now);
                    System.Console.Out.Flush();
                }
            }
        }

        /// 
        /// Since the .NET XML parser has very limited support for incorporating DTD/XSD/ENT/ELT files, this method will 
        /// perform the work on behalf of ELT utilization (i.e., by translating any ONIX shorthand codes that map to Unicode encodings).
        /// In particular, this method places on a focus on ISO Latin encodings.
        /// 
        /// <param name="poParserFileBlock">Text block whose content needs to be filtered (encoding replacement, etc.)</param>
        /// <returns>N/A</returns>
        /// </summary>
        public static void PersistTextBlocks(Thread[] paFilterThreads, StringBuilder[] paFileBlocks, StreamWriter poOnixFileWriter, ref int pnBlockCount)
        {
            for (int nIdx = 0; nIdx < paFilterThreads.GetLength(0); ++nIdx)
            {
                StringBuilder CurrentBlock = paFileBlocks[nIdx];

                paFilterThreads[nIdx].Join();
                System.Console.WriteLine("ReplaceIsoLatinEncodings in Multithread Mode - Finished Block [" + pnBlockCount + "].");

                pnBlockCount++;

                poOnixFileWriter.WriteLine(CurrentBlock);
                poOnixFileWriter.Flush();

                CurrentBlock.Clear();
            }
        }

        /// <summary>
        /// Due to the perform storm of inadequate DTD implementation of Microsoft XML libs 
        /// and the baffling acceptance by the ONIX standard of XHTML being inside XML, 
        /// this function will wraps possible XHTML values with CDATA blocks
        /// 
        /// NOTE: This should not be used with large files, since it will likely
        ///       load to an OutOfMemory exception being thrown
        /// 
        /// </summary>        
        /// <returns></returns>
        public static void WrapXHTMLTextWithCDATA(this FileInfo poParserFileInfo)
        {
            StringBuilder AllFileText = new StringBuilder(File.ReadAllText(poParserFileInfo.FullName));

            AllFileText.WrapXHTMLTextWithCDATA();

            File.WriteAllText(poParserFileInfo.FullName, AllFileText.ToString());
        }

        /// <summary>
        /// Wraps possible XHTML values with CDATA block
        /// </summary>        
        /// <returns></returns>
        public static void WrapXHTMLTextWithCDATA(this StringBuilder psText)
        {
            string sTextRefStartTag      = "<Text textformat=\"05\">";
            string sTextShortStartTag    = "<d104 textformat=\"05\">";
            string sTextRefStartTagAlt   = "<Text textformat='05'>";
            string sTextShortStartTagAlt = "<d104 textformat='05'>";

            string sBioNoteRefStartTag      = "<BiographicalNote textformat=\"05\">";
            string sBioNoteShortStartTag    = "<b044 textformat=\"05\">";
            string sBioNoteRefStartTagAlt   = "<BiographicalNote textformat='05'>";
            string sBioNoteShortStartTagAlt = "<b044 textformat='05'>";

            if (psText.IndexOf(sTextRefStartTag, 0) > 0)
                psText.WrapXHTMLTextWithCDATA(sTextRefStartTag, "</Text>");
            else
                psText.WrapXHTMLTextWithCDATA(sTextShortStartTag, "</d104>");

            if (psText.IndexOf(sTextRefStartTagAlt, 0) > 0)
                psText.WrapXHTMLTextWithCDATA(sTextRefStartTagAlt, "</Text>");
            else
                psText.WrapXHTMLTextWithCDATA(sTextShortStartTagAlt, "</d104>");

            if (psText.IndexOf(sBioNoteRefStartTag, 0) > 0)
                psText.WrapXHTMLTextWithCDATA(sBioNoteRefStartTag, "</BiographicalNote>");
            else
                psText.WrapXHTMLTextWithCDATA(sBioNoteShortStartTag, "</b044>");

            if (psText.IndexOf(sBioNoteRefStartTagAlt, 0) > 0)
                psText.WrapXHTMLTextWithCDATA(sBioNoteRefStartTagAlt, "</BiographicalNote>");
            else
                psText.WrapXHTMLTextWithCDATA(sBioNoteShortStartTagAlt, "</b044>");
        }

        /// <summary>
        /// Removes the commentary tag (and body) from the string
        /// </summary>        
        /// <param name="psCommTagName">The string to find</param>
        /// <returns></returns>
        public static void WrapXHTMLTextWithCDATA(this StringBuilder psText, 
                                                string psStartTagName, 
                                                string psEndTag, 
                                                   int pnMaxCommLen = 50000)
        {
            int nStartIdx = 0;
            int nEndIdx   = 0;

            string sCDATAStart    = "<![CDATA[";
            string sStartTagCDATA = psStartTagName + "<![CDATA[";
            string sEndTagCDATA   = "]]>" + psEndTag;

            int nLoopCount   = 0;
            int nCommBodyLen = 0;

            for (nLoopCount = 0; (nStartIdx < psText.Length) && (nLoopCount < 1000000); ++nLoopCount)
            {
                nStartIdx = psText.IndexOf(psStartTagName, nStartIdx);
                if (nStartIdx >= 0)
                {
                    nEndIdx = psText.IndexOf(psEndTag, nStartIdx, false, pnMaxCommLen);
                    
                    if (nEndIdx > 0)
                    {
                        nCommBodyLen = ((nEndIdx - nStartIdx) + psEndTag.Length);

                        if (psText.IndexOf(sCDATAStart, nStartIdx, false, nCommBodyLen) < 0)
                        {
                            psText.Replace(psEndTag, sEndTagCDATA, nEndIdx, psEndTag.Length);

                            psText.Replace(psStartTagName, sStartTagCDATA, nStartIdx, psStartTagName.Length);
                        }

                        nStartIdx += psStartTagName.Length + nCommBodyLen;
                    }
                    else
                    {
                        nStartIdx += pnMaxCommLen;
                    }
                }
                else
                    break;
            }
        }

    }
}