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
    public partial class OnixLegacyBaseProduct
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

        public OnixLegacyBaseProduct()
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

            ProductFormFeature = new OnixLegacyProductFormFeature();
        }

        protected ulong recordReferenceField;
        protected int   notificationTypeField;

        protected string isbnField;
        protected string eanField;
        protected string upcField;

        protected OnixLegacyProductId[] productIdentifierField;

        protected int      barCodeField;
        protected string   productFormField;
        protected string   productFormDetailField;
        protected int      numberOfPiecesField;
        protected int      tradeCategoryField;
        protected string[] productContentTypeField;
        protected string   epubTypeField;
        protected string   epubTypeVersionField;
        protected string   epubFormatDescriptionField;
        protected string   productFormDescriptionField;

        protected OnixLegacyProductFormFeature productFormFeatureField;

        protected OnixLegacyPublisher[] publisherField;

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

        public int Barcode
        {
            get
            {
                return this.barCodeField;
            }
            set
            {
                this.barCodeField = value;
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

        public int NumberOfPieces
        {
            get
            {
                return this.numberOfPiecesField;
            }
            set
            {
                this.numberOfPiecesField = value;
            }
        }

        public int TradeCategory
        {
            get
            {
                return this.tradeCategoryField;
            }
            set
            {
                this.tradeCategoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductContentType")]
        public string[] ProductContentType
        {
            get
            {
                return this.productContentTypeField;
            }
            set
            {
                this.productContentTypeField = value;
            }
        }

        public string EpubType
        {
            get
            {
                return this.epubTypeField;
            }
            set
            {
                this.epubTypeField = value;
            }
        }

        public string EpubTypeVersion
        {
            get
            {
                return this.epubTypeVersionField;
            }
            set
            {
                this.epubTypeVersionField = value;
            }
        }

        public string EpubFormatDescription
        {
            get
            {
                return this.epubFormatDescriptionField;
            }
            set
            {
                this.epubFormatDescriptionField = value;
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

    }
}
