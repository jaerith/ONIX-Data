using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyPrice
    {
        #region CONSTANTS

        public const int CONST_PRICE_TYPE_RRP_EXCL  = 1;
        public const int CONST_PRICE_TYPE_RRP_INCL  = 2;
        public const int CONST_PRICE_TYPE_FRP_EXCL  = 3;
        public const int CONST_PRICE_TYPE_FRP_INCL  = 4;
        public const int CONST_PRICE_TYPE_SUPP_COST = 5;
        public const int CONST_PRICE_TYPE_RRP_PREP  = 21;

        #endregion

        public OnixLegacyPrice()
        {
            PriceTypeCode = -1;
            DiscountCoded = new OnixLegacyDiscountCoded();
            PriceAmount   = 0;
            CurrencyCode  = "";
        }

        private int                     priceTypeCodeField;
        private OnixLegacyDiscountCoded discountCodedField;
        private decimal                 priceAmountField;
        private string                  currencyCodeField;

        /// <remarks/>
        public int PriceTypeCode
        {
            get
            {
                return this.priceTypeCodeField;
            }
            set
            {
                this.priceTypeCodeField = value;
            }
        }

        /// <remarks/>
        public OnixLegacyDiscountCoded DiscountCoded
        {
            get
            {
                return this.discountCodedField;
            }
            set
            {
                this.discountCodedField = value;
            }
        }

        /// <remarks/>
        public decimal PriceAmount
        {
            get
            {
                return this.priceAmountField;
            }
            set
            {
                this.priceAmountField = value;
            }
        }

        /// <remarks/>
        public string CurrencyCode
        {
            get
            {
                return this.currencyCodeField;
            }
            set
            {
                this.currencyCodeField = value;
            }
        }
    }
}
