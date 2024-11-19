﻿using System;
using System.Collections.Generic;
using System.Linq;
using OnixData.Standard.Version3.Content;
using OnixData.Standard.Version3.Price;
using OnixData.Standard.Version3.Publishing;
using OnixData.Standard.Version3.Related;
using OnixData.Standard.Version3.Supply;
using OnixData.Standard.Version3.Text;
using OnixData.Standard.Version3.Title;

namespace OnixData.Standard.Version3
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
            barcodeField           = shortBarcodeField           = new OnixBarcode[0];
            productSupplyField     = shortProductSupplyField     = new OnixProductSupply[0];

            ContentDetail     = new OnixContentDetail();
            DescriptiveDetail = new OnixDescriptiveDetail();
            CollateralDetail  = new OnixCollateralDetail();
            PublishingDetail  = new OnixPublishingDetail();
            RelatedMaterial   = new OnixRelatedMaterial();            

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

        private OnixBarcode[]         barcodeField;
        private OnixBarcode[]         shortBarcodeField;

        private OnixProductSupply[]     productSupplyField;
        private OnixProductSupply[]     shortProductSupplyField;

        private OnixCollateralDetail  collateralDetailField;
        private OnixContentDetail     contentDetailField;
        private OnixDescriptiveDetail descriptiveDetailField;
        private OnixPublishingDetail  publishingDetailField;
        private OnixRelatedMaterial   relatedMaterialField;

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

        public string LastDateForReturns
        {
            get
            {
                string sLastDt = "";

                var SoughtSupplyDetail = USDRetailSupplyDetail;


                if ((SoughtSupplyDetail != null) &&
                    (SoughtSupplyDetail.OnixSupplyDateList != null) &&
                    (SoughtSupplyDetail.OnixSupplyDateList.Length > 0))
                {
                    var SoughtDate =
                        SoughtSupplyDetail.OnixSupplyDateList.Where(x => x.IsLasteDateForReturns()).FirstOrDefault();

                    if ((SoughtDate != null) && !String.IsNullOrEmpty(SoughtDate.Date))
                        sLastDt = SoughtDate.Date;
                }

                return sLastDt;
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

        public string NumberOfPages
        {
            get
            {
                string sNumOfPages = "";

                if ((this.ContentDetail != null) && (this.ContentDetail.PrimaryContentItem != null))
                    sNumOfPages = this.ContentDetail.PrimaryContentItem.NumberOfPages;

                if (String.IsNullOrEmpty(sNumOfPages))
                {
                    if ((this.DescriptiveDetail != null) && (this.DescriptiveDetail.OnixExtentList != null))
                    {
                        if (this.DescriptiveDetail.PageNumber > 0)
                            sNumOfPages = Convert.ToString(this.DescriptiveDetail.PageNumber);
                    }
                }

                return sNumOfPages;
            }
        }

        public string ProductForm
        {
            get { return (this.DescriptiveDetail != null ? this.DescriptiveDetail.ProductForm : ""); }
        }

        public string ProductFormDetail
        {
            get { return (this.DescriptiveDetail != null ? this.DescriptiveDetail.ProductForm : ""); }
        }

        public string[] ProductFormDetailList
        {
            get { return (this.DescriptiveDetail != null ? this.DescriptiveDetail.OnixProductFormDetailList : new string[0]); }
        }

        public string PrimaryContentType
        {
            get { return (this.DescriptiveDetail != null ? this.DescriptiveDetail.PrimaryContentType : ""); }
        }

        public string[] AllProductContentTypeList
        {
            get { return (this.DescriptiveDetail != null ? this.DescriptiveDetail.OnixAllContentTypeList : new string[0]); }
        }

        public string[] ProductContentTypeList
        {
            get { return (this.DescriptiveDetail != null ? this.DescriptiveDetail.OnixProductContentTypeList : new string[0]); }
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

        public List<OnixSupplierId> ProprietarySupplierIds
        {
            get
            {
                List<OnixSupplierId> PropSupplierIds = new List<OnixSupplierId>();

                if (this.OnixProductSupplyList != null)
                {
                    foreach (OnixProductSupply TmpSupply in this.OnixProductSupplyList)
                    {
                        foreach (OnixSupplyDetail TmpSupplyDetail in TmpSupply.OnixSupplyDetailList)
                        {
                            TmpSupplyDetail.OnixSupplierList
                                           .Where(x => x.OnixSupplierIdList != null &&
                                                       x.OnixSupplierIdList.Any(y => y.SupplierIDType == OnixSupplierId.CONST_SUPPL_ID_TYPE_PROP))
                                           .ToList()
                                           .ForEach(x => PropSupplierIds.AddRange(x.OnixSupplierIdList));
                        }
                    }
                }

                return PropSupplierIds;
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

                    if ((FoundPublisher != null) && !String.IsNullOrEmpty(FoundPublisher.PublisherName))
                        FoundPubName = FoundPublisher.PublisherName;
                    else
                    {
                        FoundPublisher =
                            PublishingDetail.OnixPublisherList.Where(x => x.PublishingRole == OnixPublisher.CONST_PUB_ROLE_IN_ASSOC).LastOrDefault();

                        if ((FoundPublisher != null) && !String.IsNullOrEmpty(FoundPublisher.PublisherName))
                            FoundPubName = FoundPublisher.PublisherName;
                    }
                }

                return FoundPubName;
            }
        }

        public string PublishingStatus
        {
            get { return (PublishingDetail != null) ? PublishingDetail.PublishingStatus : ""; }
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

        public bool HasMissingSalesRightsData()
        {
            return (this.PublishingDetail != null) ? this.PublishingDetail.MissingSalesRightsDataFlag : false;
        }

        public bool HasNoSalesRightsinUS()
        {
            return (this.PublishingDetail != null) ? this.PublishingDetail.NoSalesRightsInUSFlag : false;
        }

        public bool HasNotForSaleRights()
        {
            bool bNotForSalesRights = false;

            if ((this.PublishingDetail != null) && (this.PublishingDetail.OnixSalesRightsList != null) && (this.PublishingDetail.OnixSalesRightsList.Count() > 0))
            {
                bNotForSalesRights = this.PublishingDetail.OnixSalesRightsList.Any(x => x.HasNotForSalesRights);
            }

            return bNotForSalesRights;
        }

        public bool HasOutsideUSCountrySalesRights()
        {
            return (this.PublishingDetail != null) ? this.PublishingDetail.SalesRightsInNonUSCountryFlag : false;
        }

        public bool HasSalesRights()
        {
            bool bSalesRights = false;

            if ((this.PublishingDetail != null) && (this.PublishingDetail.OnixSalesRightsList != null) && (this.PublishingDetail.OnixSalesRightsList.Count() > 0))
            {
                bSalesRights = this.PublishingDetail.OnixSalesRightsList.Any(x => x.HasSalesRights);
            }

            return bSalesRights;
        }

        public bool HasUSDPrice()
        {
            bool bHasUSDPrice = false;

            if (this.OnixProductSupplyList != null)
            {
                foreach (OnixProductSupply TmpProductSupply in this.OnixProductSupplyList)
                {
                    foreach (OnixSupplyDetail TmpSupplyDetail in TmpProductSupply.OnixSupplyDetailList)
                    {                        
                        OnixPrice[] Prices = TmpSupplyDetail.OnixPriceList;

                        bHasUSDPrice = Prices.Any(x => x.HasSoughtPriceTypeCode() && (x.CurrencyCode == "USD"));

                        if (bHasUSDPrice)
                            break;
                    }
                }
            }

            return bHasUSDPrice;
        }


        public bool HasUSDRetailPrice()
        {
            bool bHasSoughtPrice = false;

            if (this.OnixProductSupplyList != null)
            {
                foreach (OnixProductSupply TmpProductSupply in this.OnixProductSupplyList)
                {
                    foreach (OnixSupplyDetail TmpSupplyDetail in TmpProductSupply.OnixSupplyDetailList)
                    {
                        OnixPrice[] Prices = TmpSupplyDetail.OnixPriceList;

                        bHasSoughtPrice =
                            Prices.Any(x => (x.PriceType == OnixPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD"));

                        if (bHasSoughtPrice)
                            break;
                    }
                }
            }

            return bHasSoughtPrice;
        }

        public bool HasUSRights()
        {
            bool bHasUSRights = false;

            bHasUSRights = (this.PublishingDetail != null) ? this.PublishingDetail.SalesRightsInUSFlag : false;

            return bHasUSRights;
        }

        public bool HasWorldSalesRights()
        {
            return (this.PublishingDetail != null) ? this.PublishingDetail.SalesRightsAllWorldFlag : false;
        }

        public string SeriesNumber
        {
            get
            {
                string FoundSeriesNum = "";

                if (DescriptiveDetail != null)
                    FoundSeriesNum = DescriptiveDetail.SeriesNumber;

                return FoundSeriesNum;
            }
        }

        public string SeriesTitle
        {
            get
            {
                string FoundSeriesTitle = "";

                if (DescriptiveDetail != null)
                    FoundSeriesTitle = DescriptiveDetail.SeriesTitle;

                return FoundSeriesTitle;
            }
        }

        public OnixPrice USDCostPrice
        {
            get
            {
                OnixPrice USDPrice = new OnixPrice();

                OnixSupplyDetail TargetSupplyDetail = USDRetailSupplyDetail;

                if ((TargetSupplyDetail != null) && (TargetSupplyDetail.OnixPriceList != null) && (TargetSupplyDetail.OnixPriceList.Length > 0))
                {
                    OnixPrice[] Prices = TargetSupplyDetail.OnixPriceList;

                    USDPrice =
                        Prices.Where(x => x.HasSoughtSupplyCostPriceType() && (x.CurrencyCode == "USD")).FirstOrDefault();

                    if (USDPrice == null)
                        USDPrice = new OnixPrice();
                }

                return USDPrice;
            }
        }

        public OnixPrice USDRetailPrice
        {
            get
            {
                OnixPrice        USDPrice           = new OnixPrice();
                OnixSupplyDetail TargetSupplyDetail = USDRetailSupplyDetail;

                if ((TargetSupplyDetail != null) && (TargetSupplyDetail.OnixPriceList != null) && (TargetSupplyDetail.OnixPriceList.Length > 0))
                {
                    OnixPrice[] Prices = TargetSupplyDetail.OnixPriceList;

                    USDPrice =
                        Prices.Where(x => (x.PriceType == OnixPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD")).FirstOrDefault();

                    if (USDPrice == null)
                        USDPrice = new OnixPrice();
                }

                return USDPrice;
            }
        }

        public OnixPrice USDValidPrice
        {
            get
            {
                OnixPrice USDPrice = new OnixPrice();

                if ((USDValidPriceList != null) && (USDValidPriceList.Count > 0))
                    USDPrice = USDValidPriceList.ElementAt(0);

                return USDPrice;
            }
        }

        public List<OnixPrice> USDValidPriceList
        {
            get
            {
                List<OnixPrice> USDPriceList = new List<OnixPrice>();

                if (this.OnixProductSupplyList != null)
                {
                    foreach (OnixProductSupply TmpPrdSupply in this.OnixProductSupplyList)
                    {
                        foreach (var TmpSupplyDetail in TmpPrdSupply.OnixSupplyDetailList)
                        {
                            if (TmpSupplyDetail != null)
                            {
                                if ((TmpSupplyDetail != null) &&
                                    (TmpSupplyDetail.OnixPriceList != null) &&
                                    (TmpSupplyDetail.OnixPriceList.Length > 0))
                                {
                                    OnixPrice[] Prices = TmpSupplyDetail.OnixPriceList;

                                    var TmpPriceList =
                                        Prices.Where(x => x.HasSoughtPriceTypeCode() && (x.CurrencyCode == "USD")).ToArray();

                                    if ((TmpPriceList != null) && (TmpPriceList.Length > 0))
                                        USDPriceList.AddRange(TmpPriceList);
                                }
                            }
                        }
                    }
                }

                return USDPriceList;
            }
        }

        public OnixSupplyDetail USDRetailSupplyDetail
        {
            get
            {
                OnixSupplyDetail SupplyDetail = new OnixSupplyDetail();

                if (this.OnixProductSupplyList != null)
                {
                    foreach (OnixProductSupply TmpPrdSupply in this.OnixProductSupplyList)
                    {
                        foreach (OnixSupplyDetail TmpSupplyDetail in TmpPrdSupply.OnixSupplyDetailList)
                        {
                            OnixPrice[] Prices = TmpSupplyDetail.OnixPriceList;

                            OnixPrice USDPrice =
                                Prices.Where(x => (x.PriceType == OnixPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD")).FirstOrDefault();

                            if ((USDPrice != null) && (USDPrice.PriceAmountNum > 0))
                            {
                                SupplyDetail = TmpSupplyDetail;
                                break;
                            }
                        }
                    }
                }

                return SupplyDetail;
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

        public OnixBarcode[] OnixBarcodeList
        {
            get
            {
                OnixBarcode[] BarcodeList = null;

                if (this.barcodeField != null)
                    BarcodeList = this.barcodeField;
                else if (this.shortBarcodeField != null)
                    BarcodeList = this.shortBarcodeField;
                else
                    BarcodeList = new OnixBarcode[0];

                return BarcodeList;
            }
        }

        public OnixProductSupply[] OnixProductSupplyList
        {
            get
            {
                OnixProductSupply[] ProductSupplyList = null;

                if (this.productSupplyField != null)
                    ProductSupplyList = this.productSupplyField;
                else if (this.shortProductSupplyField != null)
                    ProductSupplyList = this.shortProductSupplyField;
                else
                    ProductSupplyList = new OnixProductSupply[0];

                return ProductSupplyList;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string RecordReference
        {
            get { return this.recordReferenceField; }
            set { this.recordReferenceField = value; }
        }

        /// <remarks/>
        public int NotificationType
        {
            get { return this.notificationTypeField; }
            set { this.notificationTypeField = value; }
        }

        /// <remarks/>
        public int RecordSourceType
        {
            get { return this.recordSourceTypeField; }
            set { this.recordSourceTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductIdentifier", IsNullable = false)]
        public OnixProductId[] ProductIdentifier
        {
            get { return this.productIdentifierField; }
            set { this.productIdentifierField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Barcode")]
        public OnixBarcode[] Barcode
        {
            get { return this.barcodeField; }
            set { this.barcodeField = value; }
        }

        public OnixCollateralDetail CollateralDetail
        {
            get { return this.collateralDetailField; }
            set { this.collateralDetailField = value; }
        }

        /// <remarks/>
        public OnixContentDetail ContentDetail
        {
            get { return this.contentDetailField; }
            set { this.contentDetailField = value; }
        }

        /// <remarks/>
        public OnixDescriptiveDetail DescriptiveDetail
        {
            get { return this.descriptiveDetailField; }
            set { this.descriptiveDetailField = value; }
        }

        public string Title
        {
            get
            {
                string ProductTitle = "";

                if ((DescriptiveDetail != null) &&
                    (DescriptiveDetail.TitleDetail != null) &&
                    (DescriptiveDetail.TitleDetail.TitleTypeNum == OnixTitleElement.CONST_TITLE_TYPE_PRODUCT))
                {
                    OnixTitleDetail ProductTitleDetail = DescriptiveDetail.TitleDetail;

                    if (ProductTitleDetail.FirstTitleElement != null)
                    {
                        ProductTitle = ProductTitleDetail.FirstTitleElement.Title;

                        if (!String.IsNullOrEmpty(ProductTitleDetail.FirstTitleElement.Subtitle))
                            ProductTitle += ": " + ProductTitleDetail.FirstTitleElement.Subtitle;
                    }
                }

                return ProductTitle;
            }
        }

        /// <remarks/>
        public OnixPublishingDetail PublishingDetail
        {
            get { return this.publishingDetailField; }
            set { this.publishingDetailField = value; }
        }

        /// <remarks/>
        public OnixRelatedMaterial RelatedMaterial
        {
            get { return this.relatedMaterialField; }
            set { this.relatedMaterialField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductSupply")]
        public OnixProductSupply[] ProductSupply
        {
            get { return this.productSupplyField; }
            set { this.productSupplyField = value; }
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
        [System.Xml.Serialization.XmlElementAttribute("barcode")]
        public OnixBarcode[] barcode
        {
            get { return this.shortBarcodeField; }
            set { this.shortBarcodeField = value; }
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
        [System.Xml.Serialization.XmlElementAttribute("productsupply")]
        public OnixProductSupply[] productsupply
        {
            get { return shortProductSupplyField; }
            set { shortProductSupplyField = value; }
        }

        #endregion

        #region Support Methods

        public OnixMeasure GetMeasurement(int pnType, bool pbMetricPreferred = false)
        {
            OnixMeasure FoundMeasurement = new OnixMeasure();

            if ((DescriptiveDetail != null) &&
                (DescriptiveDetail.OnixMeasureList != null) &&
                (DescriptiveDetail.OnixMeasureList.Length > 0))
            {
                OnixMeasure[] MeasureList = DescriptiveDetail.OnixMeasureList;

                OnixMeasure MeasureType = null;

                MeasureType = MeasureList.Where(x => (x.MeasureType == pnType) && !x.IsMetricUnitType()).LastOrDefault();
                if (MeasureType != null)
                    FoundMeasurement = MeasureType;
                
                if ((MeasureType == null) || (MeasureType.Measurement == 0) || pbMetricPreferred)
                {
                    MeasureType = MeasureList.Where(x => (x.MeasureType == pnType) && x.IsMetricUnitType()).LastOrDefault();

                    if (MeasureType != null)
                        FoundMeasurement = MeasureType;
                }
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
