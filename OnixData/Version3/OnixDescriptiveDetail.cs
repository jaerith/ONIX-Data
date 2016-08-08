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

            ProductContentType = new string[0];
            EditionType        = new string[0];
            EditionNumber      = -1;

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
        private string[] productContentTypeField;
        private string[] editionTypeField;
        private int      editionNumberField;
        private int      audienceCodeField;

        private OnixMeasure[]       measureField;
        private OnixCollection[]    collectionField;
        private OnixTitleDetail     titleDetailField;
        private OnixContributor[]   contributorField;
        private OnixExtent[]        extentField;
        private OnixSubject[]       subjectField;
        private OnixAudienceRange[] audienceRangeField;

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
            get { return ProductContentType; }
            set { ProductContentType = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x419")]
        public string[] x419
        {
            get { return EditionType; }
            set { EditionType = value; }
        }

        public int b057
        {
            get { return EditionNumber; }
            set { EditionNumber = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("measure")]
        public OnixMeasure[] measure
        {
            get { return Measure; }
            set { Measure = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("collection")]
        public OnixCollection[] collection
        {
            get { return Collection; }
            set { Collection = value; }
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
            get { return Contributor; }
            set { Contributor = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("extent")]
        public OnixExtent[] extent
        {
            get { return Extent; }
            set { Extent = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("subject")]
        public OnixSubject[] subject
        {
            get { return Subject; }
            set { Subject = value; }
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
            get { return AudienceRange; }
            set { AudienceRange = value; }
        } 

        #endregion
    }
}
