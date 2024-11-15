using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OnixData.Standard.Extensions
{
    public static class OnixXmlExtensions
    {
        public static bool ContainsBadXmlRecords(this StringBuilder psbText, 
                                                             string psRecordTagName, 
                                         Dictionary<string, string> poBadRecords,
                                                             string psRecordKeyTag = "",
                                                               bool pbPurgeBadRecords = false)
        {
            bool bContainsBadXmlRecords = false;

            string sRecordTagStart = "<" + psRecordTagName + ">";
            string sRecordTagEnd   = "</" + psRecordTagName + ">";

            int nStartIdx = 0;
            int nEndIdx   = 0;

            for (int i = 0; nStartIdx < psbText.Length; ++i)
            {
                nStartIdx = psbText.IndexOf(sRecordTagStart, nStartIdx);
                if (nStartIdx >= 0)
                {
                    nEndIdx = psbText.IndexOf(sRecordTagEnd, nStartIdx, false, 20000000);
                    if (nEndIdx > 0)
                    {
                        int nBodyLen = ((nEndIdx - nStartIdx) + sRecordTagEnd.Length);

                        string sTmpRecord = psbText.ToString(nStartIdx, nBodyLen);

                        bool bIsBadRecord = !IsValidXmlRecord(sTmpRecord, psRecordTagName);
                        if (bIsBadRecord)
                        {
                            bContainsBadXmlRecords = true;

                            if (String.IsNullOrEmpty(psRecordKeyTag))
                            {
                                poBadRecords[Convert.ToString(i)] = sTmpRecord;
                            }                                
                            else
                            {
                                string sKey = GetTagValue(sTmpRecord, psRecordKeyTag);
                                if (!String.IsNullOrEmpty(sKey))
                                {
                                    if (poBadRecords.ContainsKey(sKey))
                                        sKey = sKey + "_Record[" + Convert.ToString(i) + "]";

                                    poBadRecords[sKey] = sTmpRecord;
                                }
                                else
                                    poBadRecords[Convert.ToString(i)] = sTmpRecord;
                            }
                        }

                        if (bIsBadRecord && pbPurgeBadRecords)
                            psbText.Remove(nStartIdx, nBodyLen);
                        else
                            nStartIdx += ((nEndIdx - nStartIdx) + sRecordTagEnd.Length);
                    }
                    else
                    {
                        nStartIdx += sRecordTagStart.Length;
                    }
                }
                else
                    break;
            }

            return bContainsBadXmlRecords;
        }

        #region Support Methods

        private static string GetTagValue(string psXmlRecord, string psKeyTag)
        {
            string sValue = "";

            try
            {
                string sKeyTagStart = "<" + psKeyTag + ">";
                string sKeyTagEnd   = "</" + psKeyTag + ">";

                int nStartIdx = 0;
                int nEndIdx   = 0;

                for (int i = 0; nStartIdx < psXmlRecord.Length; ++i)
                {
                    nStartIdx = psXmlRecord.IndexOf(sKeyTagStart, nStartIdx);
                    if (nStartIdx >= 0)
                    {
                        nEndIdx = psXmlRecord.IndexOf(sKeyTagEnd, nStartIdx);
                        if (nEndIdx > 0)
                        {
                            sValue =
                                psXmlRecord.Substring(nStartIdx + sKeyTagStart.Length,
                                                      nEndIdx - (nStartIdx + sKeyTagStart.Length));

                            break;
                        }
                    }
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                sValue = String.Empty;
            }

            if (!String.IsNullOrEmpty(sValue))
            {
                sValue = sValue.Trim();

                if (sValue.StartsWith("<![CDATA["))
                {
                    sValue = sValue.Replace("<![CDATA[", "");

                    int nBlockEndIdx = sValue.LastIndexOf("]]>");
                    if (nBlockEndIdx > 0)
                        sValue = sValue.Remove(nBlockEndIdx);
                }
            }

            return sValue;
        }

        private static bool IsValidXmlRecord(string psTmpXmlRecord, string psRecordTagName)
        {
            bool   bIsValidXml = true;
            string sTmpBody    = String.Empty;

            XmlReader BadXmlReader = null;

            try
            {
                // NOTE: Since the XML body needs a root tag
                string sTmpXml = "<root>" + psTmpXmlRecord + "</root>";

                BadXmlReader =
                    new OnixXmlTextReader(sTmpXml) { DtdProcessing = DtdProcessing.Ignore };

                BadXmlReader.MoveToContent();

                while (BadXmlReader.Read())
                {
                    if ((BadXmlReader.NodeType == XmlNodeType.Element) && (BadXmlReader.Name == psRecordTagName))
                    {
                        sTmpBody = BadXmlReader.ReadOuterXml();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                bIsValidXml = false;
            }

            return bIsValidXml;
        }

        #endregion
    }
}

