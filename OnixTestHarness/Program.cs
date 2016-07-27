using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Legacy;

namespace OnixTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int    nPrdIdx = 0;
                string xml     = File.ReadAllText(@"C:\tmp\tmp2\onix.legacy.xml");

                OnixLegacyMessage onixLegacyMessage = xml.ParseXML<OnixLegacyMessage>();

                foreach (OnixLegacyProduct TmpProduct in onixLegacyMessage.Product)
                {
                    System.Console.WriteLine("Product [" + (nPrdIdx++) + "] has EAN(" + TmpProduct.EAN + ").");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
