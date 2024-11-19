﻿using System;
using System.IO;
using System.Linq;
using OnixData.Standard.Extensions;
using OnixData.Standard.Legacy;
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

                Assert.Equal("DG", tmpProduct.ProductForm);

                Assert.Equal("E200", tmpProduct.OnixProductFormDetailList[0]);

                Assert.Equal("fre", tmpProduct.Language.LanguageCode);

                Assert.Equal("20180328", tmpProduct.PublicationDate);

                Assert.Equal("136", tmpProduct.OnixNumberOfPages);

                Assert.Equal(10.00m, tmpProduct.Height.MeasurementNum);

                Assert.Equal(254.00m, tmpProduct.OnixMeasureList
                                                .FirstOrDefault(x => x.MeasureUnitCode == "mm" && x.MeasureTypeCode == OnixLegacyMeasure.CONST_MEASURE_TYPE_HEIGHT)
                                                .MeasurementNum);

                Assert.Equal(286.22m, tmpProduct.OnixMeasureList
                                                .FirstOrDefault(x => x.MeasureUnitCode == "gr" && x.MeasureTypeCode == OnixLegacyMeasure.CONST_MEASURE_TYPE_WEIGHT)
                                                .MeasurementNum);

                Assert.Equal("01", tmpProduct.OnixAudienceCode);

                string sMainSubjSchemeId = OnixLegacySubject.CONST_SUBJ_SCHEME_BISAC_CAT_ID.ToString();
                Assert.Equal("FIC027000", tmpProduct.OnixMainSubjectList.FirstOrDefault(x => x.MainSubjectSchemeIdentifier == sMainSubjSchemeId).SubjectCode);

                string sIllTypeDesc = tmpProduct.Illustrations.IllustrationTypeDescription.Substring(0, 6);
                Assert.Equal("SAMPLE", sIllTypeDesc);

                Assert.Equal("Spiritual Views from a Sad Sack", tmpProduct.SeriesTitle);

                Assert.Equal("Le Merde", tmpProduct.PrimaryAuthor.OnixKeyNames);

                Assert.True(tmpProduct.PrimaryAuthor.BiographicalNote.Contains("Il sent mauvais"));

                Assert.Equal("We Print Stuff", tmpProduct.OnixPublisherName);

                Assert.Equal(false, tmpProduct.HasUSRights());

                Assert.Equal(6.99m, tmpProduct.USDRetailPrice.PriceAmountNum);

                Assert.Equal("9782234567890.epub", tmpProduct.OnixMediaFileList[0].MediaFileLink);

                Assert.Equal("Mundane Hippo", tmpProduct.OnixSupplyDetailList[0].SupplierName);

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

                Assert.Equal("sports leadership;sports metaphors", tmpProduct.Keywords);

                Assert.Equal("9781234567891", tmpProduct.OnixRelatedProductList[0].EAN);

                /**
                 ** NOTE: Add more tests here
                 **/
            }
        }
    }
}