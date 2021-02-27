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

        private const int  CONST_MSG_REFERENCE_LENGTH = 2048;
        private const int  CONST_BLOCK_COUNT_SIZE     = 50000000;
        private const long CONST_LARGE_FILE_MINIMUM   = 250000000;
        private const int  FILTER_THREAD_COUNT        = 3;

        private const string CONST_FILENAME_SKIP_REPLACE_MARKER = "SKIPREPLACECHARENC";

        private const string CONST_ONIX_MSG_REF_TAG_START   = "<" + OnixParser.CONST_ONIX_MESSAGE_REFERENCE_TAG;
        private const string CONST_ONIX_MSG_SHORT_TAG_START = "<" + OnixParser.CONST_ONIX_MESSAGE_SHORT_TAG;

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
                        for (nIdx = 0; nIdx < sInsideEncoding.ToCharArray().Length; ++nIdx)
                        {
                            char cTemp = sInsideEncoding.ToCharArray()[nIdx];
                            if (!(cTemp == 'x') && !Char.IsDigit(cTemp))
                                break;
                        }

                        if (nIdx == sInsideEncoding.ToCharArray().Length)
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
                    char[] buffer = new char[256];

                    int n = reader.ReadBlock(buffer, 0, 256);

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
                    char[] buffer = new char[256];

                    int n = reader.ReadBlock(buffer, 0, 256);

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
        /// 
        /// Since the .NET XML parser has very limited support for incorporating DTD/XSD/ENT/ELT files, this method will 
        /// perform the work on behalf of ELT utilization (i.e., by translating any ONIX shorthand codes that map to Unicode encodings).
        /// In particular, this method places on a focus on ISO Latin encodings.
        /// 
        /// <param name="ParserFileContent">Text to be  performed entirely in memory.</param>
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
                LegacyOnixFileText.Replace("&utdot;", "&#x022F0;"); //<!--three dots, ascending -->
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
                LegacyOnixFileText.Replace("&frasl;",  "&#8260;");   //< !--fraction slash, U + 2044 NEW-- >
                LegacyOnixFileText.Replace("&gt;",     "&#x0003E;"); //<!--=greater-than sign R: -->^M
                LegacyOnixFileText.Replace("&half;",   "&#x000BD;"); //<!--=fraction one-half -->^M
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
                LegacyOnixFileText.Replace("&lt;", "&#x0003C;"); //<!--=less-than sign R: -->^M
                LegacyOnixFileText.Replace("&micro;", "&#x000B5;"); //<!--=micro sign -->^M
                LegacyOnixFileText.Replace("&middot;", "&#x000B7;"); //<!--/centerdot B: =middle dot -->^M
                LegacyOnixFileText.Replace("&nbsp;", "&#x000A0;"); //<!--=no break (required) space -->^M
                LegacyOnixFileText.Replace("&not;",   "&#x000AC;"); //<!--/neg /lnot =not sign -->^M
                LegacyOnixFileText.Replace("&num;",   "&#x00023;"); //<!--=number sign -->^M
                LegacyOnixFileText.Replace("&ohm;",   "&#x02126;"); //<!--=ohm sign -->^M
                LegacyOnixFileText.Replace("&oline;", "&#8254;"); // < !--overline = spacing overscore, U + 203E NEW-- >
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

                // < !--Letterlike Symbols-- >
                LegacyOnixFileText.Replace("&weierp;",  "&#8472;"); // < !--script capital P = power set = Weierstrass p, U + 2118 ISOamso-- >
                LegacyOnixFileText.Replace("&image;",   "&#8465;"); // < !--blackletter capital I = imaginary part, U + 2111 ISOamso-- >
                LegacyOnixFileText.Replace("&real;",    "&#8476;"); // < !--blackletter capital R = real part symbol, U + 211C ISOamso -->
                LegacyOnixFileText.Replace("&trade;",   "&#8482;"); // < !--trade mark sign, U+2122 ISOnum-- >
                LegacyOnixFileText.Replace("&alefsym;", "&#8501;"); // < !--alef symbol = first transfinite cardinal, U + 2135 NEW-- >

                // < !--Mathematical Operators-- >
                LegacyOnixFileText.Replace("&forall;", "&#8704;"); // < !-- for all, U + 2200 ISOtech-- >
                LegacyOnixFileText.Replace("&part;",   "&#8706;"); // < !--partial differential, U + 2202 ISOtech-- >
                LegacyOnixFileText.Replace("&exist;",  "&#8707;"); // < !--there exists, U + 2203 ISOtech-- >
                LegacyOnixFileText.Replace("&empty;",  "&#8709;"); // < !--empty set = null set = diameter, U + 2205 ISOamso-- >
                LegacyOnixFileText.Replace("&nabla;",  "&#8711;"); // < !--nabla = backward difference, U + 2207 ISOtech-- >
                LegacyOnixFileText.Replace("&isin;",   "&#8712;"); // < !--element of, U + 2208 ISOtech-- >
                LegacyOnixFileText.Replace("&notin;",  "&#8713;"); // < !--not an element of, U + 2209 ISOtech-- >
                LegacyOnixFileText.Replace("&ni;",     "&#8715;"); // < !--contains as member, U + 220B ISOtech-- >               
                LegacyOnixFileText.Replace("&prod;",   "&#8719;"); // < !--n - ary product = product sign, U + 220F ISOamsb-- >
                LegacyOnixFileText.Replace("&sum;",    "&#8721;"); // < !--n - ary sumation, U + 2211 ISOamsb-- >, NOT the same character as U + 03A3 'greek capital letter sigma'
                LegacyOnixFileText.Replace("&minus;",  "&#8722;"); // < !--minus sign, U + 2212 ISOtech-- >
                LegacyOnixFileText.Replace("&lowast;", "&#8727;"); // < !--asterisk operator, U + 2217 ISOtech-- >
                LegacyOnixFileText.Replace("&radic;",  "&#8730;"); // < !--square root = radical sign, U + 221A ISOtech-- >
                LegacyOnixFileText.Replace("&prop;",   "&#8733;"); // < !--proportional to, U + 221D ISOtech-- >
                LegacyOnixFileText.Replace("&infin;",  "&#8734;"); // < !--infinity, U + 221E ISOtech-- >
                LegacyOnixFileText.Replace("&ang;",    "&#8736;"); // < !--angle, U + 2220 ISOamso-- >
                LegacyOnixFileText.Replace("&and;",    "&#8743;"); // < !--logical and = wedge, U + 2227 ISOtech-- >
                LegacyOnixFileText.Replace("&or;",     "&#8744;"); // < !--logical or = vee, U + 2228 ISOtech-- >
                LegacyOnixFileText.Replace("&cap;",    "&#8745;"); // < !--intersection = cap, U + 2229 ISOtech-- >
                LegacyOnixFileText.Replace("&cup;",    "&#8746;"); // < !--union = cup, U + 222A ISOtech-- >
                LegacyOnixFileText.Replace("&int;",    "&#8747;"); // < !--integral, U + 222B ISOtech-- >
                LegacyOnixFileText.Replace("&there4;", "&#8756;"); // < !--therefore, U + 2234 ISOtech-- >
                LegacyOnixFileText.Replace("&sim;",    "&#8764;"); // < !--tilde operator = varies with = similar to, U + 223C ISOtech-- >
                LegacyOnixFileText.Replace("&cong;",   "&#8773;"); // < !--approximately equal to, U + 2245 ISOtech-- >
                LegacyOnixFileText.Replace("&asymp;",  "&#8776;"); // < !--almost equal to = asymptotic to, U + 2248 ISOamsr-- >
                LegacyOnixFileText.Replace("&ne;",     "&#8800;"); // < !--not equal to, U + 2260 ISOtech-- >
                LegacyOnixFileText.Replace("&equiv;",  "&#8801;"); // < !--identical to, U + 2261 ISOtech-- >
                LegacyOnixFileText.Replace("&le;",     "&#8804;"); // < !--less - than or equal to, U + 2264 ISOtech-- >
                LegacyOnixFileText.Replace("&ge;",     "&#8805;"); // < !--greater - than or equal to, U + 2265 ISOtech-- >
                LegacyOnixFileText.Replace("&sub;",    "&#8834;"); // < !--subset of, U + 2282 ISOtech-- >
                LegacyOnixFileText.Replace("&sup;",    "&#8835;"); // < !--superset of, U + 2283 ISOtech-- >

                LegacyOnixFileText.Replace("&angzarr;",  "&#x0237C;"); // <!--angle with down zig-zag arrow -->
                LegacyOnixFileText.Replace("&crarr;",    "&#8629;");   // < !--downwards arrow with corner leftwards = carriage return, U + 21B5 NEW -->
                LegacyOnixFileText.Replace("&cirmid;",   "&#x02AEF;"); //<!--circle, mid below -->
                LegacyOnixFileText.Replace("&cudarrl;",  "&#x02938;"); //<!--left, curved, down arrow -->
                LegacyOnixFileText.Replace("&cudarrr;",  "&#x02935;"); //<!--right, curved, down arrow -->
                LegacyOnixFileText.Replace("&cularr;",   "&#x021B6;"); //<!--/curvearrowleft A: left curved arrow -->
                LegacyOnixFileText.Replace("&cularrp;",  "&#x0293D;"); //<!--curved left arrow with plus -->
                LegacyOnixFileText.Replace("&curarr;",   "&#x021B7;"); //<!--/curvearrowright A: rt curved arrow -->
                LegacyOnixFileText.Replace("&curarrm;",  "&#x0293C;"); //<!--curved right arrow with minus -->
                LegacyOnixFileText.Replace("&dArr;",     "&#x021D3;"); //<!--/Downarrow A: down dbl arrow -->
                LegacyOnixFileText.Replace("&Darr;",     "&#x021A1;"); //<!--down two-headed arrow -->
                LegacyOnixFileText.Replace("&ddarr;",    "&#x021CA;"); //<!--/downdownarrows A: two down arrows -->
                LegacyOnixFileText.Replace("&DDotrahd;", "&#x02911;"); //<!--right arrow with dotted stem -->
                LegacyOnixFileText.Replace("&dfisht;",   "&#x0297F;"); //<!--down fish tail -->
                LegacyOnixFileText.Replace("&dHar;",     "&#x02965;"); //<!--down harpoon-left, down harpoon-right -->
                LegacyOnixFileText.Replace("&dharl;",    "&#x021C3;"); //<!--/downharpoonleft A: dn harpoon-left -->
                LegacyOnixFileText.Replace("&dharr;",    "&#x021C2;"); //<!--/downharpoonright A: down harpoon-rt -->
                LegacyOnixFileText.Replace("&duarr;",    "&#x021F5;"); //<!--down arrow, up arrow -->
                LegacyOnixFileText.Replace("&duhar;",    "&#x0296F;"); //<!--down harp, up harp -->
                LegacyOnixFileText.Replace("&dzigrarr;", "&#x0F5A2;"); //<!--right long zig-zag arrow -->
                LegacyOnixFileText.Replace("&erarr;",    "&#x02971;"); //<!--equal, right arrow below -->
                LegacyOnixFileText.Replace("&harr;",     "&#x02194;"); //<!--/leftrightarrow A: l&r arrow -->
                LegacyOnixFileText.Replace("&hArr;",     "&#x021D4;"); //<!--/Leftrightarrow A: l&r dbl arrow -->
                LegacyOnixFileText.Replace("&harrcir;",  "&#x02948;"); //<!--left and right arrow with a circle -->
                LegacyOnixFileText.Replace("&harrw;",    "&#x021AD;"); //<!--/leftrightsquigarrow A: l&r arr-wavy -->
                LegacyOnixFileText.Replace("&hoarr;",    "&#x021FF;"); //<!--horizontal open arrow -->
                LegacyOnixFileText.Replace("&imof;",     "&#x022B7;"); //<!--image of -->
                LegacyOnixFileText.Replace("&lAarr;",    "&#x021DA;"); //<!--/Lleftarrow A: left triple arrow -->
                LegacyOnixFileText.Replace("&Larr;",     "&#x0219E;"); //<!--/twoheadleftarrow A: -->
                LegacyOnixFileText.Replace("&larrbfs;",  "&#x0291F;"); //<!--left arrow-bar, filled square -->
                LegacyOnixFileText.Replace("&larrfs;",   "&#x0291D;"); //<!--left arrow, filled square -->
                LegacyOnixFileText.Replace("&larrhk;",   "&#x021A9;"); //<!--/hookleftarrow A: left arrow-hooked -->
                LegacyOnixFileText.Replace("&larrlp;",   "&#x021AB;"); //<!--/looparrowleft A: left arrow-looped -->
                LegacyOnixFileText.Replace("&larrpl;",   "&#x02939;"); //<!--left arrow, plus -->
                LegacyOnixFileText.Replace("&larrsim;",  "&#x02973;"); //<!--left arrow, similar -->
                LegacyOnixFileText.Replace("&larrtl;",   "&#x021A2;"); //<!--/leftarrowtail A: left arrow-tailed -->
                LegacyOnixFileText.Replace("&latail;",   "&#x02919;"); //<!--left arrow-tail -->
                LegacyOnixFileText.Replace("&lAtail;",   "&#x0291B;"); //<!--left double arrow-tail -->
                LegacyOnixFileText.Replace("&lbarr;",    "&#x0290C;"); //<!--left broken arrow -->
                LegacyOnixFileText.Replace("&lBarr;",    "&#x0290E;"); //<!--left doubly broken arrow -->
                LegacyOnixFileText.Replace("&ldca;",     "&#x02936;"); //<!--left down curved arrow -->
                LegacyOnixFileText.Replace("&ldrdhar;",  "&#x02967;"); //<!--left harpoon-down over right harpoon-down -->
                LegacyOnixFileText.Replace("&ldrushar;", "&#x0294B;"); //<!--left-down-right-up harpoon -->
                LegacyOnixFileText.Replace("&ldsh;",     "&#x021B2;"); //<!--left down angled arrow -->
                LegacyOnixFileText.Replace("&lfisht;",   "&#x0297C;"); //<!--left fish tail -->
                LegacyOnixFileText.Replace("&lHar;",     "&#x02962;"); //<!--left harpoon-up over left harpoon-down -->
                LegacyOnixFileText.Replace("&lhard;",    "&#x021BD;"); //<!--/leftharpoondown A: l harpoon-down -->
                LegacyOnixFileText.Replace("&lharu;",    "&#x021BC;"); //<!--/leftharpoonup A: left harpoon-up -->
                LegacyOnixFileText.Replace("&lharul;",   "&#x0296A;"); //<!--left harpoon-up over long dash -->
                LegacyOnixFileText.Replace("&llarr;",    "&#x021C7;"); //<!--/leftleftarrows A: two left arrows -->
                LegacyOnixFileText.Replace("&llhard;",   "&#x0296B;"); //<!--left harpoon-down below long dash -->
                LegacyOnixFileText.Replace("&loarr;",    "&#x021FD;"); //<!--left open arrow -->
                LegacyOnixFileText.Replace("&lrarr;",    "&#x021C6;"); //<!--/leftrightarrows A: l arr over r arr -->
                LegacyOnixFileText.Replace("&lrhar;",    "&#x021CB;"); //<!--/leftrightharpoons A: l harp over r -->
                LegacyOnixFileText.Replace("&lrhard;",   "&#x0296D;"); //<!--right harpoon-down below long dash -->
                LegacyOnixFileText.Replace("&lsh;",      "&#x021B0;"); //<!--/Lsh A: -->
                LegacyOnixFileText.Replace("&lurdshar;", "&#x0294A;"); //<!--left-up-right-down harpoon -->
                LegacyOnixFileText.Replace("&luruhar;",  "&#x02966;"); //<!--left harpoon-up over right harpoon-up -->
                LegacyOnixFileText.Replace("&map;",      "&#x021A6;"); //<!--/mapsto A: -->
                LegacyOnixFileText.Replace("&Map;",      "&#x02905;"); //<!--twoheaded mapsto -->
                LegacyOnixFileText.Replace("&midcir;",   "&#x02AF0;"); //<!--mid, circle below  -->
                LegacyOnixFileText.Replace("&mumap;",    "&#x022B8;"); //<!--/multimap A: -->
                LegacyOnixFileText.Replace("&nearhk;",   "&#x02924;"); //<!--NE arrow-hooked -->
                LegacyOnixFileText.Replace("&nearr;",    "&#x02197;"); //<!--/nearrow A: NE pointing arrow -->
                LegacyOnixFileText.Replace("&neArr;",    "&#x021D7;"); //<!--NE pointing dbl arrow -->
                LegacyOnixFileText.Replace("&nesear;",   "&#x02928;"); //<!--/toea A: NE & SE arrows -->
                LegacyOnixFileText.Replace("&nharr;",    "&#x021AE;"); //<!--/nleftrightarrow A: not l&r arrow -->
                LegacyOnixFileText.Replace("&nhArr;",    "&#x021CE;"); //<!--/nLeftrightarrow A: not l&r dbl arr -->
                LegacyOnixFileText.Replace("&nlarr;",    "&#x0219A;"); //<!--/nleftarrow A: not left arrow -->
                LegacyOnixFileText.Replace("&nlArr;",    "&#x021CD;"); //<!--/nLeftarrow A: not implied by -->
                LegacyOnixFileText.Replace("&nrarr;",    "&#x0219B;"); //<!--/nrightarrow A: not right arrow -->
                LegacyOnixFileText.Replace("&nrArr;",    "&#x021CF;"); //<!--/nRightarrow A: not implies -->
                LegacyOnixFileText.Replace("&nrarrc;",   "&#x02933;&#x00338;"); //<!--not right arrow-curved -->
                LegacyOnixFileText.Replace("&nrarrw;",   "&#x0219D;&#x00338;"); //<!--not right arrow-wavy -->
                LegacyOnixFileText.Replace("&nvHarr;",   "&#x021CE;"); //<!--not, vert, left and right double arrow  -->
                LegacyOnixFileText.Replace("&nvlArr;",   "&#x021CD;"); //<!--not, vert, left double arrow -->
                LegacyOnixFileText.Replace("&nvrArr;",   "&#x021CF;"); //<!--not, vert, right double arrow -->
                LegacyOnixFileText.Replace("&nwarhk;",   "&#x02923;"); //<!--NW arrow-hooked -->
                LegacyOnixFileText.Replace("&nwarr;",    "&#x02196;"); //<!--/nwarrow A: NW pointing arrow -->
                LegacyOnixFileText.Replace("&nwArr;",    "&#x021D6;"); //<!--NW pointing dbl arrow -->
                LegacyOnixFileText.Replace("&nwnear;",   "&#x02927;"); //<!--NW & NE arrows -->
                LegacyOnixFileText.Replace("&olarr;",    "&#x021BA;"); //<!--/circlearrowleft A: l arr in circle -->
                LegacyOnixFileText.Replace("&orarr;",    "&#x021BB;"); //<!--/circlearrowright A: r arr in circle -->
                LegacyOnixFileText.Replace("&origof;",   "&#x022B6;"); //<!--original of -->
                LegacyOnixFileText.Replace("&rAarr;",    "&#x021DB;"); //<!--/Rrightarrow A: right triple arrow -->
                LegacyOnixFileText.Replace("&Rarr;",     "&#x021A0;"); //<!--/twoheadrightarrow A: -->
                LegacyOnixFileText.Replace("&rarrap;",   "&#x02975;"); //<!--approximate, right arrow above -->
                LegacyOnixFileText.Replace("&rarrbfs;",  "&#x02920;"); //<!--right arrow-bar, filled square -->
                LegacyOnixFileText.Replace("&rarrc;",    "&#x02933;"); //<!--right arrow-curved -->
                LegacyOnixFileText.Replace("&rarrfs;",   "&#x0291E;"); //<!--right arrow, filled square -->
                LegacyOnixFileText.Replace("&rarrhk;",   "&#x021AA;"); //<!--/hookrightarrow A: rt arrow-hooked -->
                LegacyOnixFileText.Replace("&rarrlp;",   "&#x021AC;"); //<!--/looparrowright A: rt arrow-looped -->
                LegacyOnixFileText.Replace("&rarrpl;",   "&#x02945;"); //<!--right arrow, plus -->
                LegacyOnixFileText.Replace("&rarrsim;",  "&#x02974;"); //<!--right arrow, similar -->
                LegacyOnixFileText.Replace("&rarrtl;",   "&#x021A3;"); //<!--/rightarrowtail A: rt arrow-tailed -->
                LegacyOnixFileText.Replace("&Rarrtl;",   "&#x02916;"); //<!--right two-headed arrow with tail -->
                LegacyOnixFileText.Replace("&rarrw;",    "&#x0219D;"); //<!--/rightsquigarrow A: rt arrow-wavy -->
                LegacyOnixFileText.Replace("&ratail;",   "&#x021A3;"); //<!--right arrow-tail -->
                LegacyOnixFileText.Replace("&rAtail;",   "&#x0291C;"); //<!--right double arrow-tail -->
                LegacyOnixFileText.Replace("&rbarr;",    "&#x0290D;"); //<!--/bkarow A: right broken arrow -->
                LegacyOnixFileText.Replace("&rBarr;",    "&#x0290F;"); //<!--/dbkarow A: right doubly broken arrow -->
                LegacyOnixFileText.Replace("&RBarr;",    "&#x02910;"); //<!--/drbkarow A: twoheaded right broken arrow -->
                LegacyOnixFileText.Replace("&rdca;",     "&#x02937;"); //<!--right down curved arrow -->
                LegacyOnixFileText.Replace("&rdldhar;",  "&#x02969;"); //<!--right harpoon-down over left harpoon-down -->
                LegacyOnixFileText.Replace("&rdsh;",     "&#x021B3;"); //<!--right down angled arrow -->
                LegacyOnixFileText.Replace("&rfisht;",   "&#x0297D;"); //<!--right fish tail -->
                LegacyOnixFileText.Replace("&rHar;",     "&#x02964;"); //<!--right harpoon-up over right harpoon-down -->
                LegacyOnixFileText.Replace("&rhard;",    "&#x021C1;"); //<!--/rightharpoondown A: rt harpoon-down -->
                LegacyOnixFileText.Replace("&rharu;",    "&#x021C0;"); //<!--/rightharpoonup A: rt harpoon-up -->
                LegacyOnixFileText.Replace("&rharul;",   "&#x0296C;"); //<!--right harpoon-up over long dash -->
                LegacyOnixFileText.Replace("&rlarr;",    "&#x021C4;"); //<!--/rightleftarrows A: r arr over l arr -->
                LegacyOnixFileText.Replace("&rlhar;",    "&#x021CC;"); //<!--/rightleftharpoons A: r harp over l -->
                LegacyOnixFileText.Replace("&roarr;",    "&#x021FE;"); //<!--right open arrow -->
                LegacyOnixFileText.Replace("&rrarr;",    "&#x021C9;"); //<!--/rightrightarrows A: two rt arrows -->
                LegacyOnixFileText.Replace("&rsh;",      "&#x021B1;"); //<!--/Rsh A: -->
                LegacyOnixFileText.Replace("&ruluhar;",  "&#x02968;"); //<!--right harpoon-up over left harpoon-up -->
                LegacyOnixFileText.Replace("&searhk;",   "&#x02925;"); //<!--/hksearow A: SE arrow-hooken -->
                LegacyOnixFileText.Replace("&searr;",    "&#x02198;"); //<!--/searrow A: SE pointing arrow -->
                LegacyOnixFileText.Replace("&seArr;",    "&#x021D8;"); //<!--SE pointing dbl arrow -->
                LegacyOnixFileText.Replace("&seswar;",   "&#x02929;"); //<!--/tosa A: SE & SW arrows -->
                LegacyOnixFileText.Replace("&simrarr;",  "&#x02972;"); //<!--similar, right arrow below -->
                LegacyOnixFileText.Replace("&slarr;",    "&#x02190;&#x0FE00;"); //<!--short left arrow -->
                LegacyOnixFileText.Replace("&srarr;",    "&#x02192;&#x0FE00;"); //<!--short right arrow -->
                LegacyOnixFileText.Replace("&swarhk;",   "&#x02926;"); //<!--/hkswarow A: SW arrow-hooked -->
                LegacyOnixFileText.Replace("&swarr;",    "&#x02199;"); //<!--/swarrow A: SW pointing arrow -->
                LegacyOnixFileText.Replace("&swArr;",    "&#x021D9;"); //<!--SW pointing dbl arrow -->
                LegacyOnixFileText.Replace("&swnwar;",   "&#x0292A;"); //<!--SW & NW arrows -->
                LegacyOnixFileText.Replace("&uArr;",     "&#x021D1;"); //<!--/Uparrow A: up dbl arrow -->
                LegacyOnixFileText.Replace("&Uarr;",     "&#x0219F;"); //<!--up two-headed arrow -->
                LegacyOnixFileText.Replace("&Uarrocir;", "&#x02949;"); //<!--up two-headed arrow above circle -->
                LegacyOnixFileText.Replace("&udarr;",    "&#x021C5;"); //<!--up arrow, down arrow -->
                LegacyOnixFileText.Replace("&udhar;",    "&#x0296E;"); //<!--up harp, down harp -->
                LegacyOnixFileText.Replace("&ufisht;",   "&#x0297E;"); //<!--up fish tail -->
                LegacyOnixFileText.Replace("&uHar;",     "&#x02963;"); //<!--up harpoon-left, up harpoon-right -->
                LegacyOnixFileText.Replace("&uharl;",    "&#x021BF;"); //<!--/upharpoonleft A: up harpoon-left -->
                LegacyOnixFileText.Replace("&uharr;",    "&#x021BE;"); //<!--/upharpoonright /restriction A: up harp-r -->
                LegacyOnixFileText.Replace("&uuarr;",    "&#x021C8;"); //<!--/upuparrows A: two up arrows -->
                LegacyOnixFileText.Replace("&varr;",     "&#x02195;"); //<!--/updownarrow A: up&down arrow -->
                LegacyOnixFileText.Replace("&vArr;",     "&#x021D5;"); //<!--/Updownarrow A: up&down dbl arrow -->
                LegacyOnixFileText.Replace("&xharr;",    "&#x0F578;"); //<!--/longleftrightarrow A: long l&r arr -->
                LegacyOnixFileText.Replace("&xhArr;",    "&#x0F57B;"); //<!--/Longleftrightarrow A: long l&r dbl arr -->
                LegacyOnixFileText.Replace("&xlarr;",    "&#x0F576;"); //<!--/longleftarrow A: long left arrow -->
                LegacyOnixFileText.Replace("&xlArr;",    "&#x0F579;"); //<!--/Longleftarrow A: long l dbl arrow -->
                LegacyOnixFileText.Replace("&xmap;",     "&#x0F57D;"); //<!--/longmapsto A: -->
                LegacyOnixFileText.Replace("&xrarr;",    "&#x0F577;"); //<!--/longrightarrow A: long right arrow -->
                LegacyOnixFileText.Replace("&xrArr;",    "&#x0F57A;"); //<!--/Longrightarrow A: long rt dbl arr -->
                LegacyOnixFileText.Replace("&zigrarr;",  "&#x021DD;"); //<!--right zig-zag arrow -->

                LegacyOnixFileText.Replace("&nsub;", "&#8836;"); // < !--not a subset of, U + 2284 ISOamsn-- >
                LegacyOnixFileText.Replace("&sube;", "&#8838;"); // < !--subset of or equal to, U + 2286 ISOtech-- >
                LegacyOnixFileText.Replace("&supe;", "&#8839;"); // < !--superset of or equal to, U + 2287 ISOtech-- >
                LegacyOnixFileText.Replace("&oplus;", "&#8853;"); // < !--circled plus = direct sum, U + 2295 ISOamsb-- >
                LegacyOnixFileText.Replace("&otimes;", "&#8855;"); // < !--circled times = vector product, U + 2297 ISOamsb-- >
                LegacyOnixFileText.Replace("&sdot;", "&#8901;"); // < !--dot operator, U + 22C5 ISOamsb-- >
                // < !--dot operator is NOT the same character as U + 00B7 middle dot-- >
                // < !--Miscellaneous Technical-- >
                LegacyOnixFileText.Replace("&lceil;", "&#8968;"); // < !--left ceiling = apl upstile, U + 2308 ISOamsc-- >
                LegacyOnixFileText.Replace("&rceil;", "&#8969;"); // < !--right ceiling, U + 2309 ISOamsc-- >
                LegacyOnixFileText.Replace("&lfloor;", "&#8970;"); // < !--left floor = apl downstile, U + 230A ISOamsc-- >
                LegacyOnixFileText.Replace("&rfloor;", "&#8971;"); // < !--right floor, U + 230B ISOamsc-- >
                // < !--black here seems to mean filled as opposed to hollow-- >
                LegacyOnixFileText.Replace("&hearts;", "&#9829;"); // < !--black heart suit = valentine, U + 2665 ISOpub-- >

                // From the "iso-pub.ent" file
                LegacyOnixFileText.Replace("&blank;",    "&#x02423;"); //<!--=significant blank symbol -->
                LegacyOnixFileText.Replace("&blk12;",    "&#x02592;"); //<!--=50% shaded block -->
                LegacyOnixFileText.Replace("&blk14;",    "&#x02591;"); //<!--=25% shaded block -->
                LegacyOnixFileText.Replace("&blk34;",    "&#x02593;"); //<!--=75% shaded block -->
                LegacyOnixFileText.Replace("&block;",    "&#x02588;"); //<!--=full block -->
                LegacyOnixFileText.Replace("&bull;",     "&#x02022;"); //<!--/bullet B: =round bullet, filled -->
                LegacyOnixFileText.Replace("&caret;",    "&#x02041;"); //<!--=caret (insertion mark) -->
                LegacyOnixFileText.Replace("&check;",    "&#x02713;"); //<!--/checkmark =tick, check mark -->
                LegacyOnixFileText.Replace("&cir;",      "&#x025CB;"); //<!--/circ B: =circle, open -->
                LegacyOnixFileText.Replace("&clubs;",    "&#x02663;"); //<!--/clubsuit =club suit symbol  -->
                LegacyOnixFileText.Replace("&copysr;",   "&#x02117;"); //<!--=sound recording copyright sign -->
                LegacyOnixFileText.Replace("&cross;",    "&#x02717;"); //<!--=ballot cross -->
                LegacyOnixFileText.Replace("&dagger;",   "&#x02020;"); //<!--/dagger B: =dagger -->
                LegacyOnixFileText.Replace("&Dagger;",   "&#x02021;"); //<!--/ddagger B: =double dagger -->
                LegacyOnixFileText.Replace("&dash;",     "&#x02010;"); //<!--=hyphen (true graphic) -->
                LegacyOnixFileText.Replace("&diams;",    "&#x02666;"); //<!--/diamondsuit =diamond suit symbol  -->
                LegacyOnixFileText.Replace("&dlcrop;",   "&#x0230D;"); //<!--downward left crop mark  -->
                LegacyOnixFileText.Replace("&drcrop;",   "&#x0230C;"); //<!--downward right crop mark  -->
                LegacyOnixFileText.Replace("&dtri;",     "&#x025BF;"); //<!--/triangledown =down triangle, open -->
                LegacyOnixFileText.Replace("&dtrif;",    "&#x025BE;"); //<!--/blacktriangledown =dn tri, filled -->
                LegacyOnixFileText.Replace("&emsp;",     "&#x02003;"); //<!--=em space -->
                LegacyOnixFileText.Replace("&emsp13;",   "&#x02004;"); //<!--=1/3-em space -->
                LegacyOnixFileText.Replace("&emsp14;",   "&#x02005;"); //<!--=1/4-em space -->
                LegacyOnixFileText.Replace("&ensp;",     "&#x02002;"); //<!--=en space (1/2-em) -->
                LegacyOnixFileText.Replace("&female;",   "&#x02640;"); //<!--=female symbol -->
                LegacyOnixFileText.Replace("&ffilig;",   "&#x0FB03;"); //<!--small ffi ligature -->
                LegacyOnixFileText.Replace("&fflig;",    "&#x0FB00;"); //<!--small ff ligature -->
                LegacyOnixFileText.Replace("&ffllig;",   "&#x0FB04;"); //<!--small ffl ligature -->
                LegacyOnixFileText.Replace("&filig;",    "&#x0FB01;"); //<!--small fi ligature -->
                LegacyOnixFileText.Replace("&flat;",     "&#x0266D;"); //<!--/flat =musical flat -->
                LegacyOnixFileText.Replace("&fllig;",    "&#x0FB02;"); //<!--small fl ligature -->
                LegacyOnixFileText.Replace("&frac13;",   "&#x02153;"); //<!--=fraction one-third -->
                LegacyOnixFileText.Replace("&frac15;",   "&#x02155;"); //<!--=fraction one-fifth -->
                LegacyOnixFileText.Replace("&frac16;",   "&#x02159;"); //<!--=fraction one-sixth -->
                LegacyOnixFileText.Replace("&frac23;",   "&#x02154;"); //<!--=fraction two-thirds -->
                LegacyOnixFileText.Replace("&frac25;",   "&#x02156;"); //<!--=fraction two-fifths -->
                LegacyOnixFileText.Replace("&frac35;",   "&#x02157;"); //<!--=fraction three-fifths -->
                LegacyOnixFileText.Replace("&frac45;",   "&#x02158;"); //<!--=fraction four-fifths -->
                LegacyOnixFileText.Replace("&frac56;",   "&#x0215A;"); //<!--=fraction five-sixths -->
                LegacyOnixFileText.Replace("&hairsp;",   "&#x0200A;"); //<!--=hair space -->
                LegacyOnixFileText.Replace("&hellip;",   "&#x02026;"); //<!--=ellipsis (horizontal) -->
                LegacyOnixFileText.Replace("&hybull;",   "&#x02043;"); //<!--rectangle, filled (hyphen bullet) -->
                LegacyOnixFileText.Replace("&incare;",   "&#x02105;"); //<!--=in-care-of symbol -->
                LegacyOnixFileText.Replace("&ldquor;",   "&#x0201E;"); //<!--=rising dbl quote, left (low) -->
                LegacyOnixFileText.Replace("&lhblk;",    "&#x02584;"); //<!--=lower half block -->
                LegacyOnixFileText.Replace("&loz;",      "&#x025CA;"); //<!--/lozenge - lozenge or total mark -->
                LegacyOnixFileText.Replace("&lozf;",     "&#x029EB;"); //<!--/blacklozenge - lozenge, filled -->
                LegacyOnixFileText.Replace("&lsquor;",   "&#x0201A;"); //<!--=rising single quote, left (low) -->
                LegacyOnixFileText.Replace("&ltri;",     "&#x025C3;"); //<!--/triangleleft B: l triangle, open -->
                LegacyOnixFileText.Replace("&ltrif;",    "&#x025C2;"); //<!--/blacktriangleleft R: =l tri, filled -->
                LegacyOnixFileText.Replace("&male;",     "&#x02642;"); //<!--=male symbol -->
                LegacyOnixFileText.Replace("&malt;",     "&#x02720;"); //<!--/maltese =maltese cross -->
                LegacyOnixFileText.Replace("&marker;",   "&#x025AE;"); //<!--=histogram marker -->
                LegacyOnixFileText.Replace("&mdash;",    "&#x02014;"); //<!--=em dash  -->
                LegacyOnixFileText.Replace("&mldr;",     "&#x02026;"); //<!--em leader -->
                LegacyOnixFileText.Replace("&natur;",    "&#x0266E;"); //<!--/natural - music natural -->
                LegacyOnixFileText.Replace("&ndash;",    "&#x02013;"); //<!--=en dash -->
                LegacyOnixFileText.Replace("&nldr;",     "&#x02025;"); //<!--=double baseline dot (en leader) -->
                LegacyOnixFileText.Replace("&numsp;",    "&#x02007;"); //<!--=digit space (width of a number) -->
                LegacyOnixFileText.Replace("&phone;",    "&#x0260E;"); //<!--=telephone symbol  -->
                LegacyOnixFileText.Replace("&puncsp;",   "&#x02008;"); //<!--=punctuation space (width of comma) -->
                LegacyOnixFileText.Replace("&rdquor;",   "&#x0201D;"); //<!--rising dbl quote, right (high) -->
                LegacyOnixFileText.Replace("&rect;",     "&#x025AD;"); //<!--=rectangle, open -->
                LegacyOnixFileText.Replace("&rsquor;",   "&#x02019;"); //<!--rising single quote, right (high) -->
                LegacyOnixFileText.Replace("&rtri;",     "&#x025B9;"); //<!--/triangleright B: r triangle, open -->
                LegacyOnixFileText.Replace("&rtrif;",    "&#x025B8;"); //<!--/blacktriangleright R: =r tri, filled -->
                LegacyOnixFileText.Replace("&rx;",       "&#x0211E;"); //<!--pharmaceutical prescription (Rx) -->
                LegacyOnixFileText.Replace("&sext;",     "&#x02736;"); //<!--sextile (6-pointed star) -->
                LegacyOnixFileText.Replace("&sharp;",    "&#x0266F;"); //<!--/sharp =musical sharp -->
                LegacyOnixFileText.Replace("&spades;",   "&#x02660;"); //<!--/spadesuit =spades suit symbol  -->
                LegacyOnixFileText.Replace("&squ;",      "&#x025A1;"); //<!--=square, open -->
                LegacyOnixFileText.Replace("&squf;",     "&#x025AA;"); //<!--/blacksquare =sq bullet, filled -->
                LegacyOnixFileText.Replace("&star;",     "&#x022C6;"); //<!--=star, open -->
                LegacyOnixFileText.Replace("&starf;",    "&#x02605;"); //<!--/bigstar - star, filled  -->
                LegacyOnixFileText.Replace("&target;",   "&#x02316;"); //<!--register mark or target -->
                LegacyOnixFileText.Replace("&telrec;",   "&#x02315;"); //<!--=telephone recorder symbol -->
                LegacyOnixFileText.Replace("&thinsp;",   "&#x02009;"); //<!--=thin space (1/6-em) -->
                LegacyOnixFileText.Replace("&uhblk;",    "&#x02580;"); //<!--=upper half block -->
                LegacyOnixFileText.Replace("&ulcrop;",   "&#x0230F;"); //<!--upward left crop mark  -->
                LegacyOnixFileText.Replace("&urcrop;",   "&#x0230E;"); //<!--upward right crop mark  -->
                LegacyOnixFileText.Replace("&utri;",     "&#x025B5;"); //<!--/triangle =up triangle, open -->
                LegacyOnixFileText.Replace("&utrif;",    "&#x025B4;"); //<!--/blacktriangle =up tri, filled -->
                LegacyOnixFileText.Replace("&vellip;",   "&#x022EE;"); //<!--vertical ellipsis -->
            }
        }
    }
}