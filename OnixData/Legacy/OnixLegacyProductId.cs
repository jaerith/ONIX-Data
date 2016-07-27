using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyProductId
    {
        public OnixLegacyProductId()
        {
            ProductIDType = -1;
            IDValue       = "";
        }

        private int    productIDTypeField;
        private string iDValueField;

        /// <remarks/>
        public int ProductIDType
        {
            get
            {
                return this.productIDTypeField;
            }
            set
            {
                this.productIDTypeField = value;
            }
        }

        /// <remarks/>
        public string IDValue
        {
            get
            {
                return this.iDValueField;
            }
            set
            {
                this.iDValueField = value;
            }
        }
    }
}
