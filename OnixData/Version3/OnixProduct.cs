using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Content;
using OnixData.Version3.Language;
using OnixData.Version3.Market;
using OnixData.Version3.Price;
using OnixData.Version3.Publishing;
using OnixData.Version3.Related;
using OnixData.Version3.Supply;
using OnixData.Version3.Text;
using OnixData.Version3.Title;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixProduct
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

        public OnixProduct()
        {
            RecordReference = "";

            NotificationType = RecordSourceType = -1;

            ISBN = UPC = "";

            eanField = -1;

            productIdentifierField = shortProductIdentifierField = new OnixProductId[0];
            languageField          = shortLanguageField          = new OnixLanguage[0];

            ContentDetail     = new OnixContentDetail();
            DescriptiveDetail = new OnixDescriptiveDetail();
            CollateralDetail  = new OnixCollateralDetail();
            PublishingDetail  = new OnixPublishingDetail();
            RelatedMaterial   = new OnixRelatedMaterial();
            ProductSupply     = new OnixProductSupply();

            ParsingError = null;
        }

        private string recordReferenceField;
        private int    notificationTypeField;
        private int    recordSourceTypeField;

        private string isbnField;
        private long   eanField;
        private string upcField;

        private OnixProductId[]       productIdentifierField;
        private OnixProductId[]       shortProductIdentifierField;

        private OnixLanguage[]        languageField;
        private OnixLanguage[]        shortLanguageField;

        private OnixCollateralDetail  collateralDetailField;
        private OnixContentDetail     contentDetailField;
        private OnixDescriptiveDetail descriptiveDetailField;
        private OnixPublishingDetail  publishingDetailField;
        private OnixRelatedMaterial   relatedMaterialField;
        private OnixProductSupply     productSupplyField;

        #region Parsing Error

        private string    InputXml;
        private Exception ParsingError;

        public string    GetInputXml() { return InputXml; }
        public void      SetInputXml(string value) { InputXml = value; }

        public bool IsValid() { return (ParsingError == null); }

        public Exception GetParsingError() { return ParsingError; }
        public void      SetParsingError(Exception value) { ParsingError = value; }

        #endregion

        #region ONIX Helpers

        public OnixSubject BisacCategoryCode
        {
            get
            {
                OnixSubject FoundSubject = new OnixSubject();

                if ((DescriptiveDetail != null) &&
                    (DescriptiveDetail.OnixSubjectList != null) &&
                    (DescriptiveDetail.OnixSubjectList.Length > 0))
                {
                    FoundSubject =
                        DescriptiveDetail.OnixSubjectList.Where(x => x.SubjectSchemeIdentifierNum == OnixSubject.CONST_SUBJ_SCHEME_BISAC_CAT_ID).LastOrDefault();
                }

                return FoundSubject;
            }
        }

        public OnixSubject BisacRegionCode
        {
            get
            {
                OnixSubject FoundSubject = new OnixSubject();

                if ((DescriptiveDetail != null) &&
                    (DescriptiveDetail.OnixSubjectList != null) &&
                    (DescriptiveDetail.OnixSubjectList.Length > 0))
                {
                    FoundSubject =
                        DescriptiveDetail.OnixSubjectList.Where(x => x.SubjectSchemeIdentifierNum == OnixSubject.CONST_SUBJ_SCHEME_REGION_ID).LastOrDefault();
                }

                return FoundSubject;
            }
        }

        public string ISBN
        {
            get
            {
                OnixProductId[] ProductIdList = OnixProductIdList;

                string TempISBN = this.isbnField;
                if (String.IsNullOrEmpty(TempISBN))
                {
                    if ((ProductIdList != null) && (ProductIdList.Length > 0))
                    {
                        OnixProductId IsbnProductId =
                            ProductIdList.Where(x => x.ProductIDType == CONST_PRODUCT_TYPE_ISBN).LastOrDefault();

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

        public long EAN
        {
            get
            {
                OnixProductId[] ProductIdList = OnixProductIdList;

                long TempEAN = this.eanField;
                if (TempEAN <= 0)
                {
                    if ((ProductIdList != null) && (ProductIdList.Length > 0))
                    {
                        OnixProductId EanProductId =
                            ProductIdList.Where(x => (x.ProductIDType == CONST_PRODUCT_TYPE_EAN) ||
                                                     (x.ProductIDType == CONST_PRODUCT_TYPE_ISBN13)).LastOrDefault();

                        if ((EanProductId != null) && !String.IsNullOrEmpty(EanProductId.IDValue))
                            TempEAN = this.eanField = Convert.ToInt64(EanProductId.IDValue);
                    }
                }

                return TempEAN;
            }
            set
            {
                this.eanField = value;
            }
        }

        public string ImprintName
        {
            get
            {
                string FoundImprintName = "";

                if ((PublishingDetail != null) &&
                    (PublishingDetail.OnixImprintList != null) &&
                    (PublishingDetail.OnixImprintList.Length > 0))
                {
                    FoundImprintName = PublishingDetail.OnixImprintList[0].ImprintName;
                }

                return FoundImprintName;
            }
        }

        public string LIBRARY_CONGRESS_NUM
        {
            get
            {
                string sLibCongressNum = "";

                OnixProductId[] ProductIdList = OnixProductIdList;

                if ((ProductIdList != null) && (ProductIdList.Length > 0))
                {
                    OnixProductId LccnProductId =
                        ProductIdList.Where(x => (x.ProductIDType == CONST_PRODUCT_TYPE_LCCN)).LastOrDefault();

                    if ((LccnProductId != null) && !String.IsNullOrEmpty(LccnProductId.IDValue))
                        sLibCongressNum = LccnProductId.IDValue;
                }

                return sLibCongressNum;
            }
        }

        public string PROPRIETARY_ID
        {
            get
            {
                string sPropId = "";

                OnixProductId[] ProductIdList = OnixProductIdList;

                if ((ProductIdList != null) && (ProductIdList.Length > 0))
                {
                    OnixProductId PropProductId =
                        ProductIdList.Where(x => (x.ProductIDType == CONST_PRODUCT_TYPE_PROP)).LastOrDefault();

                    if ((PropProductId != null) && !String.IsNullOrEmpty(PropProductId.IDValue))
                        sPropId = PropProductId.IDValue;
                }

                return sPropId;
            }
        }

        public string NumberOfPages
        {
            get
            {
                OnixContentItem PrimaryContentItem = new OnixContentItem();

                if ((this.ContentDetail != null) && (this.ContentDetail.PrimaryContentItem != null))
                    PrimaryContentItem = this.ContentDetail.PrimaryContentItem;

                return PrimaryContentItem.NumberOfPages;
            } 
        }

        public string ProprietaryImprintName
        {
            get
            {
                string FoundImprintName = "";

                if (this.PublishingDetail != null)
                {
                    OnixImprint[] ImprintList = this.PublishingDetail.OnixImprintList;
                    if ((ImprintList != null) && (ImprintList.Length > 0))
                    {
                        OnixImprint FoundImprint = ImprintList.Where(x => x.IsProprietaryName()).LastOrDefault();

                        if (FoundImprint != null)
                            FoundImprintName = FoundImprint.ImprintName;
                    }
                }

                return FoundImprintName;
            }
        }

        public string PublisherName
        {
            get
            {
                string FoundPubName = "";

                if ((PublishingDetail != null) &&
                    (PublishingDetail.OnixPublisherList != null) &&
                    (PublishingDetail.OnixPublisherList.Length > 0))
                {
                    List<int> SoughtPubTypes =
                        new List<int>() { 0, OnixPublisher.CONST_PUB_ROLE_PUBLISHER, OnixPublisher.CONST_PUB_ROLE_CO_PUB };

                    OnixPublisher FoundPublisher =
                        PublishingDetail.OnixPublisherList.Where(x => SoughtPubTypes.Contains(x.PublishingRole)).LastOrDefault();

                    FoundPubName = FoundPublisher.PublisherName;
                }

                return FoundPubName;
            }
        }

        public OnixContributor PrimaryAuthor
        {
            get
            {
                OnixContributor MainAuthor = new OnixContributor();

                if ((DescriptiveDetail.OnixContributorList != null) && (DescriptiveDetail.OnixContributorList.Length > 0))
                {
                    MainAuthor =
                        DescriptiveDetail.OnixContributorList.Where(x => x.ContributorRole == OnixContributor.CONST_CONTRIB_ROLE_AUTHOR).FirstOrDefault();
                }

                return MainAuthor;
            }
        }

        public string SeriesTitle
        {
            get
            {
                string FoundSeriesTitle = "";

                if ((DescriptiveDetail != null) &&
                    (DescriptiveDetail.OnixCollectionList != null) &&
                    (DescriptiveDetail.OnixCollectionList.Length > 0))
                {
                    OnixCollection seriesCollection =
                        DescriptiveDetail.OnixCollectionList.Where(x => x.CollectionType == OnixCollection.CONST_COLL_TYPE_SERIES).LastOrDefault();

                    if ((seriesCollection.TitleDetail != null) && (seriesCollection.TitleDetail.TitleElement != null))
                        FoundSeriesTitle = seriesCollection.TitleDetail.TitleElement.Title;
                }

                return FoundSeriesTitle;
            }
        }

        public OnixMeasure Height
        {
            get { return GetMeasurement(OnixMeasure.CONST_MEASURE_TYPE_HEIGHT); }
        }

        public OnixMeasure Thick
        {
            get { return GetMeasurement(OnixMeasure.CONST_MEASURE_TYPE_THICK); }
        }

        public OnixMeasure Weight
        {
            get { return GetMeasurement(OnixMeasure.CONST_MEASURE_TYPE_WEIGHT); }
        }

        public OnixMeasure Width
        {
            get { return GetMeasurement(OnixMeasure.CONST_MEASURE_TYPE_WIDTH); }
        }

        public bool HasUSDRetailPrice()
        {
            bool bHasUSDPrice = false;

            if ((ProductSupply != null) &&
                (ProductSupply.SupplyDetail != null) &&
                (ProductSupply.SupplyDetail.OnixPriceList != null))
            {
                OnixPrice[] Prices = ProductSupply.SupplyDetail.OnixPriceList;

                bHasUSDPrice =
                    Prices.Any(x => (x.PriceType == OnixPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD"));
            }

            return bHasUSDPrice;
        }

        public OnixPrice USDRetailPrice
        {
            get
            {
                OnixPrice USDPrice = new OnixPrice();

                if ((ProductSupply != null) &&
                    (ProductSupply.SupplyDetail != null) &&
                    (ProductSupply.SupplyDetail.OnixPriceList != null))
                {
                    OnixPrice[] Prices = ProductSupply.SupplyDetail.OnixPriceList;

                    USDPrice =
                        Prices.Where(x => (x.PriceType == OnixPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD")).FirstOrDefault();

                    if (USDPrice == null)
                        USDPrice = new OnixPrice();
                }

                return USDPrice;
            }
        }

        public string UPC
        {
            get
            {
                OnixProductId[] ProductIdList = OnixProductIdList;

                string TempUPC = this.upcField;
                if (String.IsNullOrEmpty(TempUPC))
                {
                    if ((ProductIdList != null) && (ProductIdList.Length > 0))
                    {
                        OnixProductId UpcProductId =
                            ProductIdList.Where(x => x.ProductIDType == CONST_PRODUCT_TYPE_UPC).LastOrDefault();

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

        public bool HasUSRights()
        {
            bool bHasUSRights = false;

            int[] aSalesRightsColl = new int[] { OnixMarketTerritory.CONST_SR_TYPE_FOR_SALE_WITH_EXCL_RIGHTS,
                                                 OnixMarketTerritory.CONST_SR_TYPE_FOR_SALE_WITH_NONEXCL_RIGHTS };

            if ((ProductSupply != null) &&
                (ProductSupply.Market != null) &&
                (ProductSupply.Market.Territory != null))
            {
                List<string> TempCountriesIncluded = ProductSupply.Market.Territory.CountriesIncludedList;

                bHasUSRights = TempCountriesIncluded.Contains("US");
            }

            return bHasUSRights;
        }

        #endregion

        #region ONIX Lists

        public OnixProductId[] OnixProductIdList
        {
            get
            {
                OnixProductId[] ProductIdList = null;

                if (this.productIdentifierField != null)
                    ProductIdList = this.productIdentifierField;
                else if (this.shortProductIdentifierField != null)
                    ProductIdList = this.shortProductIdentifierField;
                else
                    ProductIdList = new OnixProductId[0];

                return ProductIdList;
            }
        }

        public OnixLanguage[] OnixLanguageList
        {
            get
            {
                OnixLanguage[] LangList = null;

                if (this.languageField != null)
                    LangList = this.languageField;
                else if (this.shortLanguageField != null)
                    LangList = this.shortLanguageField;
                else
                    LangList = new OnixLanguage[0];

                return LangList;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string RecordReference
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

        /// <remarks/>
        public int RecordSourceType
        {
            get
            {
                return this.recordSourceTypeField;
            }
            set
            {
                this.recordSourceTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductIdentifier", IsNullable = false)]
        public OnixProductId[] ProductIdentifier
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

        public OnixCollateralDetail CollateralDetail
        {
            get
            {
                return this.collateralDetailField;
            }
            set
            {
                this.collateralDetailField = value;
            }
        }

        /// <remarks/>
        public OnixContentDetail ContentDetail
        {
            get
            {
                return this.contentDetailField;
            }
            set
            {
                this.contentDetailField = value;
            }
        }

        /// <remarks/>
        public OnixDescriptiveDetail DescriptiveDetail
        {
            get
            {
                return this.descriptiveDetailField;
            }
            set
            {
                this.descriptiveDetailField = value;
            }
        }

        public string Title
        {
            get
            {
                string ProductTitle = "";

                if ((DescriptiveDetail != null)             &&
                    (DescriptiveDetail.TitleDetail != null) &&
                    (DescriptiveDetail.TitleDetail.TitleType == OnixTitleElement.CONST_TITLE_TYPE_PRODUCT))
                {
                    OnixTitleDetail ProductTitleDetail = DescriptiveDetail.TitleDetail;

                    if (ProductTitleDetail.TitleElement != null)
                        ProductTitle = ProductTitleDetail.TitleElement.Title;
                }

                return ProductTitle;
            }
        }

        /// <remarks/>
        public OnixPublishingDetail PublishingDetail
        {
            get
            {
                return this.publishingDetailField;
            }
            set
            {
                this.publishingDetailField = value;
            }
        }

        /// <remarks/>
        public OnixRelatedMaterial RelatedMaterial
        {
            get
            {
                return this.relatedMaterialField;
            }
            set
            {
                this.relatedMaterialField = value;
            }
        }

        /// <remarks/>
        public OnixProductSupply ProductSupply
        {
            get
            {
                return this.productSupplyField;
            }
            set
            {
                this.productSupplyField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string a001
        {
            get { return this.recordReferenceField; }
            set { this.recordReferenceField = value; }
        }

        /// <remarks/>
        public int a002
        {
            get { return this.notificationTypeField; }
            set { this.notificationTypeField = value; }
        }

        /// <remarks/>
        public int a194
        {
            get { return this.recordSourceTypeField; }
            set { this.recordSourceTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("productidentifier", IsNullable = false)]
        public OnixProductId[] productidentifier
        {
            get { return this.shortProductIdentifierField; }
            set { this.shortProductIdentifierField = value; }
        }

        /// <remarks/>
        public OnixContentDetail contentdetail
        {
            get { return ContentDetail; }
            set { ContentDetail = value; }
        }

        /// <remarks/>
        public OnixDescriptiveDetail descriptivedetail
        {
            get { return DescriptiveDetail; }
            set { DescriptiveDetail = value; }
        }

        /// <remarks/>
        public OnixCollateralDetail collateraldetail
        {
            get { return CollateralDetail; }
            set { CollateralDetail = value; }
        }

        /// <remarks/>
        public OnixPublishingDetail publishingdetail
        {
            get { return PublishingDetail; }
            set { PublishingDetail = value; }
        }

        /// <remarks/>
        public OnixRelatedMaterial relatedmaterial
        {
            get { return RelatedMaterial; }
            set { RelatedMaterial = value; }
        }

        /// <remarks/>
        public OnixProductSupply productsupply
        {
            get { return ProductSupply; }
            set { ProductSupply = value; }
        }

        #endregion

        #region Support Methods

        public OnixMeasure GetMeasurement(int Type)
        {
            OnixMeasure FoundMeasurement = new OnixMeasure();

            if ((DescriptiveDetail != null) &&
                (DescriptiveDetail.OnixMeasureList != null) &&
                (DescriptiveDetail.OnixMeasureList.Length > 0))
            {
                OnixMeasure[] MeasureList = DescriptiveDetail.OnixMeasureList;

                var MeasureType = MeasureList.Where(x => x.MeasureType == Type).LastOrDefault();

                if (MeasureType != null)
                    FoundMeasurement = MeasureType;
            }

            return FoundMeasurement;
        }

        public string GetVolumeMeasurementUnit()
        {
            string sVolumeUnit = "";

            if (!String.IsNullOrEmpty(this.Thick.MeasureUnitCode))
                sVolumeUnit = this.Thick.MeasureUnitCode;
            else if (!String.IsNullOrEmpty(this.Height.MeasureUnitCode))
                sVolumeUnit = this.Height.MeasureUnitCode;
            else if (!String.IsNullOrEmpty(this.Width.MeasureUnitCode))
                sVolumeUnit = this.Width.MeasureUnitCode;

            return sVolumeUnit;
        }

        #endregion
    }
}
