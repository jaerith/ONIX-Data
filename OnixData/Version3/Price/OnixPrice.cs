using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Price
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixPrice
    {
        #region CONSTANTS

        public const int CONST_PRICE_TYPE_RRP_EXCL  = 1;
        public const int CONST_PRICE_TYPE_RRP_INCL  = 2;
        public const int CONST_PRICE_TYPE_FRP_EXCL  = 3;
        public const int CONST_PRICE_TYPE_FRP_INCL  = 4;
        public const int CONST_PRICE_TYPE_SUPP_COST = 5;
        public const int CONST_PRICE_TYPE_RRP_PREP  = 21;

        #endregion

        public OnixPrice()
        {
            PriceType    = -1;
            PriceAmount  = 0;
            Tax          = new OnixPriceTax();
            CurrencyCode = "";
        }

        private int          priceTypeField;
        private decimal      priceAmountField;
        private OnixPriceTax taxField;
        private string       currencyCodeField;

        /// <remarks/>
        public int PriceType
        {
            get
            {
                return this.priceTypeField;
            }
            set
            {
                this.priceTypeField = value;
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
        public OnixPriceTax Tax
        {
            get
            {
                return this.taxField;
            }
            set
            {
                this.taxField = value;
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
