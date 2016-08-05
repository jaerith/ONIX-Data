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
        #region CONSTANTS

        private const int CONST_PRD_PACK_SLIPSLEEVE      = 1;
        private const int CONST_PRD_PACK_CLAMSHELL       = 2;
        private const int CONST_PRD_PACK_JEWELCASE       = 5;
        private const int CONST_PRD_PACK_IN_BOX          = 9;
        private const int CONST_PRD_PACK_SLIP_CASE_SNGL  = 10;
        private const int CONST_PRD_PACK_SLIP_CASE_MULT  = 11;
        private const int CONST_PRD_PACK_TUBE_ROLL       = 12;
        private const int CONST_PRD_PACK_BINDER          = 13;
        private const int CONST_PRD_PACK_WALLET          = 14;

        #endregion

        public OnixLegacyProduct()
        {
            RecordReference  = 0;
            NotificationType = NumberOfPieces = TradeCategory = Barcode = -1;

            ProductIdentifier = new OnixLegacyProductId[0];

            BookFormDetail         = "";
            ProductPackaging       = 0;
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
            Complexity    = new OnixLegacyComplexity[0];
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

        private string bookFormDetailField;
        private int    productPackagingField;

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

        private OnixLegacyExtent[]     extentField;
        private OnixLegacySeries[]     seriesField;
        private OnixLegacySubject[]    subjectField;
        private OnixLegacyComplexity[] complexityField;
        private OnixLegacyAudRange[]   audienceRangeField;
        private OnixLegacyOtherText[]  otherTextField;
        private OnixLegacyMediaFile[]  mediaFileField;
        private OnixLegacyImprint[]    imprintField;

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

        #region Reference Tags

        /// <remarks/>
        public string BookFormDetail
        {
            get
            {
                return this.bookFormDetailField;
            }
            set
            {
                this.bookFormDetailField = value;
            }
        }

        /// <remarks/>
        public int ProductPackaging
        {
            get
            {
                return this.productPackagingField;
            }
            set
            {
                this.productPackagingField = value;
            }
        }

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
        [System.Xml.Serialization.XmlElementAttribute("Complexity")]
        public OnixLegacyComplexity[] Complexity
        {
            get
            {
                return this.complexityField;
            }
            set
            {
                this.complexityField = value;
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

        public List<OnixLegacySupplierId> ProprietarySuppliers
        {
            get
            {
                List<OnixLegacySupplierId> PropSuppliers = new List<OnixLegacySupplierId>();

                if ((SupplyDetail != null) && (SupplyDetail.SupplierIdentifier != null) && (SupplyDetail.SupplierIdentifier.Length > 0))
                {
                    PropSuppliers =
                        SupplyDetail.SupplierIdentifier.Where(x => x.SupplierIDType == OnixLegacySupplierId.CONST_SUPPL_ID_TYPE_PROP).ToList();
                }

                return PropSuppliers;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b013
        {
            get { return BookFormDetail; }
            set { BookFormDetail = value; }
        }

        /// <remarks/>
        public int b225
        {
            get { return ProductPackaging; }
            set { ProductPackaging = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b056")]
        public string[] b056
        {
            get { return EditionTypeCode; }
            set { EditionTypeCode = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b057")]
        public int[] b057
        {
            get { return EditionNumber; }
            set { EditionNumber = value; }
        }

        /// <remarks/>
        public OnixLegacyTitle title
        {
            get { return Title; }
            set { Title = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contributor")]
        public OnixLegacyContributor[] contributor
        {
            get { return Contributor; }
            set { Contributor = value; }
        }

        /// <remarks/>
        public string b049
        {
            get { return ContributorStatement; }
            set { ContributorStatement = value; }
        }

        /// <remarks/>
        public OnixLegacyLanguage language
        {
            get { return Language; }
            set { Language = value; }
        }


        /// <remarks/>
        public int b026
        {
            get { return ItemNumberWithinSet; }
            set { ItemNumberWithinSet = value; }
        }

        /// <remarks/>
        public int b061
        {
            get { return NumberOfPages; }
            set { NumberOfPages = value; }
        }

        /// <remarks/>
        public int b254
        {
            get { return PagesRoman; }
            set { PagesRoman = value; }
        }

        /// <remarks/>
        public int b255
        {
            get { return PagesArabic; }
            set { PagesArabic = value; }
        }

        /// <remarks/>
        public OnixLegacyIllustrations illustrations
        {
            get { return Illustrations; }
            set { Illustrations = value; }
        }

        /// <remarks/>
        public string b064
        {
            get { return BASICMainSubject; }
            set { BASICMainSubject = value; }
        }

        /// <remarks/> 
        public string b073
        {
            get { return AudienceCode; }
            set { AudienceCode = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("extent")]
        public OnixLegacyExtent[] extent
        {
            get { return Extent; }
            set { Extent = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("series")]
        public OnixLegacySeries[] series
        {
            get { return Series; }
            set { Series = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("subject")]
        public OnixLegacySubject[] subject
        {
            get { return Subject; }
            set { Subject = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("complexity")]
        public OnixLegacyComplexity[] complexity
        {
            get { return Complexity; }
            set { Complexity = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("audiencerange")]
        public OnixLegacyAudRange[] audiencerange
        {
            get { return AudienceRange; }
            set { AudienceRange = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("othertext")]
        public OnixLegacyOtherText[] othertext
        {
            get { return OtherText; }
            set { OtherText = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("mediafile")]
        public OnixLegacyMediaFile[] mediafile
        {
            get { return MediaFile; }
            set { MediaFile = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("imprint")]
        public OnixLegacyImprint[] imprint
        {
            get { return Imprint; }
            set { Imprint = value; }
        }

        /// <remarks/>
        public string b209
        {
            get { return CityOfPublication; }
            set { CityOfPublication = value; }
        }

        /// <remarks/>
        public string b083
        {
            get { return CountryOfPublication; }
            set { CountryOfPublication = value; }
        }

        /// <remarks/>
        public string b394
        {
            get { return PublishingStatus; }
            set { PublishingStatus = value; }
        }

        /// <remarks/>
        public string b086
        {
            get { return AnnouncementDate; }
            set { AnnouncementDate = value; }
        }

        /// <remarks/>
        public string b362
        {
            get { return TradeAnnouncementDate; }
            set { TradeAnnouncementDate = value; }
        }

        /// <remarks/>
        public uint b003
        {
            get { return PublicationDate; }
            set { PublicationDate = value; }
        }

        /// <remarks/>
        public uint b088
        {
            get { return YearFirstPublished; }
            set { YearFirstPublished = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("salesrights")]
        public OnixLegacySalesRights[] salesrights
        {
            get { return SalesRights; }
            set { SalesRights = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("measure")]
        public OnixLegacyMeasure[] measure
        {
            get { return Measure; }
            set { Measure = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("relatedproduct")]
        public OnixLegacyRelatedProduct[] relatedproduct
        {
            get { return RelatedProduct; }
            set { RelatedProduct = value; }
        }

        /// <remarks/>
        public OnixLegacySupplyDetail supplydetail
        {
            get { return SupplyDetail; }
            set { SupplyDetail = value; }
        }

        #endregion
    }
}
