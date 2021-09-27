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

            Assert.Equal("Global Bookinfo", v3Parser.MessageHeader.Sender.SenderName);

            Assert.Equal("BooksBooksBooks.com", v3Parser.MessageHeader.Addressee.AddresseeName);

            foreach (OnixProduct tmpProduct in v3Parser)
            {                
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal(9780007232833, tmpProduct.EAN);
                
                Assert.True(tmpProduct.EAN.ToString().IsValidEAN());

                Assert.Equal("Roseanna", tmpProduct.Title);

                Assert.Equal("BC", tmpProduct.ProductForm);

                Assert.Equal("eng", tmpProduct.DescriptiveDetail.LanguageOfText);

                Assert.Equal(245, tmpProduct.DescriptiveDetail.PageNumber);

                Assert.Equal("Sjöwall", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.Equal("HarperCollins Publishers", tmpProduct.PublisherName);

                Assert.Equal("The Martin Beck series", tmpProduct.SeriesTitle);
                Assert.Equal("1", tmpProduct.SeriesNumber);

                Assert.Equal("FIC022000", tmpProduct.BisacCategoryCode.SubjectCode);

                Assert.Equal(130.00m, tmpProduct.Width.Measurement);
                Assert.Equal("mm", tmpProduct.Width.MeasureUnitCode);

                Assert.Equal("GB", tmpProduct.DescriptiveDetail.CountryOfManufacture);

                Assert.Equal(false, tmpProduct.PublishingDetail.SalesRightsInUSFlag);

                Assert.Equal("20060807", tmpProduct.PublishingDetail.PublicationDate);

                Assert.Equal(10.99m, tmpProduct.USDRetailPrice.PriceAmountNum);

                Assert.Equal(7.99m, tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].PriceAmountNum);
                Assert.Equal("GBP", tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].CurrencyCode);

                Assert.Equal("01/21/2022", tmpProduct.LastDateForReturns);

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

            Assert.Equal("Global Bookinfo", v3Parser.MessageHeader.Sender.SenderName);

            Assert.Equal("BooksBooksBooks.com", v3Parser.MessageHeader.Addressee.AddresseeName);

            foreach (OnixProduct tmpProduct in v3Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal(9780007232833, tmpProduct.EAN);

                Assert.True(tmpProduct.EAN.ToString().IsValidEAN());

                Assert.Equal("0007232837", tmpProduct.EAN.ToString().ConvertEANToISBN());

                Assert.Equal("Roseanna", tmpProduct.Title);

                Assert.Equal("Sjöwall", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.Equal("eng", tmpProduct.DescriptiveDetail.LanguageOfText);

                Assert.Equal("HarperCollins Publishers", tmpProduct.PublisherName);

                Assert.Equal(130.00m, tmpProduct.Width.Measurement);
                Assert.Equal("mm", tmpProduct.Width.MeasureUnitCode);

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
