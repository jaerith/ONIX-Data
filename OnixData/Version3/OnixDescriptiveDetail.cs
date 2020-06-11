using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Audience;
using OnixData.Version3.Epub;
using OnixData.Version3.Language;
using OnixData.Version3.ProductPart;
using OnixData.Version3.Title;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixDescriptiveDetail
    {
        public OnixDescriptiveDetail()
        {
            ProductComposition = -1;

            ProductForm            = ProductPackaging = AudienceCode = "";
            ProductFormDescription = CountryOfManufacture = "";
            EpubType               = EpubTypeVersion = EpubFormatDescription = EpubTypeNote = "";
            IllustrationsNote      = NumberOfIllustrations = "";

            productContentTypeField = shortProductContentTypeField = new string[0];
            editionTypeField        = shortEditionTypeField        = new string[0];
            productFormDetailField  = shortProductFormDetailField  = new string[0];
            epubTechProtectionField = shortEpubTechProtectionField = new string[0];
            audienceField           = shortAudienceField           = new OnixAudience[0];
            languageField           = shortLanguageField           = new OnixLanguage[0];
            prodPartField           = shortProdPartField           = new OnixProductPart[0];

            epubUsageConstraintField = shortEpubUsageConstraintField = new OnixEpubUsageConstraint[0];

            EditionNumber    = -1;
            EditionStatement = "";

            Measure       = new OnixMeasure[0];
            Collection    = new OnixCollection[0];
            TitleDetail   = new OnixTitleDetail();
            Contributor   = new OnixContributor[0];
            Extent        = new OnixExtent[0];
            Subject       = new OnixSubject[0];
            AudienceRange = new OnixAudienceRange[0];
        }

        private int      productCompositionField;
        private string   productFormField;
        private string   productFormDescriptionField;
        private string   productPackagingField;
        private int      editionNumberField;
        private string   editionStatementField;
        private string   audienceCodeField;
        private string   countryOfManufactureField;

        protected string epubTypeField;
        protected string epubTypeVersionField;
        protected string epubFormatDescriptionField;
        protected string epubTypeNoteField;

        protected string numOfIllustrationsField;
        protected string illustrationsNoteField;


        private string[]   productContentTypeField;
        private string[]   shortProductContentTypeField;
        private string[]   editionTypeField;
        private string[]   shortEditionTypeField;
        protected string[] productFormDetailField;
        protected string[] shortProductFormDetailField;
        protected string[] epubTechProtectionField;
        protected string[] shortEpubTechProtectionField;

        private OnixTitleDetail     titleDetailField;

        private OnixAudience[]      audienceField;
        private OnixAudience[]      shortAudienceField;
        private OnixAudienceRange[] audienceRangeField;
        private OnixAudienceRange[] shortAudienceRangeField;
        private OnixCollection[]    collectionField;
        private OnixCollection[]    shortCollectionField;
        private OnixContributor[]   contributorField;
        private OnixContributor[]   shortContributorField;
        private OnixExtent[]        extentField;
        private OnixExtent[]        shortExtentField;
        private OnixLanguage[]      languageField;
        private OnixLanguage[]      shortLanguageField;
        private OnixMeasure[]       measureField;
        private OnixMeasure[]       shortMeasureField;
        private OnixProductPart[]   prodPartField;
        private OnixProductPart[]   shortProdPartField;
        private OnixSubject[]       subjectField;
        private OnixSubject[]       shortSubjectField;

        public OnixEpubUsageConstraint[] epubUsageConstraintField;
        public OnixEpubUsageConstraint[] shortEpubUsageConstraintField;

        #region ONIX Lists

        public string[] OnixProductContentTypeList
        {
            get
            {
                string[] ProductContentTypes = null;

                if (this.productContentTypeField != null)
                    ProductContentTypes = this.productContentTypeField;
                else if (this.shortProductContentTypeField != null)
                    ProductContentTypes = this.shortProductContentTypeField;
                else
                    ProductContentTypes = new string[0];

                return ProductContentTypes;
            }
        }

        public string[] OnixEditionTypeList
        {
            get
            {
                string[] EditionTypes = null;

                if (this.editionTypeField != null)
                    EditionTypes = this.editionTypeField;
                else if (this.shortEditionTypeField != null)
                    EditionTypes = this.shortEditionTypeField;
                else
                    EditionTypes = new string[0];

                return EditionTypes;
            }
        }

        public string[] OnixEpubTechProtectionList
        {
            get
            {
                string[] TechProtections = null;

                if (this.epubTechProtectionField != null)
                    TechProtections = this.epubTechProtectionField;
                else if (this.shortEpubTechProtectionField != null)
                    TechProtections = this.shortEpubTechProtectionField;
                else
                    TechProtections = new string[0];

                return TechProtections;
            }
        }

        public string[] OnixAudCodeList
        {
            get
            {
                string[] AudCodes = null;

                OnixAudience[] AudienceList = audienceField;
                if ((AudienceList == null) || (AudienceList.Length <= 0))
                    AudienceList = shortAudienceField;

                if ((AudienceList != null) && (AudienceList.Length > 0))
                {
                    OnixAudience[] OnixAudCodeList =
                        AudienceList.Where(x => x.AudienceCodeType == OnixAudience.CONST_AUD_TYPE_ONIX).ToArray();

                    if ((OnixAudCodeList != null) && (OnixAudCodeList.Length > 0))
                    {
                        AudCodes = new string[OnixAudCodeList.Length];

                        for (int i = 0; i < OnixAudCodeList.Length; ++i)
                        {
                            OnixAudience TmpAud = OnixAudCodeList[i];

                            AudCodes[i] = (!String.IsNullOrEmpty(TmpAud.AudienceCodeValue)) ? TmpAud.AudienceCodeValue : "";
                        }
                    }
                }
                else
                    AudCodes = new string[0];

                return AudCodes;
            }
        }

        public OnixAudienceRange[] OnixAudRangeList
        {
            get
            {
                OnixAudienceRange[] AudRanges = null;

                if (this.audienceRangeField != null)
                    AudRanges = this.audienceRangeField;
                else if (this.shortAudienceRangeField != null)
                    AudRanges = this.shortAudienceRangeField;
                else
                    AudRanges = new OnixAudienceRange[0];

                return AudRanges;
            }
        }

        public OnixCollection[] OnixCollectionList
        {
            get
            {
                OnixCollection[] Collections = null;

                if (this.collectionField != null)
                    Collections = this.collectionField;
                else if (this.shortCollectionField != null)
                    Collections = this.shortCollectionField;
                else
                    Collections = new OnixCollection[0];

                return Collections;
            }
        }

        public OnixContributor[] OnixContributorList
        {
            get
            {
                OnixContributor[] Contributors = null;

                if (this.contributorField != null)
                    Contributors = this.contributorField;
                else if (this.shortContributorField != null)
                    Contributors = this.shortContributorField;
                else
                    Contributors = new OnixContributor[0];

                return Contributors;
            }
        }

        public OnixEpubUsageConstraint[] OnixEpubUsageConstraintList
        {
            get
            {
                OnixEpubUsageConstraint[] Constraints = null;

                if (this.epubUsageConstraintField != null)
                    Constraints = this.epubUsageConstraintField;
                else if (this.shortEpubUsageConstraintField != null)
                    Constraints = this.shortEpubUsageConstraintField;
                else
                    Constraints = new OnixEpubUsageConstraint[0];

                return Constraints;
            }
        }

        public OnixExtent[] OnixExtentList
        {
            get
            {
                OnixExtent[] Extents = null;

                if (this.extentField != null)
                    Extents = this.extentField;
                else if (this.shortExtentField != null)
                    Extents = this.shortExtentField;
                else
                    Extents = new OnixExtent[0];

                return Extents;
            }
        }

        public OnixLanguage[] OnixLanguageList
        {
            get
            {
                OnixLanguage[] Languages = null;

                if (this.languageField != null)
                    Languages = this.languageField;
                else if (this.shortLanguageField != null)
                    Languages = this.shortLanguageField;
                else
                    Languages = new OnixLanguage[0];

                return Languages;
            }
        }

        public OnixSubject[] OnixMainSubjectList
        {
            get
            {
                OnixSubject[] MainSubjects = new OnixSubject[0];

                if ((OnixSubjectList != null) && (OnixSubjectList.Length > 0))
                {
                    var MainSubjList =
                        OnixSubjectList.Where(x => x.IsMainSubject()).ToList();

                    if (MainSubjList != null)
                        MainSubjects = MainSubjList.ToArray();
                }

                return MainSubjects;
            }
        }

        public OnixMeasure[] OnixMeasureList
        {
            get
            {
                OnixMeasure[] Measures = null;

                if (this.measureField != null)
                    Measures = this.measureField;
                else if (this.shortMeasureField != null)
                    Measures = this.shortMeasureField;
                else
                    Measures = new OnixMeasure[0];

                return Measures;
            }
        }

        public string[] OnixProductFormDetailList
        {
            get
            {
                string[] ProductFormDetailList = null;

                if (this.productFormDetailField != null)
                    ProductFormDetailList = this.productFormDetailField;
                else if (this.shortProductFormDetailField != null)
                    ProductFormDetailList = this.shortProductFormDetailField;
                else
                    ProductFormDetailList = new string[0];

                return ProductFormDetailList;
            }
        }

        public OnixProductPart[] OnixProductPartList
        {
            get
            {
                OnixProductPart[] ProductParts = null;

                if (this.prodPartField != null)
                    ProductParts = this.prodPartField;
                else if (this.shortProdPartField != null)
                    ProductParts = this.shortProdPartField;
                else
                    ProductParts = new OnixProductPart[0];

                return ProductParts;
            }
        }

        public OnixSubject[] OnixSubjectList
        {
            get
            {
                OnixSubject[] Subjects = null;

                if (this.subjectField != null)
                    Subjects = this.subjectField;
                else if (this.shortSubjectField != null)
                    Subjects = this.shortSubjectField;
                else
                    Subjects = new OnixSubject[0];

                return Subjects;
            }
        }

        #endregion

        #region Helper Methods

        public string AudienceAgeFrom
        {
            get
            {
                string sAudAgeFrom = "";

                OnixAudienceRange[] AudRangeList = this.OnixAudRangeList;
                if ((AudRangeList == null) || (AudRangeList.Length <= 0))
                    AudRangeList = shortAudienceRangeField;

                if ((AudRangeList != null) && (AudRangeList.Length > 0))
                {
                    foreach (OnixAudienceRange TempAudRange in AudRangeList)
                    {
                        if (!String.IsNullOrEmpty(TempAudRange.USAgeFrom))
                            sAudAgeFrom = TempAudRange.USAgeFrom;
                    }
                }

                if (String.IsNullOrEmpty(sAudAgeFrom))
                    sAudAgeFrom = TranslateAudienceGradeToAge(AudienceGradeFrom);

                return sAudAgeFrom;
            }
        }

        public string AudienceAgeTo
        {
            get
            {
                string sAudAgeTo = "";

                OnixAudienceRange[] AudRangeList = this.OnixAudRangeList;
                if ((AudRangeList == null) || (AudRangeList.Length <= 0))
                    AudRangeList = shortAudienceRangeField;

                if ((AudRangeList != null) && (AudRangeList.Length > 0))
                {
                    foreach (OnixAudienceRange TempAudRange in AudRangeList)
                    {
                        if (!String.IsNullOrEmpty(TempAudRange.USAgeTo))
                            sAudAgeTo = TempAudRange.USAgeTo;
                    }
                }

                if (String.IsNullOrEmpty(sAudAgeTo))
                    sAudAgeTo = TranslateAudienceGradeToAge(AudienceGradeTo);

                return sAudAgeTo;
            }
        }

        public string AudienceGradeFrom
        {
            get
            {
                string sAudGradeFrom = "";

                OnixAudienceRange[] AudRangeList = this.OnixAudRangeList;
                if ((AudRangeList == null) || (AudRangeList.Length <= 0))
                    AudRangeList = shortAudienceRangeField;

                if ((AudRangeList != null) && (AudRangeList.Length > 0))
                {
                    foreach (OnixAudienceRange TempAudRange in AudRangeList)
                    {
                        if (!String.IsNullOrEmpty(TempAudRange.USGradeFrom))
                            sAudGradeFrom = TempAudRange.USGradeFrom;
                        else if (!String.IsNullOrEmpty(TempAudRange.USGradeExact))
                            sAudGradeFrom = TempAudRange.USGradeExact;
                    }
                }

                return sAudGradeFrom;
            }
        }

        public string AudienceGradeTo
        {
            get
            {
                string sAudGradeTo = "";

                OnixAudienceRange[] AudRangeList = this.OnixAudRangeList;
                if ((AudRangeList == null) || (AudRangeList.Length <= 0))
                    AudRangeList = shortAudienceRangeField;

                if ((AudRangeList != null) && (AudRangeList.Length > 0))
                {
                    foreach (OnixAudienceRange TempAudRange in AudRangeList)
                    {
                        if (!String.IsNullOrEmpty(TempAudRange.USGradeTo))
                            sAudGradeTo = TempAudRange.USGradeTo;
                        else if (!String.IsNullOrEmpty(TempAudRange.USGradeExact))
                            sAudGradeTo = TempAudRange.USGradeExact;
                    }
                }

                return sAudGradeTo;
            }
        }

        public string LanguageOfText
        {
            get
            {
                OnixLanguage[] LangList = OnixLanguageList;

                string sLangOfText = "";

                if ((LangList != null) && (LangList.Length > 0))
                {
                    OnixLanguage LangOfText =
                        LangList.Where(x => x.IsLanguageOfText()).LastOrDefault();

                    if ((LangOfText != null) && !String.IsNullOrEmpty(LangOfText.LanguageCode))
                        sLangOfText = LangOfText.LanguageCode;
                }

                return sLangOfText;
            }
        }

        public int PageNumber
        {
            get
            {
                int nPageNum = 0;

                if (this.OnixExtentList != null)
                {
                    OnixExtent PageAmtExtent =
                        this.OnixExtentList
                            .Where(x => x.ExtentType == OnixExtent.CONST_EXT_TYPE_TOTAL_PG_CT)
                            .FirstOrDefault();

                    if ((PageAmtExtent != null) && (PageAmtExtent.ExtentValueNum > 0))
                        nPageNum = PageAmtExtent.ExtentValueNum;
                    else
                    {
                        PageAmtExtent =
                            this.OnixExtentList
                                .Where(x => x.ExtentType == OnixExtent.CONST_EXT_TYPE_MAIN_PPG_CNT)
                                .FirstOrDefault();

                        if ((PageAmtExtent != null) && (PageAmtExtent.ExtentValueNum > 0))
                            nPageNum = PageAmtExtent.ExtentValueNum;
                    }
                }

                return nPageNum;
            }
        }

        public string SeriesNumber
        {
            get
            {
                OnixCollection[] CollList = this.OnixCollectionList;

                string sSeriesNum = "";

                if ((CollList != null) && (CollList.Length > 0))
                {
                    OnixCollection SeriesCollection =
                        CollList.Where(x => x.IsSeriesType()).LastOrDefault();

                    if (SeriesCollection != null)
                    {
                        sSeriesNum = SeriesCollection.TitleSequence;

                        if (String.IsNullOrEmpty(sSeriesNum))
                        {
                            var CollSeqList = SeriesCollection.OnixCollectionSequenceList;

                            var CollSeq = CollSeqList.Where(x => x.IsSeriesSeq()).LastOrDefault();

                            if ((CollSeq != null) && (CollSeq.CollectionSequenceNum > 0))
                                sSeriesNum = CollSeq.CollectionSequence;
                        }
                    }
                }

                return sSeriesNum;
            }
        }

        public string SeriesTitle
        {
            get
            {
                OnixCollection[] CollList = this.OnixCollectionList;

                string sSeriesTitle = "";

                if ((CollList != null) && (CollList.Length > 0))
                {
                    OnixCollection SeriesCollection =
                        CollList.Where(x => x.IsSeriesType()).FirstOrDefault();

                    if (SeriesCollection != null)
                        sSeriesTitle = SeriesCollection.CollectionName;
                }

                return sSeriesTitle;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public int ProductComposition
        {
            get { return this.productCompositionField; }
            set { this.productCompositionField = value; }
        }

        /// <remarks/>
        public string ProductForm
        {
            get { return this.productFormField; }
            set { this.productFormField = value; }
        }

        /// <remarks/>
        public string ProductFormDescription
        {
            get { return this.productFormDescriptionField; }
            set { this.productFormDescriptionField = value; }
        }

        [System.Xml.Serialization.XmlElementAttribute("ProductFormDetail")]
        public string[] ProductFormDetail
        {
            get { return this.productFormDetailField; }
            set { this.productFormDetailField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductContentType")]
        public string[] ProductContentType
        {
            get { return this.productContentTypeField; }
            set { this.productContentTypeField = value; }
        }

        /// <remarks/>
        public string ProductPackaging
        {
            get { return this.productPackagingField; }
            set { this.productPackagingField = value; }
        }

        /// <remarks/>
        public string AudienceCode
        {
            get { return this.audienceCodeField; }
            set { this.audienceCodeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Audience")]
        public OnixAudience[] Audience
        {
            get { return this.audienceField; }
            set { this.audienceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AudienceRange")]
        public OnixAudienceRange[] AudienceRange
        {
            get { return this.audienceRangeField; }
            set { this.audienceRangeField = value; }
        }

        public string CountryOfManufacture
        {
            get { return this.countryOfManufactureField; }
            set { this.countryOfManufactureField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EditionType")]
        public string[] EditionType
        {
            get { return this.editionTypeField; }
            set { this.editionTypeField = value; }
        }

        /// <remarks/>
        public int EditionNumber
        {
            get { return this.editionNumberField; }
            set { this.editionNumberField = value; }
        }

        /// <remarks/>
        public string EditionStatement
        {
            get { return this.editionStatementField; }
            set { this.editionStatementField = value; }
        }

        public string EpubType
        {
            get { return this.epubTypeField; }
            set { this.epubTypeField = value; }
        }

        public string EpubTypeVersion
        {
            get { return this.epubTypeVersionField; }
            set { this.epubTypeVersionField = value; }
        }

        public string EpubFormatDescription
        {
            get { return this.epubFormatDescriptionField; }
            set { this.epubFormatDescriptionField = value; }
        }

        public string EpubTypeNote
        {
            get { return this.epubTypeNoteField; }
            set { this.epubTypeNoteField = value; }
        }

        [System.Xml.Serialization.XmlElementAttribute("EpubTechnicalProtection")]
        public string[] EpubTechnicalProtection
        {
            get { return this.epubTechProtectionField; }
            set { this.epubTechProtectionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Collection")]
        public OnixCollection[] Collection
        {
            get { return this.collectionField; }
            set { this.collectionField = value; }
        }

        public string IllustrationsNote
        {
            get { return this.illustrationsNoteField; }
            set { this.illustrationsNoteField = value; }
        }

        public string NumberOfIllustrations
        {
            get { return this.numOfIllustrationsField; }
            set { this.numOfIllustrationsField = value; }
        }

        /*
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
        */

        /// <remarks/>
        public OnixTitleDetail TitleDetail
        {
            get { return this.titleDetailField; }
            set { this.titleDetailField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Contributor")]
        public OnixContributor[] Contributor
        {
            get { return this.contributorField; }
            set { this.contributorField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EpubUsageConstraint")]
        public OnixEpubUsageConstraint[] EpubUsageConstraint
        {
            get { return this.epubUsageConstraintField; }
            set { this.epubUsageConstraintField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Extent")]
        public OnixExtent[] Extent
        {
            get { return this.extentField; }
            set { this.extentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Language")]
        public OnixLanguage[] Language
        {
            get { return this.languageField; }
            set { this.languageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Measure")]
        public OnixMeasure[] Measure
        {
            get { return this.measureField; }
            set { this.measureField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductPart")]
        public OnixProductPart[] ProductPart
        {
            get { return this.prodPartField; }
            set { this.prodPartField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Subject")]
        public OnixSubject[] Subject
        {
            get { return this.subjectField; }
            set { this.subjectField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x314
        {
            get { return ProductComposition; }
            set { ProductComposition = value; }
        }

        /// <remarks/>
        public string b012
        {
            get { return ProductForm; }
            set { ProductForm = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b333")]
        public string[] b333
        {
            get { return shortProductFormDetailField; }
            set { shortProductFormDetailField = value; }
        }

        /// <remarks/>
        public string b014
        {
            get { return ProductFormDescription; }
            set { ProductFormDescription = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b385")]
        public string[] b385
        {
            get { return shortProductContentTypeField; }
            set { shortProductContentTypeField = value; }
        }

        /// <remarks/>
        public string b225
        {
            get { return ProductPackaging; }
            set { ProductPackaging = value; }
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

        public string b277
        {
            get { return EpubTypeNote; }
            set { EpubTypeNote = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x419")]
        public string[] x419
        {
            get { return shortEditionTypeField; }
            set { shortEditionTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x317")]
        public string[] x317
        {
            get { return shortEpubTechProtectionField; }
            set { shortEpubTechProtectionField = value; }
        }

        public int b057
        {
            get { return EditionNumber; }
            set { EditionNumber = value; }
        }

        /// <remarks/>
        public string b058
        {
            get { return EditionStatement; }
            set { EditionStatement = value; }
        }

        /// <remarks/>
        public string b073
        {
            get { return AudienceCode; }
            set { AudienceCode = value; }
        }

        public string b062
        {
            get { return IllustrationsNote; }
            set { IllustrationsNote = value; }
        }

        public string b125
        {
            get { return NumberOfIllustrations; }
            set { NumberOfIllustrations = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("audience")]
        public OnixAudience[] audience
        {
            get { return this.shortAudienceField; }
            set { this.shortAudienceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("audiencerange")]
        public OnixAudienceRange[] audiencerange
        {
            get { return shortAudienceRangeField; }
            set { shortAudienceRangeField = value; }
        }

        public string x316
        {
            get { return this.CountryOfManufacture; }
            set { this.CountryOfManufacture = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("collection")]
        public OnixCollection[] collection
        {
            get { return shortCollectionField; }
            set { shortCollectionField = value; }
        }

        /// <remarks/>
        public OnixTitleDetail titledetail
        {
            get { return TitleDetail; }
            set { TitleDetail = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contributor")]
        public OnixContributor[] contributor
        {
            get { return shortContributorField; }
            set { shortContributorField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("epubusageconstraint")]
        public OnixEpubUsageConstraint[] epubusageconstraint
        {
            get { return this.shortEpubUsageConstraintField; }
            set { this.shortEpubUsageConstraintField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("extent")]
        public OnixExtent[] extent
        {
            get { return shortExtentField; }
            set { shortExtentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("language")]
        public OnixLanguage[] language
        {
            get { return shortLanguageField; }
            set { shortLanguageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("measure")]
        public OnixMeasure[] measure
        {
            get { return shortMeasureField; }
            set { shortMeasureField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("productpart")]
        public OnixProductPart[] productpart
        {
            get { return this.shortProdPartField; }
            set { this.shortProdPartField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("subject")]
        public OnixSubject[] subject
        {
            get { return shortSubjectField; }
            set { shortSubjectField = value; }
        }

        #endregion

        #region Support Methods

        public string TranslateAudienceGradeToAge(string psGradeValue)
        {
            int nAgeVal = 0;
            string sAgeVal = "";

            if (!String.IsNullOrEmpty(psGradeValue))
            {
                if ((psGradeValue == OnixAudienceRange.CONST_AUD_GRADE_PRESCHOOL_CD) || (psGradeValue == OnixAudienceRange.CONST_AUD_GRADE_PRESCHOOL_TXT))
                    nAgeVal = 4;
                else if ((psGradeValue == OnixAudienceRange.CONST_AUD_GRADE_KNDGRTN_CD) || (psGradeValue == OnixAudienceRange.CONST_AUD_GRADE_KNDGRTN_TXT))
                    nAgeVal = 5;
                else if (Int32.TryParse(psGradeValue, out nAgeVal))
                    nAgeVal += 5;
            }

            if (nAgeVal > 0)
                sAgeVal = Convert.ToString(nAgeVal);

            return sAgeVal;
        }

        #endregion 
    }
}
