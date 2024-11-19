using System;

namespace OnixData.Standard.Version3.Price
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixDiscountCoded
    {
        #region CONSTANTS

        public const int CONST_DISC_CD_TYPE_BIC1     = 1;
        public const int CONST_DISC_CD_TYPE_PROP_TRD = 2;
        public const int CONST_DISC_CD_TYPE_NETH     = 3;
        public const int CONST_DISC_CD_TYPE_GERMAN   = 4;
        public const int CONST_DISC_CD_TYPE_PROP_CR  = 5;
        public const int CONST_DISC_CD_TYPE_BIC2     = 6;
        public const int CONST_DISC_CD_TYPE_ISNI     = 7;

        #endregion 

        public OnixDiscountCoded()
        {
            DiscountCodeType = DiscountCode = "";
        }            

        private string discountCodeTypeField;
        private string discountCodeField;

        #region Helper Methods

        public int DiscCodeTypeNum
        {
            get
            {
                int nTypeNum = -1;

                if (!String.IsNullOrEmpty(DiscountCodeType))
                    Int32.TryParse(DiscountCodeType, out nTypeNum);

                return nTypeNum;
            }
        }

        public bool IsProprietaryType()
        {
            return ((DiscCodeTypeNum == CONST_DISC_CD_TYPE_PROP_TRD) || (DiscCodeTypeNum == CONST_DISC_CD_TYPE_PROP_CR));
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string DiscountCodeType
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
        public string DiscountCode
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public string j363
        {
            get { return DiscountCodeType; }
            set { DiscountCodeType = value; }
        }

        /// <remarks/>
        public string j364
        {
            get { return DiscountCode; }
            set { DiscountCode = value; }
        }

        #endregion
    }

}
