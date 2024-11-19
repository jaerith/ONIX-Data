using System;

namespace OnixData.Standard.Version3.Supply
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

        public const int CONST_RET_CODE_TYPE_BISAC = 2;

        #endregion

        public OnixReturnsConditions()
        {
            ReturnsCodeType = ReturnsCodeTypeName = ReturnsCode = "";
        }

        private string returnsCodeTypeField;
        private string returnsCodeTypeNameField;
        private string returnsCodeField;

        #region Helper Methods

        public int ReturnsCodeTypeNum
        {
            get
            {
                int nRetCodeTypeNum = 0;

                if (!String.IsNullOrEmpty(ReturnsCodeType))
                    Int32.TryParse(ReturnsCodeType, out nRetCodeTypeNum);

                return nRetCodeTypeNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string ReturnsCodeType
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public string j268
        {
            get { return ReturnsCodeType; }
            set { ReturnsCodeType = value; }
        }

        /// <remarks/>
        public string x460
        {
            get { return ReturnsCodeTypeName; }
            set { ReturnsCodeTypeName = value; }
        }

        /// <remarks/>
        public string j269
        {
            get { return ReturnsCode; }
            set { ReturnsCode = value; }
        }

        #endregion
    }
}
