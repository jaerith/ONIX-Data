using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Title;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixDescriptiveDetail
    {
        public OnixDescriptiveDetail()
        {
            ProductComposition = AudienceCode = -1;

            ProductForm            = "";
            ProductFormDescription = "";

            productContentTypeField = shortProductContentTypeField = new string[0];
            editionTypeField        = shortEditionTypeField = new string[0];

            EditionNumber = -1;
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
        private int      editionNumberField;
        private int      audienceCodeField;

        private string[] productContentTypeField;
        private string[] shortProductContentTypeField;
        private string[] editionTypeField;
        private string[] shortEditionTypeField;

        private OnixTitleDetail     titleDetailField;

        private OnixAudienceRange[] audienceRangeField;
        private OnixAudienceRange[] shortAudienceRangeField;
        private OnixCollection[]    collectionField;
        private OnixCollection[]    shortCollectionField;
        private OnixContributor[]   contributorField;
        private OnixContributor[]   shortContributorField;
        private OnixExtent[]        extentField;
        private OnixExtent[]        shortExtentField;
        private OnixMeasure[]       measureField;
        private OnixMeasure[]       shortMeasureField;
        private OnixSubject[]       subjectField;
        private OnixSubject[]       shortSubjectField;

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

        #region Reference Tags

        /// <remarks/>
        public int ProductComposition
        {
            get
            {
                return this.productCompositionField;
            }
            set
            {
                this.productCompositionField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EditionType")]
        public string[] EditionType
        {
            get
            {
                return this.editionTypeField;
            }
            set
            {
                this.editionTypeField = value;
            }
        }

        /// <remarks/>
        public int EditionNumber
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
        [System.Xml.Serialization.XmlElementAttribute("Measure")]
        public OnixMeasure[] Measure
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
        [System.Xml.Serialization.XmlElementAttribute("Collection")]
        public OnixCollection[] Collection
        {
            get
            {
                return this.collectionField;
            }
            set
            {
                this.collectionField = value;
            }
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
            get
            {
                return this.titleDetailField;
            }
            set
            {
                this.titleDetailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Contributor")]
        public OnixContributor[] Contributor
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
        [System.Xml.Serialization.XmlElementAttribute("Extent")]
        public OnixExtent[] Extent
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
        [System.Xml.Serialization.XmlElementAttribute("Subject")]
        public OnixSubject[] Subject
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
        public int AudienceCode
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
        [System.Xml.Serialization.XmlElementAttribute("AudienceRange")]
        public OnixAudienceRange[] AudienceRange
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
        [System.Xml.Serialization.XmlElementAttribute("x419")]
        public string[] x419
        {
            get { return shortEditionTypeField; }
            set { shortEditionTypeField = value; }
        }

        public int b057
        {
            get { return EditionNumber; }
            set { EditionNumber = value; }
        }

        /// <remarks/>
        public int b073
        {
            get { return AudienceCode; }
            set { AudienceCode = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("audiencerange")]
        public OnixAudienceRange[] audiencerange
        {
            get { return shortAudienceRangeField; }
            set { shortAudienceRangeField = value; }
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
        [System.Xml.Serialization.XmlElementAttribute("extent")]
        public OnixExtent[] extent
        {
            get { return shortExtentField; }
            set { shortExtentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("measure")]
        public OnixMeasure[] measure
        {
            get { return shortMeasureField; }
            set { shortMeasureField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("subject")]
        public OnixSubject[] subject
        {
            get { return shortSubjectField; }
            set { shortSubjectField = value; }
        }

        #endregion
    }
}
