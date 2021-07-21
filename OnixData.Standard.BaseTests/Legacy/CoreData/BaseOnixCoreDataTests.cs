using System.IO;
using System.Linq;
using OnixData.Legacy;
using OnixData.Extensions;
using Xunit;

namespace OnixData.Standard.BaseTests.Legacy.CoreData
{
    public abstract class BaseOnixCoreDataTests
    {
        [Fact]
        public void ParseOnix2CoreDataSample1()
        {
            var sFilepath = "Samples/Onix2CoreDataSample1.xml";

            var currentFileInfo = new FileInfo(sFilepath);
            using OnixLegacyParser v2Parser = new(currentFileInfo, true);

            foreach (OnixLegacyProduct tmpProduct in v2Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal("9782234567890", tmpProduct.EAN);

                Assert.Equal("Le Merde, C'est La Vie", tmpProduct.OnixTitle);

                Assert.Equal("Le Merde", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.Equal("fre", tmpProduct.Language.LanguageCode);

                Assert.Equal("We Print Stuff", tmpProduct.OnixPublisherName);

                Assert.Equal(6.99m, tmpProduct.USDRetailPrice.PriceAmountNum);

                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
        
        [Fact]
        protected void ParseOnix2CoreDataSample1ShortTags()
        {
            var sFilepath = "Samples/Onix2CoreDataSample1ShortTags.xml";

            var currentFileInfo = new FileInfo(sFilepath);
            using OnixLegacyParser v2Parser = new(currentFileInfo, true);

            foreach (OnixLegacyProduct tmpProduct in v2Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal("9781234567890", tmpProduct.EAN);

                Assert.Equal("Generic Title: Bland and Boring", tmpProduct.OnixTitle);

                Assert.Equal("Smith", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.Equal("eng", tmpProduct.Language.LanguageCode);

                Assert.Equal("Mundane Hippo Press", tmpProduct.OnixPublisherName);

                Assert.Equal(19.95m, tmpProduct.USDRetailPrice.PriceAmountNum);

                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
    }
}