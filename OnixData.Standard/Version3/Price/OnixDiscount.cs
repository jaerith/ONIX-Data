using System;

namespace OnixData.Standard.Version3.Price
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixDiscount
    {
        #region CONSTANTS

        public const int CONST_DSCNT_TYPE_RISING      = 1;
        public const int CONST_DSCNT_TYPE_RISING_CUMU = 2;
        public const int CONST_DSCNT_TYPE_PROGR       = 3;
        public const int CONST_DSCNT_TYPE_PROGR_CUMU  = 4;

        #endregion 

        public OnixDiscount()
        {
            DiscountType = Quantity = DiscountPercent = DiscountAmount = "";
        }            

        private string discountTypeField;
        private string quantityField;
        private string discountPctField;
        private string discountAmtField;

        #region Helper Methods

        public int DiscTypeNum
        {
            get
            {
                int nTypeNum = -1;

                if (!String.IsNullOrEmpty(DiscountType))
                    Int32.TryParse(DiscountType, out nTypeNum);

                return nTypeNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string DiscountAmount
        {
            get
            {
                return this.discountAmtField;
            }
            set
            {
                this.discountAmtField = value;
            }
        }

        /// <remarks/>
        public string DiscountPercent
        {
            get
            {
                return this.discountPctField;
            }
            set
            {
                this.discountPctField = value;
            }
        }

        /// <remarks/>
        public string DiscountType
        {
            get
            {
                return this.discountTypeField;
            }
            set
            {
                this.discountTypeField = value;
            }
        }

        /// <remarks/>
        public string Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x469
        {
            get
            {
                return this.discountAmtField;
            }
            set
            {
                this.discountAmtField = value;
            }
        }

        /// <remarks/>
        public string j267
        {
            get
            {
                return this.discountPctField;
            }
            set
            {
                this.discountPctField = value;
            }
        }

        /// <remarks/>
        public string x467
        {
            get
            {
                return this.discountTypeField;
            }
            set
            {
                this.discountTypeField = value;
            }
        }

        /// <remarks/>
        public string x320
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }


        #endregion
    }
}
