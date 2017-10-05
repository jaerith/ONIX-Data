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

        private const char CONST_KEYWORDS_DELIM = ';';

        #endregion

        public OnixLegacyProduct()
        {
            SalesRightsInUS   = SalesRightsInNonUSCountry = false;
            NoSalesRightsInUS = SalesRightsAllWorld = false;

            RecordReference  = "";
            NotificationType = NumberOfPieces = TradeCategory = -1;

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

            ContributorStatement = "";

            Language    = new OnixLegacyLanguage();

            ItemNumberWithinSet = NumberOfPages = -1;

            PagesRoman = PagesArabic = "";

            Illustrations = new OnixLegacyIllustrations();

            BASICMainSubject = "";
            MainSubject      = new OnixLegacySubject();
            AudienceCode     = "";

            titleField = shortTitleField = new OnixLegacyTitle[0];

            audienceField       = shortAudienceField       = new OnixLegacyAudience[0];
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

            notForSaleRightsField = shortNotForSaleRightsField = new OnixLegacyNotForSale[0];

            cityOfPublicationField = countryOfPublicationField  = publishingStatusField = "";
            announcementDateField  = tradeAnnouncementDateField = "";

            PublicationDate = YearFirstPublished = 0;

            usdPriceField = null;

            supplyDetailField = shortSupplyDetailField = new OnixLegacySupplyDetail[0];
            ParsingError = null;
        }

        private bool SalesRightsInUS;
        private bool SalesRightsInNonUSCountry;
        private bool NoSalesRightsInUS;
        private bool SalesRightsAllWorld;

        private string bookFormDetailField;
        private int    productPackagingField;
        private string distinctiveTitleField;

        private string[] editionTypeCodeField;
        private string[] shortEditionTypeCodeField;
        private string[] editionNumberField;
        private string[] shortEditionNumberField;
        private string   editionStatementField;

        private OnixLegacyTitle[] titleField;
        private OnixLegacyTitle[] shortTitleField;

        private string contributorStatementField;

        private OnixLegacyLanguage languageField;
        private OnixLegacySubject  mainSubjectField;

        private int itemNumberWithinSetField;
        private int numberOfPagesField;

        private string pagesRomanField;
        private string pagesArabicField;

        private OnixLegacyIllustrations illustrationsField;

        private string basicMainSubjectField;
        private string audienceCodeField;

        private OnixLegacyAudience[]       audienceField;
        private OnixLegacyAudience[]       shortAudienceField;
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
        private OnixLegacyNotForSale[]     notForSaleRightsField;
        private OnixLegacyNotForSale[]     shortNotForSaleRightsField;

        private string cityOfPublicationField;
        private string countryOfPublicationField;
        private string publishingStatusField;
        private string announcementDateField;
        private string tradeAnnouncementDateField;
        private uint   publicationDateField;
        private uint   yearFirstPublishedField;

        private OnixLegacySupplyDetail[] supplyDetailField;
        private OnixLegacySupplyDetail[] shortSupplyDetailField;

        private OnixLegacyPrice usdPriceField;

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

            if (OnixSupplyDetailList != null)
            {
                foreach (OnixLegacySupplyDetail TmpSupplyDetail in OnixSupplyDetailList)
                {
                    if ((TmpSupplyDetail.OnixPriceList != null) && (TmpSupplyDetail.OnixPriceList.Length > 0))
                    {
                        bHasFuturePrice =
                            TmpSupplyDetail.OnixPriceList.Any(x => !String.IsNullOrEmpty(x.PriceEffectiveFrom) && (x.PriceTypeCode == OnixLegacyPrice.CONST_PRICE_TYPE_RRP_EXCL));

                        if (bHasFuturePrice)
                            break;
                    }
                }
            }

            return bHasFuturePrice;
        }

        public bool HasUSDPrice()
        {
            bool bHasUSDPrice = false;

            if (OnixSupplyDetailList != null)
            {
                foreach (OnixLegacySupplyDetail TmpSupplyDetail in OnixSupplyDetailList)
                {
                    if ((TmpSupplyDetail.OnixPriceList != null) && (TmpSupplyDetail.OnixPriceList.Length > 0))
                    {
                        bHasUSDPrice =
                            TmpSupplyDetail.OnixPriceList.Any(x => x.HasSoughtPriceTypeCode() && (x.CurrencyCode == "USD"));

                        if (bHasUSDPrice)
                            break;
                    }
                }
            }

            return bHasUSDPrice;
        }

        public bool HasUSDRetailPrice()
        {
            bool bHasUSDPrice = false;

            if (OnixSupplyDetailList != null)
            {
                foreach (OnixLegacySupplyDetail TmpSupplyDetail in OnixSupplyDetailList)
                {
                    if ((TmpSupplyDetail.OnixPriceList != null) && (TmpSupplyDetail.OnixPriceList.Length > 0))
                    {
                        bHasUSDPrice =
                            TmpSupplyDetail.OnixPriceList.Any(x => x.HasSoughtRetailPriceType() && (x.CurrencyCode == "USD"));

                        if (bHasUSDPrice)
                            break;
                    }
                }
            }

            return bHasUSDPrice;
        }

        public bool HasOtherCountryRights()
        {
            SetRightsFlags();

            return SalesRightsInNonUSCountry;
        }

        public bool HasNoneUSRights()
        {
            SetRightsFlags();

            return NoSalesRightsInUS;
        }

        public bool HasUSRights()
        {
            SetRightsFlags();

            return SalesRightsInUS;
        }

        public bool HasWorldRights()
        {
            SetRightsFlags();

            return SalesRightsAllWorld;
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

        public string Keywords
        {
            get
            {
                string            FoundKeywords = "";
                OnixLegacySubject FoundSubject  = new OnixLegacySubject();

                OnixLegacySubject[] SubjectList = OnixSubjectList;
                if ((SubjectList != null) && (SubjectList.Length > 0))
                {
                    FoundSubject =
                        SubjectList.Where(x => x.SubjectSchemeIdentifier == OnixLegacySubject.CONST_SUBJ_SCHEME_KEYWORDS).FirstOrDefault();
                }

                if ((FoundSubject != null) && !String.IsNullOrEmpty(FoundSubject.SubjectHeadingText))
                    FoundKeywords = FoundSubject.SubjectHeadingText;

                return FoundKeywords;
            }
        }

        public string[] KeywordsList
        {
            get
            {
                string[] asKeywordsList = new string[0];

                if ((Keywords != null) && !String.IsNullOrEmpty(Keywords) && Keywords.Contains(CONST_KEYWORDS_DELIM))
                    asKeywordsList = Keywords.Split(CONST_KEYWORDS_DELIM);

                return asKeywordsList;
            }
        }
		
        public string OnixAudienceAgeFrom
        {
            get
            {
                string sAudAgeFrom = "";

                OnixLegacyAudRange[] AudRangeList = audienceRangeField;
                if ((AudRangeList == null) || (AudRangeList.Length <= 0))
                    AudRangeList = shortAudienceRangeField;

                if ((AudRangeList != null) && (AudRangeList.Length > 0))
                {
                    foreach (OnixLegacyAudRange TempAudRange in AudRangeList)
                    {
                        if (!String.IsNullOrEmpty(TempAudRange.USAgeFrom))
                            sAudAgeFrom = TempAudRange.USAgeFrom;
                    }
                }

                return sAudAgeFrom;
            }
        }

        public string OnixAudienceAgeTo
        {
            get
            {
                string sAudAgeTo = "";

                OnixLegacyAudRange[] AudRangeList = audienceRangeField;
                if ((AudRangeList == null) || (AudRangeList.Length <= 0))
                    AudRangeList = shortAudienceRangeField;

                if ((AudRangeList != null) && (AudRangeList.Length > 0))
                {
                    foreach (OnixLegacyAudRange TempAudRange in AudRangeList)
                    {
                        if (!String.IsNullOrEmpty(TempAudRange.USAgeTo))
                            sAudAgeTo = TempAudRange.USAgeTo;
                    }
                }

                return sAudAgeTo;
            }
        }
		
        public string OnixAudienceCode
        {
            get
            {
                string sAudCode = "";

                if (!String.IsNullOrEmpty(AudienceCode))
                    sAudCode = AudienceCode;
                else
                {
                    OnixLegacyAudience[] AudienceList = audienceField;
                    if ((AudienceList == null) || (AudienceList.Length <= 0))
                        AudienceList = shortAudienceField;

                    if ((AudienceList != null) && (AudienceList.Length > 0))
                    {
                        OnixLegacyAudience OnixAudCode = 
                            AudienceList.Where(x => x.AudienceCodeType == OnixLegacyAudience.CONST_AUD_TYPE_ONIX).FirstOrDefault();

                        if ((OnixAudCode != null) && !String.IsNullOrEmpty(OnixAudCode.AudienceCodeValue))
                            sAudCode = OnixAudCode.AudienceCodeValue;

                    }
                }

                return sAudCode;
            }
        }

        public string OnixTitle
        {
            get
            {
                StringBuilder TitleBuilder = new StringBuilder();

                if (!String.IsNullOrEmpty(this.DistinctiveTitle))
                    TitleBuilder.Append(this.DistinctiveTitle);
                else
                {
                    OnixLegacyTitle[] TitleList = titleField;
                    if ((TitleList == null) || (TitleList.Length <= 0))
                        TitleList = shortTitleField;

                    if ((TitleList != null) && (TitleList.Length > 0))
                    {
                        OnixLegacyTitle FoundTitle =
                            TitleList.Where(x => 
                                x.TitleType == OnixLegacyTitle.CONST_TITLE_TYPE_DIST_TITLE || x.TitleType == OnixLegacyTitle.CONST_TITLE_TYPE_UN_TITLE).FirstOrDefault();

                        if ((FoundTitle == null) || String.IsNullOrEmpty(FoundTitle.OnixTitle))
                            FoundTitle = TitleList.Where(x => (x.TitleType < 0)).FirstOrDefault();

                        if ((FoundTitle != null) && !String.IsNullOrEmpty(FoundTitle.OnixTitle))
                            TitleBuilder.Append(FoundTitle.OnixTitle);
                    }
                }

                return TitleBuilder.ToString();
            }
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

                    if (FoundImprint != null)
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

                if (OnixSupplyDetailList != null)
                {
                    foreach (OnixLegacySupplyDetail TmpSupplyDetail in OnixSupplyDetailList)
                    {
                        if ((TmpSupplyDetail.OnixSupplierIdList != null) && (TmpSupplyDetail.OnixSupplierIdList.Length > 0))
                        {
                            List<OnixLegacySupplierId> TmpPropSuppliers =
                                TmpSupplyDetail.OnixSupplierIdList.Where(x => x.SupplierIDType == OnixLegacySupplierId.CONST_SUPPL_ID_TYPE_PROP).ToList();

                            PropSuppliers.AddRange(TmpPropSuppliers);
                        }
                    }
                }

                return PropSuppliers;
            }
        }

        public string SeriesNumber
        {
            get
            {
                string FoundSeriesNum = "";

                OnixLegacySeries[] SeriesList = OnixSeriesList;
                if ((SeriesList != null) && (SeriesList.Length > 0))
                {
                    OnixLegacySeries FoundSeries = SeriesList.Where(x => !String.IsNullOrEmpty(x.NumberWithinSeries)).FirstOrDefault();
                    if (FoundSeries != null)
                        FoundSeriesNum = FoundSeries.NumberWithinSeries;
                }

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

        public OnixLegacyPrice USDValidPrice
        {
            get
            {
                if (usdPriceField != null)
                    return usdPriceField;

                OnixLegacyPrice USDPrice = USDRetailPrice;

                if ((USDRetailPrice == null) || (USDRetailPrice.PriceAmount == 0))
                {
                    if (OnixSupplyDetailList != null)
                    {
                        foreach (OnixLegacySupplyDetail TmpSupplyDetail in OnixSupplyDetailList)
                        {
                            if ((TmpSupplyDetail.OnixPriceList != null) && (TmpSupplyDetail.OnixPriceList.Length > 0))
                            {
                                OnixLegacyPrice[] Prices = TmpSupplyDetail.OnixPriceList;

                                USDPrice =
                                    Prices.Where(x => x.HasSoughtPriceTypeCode() && (x.CurrencyCode == "USD")).FirstOrDefault();

                                if ((USDPrice != null) && (USDPrice.PriceAmount > 0))
                                    break;
                            }
                        }
                    }
                }

                usdPriceField = USDPrice;

                return USDPrice;
            }
        }

        public OnixLegacyPrice USDRetailPrice
        {
            get
            {
                OnixLegacyPrice USDPrice = new OnixLegacyPrice();

                if (OnixSupplyDetailList != null)
                {
                    foreach (OnixLegacySupplyDetail TmpSupplyDetail in OnixSupplyDetailList)
                    {
                        if ((TmpSupplyDetail.OnixPriceList != null) && (TmpSupplyDetail.OnixPriceList.Length > 0))
                        {
                            OnixLegacyPrice[] Prices = TmpSupplyDetail.OnixPriceList;

                            USDPrice =
                                Prices.Where(x => x.HasSoughtRetailPriceType() && (x.CurrencyCode == "USD")).FirstOrDefault();

                            if ((USDPrice != null) && (USDPrice.PriceAmount > 0))
                                break;
                        }
                    }
                }

                return USDPrice;
            }
        }

        public OnixLegacySupplyDetail USDSupplyDetail
        {
            get
            {
                bool bFoundDetails = false;

                OnixLegacySupplyDetail FoundSupplyDetail = new OnixLegacySupplyDetail();

                if (OnixSupplyDetailList != null)
                {
                    foreach (OnixLegacySupplyDetail TmpSupplyDetail in OnixSupplyDetailList)
                    {
                        if ((TmpSupplyDetail.OnixPriceList != null) && (TmpSupplyDetail.OnixPriceList.Length > 0))
                        {
                            OnixLegacyPrice[] Prices = TmpSupplyDetail.OnixPriceList;

                            OnixLegacyPrice USDPrice =
                                Prices.Where(x => x.HasSoughtRetailPriceType() && (x.CurrencyCode == "USD")).FirstOrDefault();

                            if ((USDPrice != null) && (USDPrice.PriceAmount > 0))
                            {
                                bFoundDetails     = true;
                                FoundSupplyDetail = TmpSupplyDetail;
                                break;
                            }
                        }
                    }
                }

                // If we can't find one that mentions a USD price, we'll just accept the first one
                if (!bFoundDetails)
                {
                    if ((OnixSupplyDetailList != null) && (OnixSupplyDetailList.Length > 0))
                        FoundSupplyDetail = OnixSupplyDetailList[0];
                }

                return FoundSupplyDetail;
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

        public string[] OnixEditionNumberList
        {
            get
            {
                string[] EditionNumbers = null;

                if (editionNumberField != null)
                    EditionNumbers = this.editionNumberField;
                else if (shortEditionNumberField != null)
                    EditionNumbers = this.shortEditionNumberField;
                else
                    EditionNumbers = new string[0];

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

        public OnixLegacyNotForSale[] OnixNotForSaleRightsList
        {
            get
            {
                OnixLegacyNotForSale[] NotForSaleRights = null;

                if (this.notForSaleRightsField != null)
                    NotForSaleRights = this.notForSaleRightsField;
                else if (this.shortNotForSaleRightsField != null)
                    NotForSaleRights = this.shortNotForSaleRightsField;
                else
                    NotForSaleRights = new OnixLegacyNotForSale[0];

                return NotForSaleRights;
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

        public OnixLegacySupplyDetail[] OnixSupplyDetailList
        {
            get
            {
                OnixLegacySupplyDetail[] SupplyDetails = null;

                if (this.supplyDetailField != null)
                    SupplyDetails = this.supplyDetailField;
                else if (this.shortSupplyDetailField != null)
                    SupplyDetails = this.shortSupplyDetailField;
                else
                    SupplyDetails = new OnixLegacySupplyDetail[0];

                return SupplyDetails;
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
        public string[] EditionNumber
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
        public string EditionStatement
        {
            get
            {
                return this.editionStatementField;
            }
            set
            {
                this.editionStatementField = value;
            }
        }

        /// <remarks/>
        public string DistinctiveTitle
        {
            get
            {
                return this.distinctiveTitleField;
            }
            set
            {
                this.distinctiveTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Title")]
        public OnixLegacyTitle[] Title
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
        public string PagesRoman
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
        public string PagesArabic
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
        public OnixLegacySubject MainSubject
        {
            get
            {
                return this.mainSubjectField;
            }
            set
            {
                this.mainSubjectField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("Audience")]
        public OnixLegacyAudience[] Audience
        {
            get
            {
                return this.audienceField;
            }
            set
            {
                this.audienceField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("NotForSale")]
        public OnixLegacyNotForSale[] NotForSale
        {
            get
            {
                return this.notForSaleRightsField;
            }
            set
            {
                this.notForSaleRightsField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("SupplyDetail")]
        public OnixLegacySupplyDetail[] SupplyDetail
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
        public string[] b057
        {
            get { return shortEditionNumberField; }
            set { shortEditionNumberField = value; }
        }

        /// <remarks/>
        public string b058
        {
            get { return EditionStatement; }
            set { EditionStatement = value; }
        }

        // EditionStatement

        /// <remarks/>
        public string b028
        {
            get { return DistinctiveTitle; }
            set { DistinctiveTitle = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("title")]
        public OnixLegacyTitle[] title
        {
            get { return shortTitleField; }
            set { shortTitleField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("audience")]
        public OnixLegacyAudience[] audience
        {
            get { return shortAudienceField; }
            set { shortAudienceField = value; }
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
        public string b254
        {
            get { return PagesRoman; }
            set { PagesRoman = value; }
        }

        /// <remarks/>
        public string b255
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
        public OnixLegacySubject mainsubject
        {
            get { return this.mainSubjectField; }
            set { this.mainSubjectField = value; }
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
        [System.Xml.Serialization.XmlElementAttribute("notforsale")]
        public OnixLegacyNotForSale[] notforsale
        {
            get { return shortNotForSaleRightsField; }
            set { shortNotForSaleRightsField = value; }
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
        [System.Xml.Serialization.XmlElementAttribute("supplydetail")]
        public OnixLegacySupplyDetail[] supplydetail
        {
            get { return shortSupplyDetailField; }
            set { shortSupplyDetailField = value; }
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

                if (FoundMeasurement == null)
                    FoundMeasurement = new OnixLegacyMeasure();
            }

            return FoundMeasurement;
        }

        public void SetRightsFlags()
        {
            int[] aSalesRightsColl = new int[] { OnixLegacySalesRights.CONST_SR_TYPE_FOR_SALE_WITH_EXCL_RIGHTS,
                                                 OnixLegacySalesRights.CONST_SR_TYPE_FOR_SALE_WITH_NONEXCL_RIGHTS };

            int[] aNonSalesRightsColl = new int[] { OnixLegacySalesRights.CONST_SR_TYPE_NOT_FOR_SALE };

            OnixLegacySalesRights[] SalesRightsList      = OnixSalesRightsList;
            OnixLegacyNotForSale[]  NotForSaleRightsList = OnixNotForSaleRightsList;

            if ((SalesRightsList != null) && (SalesRightsList.Length > 0))
            {
                SalesRightsInUS =
                    SalesRightsList.Any(x => aSalesRightsColl.Contains(x.SalesRightsType) && (x.RightsCountryList.Contains("US")));

                SalesRightsInNonUSCountry =
                    SalesRightsList.Any(x => aSalesRightsColl.Contains(x.SalesRightsType) &&
                                             !x.RightsCountryList.Contains("US")          &&
                                             !x.RightsTerritoryList.Contains("WORLD")     &&
                                             !x.RightsTerritoryList.Contains("ROW"));

                NoSalesRightsInUS =
                    SalesRightsList.Any(x => aNonSalesRightsColl.Contains(x.SalesRightsType) && x.RightsCountryList.Contains("US"));

                SalesRightsAllWorld =
                    SalesRightsList.Any(x => aSalesRightsColl.Contains(x.SalesRightsType) && 
                                             (x.RightsTerritoryList.Contains("WORLD") || x.RightsTerritoryList.Contains("ROW")));
            }

            if ((NotForSaleRightsList != null) && (NotForSaleRightsList.Length > 0))
            {
                if (!NoSalesRightsInUS)
                {
                    NoSalesRightsInUS =
                        NotForSaleRightsList.Any(x => x.RightsCountryList.Contains("US"));
                }
            }
        }

        #endregion
    }
}
