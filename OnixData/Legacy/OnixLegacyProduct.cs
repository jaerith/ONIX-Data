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
            RecordReference  = "";
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

            editionTypeCodeField = shortEditionTypeCodeField = null;
            editionNumberField   = shortEditionNumberField   = null;

            ProductFormFeature = new OnixLegacyProductFormFeature();

            Title = new OnixLegacyTitle();

            ContributorStatement = "";

            Language = new OnixLegacyLanguage();

            ItemNumberWithinSet = -1;

            NumberOfPages = PagesRoman = PagesArabic = -1;

            Illustrations = new OnixLegacyIllustrations();

            BASICMainSubject = "";
            AudienceCode     = "";

            audienceRangeField  = shortAudienceRangeField  = new OnixLegacyAudRange[0];
            contributorField    = shortContributorField    = new OnixLegacyContributor[0];
            extentField         = shortExtentField         = new OnixLegacyExtent[0];
            seriesField         = shortSeriesField         = new OnixLegacySeries[0];
            subjectField        = shortSubjectField        = new OnixLegacySubject[0];
            complexityField     = shortComplexityField     = new OnixLegacyComplexity[0];            
            imprintField        = shortImprintField        = new OnixLegacyImprint[0];
            measureField        = shortMeasureField        = new OnixLegacyMeasure[0];
            mediaFileField      = shortMediaFileField      = new OnixLegacyMediaFile[0];
            otherTextField      = shortOtherTextField      = new OnixLegacyOtherText[0];
            relatedProductField = shortRelatedProductField = new OnixLegacyRelatedProduct[0];
            salesRightsField    = shortSalesRightsField    = new OnixLegacySalesRights[0];

            cityOfPublicationField = countryOfPublicationField  = publishingStatusField = "";
            announcementDateField  = tradeAnnouncementDateField = "";

            PublicationDate = YearFirstPublished = 0;

            SupplyDetail = new OnixLegacySupplyDetail();
            ParsingError = null;
        }

        private string bookFormDetailField;
        private int    productPackagingField;

        private string[] editionTypeCodeField;
        private string[] shortEditionTypeCodeField;
        private int[]    editionNumberField;
        private int[]    shortEditionNumberField;

        private OnixLegacyTitle titleField;

        private string contributorStatementField;

        private OnixLegacyLanguage languageField;

        private int itemNumberWithinSetField;
        private int numberOfPagesField;
        private int pagesRomanField;
        private int pagesArabicField;

        private OnixLegacyIllustrations illustrationsField;

        private string basicMainSubjectField;
        private string audienceCodeField;

        private OnixLegacyAudRange[]       audienceRangeField;
        private OnixLegacyAudRange[]       shortAudienceRangeField;
        private OnixLegacyContributor[]    contributorField;
        private OnixLegacyContributor[]    shortContributorField;
        private OnixLegacyExtent[]         extentField;
        private OnixLegacyExtent[]         shortExtentField;
        private OnixLegacySeries[]         seriesField;
        private OnixLegacySeries[]         shortSeriesField;
        private OnixLegacySubject[]        subjectField;
        private OnixLegacySubject[]        shortSubjectField;
        private OnixLegacyComplexity[]     complexityField;
        private OnixLegacyComplexity[]     shortComplexityField;
        private OnixLegacyImprint[]        imprintField;
        private OnixLegacyImprint[]        shortImprintField;
        private OnixLegacyMediaFile[]      mediaFileField;
        private OnixLegacyMediaFile[]      shortMediaFileField;
        private OnixLegacyMeasure[]        measureField;
        private OnixLegacyMeasure[]        shortMeasureField;
        private OnixLegacyRelatedProduct[] relatedProductField;
        private OnixLegacyRelatedProduct[] shortRelatedProductField;
        private OnixLegacyOtherText[]      otherTextField;
        private OnixLegacyOtherText[]      shortOtherTextField;
        private OnixLegacySalesRights[]    salesRightsField;
        private OnixLegacySalesRights[]    shortSalesRightsField;

        private string cityOfPublicationField;
        private string countryOfPublicationField;
        private string publishingStatusField;
        private string announcementDateField;
        private string tradeAnnouncementDateField;
        private uint   publicationDateField;
        private uint   yearFirstPublishedField;

        private OnixLegacySupplyDetail supplyDetailField;

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

        public OnixLegacySubject BisacCategoryCode
        {
            get
            {
                OnixLegacySubject FoundSubject = new OnixLegacySubject();

                OnixLegacySubject[] SubjectList = OnixSubjectList;
                if ((SubjectList != null) && (SubjectList.Length > 0))
                {
                    FoundSubject =
                        SubjectList.Where(x => x.SubjectSchemeIdentifier == OnixLegacySubject.CONST_SUBJ_SCHEME_BISAC_CAT_ID).FirstOrDefault();
                }

                return FoundSubject;
            }
        }

        public OnixLegacySubject BisacRegionCode
        {
            get
            {
                OnixLegacySubject FoundSubject = new OnixLegacySubject();

                OnixLegacySubject[] SubjectList = OnixSubjectList;
                if ((SubjectList != null) && (SubjectList.Length > 0))
                {
                    FoundSubject =
                        SubjectList.Where(x => x.SubjectSchemeIdentifier == OnixLegacySubject.CONST_SUBJ_SCHEME_REGION_ID).FirstOrDefault();
                }

                return FoundSubject;
            }
        }

        public bool HasFutureRetailPrice()
        {
            bool bHasFuturePrice = false;

            if ((SupplyDetail != null) && (SupplyDetail.OnixPriceList != null) && (SupplyDetail.OnixPriceList.Length > 0))
            {
                bHasFuturePrice =
                    SupplyDetail.OnixPriceList.Any(x => !String.IsNullOrEmpty(x.PriceEffectiveFrom));
            }

            return bHasFuturePrice;
        }

        public bool HasUSDRetailPrice()
        {
            bool bHasUSDPrice = false;

            if ((SupplyDetail != null) && (SupplyDetail.OnixPriceList != null) && (SupplyDetail.OnixPriceList.Length > 0))
            {
                bHasUSDPrice =
                    SupplyDetail.OnixPriceList.Any(x => (x.PriceTypeCode == OnixLegacyPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD"));
            }

            return bHasUSDPrice;
        }

        public bool HasUSRights()
        {
            bool bHasUSRights = false;

            int[] aSalesRightsColl = new int[] { OnixLegacySalesRights.CONST_SR_TYPE_FOR_SALE_WITH_EXCL_RIGHTS,
                                                 OnixLegacySalesRights.CONST_SR_TYPE_FOR_SALE_WITH_NONEXCL_RIGHTS };

            OnixLegacySalesRights[] SalesRightsList = OnixSalesRightsList;
            if ((SalesRightsList != null) && (SalesRightsList.Length > 0))
            {
                bHasUSRights =
                    SalesRightsList.Any(x => aSalesRightsColl.Contains(x.SalesRightsType) &&
                                              (x.RightsCountryList.Contains("US") ||
                                               x.RightsTerritoryList.Contains("WORLD") ||
                                               x.RightsTerritoryList.Contains("ROW")));
            }

            return bHasUSRights;
        }

        public OnixLegacyMeasure Height
        {
            get { return GetMeasurement(OnixLegacyMeasure.CONST_MEASURE_TYPE_HEIGHT); }
        }

        public OnixLegacyMeasure Thick
        {
            get { return GetMeasurement(OnixLegacyMeasure.CONST_MEASURE_TYPE_THICK); }
        }

        public OnixLegacyMeasure Weight
        {
            get { return GetMeasurement(OnixLegacyMeasure.CONST_MEASURE_TYPE_WEIGHT); }
        }

        public OnixLegacyMeasure Width
        {
            get { return GetMeasurement(OnixLegacyMeasure.CONST_MEASURE_TYPE_WIDTH); }
        }

        public OnixLegacyContributor PrimaryAuthor
        {
            get
            {
                OnixLegacyContributor PrimaryAuthor = new OnixLegacyContributor();

                OnixLegacyContributor[] ContributorList = OnixContributorList;
                if ((ContributorList != null) && (ContributorList.Length > 0))
                {
                    PrimaryAuthor =
                        ContributorList.Where(x => x.ContributorRole == OnixLegacyContributor.CONST_CONTRIB_ROLE_AUTHOR).FirstOrDefault();
                }

                return PrimaryAuthor;
            }
        }

        public string ProprietaryImprintName
        {
            get
            {
                string FoundImprintName = "";

                OnixLegacyImprint[] ImprintList = OnixImprintList;
                if ((ImprintList != null) && (ImprintList.Length > 0))
                {
                    OnixLegacyImprint FoundImprint =
                        ImprintList.Where(x => x.NameCodeType == OnixLegacyImprint.CONST_IMPRINT_ROLE_PROP).FirstOrDefault();

                    FoundImprintName = FoundImprint.ImprintName;
                }

                return FoundImprintName;
            }
        }

        public List<OnixLegacySupplierId> ProprietarySuppliers
        {
            get
            {
                List<OnixLegacySupplierId> PropSuppliers = new List<OnixLegacySupplierId>();

                if ((SupplyDetail != null) && (SupplyDetail.OnixSupplierIdList != null) && (SupplyDetail.OnixSupplierIdList.Length > 0))
                {
                    PropSuppliers =
                        SupplyDetail.OnixSupplierIdList.Where(x => x.SupplierIDType == OnixLegacySupplierId.CONST_SUPPL_ID_TYPE_PROP).ToList();
                }

                return PropSuppliers;
            }
        }

        public int SeriesNumber
        {
            get
            {
                int FoundSeriesNum = 0;

                OnixLegacySeries[] SeriesList = OnixSeriesList;
                if ((SeriesList != null) && (SeriesList.Length > 0))
                    FoundSeriesNum = SeriesList[0].NumberWithinSeries;

                return FoundSeriesNum;
            }
        }

        public string SeriesTitle
        {
            get
            {
                string FoundSeriesTitle = "";

                OnixLegacySeries[] SeriesList = OnixSeriesList;
                if ((SeriesList != null) && (SeriesList.Length > 0))
                    FoundSeriesTitle = SeriesList[0].TitleOfSeries;

                return FoundSeriesTitle;
            }
        }

        public OnixLegacyPrice USDRetailPrice
        {
            get
            {
                OnixLegacyPrice USDPrice = new OnixLegacyPrice();

                if ((SupplyDetail != null) && (SupplyDetail.OnixPriceList != null) && (SupplyDetail.OnixPriceList.Length > 0))
                {
                    OnixLegacyPrice[] Prices = SupplyDetail.OnixPriceList;

                    USDPrice =
                        Prices.Where(x => (x.PriceTypeCode == OnixLegacyPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD")).FirstOrDefault();
                }

                return USDPrice;
            }
        }

        #endregion

        #region ONIX Lists

        public OnixLegacyAudRange[] OnixAudRangeList
        {
            get
            {
                OnixLegacyAudRange[] AudRanges = null;

                if (this.audienceRangeField != null)
                    AudRanges = this.audienceRangeField;
                else if (this.shortAudienceRangeField != null)
                    AudRanges = this.shortAudienceRangeField;
                else
                    AudRanges = new OnixLegacyAudRange[0];

                return AudRanges;
            }
        }

        public string[] OnixEditionTypeCodeList
        {
            get
            {
                string[] EditionTypeCodes = null;

                if (editionTypeCodeField != null)
                    EditionTypeCodes = this.editionTypeCodeField;
                else if (shortEditionTypeCodeField != null)
                    EditionTypeCodes = this.shortEditionTypeCodeField;
                else
                    EditionTypeCodes = new string[0];

                return EditionTypeCodes;
            }
        }

        public int[] OnixEditionNumberList
        {
            get
            {
                int[] EditionNumbers = null;

                if (editionNumberField != null)
                    EditionNumbers = this.editionNumberField;
                else if (shortEditionNumberField != null)
                    EditionNumbers = this.shortEditionNumberField;
                else
                    EditionNumbers = new int[0];

                return EditionNumbers;
            }
        }

        public OnixLegacyComplexity[] OnixComplexityList
        {
            get
            {
                OnixLegacyComplexity[] Complexities = null;

                if (this.complexityField != null)
                    Complexities = this.complexityField;
                else if (this.shortComplexityField != null)
                    Complexities = this.shortComplexityField;
                else
                    Complexities = new OnixLegacyComplexity[0];

                return Complexities;
            }
        }

        public OnixLegacyContributor[] OnixContributorList
        {
            get
            {
                OnixLegacyContributor[] Contributors = null;

                if (this.contributorField != null)
                    Contributors = this.contributorField;
                else if (this.shortContributorField != null)
                    Contributors = this.shortContributorField;
                else
                    Contributors = new OnixLegacyContributor[0];

                return Contributors;
            }
        }

        public OnixLegacyExtent[] OnixExtentList
        {
            get
            {
                OnixLegacyExtent[] Extents = null;

                if (this.extentField != null)
                    Extents = this.extentField;
                else if (this.shortExtentField != null)
                    Extents = this.shortExtentField;
                else
                    Extents = new OnixLegacyExtent[0];

                return Extents;
            }
        }

        public OnixLegacyImprint[] OnixImprintList
        {
            get
            {
                OnixLegacyImprint[] Imprints = null;

                if (this.imprintField != null)
                    Imprints = this.imprintField;
                else if (this.shortImprintField != null)
                    Imprints = this.shortImprintField;
                else
                    Imprints = new OnixLegacyImprint[0];

                return Imprints;
            }
        }

        public OnixLegacyMediaFile[] OnixMediaFileList
        {
            get
            {
                OnixLegacyMediaFile[] MediaFiles = null;

                if (this.mediaFileField != null)
                    MediaFiles = this.mediaFileField;
                else if (this.shortMediaFileField != null)
                    MediaFiles = this.shortMediaFileField;
                else
                    MediaFiles = new OnixLegacyMediaFile[0];

                return MediaFiles;
            }
        }

        public OnixLegacyMeasure[] OnixMeasureList
        {
            get
            {
                OnixLegacyMeasure[] Measures = null;

                if (this.measureField != null)
                    Measures = this.measureField;
                else if (this.shortMeasureField != null)
                    Measures = this.shortMeasureField;
                else
                    Measures = new OnixLegacyMeasure[0];

                return Measures;
            }
        }

        public OnixLegacyOtherText[] OnixOtherTextList
        {
            get
            {
                OnixLegacyOtherText[] OtherTexts = null;

                if (this.otherTextField != null)
                    OtherTexts = this.otherTextField;
                else if (this.shortOtherTextField != null)
                    OtherTexts = this.shortOtherTextField;
                else
                    OtherTexts = new OnixLegacyOtherText[0];

                return OtherTexts;
            }
        }

        public OnixLegacyRelatedProduct[] OnixRelatedProductList
        {
            get
            {
                OnixLegacyRelatedProduct[] RelatedProducts = null;

                if (this.relatedProductField != null)
                    RelatedProducts = this.relatedProductField;
                else if (this.shortRelatedProductField != null)
                    RelatedProducts = this.shortRelatedProductField;
                else
                    RelatedProducts = new OnixLegacyRelatedProduct[0];

                return RelatedProducts;
            }
        }

        public OnixLegacySalesRights[] OnixSalesRightsList
        {
            get
            {
                OnixLegacySalesRights[] SalesRights = null;

                if (this.salesRightsField != null)
                    SalesRights = this.salesRightsField;
                else if (this.shortSalesRightsField != null)
                    SalesRights = this.shortSalesRightsField;
                else
                    SalesRights = new OnixLegacySalesRights[0];

                return SalesRights;
            }
        }

        public OnixLegacySeries[] OnixSeriesList
        {
            get
            {
                OnixLegacySeries[] Series = null;

                if (this.seriesField != null)
                    Series = this.seriesField;
                else if (this.shortSeriesField != null)
                    Series = this.shortSeriesField;
                else
                    Series = new OnixLegacySeries[0];

                return Series;
            }
        }

        public OnixLegacySubject[] OnixSubjectList
        {
            get
            {
                OnixLegacySubject[] Subjects = null;

                if (this.subjectField != null)
                    Subjects = this.subjectField;
                else if (this.shortSubjectField != null)
                    Subjects = this.shortSubjectField;
                else
                    Subjects = new OnixLegacySubject[0];

                return Subjects;
            }
        }

        #endregion

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
            get { return shortEditionTypeCodeField; }
            set { shortEditionTypeCodeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b057")]
        public int[] b057
        {
            get { return shortEditionNumberField; }
            set { shortEditionNumberField = value; }
        }

        /// <remarks/>
        public OnixLegacyTitle title
        {
            get { return Title; }
            set { Title = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("audiencerange")]
        public OnixLegacyAudRange[] audiencerange
        {
            get { return shortAudienceRangeField; }
            set { shortAudienceRangeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contributor")]
        public OnixLegacyContributor[] contributor
        {
            get { return this.shortContributorField; }
            set { this.shortContributorField = value; }
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
            get { return shortExtentField; }
            set { shortExtentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("series")]
        public OnixLegacySeries[] series
        {
            get { return shortSeriesField; }
            set { shortSeriesField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("subject")]
        public OnixLegacySubject[] subject
        {
            get { return shortSubjectField; }
            set { shortSubjectField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("complexity")]
        public OnixLegacyComplexity[] complexity
        {
            get { return shortComplexityField; }
            set { shortComplexityField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("imprint")]
        public OnixLegacyImprint[] imprint
        {
            get { return shortImprintField; }
            set { shortImprintField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("othertext")]
        public OnixLegacyOtherText[] othertext
        {
            get { return shortOtherTextField; }
            set { shortOtherTextField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("measure")]
        public OnixLegacyMeasure[] measure
        {
            get { return shortMeasureField; }
            set { shortMeasureField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("mediafile")]
        public OnixLegacyMediaFile[] mediafile
        {
            get { return shortMediaFileField; }
            set { shortMediaFileField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("relatedproduct")]
        public OnixLegacyRelatedProduct[] relatedproduct
        {
            get { return shortRelatedProductField; }
            set { shortRelatedProductField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("salesrights")]
        public OnixLegacySalesRights[] salesrights
        {
            get { return shortSalesRightsField; }
            set { shortSalesRightsField = value; }
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
        public OnixLegacySupplyDetail supplydetail
        {
            get { return SupplyDetail; }
            set { SupplyDetail = value; }
        }

        #endregion

        #region Support Methods

        public OnixLegacyMeasure GetMeasurement(int Type)
        {
            OnixLegacyMeasure FoundMeasurement = new OnixLegacyMeasure();

            OnixLegacyMeasure[] MeasureList = OnixMeasureList;
            if ((MeasureList != null) && (MeasureList.Length > 0))
            {
                FoundMeasurement =
                    MeasureList.Where(x => x.MeasureTypeCode == Type).FirstOrDefault();
            }

            return FoundMeasurement;
        }

        #endregion
    }
}
