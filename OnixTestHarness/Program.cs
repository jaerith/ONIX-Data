using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData;
using OnixData.Extensions;
using OnixData.Legacy;
using OnixData.Version3;
using OnixData.Version3.Header;

namespace OnixTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool bTestLegacyParser1  = false;
                bool bTestLegacyParser2  = false;
                bool bTestV3Parser1      = true;
                bool bTestV3Parser2      = true;
                bool bTestV3StringParser = true;    
                bool bTestParseXML       = true;

                int    nLegacyShortIdx = 0;
                int    nLegacyPrdIdx   = 0;
                int    nOnixPrdIdx     = 0;
                string sLegShortFile   = @"C:\tmp\tmp2\onix.legacy.short.xml";
                string sLegacyXml      = File.ReadAllText(@"C:\tmp\tmp2\onix.legacy.xml");
                string sOnixXml        = File.ReadAllText(@"C:\tmp\tmp2\onix.sample.v3.xml");
                string sLegacyShortXml = File.ReadAllText(@"C:\tmp\tmp2\onix.legacy.short.xml");

                string sEan     = "117";
                bool   bIsValid = sEan.IsValidEAN();

                if (bTestLegacyParser1)
                {
                    // using (OnixLegacyParser onixLegacyShortParser = new OnixLegacyParser(new FileInfo(sLegShortFile), true))
                    // using (OnixLegacyParser onixLegacyShortParser = new OnixLegacyParser(new FileInfo(sLegShortFile), true, false, false))
                    using (OnixLegacyParser onixLegacyShortParser = new OnixLegacyParser(sLegacyShortXml, true, false, false))
                    {
                        bool ValidFile = onixLegacyShortParser.ValidateFile();

                        OnixLegacyHeader Header = onixLegacyShortParser.MessageHeader;

                        foreach (OnixLegacyProduct TmpProduct in onixLegacyShortParser)
                        {
                            int x = 0;

                            string[] TypeCodes = TmpProduct.OnixEditionTypeCodeList;

                            if ((TmpProduct.OnixContributorList != null) && (TmpProduct.OnixContributorList.Length > 0))
                            {
                                foreach (OnixLegacyContributor TempContributor in TmpProduct.OnixContributorList)
                                {
                                    System.Console.WriteLine("Key Names (" + TempContributor.OnixKeyNames +
                                                             "), Names before Key (" + TempContributor.OnixNamesBeforeKey + ")");
                                }
                            }

                            if ((TmpProduct.OnixAudRangeList != null) && (TmpProduct.OnixAudRangeList.Length > 0))
                            {
                                OnixLegacyAudRange AudRange = TmpProduct.OnixAudRangeList[0];

                                string AgeFrom = AudRange.USAgeFrom;
                                string AgeTo   = AudRange.USAgeTo;

                                System.Console.WriteLine("AgeFrom(" + AgeFrom + "), AgeTo(" + AgeTo + ")");
                            }

                            if (TmpProduct.IsValid())
                            {
                                string sEANStatus  = TmpProduct.HasValidEAN() ? "valid" : "invalid";
                                string sISBNStatus = TmpProduct.HasValidISBN() ? "valid" : "invalid";

                                if (TmpProduct.HasValidEAN())
                                {
                                    string sTestISBN = TmpProduct.EAN.ConvertEANToISBN();
                                }

                                if (TmpProduct.HasValidISBN())
                                {
                                    string sTestEAN = TmpProduct.ISBN.ConvertISBNToEAN();
                                }

                                System.Console.WriteLine("Legacy Product [" + (nLegacyShortIdx++) + "] has " + sEANStatus + " EAN(" +
                                                         TmpProduct.EAN + "), " + sISBNStatus + " ISBN(" + TmpProduct.ISBN + 
                                                         "), and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                         ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                            }
                            else
                            {
                                System.Console.WriteLine(TmpProduct.GetParsingError());
                            }
                        }
                    }
                }

                if (bTestLegacyParser2)
                {
                    FileInfo LegacyFileInfo = new FileInfo(@"C:\tmp\tmp2\onix.legacy.xml");
                    using (OnixLegacyParser LegacyParser = new OnixLegacyParser(LegacyFileInfo, true, false))
                    {
                        OnixLegacyHeader Header = LegacyParser.MessageHeader;

                        foreach (OnixLegacyProduct TmpProduct in LegacyParser)
                        {
                            if ((TmpProduct.OnixAudRangeList != null) && (TmpProduct.OnixAudRangeList.Length > 0))
                            {
                                OnixLegacyAudRange AudRange = TmpProduct.OnixAudRangeList[0];

                                string AgeFrom = AudRange.USAgeFrom;
                                string AgeTo   = AudRange.USAgeTo;

                                System.Console.WriteLine("AgeFrom(" + AgeFrom + "), AgeTo(" + AgeTo + ")");
                            }

                            if (TmpProduct.IsValid())
                            {
                                System.Console.WriteLine("Legacy Product [" + (nLegacyPrdIdx++) + "] has EAN(" +
                                                            TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                            ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                            }
                            else
                            {
                                System.Console.WriteLine(TmpProduct.GetParsingError());
                            }
                        }
                    }
                }

                if (bTestV3Parser1)
                {
                    nOnixPrdIdx = 0;

                    FileInfo CurrentFileInfo = new FileInfo(@"C:\tmp\tmp2\onix.sample.v3.xml");
                    using (OnixParser V3Parser = new OnixParser(CurrentFileInfo, true))
                    {
                        OnixHeader Header = V3Parser.MessageHeader;

                        foreach (OnixProduct TmpProduct in V3Parser)
                        {
                            long   lTempEAN  = TmpProduct.EAN;
                            string sTempISBN = TmpProduct.ISBN;

                            OnixData.Version3.Price.OnixPrice USDPrice = TmpProduct.USDRetailPrice;

                            if (TmpProduct.IsValid())
                            {
                                System.Console.WriteLine("Product [" + (nOnixPrdIdx++) + "] has EAN(" +
                                                         TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                         ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                            }
                            else
                            {
                                System.Console.WriteLine(TmpProduct.GetParsingError());
                            }
                        }
                    }
                }

                if (bTestV3Parser2)
                {
                    nOnixPrdIdx = 0;

                    FileInfo CurrentFileInfo = new FileInfo(@"C:\tmp\tmp2\onix.sample.v3.short.xml");
                    using (OnixParser V3Parser = new OnixParser(CurrentFileInfo, false, true))
                    {
                        OnixHeader Header = V3Parser.MessageHeader;

                        foreach (OnixProduct TmpProduct in V3Parser)
                        {
                            long   lTempEAN  = TmpProduct.EAN;
                            string sTempISBN = TmpProduct.ISBN;

                            OnixContributor MainAuthor = TmpProduct.PrimaryAuthor;

                            if (TmpProduct.HasUSDRetailPrice())
                            {
                                OnixData.Version3.Price.OnixPrice USDPrice = TmpProduct.USDRetailPrice;
                            }

                            if (TmpProduct.IsValid())
                            {
                                System.Console.WriteLine("Product [" + (nOnixPrdIdx++) + "] has EAN(" +
                                                         TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                         ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                            }
                            else
                            {
                                System.Console.WriteLine(TmpProduct.GetParsingError());
                            }
                        }
                    }
                }

                if (bTestV3StringParser)
                {
                    nOnixPrdIdx = 0;

                    using (OnixParser V3Parser = new OnixParser(sOnixXml, false, true))
                    {
                        OnixHeader Header = V3Parser.MessageHeader;

                        foreach (OnixProduct TmpProduct in V3Parser)
                        {
                            long   lTempEAN  = TmpProduct.EAN;
                            string sTempISBN = TmpProduct.ISBN;

                            OnixContributor MainAuthor = TmpProduct.PrimaryAuthor;

                            if (TmpProduct.HasUSDRetailPrice())
                            {
                                OnixData.Version3.Price.OnixPrice USDPrice = TmpProduct.USDRetailPrice;
                            }

                            if (TmpProduct.IsValid())
                            {
                                System.Console.WriteLine("Product [" + (nOnixPrdIdx++) + "] has EAN(" +
                                                         TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                         ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                            }
                            else
                            {
                                System.Console.WriteLine(TmpProduct.GetParsingError());
                            }
                        }
                    }
                }                

                if (bTestParseXML)
                {
                    OnixLegacyMessage onixLegacyMessage = sLegacyXml.ParseXML<OnixLegacyMessage>();

                    foreach (OnixLegacyProduct TmpProduct in onixLegacyMessage.Product)
                    {
                        System.Console.WriteLine("Legacy Product [" + (nLegacyPrdIdx++) + "] has EAN(" +
                                                 TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                 ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                    }

                    /**
                     ** NOTE: Not working at the moment
                     **
                    OnixMessage onixMessage = sOnixXml.ParseXML<OnixMessage>();
                    foreach (OnixProduct TmpProduct in onixMessage.Product)
                    {
                        System.Console.WriteLine("Product [" + (nOnixPrdIdx++) + "] has EAN(" +
                                                 TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                 ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                    }
                     **/
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
