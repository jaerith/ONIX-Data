using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Price;
using OnixData.Version3.Publishing;

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
    }
}
