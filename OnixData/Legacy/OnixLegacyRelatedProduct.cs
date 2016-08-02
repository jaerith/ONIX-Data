using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyRelatedProduct : OnixLegacyBaseProduct
    {
        #region CONSTANTS

        // NOTE: Where the product described in the ONIX record is X and the related product is Y
        public const int CONST_REL_PROD_TYPE_INCLUDES   = 1;
        public const int CONST_REL_PROD_TYPE_IS_PART_OF = 2;
        public const int CONST_REL_PROD_X_REPLACES_Y    = 3;
        public const int CONST_REL_PROD_X_REPLACED_BY_Y = 5;

        #endregion

        public OnixLegacyRelatedProduct()
        {
            RelationCode = -1;
        }

        private int relationCodeField;

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
    }
}
