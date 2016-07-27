using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyRelatedProduct
    {
        public OnixLegacyRelatedProduct()
        {
            RelationCode      = -1;
            ProductIdentifier = new OnixLegacyProductId();
        }

        private int relationCodeField;
        private OnixLegacyProductId productIdentifierField;

        /// <remarks/>
        public int RelationCode
        {
            get
            {
                return this.relationCodeField;
            }
            set
            {
                this.relationCodeField = value;
            }
        }

        /// <remarks/>
        public OnixLegacyProductId ProductIdentifier
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
