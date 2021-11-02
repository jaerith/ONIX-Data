using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            var sRecordTagName   = "Product";
            var sKeyTagName      = "Ean";
            var sBadCharFilepath = "Samples/Onix3sample_refnames_badchar.xml";
            var sOrigFilepath    = "Samples/Onix3sample_refnames.xml";
            var sFilepath        = sOrigFilepath + ".copy.xml";

            Dictionary<string, string> BadRecords = new Dictionary<string, string>();

            // We will make a copy, since we are going to alter it
            File.Copy(sOrigFilepath, sFilepath, true);

            StringBuilder sbXmlFileContents =
                new StringBuilder(File.ReadAllText(sBadCharFilepath));

            var bNotValid = sbXmlFileContents.ContainsBadXmlRecords(sRecordTagName, BadRecords, "Ean", true);

            Assert.Equal(true, bNotValid);

            Assert.Equal(1, BadRecords.Count);

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

                Assert.Equal("http://www.harpercollins.co.uk", tmpProduct.PublishingDetail.OnixPublisherList[0].OnixWebsiteList[0].OnixWebsiteLinkList[0]);
                Assert.Equal("http://www.harpercollins.co.uk", tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixSupplierList[0].OnixWebsiteList[0].OnixWebsiteLinkList[0]);

                Assert.Equal("The Martin Beck series", tmpProduct.SeriesTitle);
                Assert.Equal("1", tmpProduct.SeriesNumber);

                Assert.Equal("FIC022000", tmpProduct.BisacCategoryCode.SubjectCode);

                Assert.Equal("16", tmpProduct.DescriptiveDetail.OnixExtentList.FirstOrDefault(x => x.ExtentType == 4).ExtentValue);

                Assert.Equal(130.00m, tmpProduct.Width.Measurement);
                Assert.Equal("mm", tmpProduct.Width.MeasureUnitCode);

                Assert.Equal("GB", tmpProduct.DescriptiveDetail.CountryOfManufacture);

                Assert.Equal(false, tmpProduct.PublishingDetail.SalesRightsInUSFlag);

                Assert.Equal("20060807", tmpProduct.PublishingDetail.PublicationDate);

                Assert.Equal(10.99m, tmpProduct.USDRetailPrice.PriceAmountNum);
                Assert.Equal(10.99m, tmpProduct.USDValidPrice.PriceAmountNum);

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

                Assert.Equal("02", tmpProduct.OnixBarcodeList[0].BarcodeType);
                Assert.Equal("09", tmpProduct.OnixBarcodeList[0].PositionOnProduct);

                Assert.Equal("eng", tmpProduct.DescriptiveDetail.LanguageOfText);

                Assert.Equal("HarperCollins Publishers", tmpProduct.PublisherName);

                Assert.Equal("http://www.harpercollins.co.uk", tmpProduct.PublishingDetail.OnixPublisherList[0].OnixWebsiteList[0].OnixWebsiteLinkList[0]);
                Assert.Equal("http://www.harpercollins.co.uk", tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixSupplierList[0].OnixWebsiteList[0].OnixWebsiteLinkList[0]);

                Assert.Equal(130.00m, tmpProduct.Width.Measurement);
                Assert.Equal("mm", tmpProduct.Width.MeasureUnitCode);

                Assert.Equal(4, tmpProduct.OnixProductSupplyList[0].MarketPublishingDetail.MarketPublishingStatus);

                Assert.Equal("Y", tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixReturnsConditionsList[0].ReturnsCode);

                Assert.Equal(0.00m, tmpProduct.USDRetailPrice.PriceAmountNum);

                Assert.Equal(7.99m, tmpProduct.USDValidPrice.PriceAmountNum);

                Assert.Equal(7.99m, tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].PriceAmountNum);
                Assert.Equal(OnixData.Version3.Price.OnixPrice.CONST_PRICE_TYPE_RRP_INCL, tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].PriceType);
                Assert.Equal("GBP", tmpProduct.OnixProductSupplyList[0].SupplyDetail.OnixPriceList[0].CurrencyCode);

                Assert.Equal("9780007324378", tmpProduct.RelatedMaterial.OnixRelatedProductList[0].EAN);

                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
    }
}
