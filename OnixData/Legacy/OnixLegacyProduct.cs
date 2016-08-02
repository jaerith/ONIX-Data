using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class OnixLegacyProduct : OnixLegacyBaseProduct
    {
        public OnixLegacyProduct()
        {
            RecordReference  = 0;
            NotificationType = NumberOfPieces = TradeCategory = Barcode = -1;

            ProductIdentifier = new OnixLegacyProductId[0];

            ProductForm            = "";
            ProductFormDetail      = "";
            ProductFormDescription = "";
            ProductContentType     = new string[0];
            EpubType               = "";
            EpubTypeVersion        = "";
            EpubFormatDescription  = "";
            EditionTypeCode        = new string[0];
            EditionNumber          = new int[0];

            ProductFormFeature = new OnixLegacyProductFormFeature();

            Title       = new OnixLegacyTitle();
            Contributor = new OnixLegacyContributor[0];

            ContributorStatement = "";

            Language = new OnixLegacyLanguage();

            ItemNumberWithinSet = -1;

            NumberOfPages = PagesRoman = PagesArabic = -1;

            Illustrations = new OnixLegacyIllustrations();

            BASICMainSubject = "";
            AudienceCode     = "";

            Extent        = new OnixLegacyExtent[0];
            Subject       = new OnixLegacySubject[0];
            AudienceRange = new OnixLegacyAudRange[0];
            OtherText     = new OnixLegacyOtherText[0];
            MediaFile     = new OnixLegacyMediaFile[0];
            Imprint       = new OnixLegacyImprint[0];
            Publisher     = new OnixLegacyPublisher[0];

            cityOfPublicationField = countryOfPublicationField  = publishingStatusField = "";
            announcementDateField  = tradeAnnouncementDateField = "";

            PublicationDate = YearFirstPublished = 0;

            Measure        = new OnixLegacyMeasure[0];
            RelatedProduct = new OnixLegacyRelatedProduct[0];
            SupplyDetail   = new OnixLegacySupplyDetail();
        }

        private string[] editionTypeCodeField;
        private int[]    editionNumberField;

        private OnixLegacyTitle         titleField;
        private OnixLegacyContributor[] contributorField;

        private string contributorStatementField;

        private OnixLegacyLanguage languageField;

        private int itemNumberWithinSetField;
        private int numberOfPagesField;
        private int pagesRomanField;
        private int pagesArabicField;

        private OnixLegacyIllustrations illustrationsField;

        private string basicMainSubjectField;
        private string audienceCodeField;

        private OnixLegacyExtent[]    extentField;
        private OnixLegacySeries[]    seriesField;
        private OnixLegacySubject[]   subjectField;
        private OnixLegacyAudRange[]  audienceRangeField;
        private OnixLegacyOtherText[] otherTextField;
        private OnixLegacyMediaFile[] mediaFileField;
        private OnixLegacyImprint[]   imprintField;

        private string cityOfPublicationField;
        private string countryOfPublicationField;
        private string publishingStatusField;
        private string announcementDateField;
        private string tradeAnnouncementDateField;
        private uint   publicationDateField;
        private uint   yearFirstPublishedField;

        private OnixLegacySalesRights[]    salesRightsField;
        private OnixLegacyMeasure[]        measureField;
        private OnixLegacyRelatedProduct[] relatedProductField;
        private OnixLegacySupplyDetail     supplyDetailField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EditionTypeCode")]
        public string[] EditionTypeCode
        {
            get
            {
                return this.editionTypeCodeField;
            }
            set
            {
                this.editionTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EditionNumber")]
        public int[] EditionNumber
        {
            get
            {
                return this.editionNumberField;
            }
            set
            {
                this.editionNumberField = value;
            }
        }        

        /// <remarks/>
        public OnixLegacyTitle Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Contributor")]
        public OnixLegacyContributor[] Contributor
        {
            get
            {
                return this.contributorField;
            }
            set
            {
                this.contributorField = value;
            }
        }

        public OnixLegacyContributor GetPrimaryAuthor
        {
            get
            {
                OnixLegacyContributor PrimaryAuthor = new OnixLegacyContributor();

                if ((Contributor != null) && (Contributor.Length > 0))
                {
                    PrimaryAuthor =
                        Contributor.Where(x => x.ContributorRole == OnixLegacyContributor.CONST_CONTRIB_ROLE_AUTHOR).FirstOrDefault();
                }

                return PrimaryAuthor;
            }
        }

        /// <remarks/>
        public string ContributorStatement
        {
            get
            {
                return this.contributorStatementField;
            }
            set
            {
                this.contributorStatementField = value;
            }
        }

        /// <remarks/>
        public OnixLegacyLanguage Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        
        /// <remarks/>
        public int ItemNumberWithinSet
        {
            get
            {
                return this.itemNumberWithinSetField;
            }
            set
            {
                this.itemNumberWithinSetField = value;
            }
        }

        /// <remarks/>
        public int NumberOfPages
        {
            get
            {
                return this.numberOfPagesField;
            }
            set
            {
                this.numberOfPagesField = value;
            }
        }

        /// <remarks/>
        public int PagesRoman
        {
            get
            {
                return this.pagesRomanField;
            }
            set
            {
                this.pagesRomanField = value;
            }
        }

        /// <remarks/>
        public int PagesArabic
        {
            get
            {
                return this.pagesArabicField;
            }
            set
            {
                this.pagesArabicField = value;
            }
        }

        /// <remarks/>
        public OnixLegacyIllustrations Illustrations
        {
            get
            {
                return this.illustrationsField;
            }
            set
            {
                this.illustrationsField = value;
            }
        }

        /// <remarks/>
        public string BASICMainSubject
        {
            get
            {
                return this.basicMainSubjectField;
            }
            set
            {
                this.basicMainSubjectField = value;
            }
        }

        /// <remarks/> 
        public string AudienceCode
        {
            get
            {
                return this.audienceCodeField;
            }
            set
            {
                this.audienceCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Extent")]
        public OnixLegacyExtent[] Extent
        {
            get
            {
                return this.extentField;
            }
            set
            {
                this.extentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Series")]
        public OnixLegacySeries[] Series
        {
            get
            {
                return this.seriesField;
            }
            set
            {
                this.seriesField = value;
            }
        }

        public int SeriesNumber
        {
            get
            {
                int FoundSeriesNum = 0;

                if ((Series != null) && (Series.Length > 0))
                    FoundSeriesNum = Series[0].NumberWithinSeries;

                return FoundSeriesNum;
            }
        }

        public string SeriesTitle
        {
            get
            {
                string FoundSeriesTitle = "";

                if ((Series != null) && (Series.Length > 0))
                    FoundSeriesTitle = Series[0].TitleOfSeries;

                return FoundSeriesTitle;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Subject")]
        public OnixLegacySubject[] Subject
        {
            get
            {
                return this.subjectField;
            }
            set
            {
                this.subjectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AudienceRange")]
        public OnixLegacyAudRange[] AudienceRange
        {
            get
            {
                return this.audienceRangeField;
            }
            set
            {
                this.audienceRangeField = value;
            }
        }

        public OnixLegacySubject BisacCategoryCode
        {
            get
            {
                OnixLegacySubject FoundSubject = new OnixLegacySubject();

                if ((Subject != null) && (Subject.Length > 0))
                {
                    FoundSubject =
                        Subject.Where(x => x.SubjectSchemeIdentifier == OnixLegacySubject.CONST_SUBJ_SCHEME_BISAC_CAT_ID).FirstOrDefault();
                }

                return FoundSubject;
            }
        }

        public OnixLegacySubject BisacRegionCode
        {
            get
            {
                OnixLegacySubject FoundSubject = new OnixLegacySubject();

                if ((Subject != null) && (Subject.Length > 0))
                {
                    FoundSubject =
                        Subject.Where(x => x.SubjectSchemeIdentifier == OnixLegacySubject.CONST_SUBJ_SCHEME_REGION_ID).FirstOrDefault();
                }

                return FoundSubject;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OtherText")]
        public OnixLegacyOtherText[] OtherText
        {
            get
            {
                return this.otherTextField;
            }
            set
            {
                this.otherTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MediaFile")]
        public OnixLegacyMediaFile[] MediaFile
        {
            get
            {
                return this.mediaFileField;
            }
            set
            {
                this.mediaFileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Imprint")]
        public OnixLegacyImprint[] Imprint
        {
            get
            {
                return this.imprintField;
            }
            set
            {
                this.imprintField = value;
            }
        }

        public string ProprietaryImprintName
        {
            get
            {
                string FoundImprintName = "";

                if ((Imprint != null) && (Imprint.Length > 0))
                {
                    OnixLegacyImprint FoundImprint =
                        Imprint.Where(x => x.NameCodeType == OnixLegacyImprint.CONST_IMPRINT_ROLE_PROP).FirstOrDefault();

                    FoundImprintName = FoundImprint.ImprintName;
                }

                return FoundImprintName;
            }
        }

        /// <remarks/>
        public string CityOfPublication
        {
            get
            {
                return this.cityOfPublicationField;
            }
            set
            {
                this.cityOfPublicationField = value;
            }
        }

        /// <remarks/>
        public string CountryOfPublication
        {
            get
            {
                return this.countryOfPublicationField;
            }
            set
            {
                this.countryOfPublicationField = value;
            }
        }

        /// <remarks/>
        public string PublishingStatus
        {
            get
            {
                return this.publishingStatusField;
            }
            set
            {
                this.publishingStatusField = value;
            }
        }

        /// <remarks/>
        public string AnnouncementDate
        {
            get
            {
                return this.announcementDateField;
            }
            set
            {
                this.announcementDateField = value;
            }
        }

        /// <remarks/>
        public string TradeAnnouncementDate
        {
            get
            {
                return this.tradeAnnouncementDateField;
            }
            set
            {
                this.tradeAnnouncementDateField = value;
            }
        }

        /// <remarks/>
        public uint PublicationDate
        {
            get
            {
                return this.publicationDateField;
            }
            set
            {
                this.publicationDateField = value;
            }
        }

        /// <remarks/>
        public uint YearFirstPublished
        {
            get
            {
                return this.yearFirstPublishedField;
            }
            set
            {
                this.yearFirstPublishedField = value;
            }
        }

        public bool HasUSRights()
        {
            bool bHasUSRights = false;

            int[] aSalesRightsColl = new int[] { OnixLegacySalesRights.CONST_SR_TYPE_FOR_SALE_WITH_EXCL_RIGHTS,
                                                 OnixLegacySalesRights.CONST_SR_TYPE_FOR_SALE_WITH_NONEXCL_RIGHTS };

            if ((salesRightsField != null) && (salesRightsField.Length > 0))
            {
                bHasUSRights =
                    salesRightsField.Any(x => aSalesRightsColl.Contains(x.SalesRightsType) && 
                                              (x.RightsCountryList.Contains("US") || 
                                               x.RightsTerritoryList.Contains("WORLD") || 
                                               x.RightsTerritoryList.Contains("ROW")));
            }

            return bHasUSRights;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SalesRights")]
        public OnixLegacySalesRights[] SalesRights
        {
            get
            {
                return this.salesRightsField;
            }
            set
            {
                this.salesRightsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Measure")]
        public OnixLegacyMeasure[] Measure
        {
            get
            {
                return this.measureField;
            }
            set
            {
                this.measureField = value;
            }
        }

        public OnixLegacyMeasure Height
        {
            get
            {
                OnixLegacyMeasure FoundHeight = new OnixLegacyMeasure();

                if ((Measure != null) && (Measure.Length > 0))
                {
                    FoundHeight =
                        Measure.Where(x => x.MeasureTypeCode == OnixLegacyMeasure.CONST_MEASURE_TYPE_HEIGHT).FirstOrDefault();
                }

                return FoundHeight;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedProduct")]
        public OnixLegacyRelatedProduct[] RelatedProduct
        {
            get
            {
                return this.relatedProductField;
            }
            set
            {
                this.relatedProductField = value;
            }
        }

        public bool HasFutureRetailPrice()
        {
            bool bHasFuturePrice = false;

            if ((SupplyDetail != null) && (SupplyDetail.Price != null) && (SupplyDetail.Price.Length > 0))
            {
                bHasFuturePrice =
                    SupplyDetail.Price.Any(x => !String.IsNullOrEmpty(x.PriceEffectiveFrom));
            }

            return bHasFuturePrice;
        }

        public bool HasUSDRetailPrice()
        {
            bool bHasUSDPrice = false;

            if ((SupplyDetail != null) && (SupplyDetail.Price != null) && (SupplyDetail.Price.Length > 0))
            {
                bHasUSDPrice = 
                    SupplyDetail.Price.Any(x => (x.PriceTypeCode == OnixLegacyPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD"));
            }

            return bHasUSDPrice;
        }

        public OnixLegacyPrice USDRetailPrice
        {
            get
            {
                OnixLegacyPrice USDPrice = new OnixLegacyPrice();

                if ((SupplyDetail != null) && (SupplyDetail.Price != null) && (SupplyDetail.Price.Length > 0))
                {
                    OnixLegacyPrice[] Prices = SupplyDetail.Price;

                    USDPrice =
                        Prices.Where(x => (x.PriceTypeCode == OnixLegacyPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD")).FirstOrDefault();
                }

                return USDPrice;
            }
        }

        /// <remarks/>
        public OnixLegacySupplyDetail SupplyDetail
        {
            get
            {
                return this.supplyDetailField;
            }
            set
            {
                this.supplyDetailField = value;
            }
        }
    }
}
