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
        private string                  priceEffectiveFromField;
        private string                  priceEffectiveUntilField;
        private string                  priceTypeDescriptionField;
        private string                  pricePerField;
        private int                     minimumOrderQuantityField;
        private string                  priceStatusField;
        private string                  countryCodeField;

        /*
     else if (!strTagName.compare("PriceEffectiveFrom") || !strTagName.compare("j161"))
     {}
     else if (!strTagName.compare("PriceEffectiveUntil") || !strTagName.compare("j162"))
     {}
     else if (!strTagName.compare("PriceTypeDescription") || !strTagName.compare("j262"))
     {}
     else if (!strTagName.compare("PricePer") || !strTagName.compare("j239"))
     {}
     else if (!strTagName.compare("MinimumOrderQuantity") || !strTagName.compare("j263"))
     {}
     else if (!strTagName.compare("PriceStatus") || !strTagName.compare("j266"))
     {}
     else if (!strTagName.compare("CountryCode") || !strTagName.compare("j251"))

         */

        public int PriceType
        {
            get
            {
                return PriceTypeCode;
            }
            set
            {
                PriceTypeCode = value;
            }
        }

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

        /// <remarks/>
        public string PriceEffectiveFrom
        {
            get
            {
                return this.priceEffectiveFromField;
            }
            set
            {
                this.priceEffectiveFromField = value;
            }
        }

        /// <remarks/>
        public string PriceEffectiveUntil
        {
            get
            {
                return this.priceEffectiveUntilField;
            }
            set
            {
                this.priceEffectiveUntilField = value;
            }
        }

        /// <remarks/>
        public string PriceTypeDescription
        {
            get
            {
                return this.priceTypeDescriptionField;
            }
            set
            {
                this.priceTypeDescriptionField = value;
            }
        }

        /// <remarks/>
        public string PricePer
        {
            get
            {
                return this.pricePerField;
            }
            set
            {
                this.pricePerField = value;
            }
        }

        /// <remarks/>
        public int MinimumOrderQuantity
        {
            get
            {
                return this.minimumOrderQuantityField;
            }
            set
            {
                this.minimumOrderQuantityField = value;
            }
        }

        /// <remarks/>
        public string PriceStatus
        {
            get
            {
                return this.priceStatusField;
            }
            set
            {
                this.priceStatusField = value;
            }
        }

        /// <remarks/>
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }
        

    }
}
