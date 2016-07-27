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
        public OnixLegacyPrice()
        {
            PriceTypeCode = -1;
            DiscountCoded = new OnixLegacyDiscountCoded();
            PriceAmount   = 0;
            CurrencyCode  = "";
        }

        private int priceTypeCodeField;

        private OnixLegacyDiscountCoded discountCodedField;

        private decimal priceAmountField;

        private string currencyCodeField;

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
