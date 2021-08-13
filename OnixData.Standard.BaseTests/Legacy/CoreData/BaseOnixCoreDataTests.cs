using System;
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

            Assert.True(currentFileInfo.Exists, "File does not exist!");

            using OnixLegacyParser v2Parser =
                new(File.ReadAllText(currentFileInfo.FullName), false, true, true);

            Assert.Equal("Mundane Hippo Press", v2Parser.MessageHeader.FromCompany);

            foreach (OnixLegacyProduct tmpProduct in v2Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal("9782234567890", tmpProduct.EAN);

                Assert.False(tmpProduct.EAN.IsISBNValid());

                Assert.Equal("Le Merde, C'est La Vie", tmpProduct.OnixTitle);

                Assert.Equal("Le Merde", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.Equal("fre", tmpProduct.Language.LanguageCode);

                Assert.Equal("We Print Stuff", tmpProduct.OnixPublisherName);

                Assert.Equal(false, tmpProduct.HasUSRights());

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

            Assert.True(currentFileInfo.Exists, "File does not exist!");

            using OnixLegacyParser v2Parser =
                new(File.ReadAllText(currentFileInfo.FullName), false, true, true);

            Assert.Equal("Mundane Hippo Press", v2Parser.MessageHeader.FromCompany);

            foreach (OnixLegacyProduct tmpProduct in v2Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);

                Assert.Equal("9781234567890", tmpProduct.EAN);

                Assert.False(tmpProduct.EAN.IsISBNValid());

                Assert.Equal("Generic Title: Bland and Boring", tmpProduct.OnixTitle);

                Assert.Equal("Smith", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.Equal("eng", tmpProduct.Language.LanguageCode);

                Assert.Equal("250", tmpProduct.OnixNumberOfPages);                

                Assert.Equal("Mundane Hippo Press", tmpProduct.OnixPublisherName);

                Assert.Equal(true, tmpProduct.HasUSRights());

                Assert.Equal(19.95m, tmpProduct.USDRetailPrice.PriceAmountNum);

                Assert.Equal("General Trade", tmpProduct.USDRetailPrice.ClassOfTrade);

                Assert.Equal("15", tmpProduct.OnixAudienceAgeFrom);

                Assert.Equal("99", tmpProduct.OnixAudienceAgeTo);

                Assert.Equal("01", tmpProduct.OnixAudienceCode);

                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
    }
}