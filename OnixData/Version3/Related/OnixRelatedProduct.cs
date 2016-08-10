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

            productIdentifierField = shortProductIdentifierField = new OnixProductId[0];
        }

        private int productRelationCodeField;

        private OnixProductId[] productIdentifierField;
        private OnixProductId[] shortProductIdentifierField;

        #region ONIX Lists

        public OnixProductId[] OnixProductIdList
        {
            get
            {
                OnixProductId[] ProductIds = null;

                if (this.productIdentifierField != null)
                    ProductIds = this.productIdentifierField;
                else if (this.shortProductIdentifierField != null)
                    ProductIds = this.shortProductIdentifierField;
                else
                    ProductIds = new OnixProductId[0];

                return ProductIds;
            }
        }

        #endregion

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
            get { return shortProductIdentifierField; }
            set { shortProductIdentifierField = value; }
        }

        #endregion
    }
}
