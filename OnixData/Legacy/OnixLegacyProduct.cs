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
    public partial class OnixLegacyProduct
    {
        #region CONSTANTS

        public const int CONST_PRODUCT_TYPE_PROP   = 1;
        public const int CONST_PRODUCT_TYPE_ISBN   = 2;
        public const int CONST_PRODUCT_TYPE_EAN    = 3;
        public const int CONST_PRODUCT_TYPE_UPC    = 4;
        public const int CONST_PRODUCT_TYPE_ISMN   = 5;
        public const int CONST_PRODUCT_TYPE_DOI    = 6;
        public const int CONST_PRODUCT_TYPE_LCCN   = 13;
        public const int CONST_PRODUCT_TYPE_GTIN   = 14;
        public const int CONST_PRODUCT_TYPE_ISBN13 = 15;

        #endregion

        public OnixLegacyProduct()
        {
            RecordReference  = 0;
            NotificationType = -1;

            ProductIdentifier = new OnixLegacyProductId[0];

            ProductForm       = "";
            ProductFormDetail = "";

            ProductFormFeature = new OnixLegacyProductFormFeature();

            ProductFormDescription = "";

            Title       = new OnixLegacyTitle();
            Contributor = new OnixLegacyContributor[0];

            ContributorStatement = "";

            Language = new OnixLegacyLanguage();

            NumberOfPages = -1;

            Illustrations = new OnixLegacyIllustrations();

            BASICMainSubject = "";

            Extent    = new OnixLegacyExtent[0];
            Subject   = new OnixLegacySubject[0];
            OtherText = new OnixLegacyOtherText[0];
            MediaFile = new OnixLegacyMediaFile[0];
            Imprint   = new OnixLegacyImprint[0];
            Publisher = new OnixLegacyPublisher[0];

            cityOfPublicationField = countryOfPublicationField  = publishingStatusField = "";
            announcementDateField  = tradeAnnouncementDateField = "";

            PublicationDate = YearFirstPublished = 0;

            Measure = new OnixLegacyMeasure[0];

            RelatedProduct = new OnixLegacyRelatedProduct();
            SupplyDetail   = new OnixLegacySupplyDetail();
        }

        private ulong recordReferenceField;
        private int   notificationTypeField;

        private string isbnField;
        private string eanField;
        private string upcField;

        private OnixLegacyProductId[] productIdentifierField;

        private string productFormField;
        private string productFormDetailField;

        private OnixLegacyProductFormFeature productFormFeatureField;

        private string productFormDescriptionField;

        private OnixLegacyTitle         titleField;
        private OnixLegacyContributor[] contributorField;

        private string contributorStatementField;

        private OnixLegacyLanguage languageField;

        private int numberOfPagesField;

        private OnixLegacyIllustrations illustrationsField;

        private string basicMainSubjectField;

        private OnixLegacyExtent[]    extentField;
        private OnixLegacySeries[]    seriesField;
        private OnixLegacySubject[]   subjectField;
        private OnixLegacyOtherText[] otherTextField;
        private OnixLegacyMediaFile[] mediaFileField;
        private OnixLegacyImprint[]   imprintField;
        private OnixLegacyPublisher[] publisherField;

        private string cityOfPublicationField;
        private string countryOfPublicationField;
        private string publishingStatusField;
        private string announcementDateField;
        private string tradeAnnouncementDateField;
        private uint   publicationDateField;
        private uint   yearFirstPublishedField;

        private OnixLegacySalesRights[] salesRightsField;

        private OnixLegacyMeasure[] measureField;

        private OnixLegacyRelatedProduct relatedProductField;

        private OnixLegacySupplyDetail supplyDetailField;

        /// <remarks/>
        public ulong RecordReference
        {
            get
            {
                return this.recordReferenceField;
            }
            set
            {
                this.recordReferenceField = value;
            }
        }

        /// <remarks/>
        public int NotificationType
        {
            get
            {
                return this.notificationTypeField;
            }
            set
            {
                this.notificationTypeField = value;
            }
        }

        public string ISBN
        {
            get
            {
                string TempISBN = this.isbnField;

                if (String.IsNullOrEmpty(TempISBN))
                {
                    if ((ProductIdentifier != null) && (ProductIdentifier.Length > 0))
                    {
                        OnixLegacyProductId IsbnProductId = 
                            ProductIdentifier.Where(x => x.ProductIDType == CONST_PRODUCT_TYPE_ISBN).FirstOrDefault();

                        if ((IsbnProductId != null) && !String.IsNullOrEmpty(IsbnProductId.IDValue))
                            TempISBN = this.isbnField = IsbnProductId.IDValue;
                    }
                }

                return TempISBN;
            }
            set
            {
                this.isbnField = value;
            }
        }

        public string EAN
        {
            get
            {
                string TempEAN = this.eanField;

                if (String.IsNullOrEmpty(TempEAN))
                {
                    if ((ProductIdentifier != null) && (ProductIdentifier.Length > 0))
                    {
                        OnixLegacyProductId EanProductId =
                            ProductIdentifier.Where(x => (x.ProductIDType == CONST_PRODUCT_TYPE_EAN) || 
                                                         (x.ProductIDType == CONST_PRODUCT_TYPE_ISBN13)).FirstOrDefault();

                        if ((EanProductId != null) && !String.IsNullOrEmpty(EanProductId.IDValue))
                            TempEAN = this.eanField = EanProductId.IDValue;
                    }
                }

                return TempEAN;
            }
            set
            {
                this.eanField = value;
            }
        }

        public string UPC
        {
            get
            {
                string TempUPC = this.upcField;

                if (String.IsNullOrEmpty(TempUPC))
                {
                    if ((ProductIdentifier != null) && (ProductIdentifier.Length > 0))
                    {
                        OnixLegacyProductId UpcProductId =
                            ProductIdentifier.Where(x => x.ProductIDType == CONST_PRODUCT_TYPE_UPC).FirstOrDefault();

                        if ((UpcProductId != null) && !String.IsNullOrEmpty(UpcProductId.IDValue))
                            TempUPC = this.upcField = UpcProductId.IDValue;
                    }
                }

                return TempUPC;
            }
            set
            {
                this.upcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductIdentifier")]
        public OnixLegacyProductId[] ProductIdentifier
        {
            get
            {
                return this.productIdentifierField;
            }
            set
            {
                this.productIdentifierField = value;
            }
        }

        /// <remarks/>
        public string ProductForm
        {
            get
            {
                return this.productFormField;
            }
            set
            {
                this.productFormField = value;
            }
        }

        /// <remarks/>
        public string ProductFormDetail
        {
            get
            {
                return this.productFormDetailField;
            }
            set
            {
                this.productFormDetailField = value;
            }
        }

        /// <remarks/>
        public OnixLegacyProductFormFeature ProductFormFeature
        {
            get
            {
                return this.productFormFeatureField;
            }
            set
            {
                this.productFormFeatureField = value;
            }
        }

        /// <remarks/>
        public string ProductFormDescription
        {
            get
            {
                return this.productFormDescriptionField;
            }
            set
            {
                this.productFormDescriptionField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("Publisher")]
        public OnixLegacyPublisher[] Publisher
        {
            get
            {
                return this.publisherField;
            }
            set
            {
                this.publisherField = value;
            }
        }

        public string PublisherName
        {
            get
            {
                string FoundPubName = "";

                if ((Publisher != null) && (Publisher.Length > 0))
                {
                    List<int> SoughtPubTypes =
                        new List<int>() { 0, OnixLegacyPublisher.CONST_PUB_ROLE_PUBLISHER, OnixLegacyPublisher.CONST_PUB_ROLE_CO_PUB };

                    OnixLegacyPublisher FoundPublisher =
                        Publisher.Where(x => SoughtPubTypes.Contains(x.PublishingRole)).FirstOrDefault();

                    FoundPubName = FoundPublisher.PublisherName;
                }

                return FoundPubName;
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
        public OnixLegacyRelatedProduct RelatedProduct
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
