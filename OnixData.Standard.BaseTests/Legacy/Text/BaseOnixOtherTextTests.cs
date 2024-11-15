using System.IO;
using OnixData.Standard.Legacy;
using Xunit;

namespace OnixData.Standard.BaseTests.Legacy.Text
{
    public abstract class BaseOnixOtherTextTests
    {
        [Fact]
        public void ParseOnix2CoreDataSample1()
        {
            var sFilepath = "Samples/Onix2CoreDataSample1.xml";

            var currentFileInfo = new FileInfo(sFilepath);

            using OnixLegacyParser v2Parser = new(currentFileInfo, false, true);

            foreach (OnixLegacyProduct tmpProduct in v2Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal("9782234567890", tmpProduct.EAN);

                Assert.Equal("Le personnage principal dit \"Je m'en fous.\"", tmpProduct.OnixOtherTextList[0].Text);

                /**
                 ** NOTE: Add more tests here
                 **/
            }

            using OnixLegacyPlusParser v2PlusParser = new(currentFileInfo, false, true);

            foreach (OnixLegacyProduct tmpProduct in v2PlusParser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal("9782234567890", tmpProduct.EAN);
            }
        }

        [Fact]
        protected void ParseOnix2CoreDataSample1ShortTags()
        {
            var sFilepath = "Samples/Onix2CoreDataSample1ShortTags.xml";

            var currentFileInfo = new FileInfo(sFilepath);
            using OnixLegacyParser v2Parser = new(currentFileInfo, false, true, true);

            foreach (OnixLegacyProduct tmpProduct in v2Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal("9781234567890", tmpProduct.EAN);

                Assert.Equal("Man, this book is so boring.", tmpProduct.OnixOtherTextList[0].Text);
                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
    }
}