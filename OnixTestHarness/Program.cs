using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData;
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
                int    nLegacyShortIdx = 0;
                int    nLegacyPrdIdx   = 0;
                int    nOnixPrdIdx     = 0;
                string sLegShortFile   = @"C:\tmp\tmp2\onix.legacy.short.xml";
                string sLegacyXml      = File.ReadAllText(@"C:\tmp\tmp2\onix.legacy.xml");
                string sOnixXml        = File.ReadAllText(@"C:\tmp\tmp2\onix.sample.v3.xml");
                string sLegacyShortXml = File.ReadAllText(@"C:\tmp\tmp2\onix.legacy.short.xml");

                if (true)
                {
                    using (OnixLegacyParser onixLegacyShortParser = new OnixLegacyParser(new FileInfo(sLegShortFile), true))
                    {
                        bool ValidFile = onixLegacyShortParser.ValidateFile();

                        OnixLegacyHeader Header = onixLegacyShortParser.MessageHeader;

                        foreach (OnixLegacyProduct TmpProduct in onixLegacyShortParser)
                        {
                            int x = 0;

                            // string[] TypeCodes = TmpProduct.OnixEditionTypeCodeList;

                            if ((TmpProduct.OnixAudRangeList != null) && (TmpProduct.OnixAudRangeList.Length > 0))
                            {
                                OnixLegacyAudRange AudRange = TmpProduct.OnixAudRangeList[0];

                                string AgeFrom = AudRange.USAgeFrom;
                                string AgeTo   = AudRange.USAgeTo;

                                System.Console.WriteLine("AgeFrom(" + AgeFrom + "), AgeTo(" + AgeTo + ")");
                            }

                            if (TmpProduct.IsValid())
                            {
                                System.Console.WriteLine("Product [" + (nLegacyShortIdx++) + "] has EAN(" +
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

                if (true)
                {
                    FileInfo LegacyFileInfo = new FileInfo(@"C:\tmp\tmp2\onix.legacy.xml");
                    using (OnixLegacyParser LegacyParser = new OnixLegacyParser(LegacyFileInfo, true, false))
                    {
                        OnixLegacyHeader Header = LegacyParser.MessageHeader;

                        foreach (OnixLegacyProduct TmpProduct in LegacyParser)
                        {
                            // string[] TypeCodes = TmpProduct.OnixEditionTypeCodeList;

                            if ((TmpProduct.OnixAudRangeList != null) && (TmpProduct.OnixAudRangeList.Length > 0))
                            {
                                OnixLegacyAudRange AudRange = TmpProduct.OnixAudRangeList[0];

                                string AgeFrom = AudRange.USAgeFrom;
                                string AgeTo   = AudRange.USAgeTo;

                                System.Console.WriteLine("AgeFrom(" + AgeFrom + "), AgeTo(" + AgeTo + ")");
                            }

                            if (TmpProduct.IsValid())
                            {
                                System.Console.WriteLine("Product [" + (nLegacyPrdIdx++) + "] has EAN(" +
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

                if (true)
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

                            // string[] TypeCodes = TmpProduct.OnixEditionTypeCodeList;

                            if (TmpProduct.IsValid())
                            {
                                System.Console.WriteLine("Product [" + (nLegacyPrdIdx++) + "] has EAN(" +
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

                if (true)
                {
                    nOnixPrdIdx = 0;

                    FileInfo CurrentFileInfo = new FileInfo(@"C:\tmp\tmp2\onix.sample.v3.short.xml");
                    using (OnixParser V3Parser = new OnixParser(true, CurrentFileInfo, false, false))
                    {
                        OnixHeader Header = V3Parser.MessageHeader;

                        foreach (OnixProduct TmpProduct in V3Parser)
                        {
                            long   lTempEAN  = TmpProduct.EAN;
                            string sTempISBN = TmpProduct.ISBN;

                            OnixContributor MainAuthor = TmpProduct.PrimaryAuthor;

                            OnixData.Version3.Price.OnixPrice USDPrice = TmpProduct.USDRetailPrice;

                            // string[] TypeCodes = TmpProduct.OnixEditionTypeCodeList;

                            if (TmpProduct.IsValid())
                            {
                                System.Console.WriteLine("Product [" + (nLegacyPrdIdx++) + "] has EAN(" +
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

                if (false)
                {
                    OnixLegacyMessage onixLegacyMessage = sLegacyXml.ParseXML<OnixLegacyMessage>();

                    foreach (OnixLegacyProduct TmpProduct in onixLegacyMessage.Product)
                    {
                        System.Console.WriteLine("Product [" + (nLegacyPrdIdx++) + "] has EAN(" +
                                                 TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                 ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                    }

                    OnixMessage onixMessage = sOnixXml.ParseXML<OnixMessage>();
                    foreach (OnixProduct TmpProduct in onixMessage.Product)
                    {
                        System.Console.WriteLine("Product [" + (nOnixPrdIdx++) + "] has EAN(" +
                                                 TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                                 ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
