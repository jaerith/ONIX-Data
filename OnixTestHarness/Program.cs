using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Legacy;
using OnixData.Version3;

namespace OnixTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int    nLegacyPrdIdx = 0;
                int    nOnixPrdIdx   = 0;
                string sLegacyXml    = File.ReadAllText(@"C:\tmp\tmp2\onix.legacy.xml");
                string sOnixXml      = File.ReadAllText(@"C:\tmp\tmp2\onix.sample.v3.xml");

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
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
