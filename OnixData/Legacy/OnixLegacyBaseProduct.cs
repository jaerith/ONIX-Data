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
            RecordReference  = "";
            NotificationType = NumberOfPieces = TradeCategory = Barcode = -1;

            productIdentifierField  = shortProductIdentifierField  = new OnixLegacyProductId[0];
            productContentTypeField = shortProductContentTypeField = new string[0];
            publisherField          = shortPublisherField          = new OnixLegacyPublisher[0];

            ProductForm = "";
            ProductFormDetail      = "";
            ProductFormDescription = "";
            EpubType               = "";
            EpubTypeVersion        = "";
            EpubFormatDescription  = "";

            ProductFormFeature = new OnixLegacyProductFormFeature();
        }

        protected string recordReferenceField;
        protected int    notificationTypeField;

        protected string isbnField;
        protected string eanField;
        protected string upcField;

        protected OnixLegacyProductId[] productIdentifierField;
        protected OnixLegacyProductId[] shortProductIdentifierField;

        protected string[] productContentTypeField;
        protected string[] shortProductContentTypeField;

        protected int      barCodeField;
        protected string   productFormField;
        protected string   productFormDetailField;
        protected int      numberOfPiecesField;
        protected int      tradeCategoryField;
        protected string   epubTypeField;
        protected string   epubTypeVersionField;
        protected string   epubFormatDescriptionField;
        protected string   productFormDescriptionField;

        protected OnixLegacyProductFormFeature productFormFeatureField;

        protected OnixLegacyPublisher[] publisherField;
        protected OnixLegacyPublisher[] shortPublisherField;

        #region ONIX Helpers

        public string ISBN
        {
            get
            {
                OnixLegacyProductId[] ProductIdList = OnixProductIdList;

                string TempISBN = this.isbnField;
                if (String.IsNullOrEmpty(TempISBN))
                {
                    if ((ProductIdList != null) && (ProductIdList.Length > 0))
                    {
                        OnixLegacyProductId IsbnProductId =
                            ProductIdList.Where(x => x.ProductIDType == CONST_PRODUCT_TYPE_ISBN).FirstOrDefault();

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
                OnixLegacyProductId[] ProductIdList = OnixProductIdList;

                string TempEAN = this.eanField;
                if (String.IsNullOrEmpty(TempEAN))
                {
                    if ((ProductIdList != null) && (ProductIdList.Length > 0))
                    {
                        OnixLegacyProductId EanProductId =
                            ProductIdList.Where(x => (x.ProductIDType == CONST_PRODUCT_TYPE_EAN) ||
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
		
        public string LIBRARY_CONGRESS_NUM
        {
            get
            {
                string sLibCongressNum = "";

                OnixLegacyProductId[] ProductIdList = OnixProductIdList;

                if ((ProductIdList != null) && (ProductIdList.Length > 0))
                {
                    OnixLegacyProductId LccnProductId =
                        ProductIdList.Where(x => (x.ProductIDType == CONST_PRODUCT_TYPE_LCCN)).FirstOrDefault();

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

                OnixLegacyProductId[] ProductIdList = OnixProductIdList;

                if ((ProductIdList != null) && (ProductIdList.Length > 0))
                {
                    OnixLegacyProductId PropProductId =
                        ProductIdList.Where(x => (x.ProductIDType == CONST_PRODUCT_TYPE_PROP)).FirstOrDefault();

                    if ((PropProductId != null) && !String.IsNullOrEmpty(PropProductId.IDValue))
                        sPropId = PropProductId.IDValue;
                }

                return sPropId;
            }
        }		

        public string UPC
        {
            get
            {
                OnixLegacyProductId[] ProductIdList = OnixProductIdList;

                string TempUPC = this.upcField;
                if (String.IsNullOrEmpty(TempUPC))
                {
                    if ((ProductIdList != null) && (ProductIdList.Length > 0))
                    {
                        OnixLegacyProductId UpcProductId =
                            ProductIdList.Where(x => x.ProductIDType == CONST_PRODUCT_TYPE_UPC).FirstOrDefault();

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

        #endregion

        #region ONIX Lists

        public OnixLegacyProductId[] OnixProductIdList
        {
            get
            {
                OnixLegacyProductId[] ProductIdList = null;

                if (this.productIdentifierField != null)
                    ProductIdList = this.productIdentifierField;
                else if (this.shortProductIdentifierField != null)
                    ProductIdList = this.shortProductIdentifierField;
                else
                    ProductIdList = new OnixLegacyProductId[0];

                return ProductIdList;
            }
        }

        public string[] OnixProductContentTypeList
        {
            get
            {
                string[] ContentTypeList = null;

                if (this.productContentTypeField != null)
                    ContentTypeList = this.productContentTypeField;
                else if (this.shortProductContentTypeField != null)
                    ContentTypeList = this.shortProductContentTypeField;
                else
                    ContentTypeList = new string[0];

                return ContentTypeList;
            }
        }

        public OnixLegacyPublisher[] OnixPublisherList
        {
            get
            {
                OnixLegacyPublisher[] PubList = null;

                if (this.publisherField != null)
                    PubList = this.publisherField;
                else if (this.shortPublisherField != null)
                    PubList = this.shortPublisherField;
                else
                    PubList = new OnixLegacyPublisher[0];

                return PubList;
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public string a001
        {
            get { return RecordReference; }
            set { RecordReference = value; }
        }

        /// <remarks/>
        public int a002
        {
            get { return NotificationType; }
            set { NotificationType = value; }
        }

        public string b004
        {
            get { return ISBN; }
            set { ISBN = value; }
        }

        public string b005
        {
            get { return EAN; }
            set { EAN = value; }
        }

        public string b006
        {
            get { return UPC; }
            set { UPC = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("productidentifier")]
        public OnixLegacyProductId[] productidentifier
        {
            get { return shortProductIdentifierField; }
            set { shortProductIdentifierField = value; }
        }

        public int b246
        {
            get { return Barcode; }
            set { Barcode = value; }
        }

        /// <remarks/>
        public string b012
        {
            get { return ProductForm; }
            set { ProductForm = value; }
        }

        /// <remarks/>
        public string b333
        {
            get { return ProductFormDetail; }
            set { ProductFormDetail = value; }
        }

        public int b210
        {
            get { return NumberOfPieces; }
            set { NumberOfPieces = value; }
        }

        public int b384
        {
            get { return TradeCategory; }
            set { TradeCategory = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b385")]
        public string[] b385
        {
            get { return shortProductContentTypeField; }
            set { shortProductContentTypeField = value; }
        }

        public string b211
        {
            get { return EpubType; }
            set { EpubType = value; }
        }

        public string b212
        {
            get { return EpubTypeVersion; }
            set { EpubTypeVersion = value; }
        }

        public string b216
        {
            get { return EpubFormatDescription; }
            set { EpubFormatDescription = value; }
        }

        /// <remarks/>
        public OnixLegacyProductFormFeature productformfeature
        {
            get { return ProductFormFeature; }
            set { ProductFormFeature = value; }
        }

        /// <remarks/>
        public string b014
        {
            get { return ProductFormDescription; }
            set { ProductFormDescription = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("publisher")]
        public OnixLegacyPublisher[] publisher
        {
            get { return shortPublisherField; }
            set { shortPublisherField = value; }
        }

        #endregion

    }
}
