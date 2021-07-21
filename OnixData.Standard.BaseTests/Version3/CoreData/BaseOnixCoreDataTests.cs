using System.IO;
using System.Linq;
using OnixData.Extensions;
using OnixData.Version3;
using Xunit;

namespace OnixData.Standard.BaseTests.Version3.CoreData
{
    public abstract class BaseOnixCoreDataTests
    {
        [Fact]
        public void ParseOnix3CoreDataSample()
        {
            var sOrigFilepath = "Samples/Onix3sample_refnames.xml";
            var sFilepath     = sOrigFilepath + ".copy.xml";

            // We will make a copy, since we are going to alter it
            File.Copy(sOrigFilepath, sFilepath, true);

            var currentFileInfo = new FileInfo(sFilepath);

            // Preprocesing needed when there are instances of <Text textformat="05">
            currentFileInfo.WrapXHTMLTextWithCDATA();

            using OnixParser v3Parser = new(currentFileInfo, false, true, true, false);

            foreach (OnixProduct tmpProduct in v3Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal(9780007232833, tmpProduct.EAN);

                Assert.Equal("Roseanna", tmpProduct.Title);

                Assert.Equal("Sjöwall", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.Equal("eng", tmpProduct.DescriptiveDetail.LanguageOfText);

                Assert.Equal("HarperCollins Publishers", tmpProduct.PublisherName);

                Assert.Equal(0.00m, tmpProduct.USDRetailPrice.PriceAmountNum);

                Assert.Equal(7.99m, tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].PriceAmountNum);
                Assert.Equal("GBP", tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].CurrencyCode);

                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }

        [Fact]
        protected void ParseOnix3CoreDataSampleShortTags()
        {
            var sOrigFilepath = "Samples/Onix3sample_shorttags.xml";
            var sFilepath     = sOrigFilepath + ".copy.xml";

            // We will make a copy, since we are going to alter it
            File.Copy(sOrigFilepath, sFilepath, true);

            var currentFileInfo = new FileInfo(sFilepath);

            // Preprocesing needed when there are instances of <Text textformat="05">
            currentFileInfo.WrapXHTMLTextWithCDATA();

            using OnixParser v3Parser = new(currentFileInfo, false, true, true, false);

            foreach (OnixProduct tmpProduct in v3Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal(9780007232833, tmpProduct.EAN);

                Assert.Equal("Roseanna", tmpProduct.Title);

                Assert.Equal("Sjöwall", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.Equal("eng", tmpProduct.DescriptiveDetail.LanguageOfText);

                Assert.Equal("HarperCollins Publishers", tmpProduct.PublisherName);

                Assert.Equal(0.00m, tmpProduct.USDRetailPrice.PriceAmountNum);

                Assert.Equal(7.99m, tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].PriceAmountNum);
                Assert.Equal("GBP", tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].CurrencyCode);

                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
    }
}
