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

            ProductForm = "";

            Measure     = new OnixMeasure[0];
            Collection  = new OnixCollection[0];
            TitleDetail = new OnixTitleDetail();
            Contributor = new OnixContributor[0];
            Extent      = new OnixExtent[0];
            Subject     = new OnixSubject[0];
        }

        private int    productCompositionField;
        private string productFormField;
        private int    audienceCodeField;

        private OnixMeasure[]     measureField;
        private OnixCollection[]  collectionField;
        private OnixTitleDetail   titleDetailField;
        private OnixContributor[] contributorField;
        private OnixExtent[]      extentField;
        private OnixSubject[]     subjectField;

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
    }
}
