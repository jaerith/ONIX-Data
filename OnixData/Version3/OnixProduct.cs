using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Market;
using OnixData.Version3.Price;
using OnixData.Version3.Publishing;
using OnixData.Version3.Title;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixProduct
    {
        public OnixProduct()
        {
            RecordReference = "";

            NotificationType = RecordSourceType = -1;

            ISBN = UPC = "";

            EAN = -1;

            ProductIdentifier = new OnixProductId[0];
            DescriptiveDetail = new OnixDescriptiveDetail();
            CollateralDetail  = new OnixTextContent[0];
            PublishingDetail  = new OnixPublishingDetail();
            ProductSupply     = new OnixProductSupply();
        }

        private string recordReferenceField;
        private int    notificationTypeField;
        private int    recordSourceTypeField;

        private string isbnField;
        private long   eanField;
        private string upcField;

        private OnixProductId[]       productIdentifierField;
        private OnixDescriptiveDetail descriptiveDetailField;
        private OnixTextContent[]     collateralDetailField;
        private OnixPublishingDetail  publishingDetailField;
        private OnixProductSupply     productSupplyField;

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

        public string ISBN
        {
            get
            {
                string TempISBN = this.isbnField;

                if (String.IsNullOrEmpty(TempISBN))
                {
                    if ((ProductIdentifier != null) && (ProductIdentifier.Length > 0))
                    {
                        OnixProductId IsbnProductId =
                            ProductIdentifier.Where(x => x.ProductIDType == OnixProductId.CONST_PRODUCT_TYPE_ISBN).FirstOrDefault();

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
                long TempEAN = this.eanField;

                if (TempEAN <= 0)
                {
                    if ((ProductIdentifier != null) && (ProductIdentifier.Length > 0))
                    {
                        OnixProductId EanProductId =
                            ProductIdentifier.Where(x => (x.ProductIDType == OnixProductId.CONST_PRODUCT_TYPE_EAN) ||
                                                         (x.ProductIDType == OnixProductId.CONST_PRODUCT_TYPE_ISBN13)).FirstOrDefault();

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

        public string UPC
        {
            get
            {
                string TempUPC = this.upcField;

                if (String.IsNullOrEmpty(TempUPC))
                {
                    if ((ProductIdentifier != null) && (ProductIdentifier.Length > 0))
                    {
                        OnixProductId UpcProductId =
                            ProductIdentifier.Where(x => x.ProductIDType == OnixProductId.CONST_PRODUCT_TYPE_UPC).FirstOrDefault();

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

        public OnixContributor GetPrimaryAuthor
        {
            get
            {
                OnixContributor PrimaryAuthor = new OnixContributor();

                if ((DescriptiveDetail.Contributor != null) && (DescriptiveDetail.Contributor.Length > 0))
                {
                    PrimaryAuthor =
                        DescriptiveDetail.Contributor.Where(x => x.ContributorRole == OnixContributor.CONST_CONTRIB_ROLE_AUTHOR).FirstOrDefault();
                }

                return PrimaryAuthor;
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

        public string SeriesTitle
        {
            get
            {
                string FoundSeriesTitle = "";

                if ((DescriptiveDetail != null)            &&
                    (DescriptiveDetail.Collection != null) && 
                    (DescriptiveDetail.Collection.Length > 0))
                {
                    OnixCollection seriesCollection =
                        DescriptiveDetail.Collection.Where(x => x.CollectionType == OnixCollection.CONST_COLL_TYPE_SERIES).FirstOrDefault();

                    if ((seriesCollection.TitleDetail != null) && (seriesCollection.TitleDetail.TitleElement != null))
                        FoundSeriesTitle = seriesCollection.TitleDetail.TitleElement.Title;
                }

                return FoundSeriesTitle;
            }
        }

        public OnixSubject BisacCategoryCode
        {
            get
            {
                OnixSubject FoundSubject = new OnixSubject();

                if ((DescriptiveDetail != null)         &&
                    (DescriptiveDetail.Subject != null) && 
                    (DescriptiveDetail.Subject.Length > 0))
                {
                    FoundSubject =
                        DescriptiveDetail.Subject.Where(x => x.SubjectSchemeIdentifier == OnixSubject.CONST_SUBJ_SCHEME_BISAC_CAT_ID).FirstOrDefault();
                }

                return FoundSubject;
            }
        }

        public OnixSubject BisacRegionCode
        {
            get
            {
                OnixSubject FoundSubject = new OnixSubject();

                if ((DescriptiveDetail != null)         &&
                    (DescriptiveDetail.Subject != null) &&
                    (DescriptiveDetail.Subject.Length > 0))
                {
                    FoundSubject =
                        DescriptiveDetail.Subject.Where(x => x.SubjectSchemeIdentifier == OnixSubject.CONST_SUBJ_SCHEME_REGION_ID).FirstOrDefault();
                }

                return FoundSubject;
            }
        }

        public OnixMeasure Height
        {
            get
            {
                OnixMeasure FoundHeight = new OnixMeasure();

                if ((DescriptiveDetail != null)         &&
                    (DescriptiveDetail.Measure != null) && 
                    (DescriptiveDetail.Measure.Length > 0))
                {
                    OnixMeasure[] MeasureList = DescriptiveDetail.Measure;

                    FoundHeight =
                        MeasureList.Where(x => x.MeasureType == OnixMeasure.CONST_MEASURE_TYPE_HEIGHT).FirstOrDefault();
                }

                return FoundHeight;
            }
        }

        public bool HasUSDRetailPrice()
        {
            bool bHasUSDPrice = false;

            if ((ProductSupply != null)              &&
                (ProductSupply.SupplyDetail != null) &&
                (ProductSupply.SupplyDetail.Price != null))
            {
                bHasUSDPrice =
                    ProductSupply.SupplyDetail.Price.Any(x => (x.PriceType == OnixPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD"));
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
                    (ProductSupply.SupplyDetail.Price != null))
                {
                    OnixPrice[] Prices = ProductSupply.SupplyDetail.Price;

                    USDPrice =
                        Prices.Where(x => (x.PriceType == OnixPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD")).FirstOrDefault();

                    if (USDPrice == null)
                        USDPrice = new OnixPrice();
                }

                return USDPrice;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TextContent", IsNullable = false)]
        public OnixTextContent[] CollateralDetail
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

        public string ImprintName
        {
            get
            {
                string FoundImprintName = "";

                if ((PublishingDetail != null) &&
                    (PublishingDetail.Imprint != null) &&
                    (PublishingDetail.Imprint.Length > 0))
                {
                    FoundImprintName = PublishingDetail.Imprint[0].ImprintName;
                }

                return FoundImprintName;
            }
        }

        public string PublisherName
        {
            get
            {
                string FoundPubName = "";

                if ((PublishingDetail != null)           &&
                    (PublishingDetail.Publisher != null) &&
                    (PublishingDetail.Publisher.Length > 0))
                {
                    List<int> SoughtPubTypes =
                        new List<int>() { 0, OnixPublisher.CONST_PUB_ROLE_PUBLISHER, OnixPublisher.CONST_PUB_ROLE_CO_PUB };

                    OnixPublisher FoundPublisher =
                        PublishingDetail.Publisher.Where(x => SoughtPubTypes.Contains(x.PublishingRole)).FirstOrDefault();

                    FoundPubName = FoundPublisher.PublisherName;
                }

                return FoundPubName;
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

        public bool HasUSRights()
        {
            bool bHasUSRights = false;

            int[] aSalesRightsColl = new int[] { OnixMarketTerritory.CONST_SR_TYPE_FOR_SALE_WITH_EXCL_RIGHTS,
                                                 OnixMarketTerritory.CONST_SR_TYPE_FOR_SALE_WITH_NONEXCL_RIGHTS };

            if ((ProductSupply != null)        &&
                (ProductSupply.Market != null) &&
                (ProductSupply.Market.Territory != null))
            {
                List<string> TempCountriesIncluded = ProductSupply.Market.Territory.CountriesIncludedList;

                bHasUSRights = TempCountriesIncluded.Contains("US");
            }

            return bHasUSRights;
        }
    }
}
