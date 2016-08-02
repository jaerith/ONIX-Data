using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Related
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixRelatedProduct
    {
        public OnixRelatedProduct()
        {
            ProductRelationCode = -1;

            ProductIdentifier = new OnixProductId[0];
        }

        private int             productRelationCodeField;
        private OnixProductId[] productIdentifierField;

        /// <remarks/>
        public int ProductRelationCode
        {
            get
            {
                return this.productRelationCodeField;
            }
            set
            {
                this.productRelationCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductIdentifier")]
        public OnixProductId[] ProductIdentifier
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
    }
}
