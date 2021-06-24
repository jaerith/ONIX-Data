using System.IO;
using OnixData.Version3;
using OnixData.Version3.Header;
using Xunit;

namespace OnixData.Standard.BaseTests
{
    public abstract class BaseOnixV3ParserTests
    {
        [Theory]
        // [InlineData("Onix3sample_refnames")] //Broken
        [InlineData("Onix3sample_refnames_blockupdate")]
        // [InlineData("Onix3sample_shorttags")]//Broken
        [InlineData("Onix3sample_shorttags_blockupdate")]
        public void IsV3ParserGivenSampleFileManageToParseFiles(string sampleFilename)
        {
            var sFilepath = $"Samples/{sampleFilename}.xml";

            var currentFileInfo = new FileInfo(sFilepath);
            using OnixParser v3Parser = new OnixParser(currentFileInfo, true);

            foreach (OnixProduct tmpProduct in v3Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);
            }
        }
    }
}
