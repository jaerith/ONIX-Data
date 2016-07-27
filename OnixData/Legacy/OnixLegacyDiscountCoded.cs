using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyDiscountCoded
    {
        private int    discountCodeTypeField;
        private string discountCodeTypeNameField;
        private int    discountCodeField;

        public OnixLegacyDiscountCoded()
        {
            DiscountCodeType     = -1;
            DiscountCodeTypeName = "";
            DiscountCode         = -1;
        }

        /// <remarks/>
        public int DiscountCodeType
        {
            get
            {
                return this.discountCodeTypeField;
            }
            set
            {
                this.discountCodeTypeField = value;
            }
        }

        /// <remarks/>
        public string DiscountCodeTypeName
        {
            get
            {
                return this.discountCodeTypeNameField;
            }
            set
            {
                this.discountCodeTypeNameField = value;
            }
        }

        /// <remarks/>
        public int DiscountCode
        {
            get
            {
                return this.discountCodeField;
            }
            set
            {
                this.discountCodeField = value;
            }
        }
    }

}
