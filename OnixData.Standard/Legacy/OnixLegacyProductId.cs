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
            ProductIDType = IDValue = "";
        }

        private string productIDTypeField;
        private string iDValueField;

        #region Helpers

        public int ProductIDTypeNum
        {
            get
            {
                int nPidTypeNum = -1;

                if (!String.IsNullOrEmpty(this.productIDTypeField))
                    Int32.TryParse(this.productIDTypeField, out nPidTypeNum);

                return nPidTypeNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string ProductIDType
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
        public string b221
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
