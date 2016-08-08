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

        #region Reference Tags

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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x455
        {
            get { return ProductRelationCode; }
            set { ProductRelationCode = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("productidentifier")]
        public OnixProductId[] productidentifier
        {
            get { return ProductIdentifier; }
            set { ProductIdentifier = value; }
        }

        #endregion
    }
}
