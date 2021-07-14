using System.IO;
using System.Linq;
using OnixData.Legacy;
using Xunit;

namespace OnixData.Standard.BaseTests.Legacy.Text
{
    public abstract class BaseOnixSupportingResourceTests
    {
        [Fact]
        public void ParseOnix2SupportingResourceSample1()
        {
            var sFilepath = "Samples/Onix2SupportingResourceSample1.xml";

            var currentFileInfo = new FileInfo(sFilepath);
            using OnixLegacyParser v2Parser = new(currentFileInfo, true);

            foreach (OnixLegacyProduct tmpProduct in v2Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);
                
                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
        
        [Fact]
        protected void ParseOnix2SupportingResourceSample1ShortTags()
        {
            var sFilepath = "Samples/Onix2SupportingResourceSample1ShortTags.xml";

            var currentFileInfo = new FileInfo(sFilepath);
            using OnixLegacyParser v2Parser = new(currentFileInfo, true);

            foreach (OnixLegacyProduct tmpProduct in v2Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
    }
}