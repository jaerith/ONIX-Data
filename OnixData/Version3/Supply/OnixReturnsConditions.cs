using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Supply
{ 
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixReturnsConditions
    {
        #region CONSTANTS

        public const int CONST_RET_COND_PROP  = 0;
        public const int CONST_RET_COND_CLIL  = 1;
        public const int CONST_RET_COND_BISAC = 2;
        public const int CONST_RET_COND_BIC   = 3;

        #endregion

        public OnixReturnsConditions()
        {
            ReturnsCodeType = -1;

            ReturnsCodeTypeName = ReturnsCode = "";
        }

        private int    returnsCodeTypeField;
        private string returnsCodeTypeNameField;
        private string returnsCodeField;

        /// <remarks/>
        public int ReturnsCodeType
        {
            get
            {
                return this.returnsCodeTypeField;
            }
            set
            {
                this.returnsCodeTypeField = value;
            }
        }

        /// <remarks/>
        public string ReturnsCodeTypeName
        {
            get
            {
                return this.returnsCodeTypeNameField;
            }
            set
            {
                this.returnsCodeTypeNameField = value;
            }
        }

        /// <remarks/>
        public string ReturnsCode
        {
            get
            {
                return this.returnsCodeField;
            }
            set
            {
                this.returnsCodeField = value;
            }
        }
    }
}
