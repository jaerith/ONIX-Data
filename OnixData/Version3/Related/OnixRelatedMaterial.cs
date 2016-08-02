using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Related
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixRelatedMaterial
    {
        public OnixRelatedMaterial()
        {
            relatedWorkField    = new OnixRelatedWork[0];
            relatedProductField = new OnixRelatedProduct[0];
        }

        private OnixRelatedWork[]    relatedWorkField;
        private OnixRelatedProduct[] relatedProductField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedWork")]
        public OnixRelatedWork[] RelatedWork
        {
            get
            {
                return this.relatedWorkField;
            }
            set
            {
                this.relatedWorkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedProduct")]
        public OnixRelatedProduct[] RelatedProduct
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

    }
}
