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

        #region Reference Tags

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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b221
        {
            get
            {
                return ProductIDType;
            }
            set
            {
                ProductIDType = value;
            }
        }

        /// <remarks/>
        public string b244
        {
            get
            {
                return IDValue;
            }
            set
            {
                IDValue = value;
            }
        }

        #endregion
    }
}
