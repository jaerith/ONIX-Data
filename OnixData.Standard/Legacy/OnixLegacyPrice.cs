using System;
using System.Globalization;
using System.Linq;

namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyPrice
    {
        #region CONSTANTS

        public const int CONST_PRICE_TYPE_RRP_EXCL         = 1;
        public const int CONST_PRICE_TYPE_RRP_INCL         = 2;
        public const int CONST_PRICE_TYPE_FRP_EXCL         = 3;
        public const int CONST_PRICE_TYPE_FRP_INCL         = 4;
        public const int CONST_PRICE_TYPE_SUPP_COST        = 5;
        public const int CONST_PRICE_TYPE_RRP_PREP         = 21;
        public const int CONST_PRICE_TYPE_FPT_RRP_EXCL_TAX = 31;
        public const int CONST_PRICE_TYPE_FPT_BIL_EXCL_TAX = 32;
        public const int CONST_PRICE_TYPE_PROP_MISC_1      = 41;
        public const int CONST_PRICE_TYPE_PROP_MISC_2      = 99;

        public const int CONST_PUB_DISC_CD_TYPE_PTY   = 2;
        public const int CONST_PUB_DISC_CD_TYPE_PTY_2 = 5;

        public readonly int[] CONST_SOUGHT_RETAIL_PRICE_TYPES
            = {
                CONST_PRICE_TYPE_RRP_EXCL, CONST_PRICE_TYPE_RRP_PREP,
                CONST_PRICE_TYPE_FPT_BIL_EXCL_TAX, CONST_PRICE_TYPE_PROP_MISC_1
              };

        public readonly int[] CONST_SOUGHT_SUPP_COST_PRICE_TYPES
            = {
                CONST_PRICE_TYPE_SUPP_COST
              };

        public readonly int[] CONST_SOUGHT_PRICE_TYPES 
            = {
                CONST_PRICE_TYPE_RRP_EXCL, CONST_PRICE_TYPE_SUPP_COST, CONST_PRICE_TYPE_RRP_PREP,
                CONST_PRICE_TYPE_FPT_RRP_EXCL_TAX, CONST_PRICE_TYPE_FPT_BIL_EXCL_TAX,
                CONST_PRICE_TYPE_PROP_MISC_1, CONST_PRICE_TYPE_PROP_MISC_2
              };

        #endregion

        public OnixLegacyPrice()
        {
            PriceTypeCode = -1;
            CurrencyCode  = PriceAmount = ClassOfTrade = DiscountPercent = "";

            discountCodedField = shortDiscountCodedField = new OnixLegacyDiscountCoded[0];
        }

        private int      priceTypeCodeField;
        private string   classOfTradeField;
        private string   discountPercentageField;
        private string   priceAmountField;
        private string   currencyCodeField;
        private string   priceEffectiveFromField;
        private string   priceEffectiveUntilField;
        private string   priceTypeDescriptionField;
        private string   pricePerField;
        private int      minimumOrderQuantityField;
        private string   priceStatusField;
        private string   countryCodeField;

        private OnixLegacyDiscountCoded[] discountCodedField;
        private OnixLegacyDiscountCoded[] shortDiscountCodedField;

        #region ONIX helpers

        public bool HasSoughtRetailPriceType()
        {
            return CONST_SOUGHT_RETAIL_PRICE_TYPES.Contains(this.PriceType);
        }

        public bool HasSoughtSupplyCostPriceType()
        {
            return CONST_SOUGHT_SUPP_COST_PRICE_TYPES.Contains(this.PriceType);
        }

        public bool HasSoughtPriceTypeCode()
        {
            return CONST_SOUGHT_PRICE_TYPES.Contains(this.PriceType);
        }

        public bool HasViablePubDiscountCode()
        {
            return (OnixFirstViableDiscountCoded != null);
        }

        public OnixLegacyDiscountCoded OnixFirstViableDiscountCoded
        {
            get
            {
                OnixLegacyDiscountCoded ViableDiscountCoded = null;

                if ((this.OnixDiscountCodedList != null) && (this.OnixDiscountCodedList.Length > 0))
                {
                    OnixLegacyDiscountCoded DiscountCodedCandidate =
                        OnixDiscountCodedList.Where(x => (x.DiscountCodeType == CONST_PUB_DISC_CD_TYPE_PTY) || (x.DiscountCodeType == CONST_PUB_DISC_CD_TYPE_PTY_2)).FirstOrDefault();

                    if ((DiscountCodedCandidate != null) && !String.IsNullOrEmpty(DiscountCodedCandidate.DiscountCode))
                        ViableDiscountCoded = DiscountCodedCandidate;
                }

                return ViableDiscountCoded;
            }
        }

        public OnixLegacyDiscountCoded[] OnixDiscountCodedList
        {
            get
            {
                OnixLegacyDiscountCoded[] DiscountCodedList = null;

                if (discountCodedField != null)
                    DiscountCodedList = this.discountCodedField;
                else if (shortDiscountCodedField != null)
                    DiscountCodedList = this.shortDiscountCodedField;
                else
                    DiscountCodedList = new OnixLegacyDiscountCoded[0];

                return DiscountCodedList;
            }
        }

        #endregion

        #region Reference Tags

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
        public string ClassOfTrade
        {
            get
            {
                return this.classOfTradeField;
            }
            set
            {
                this.classOfTradeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DiscountCoded")]
        public OnixLegacyDiscountCoded[] DiscountCoded
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
        public string DiscountPercent
        {
            get
            {
                return this.discountPercentageField;
            }
            set
            {
                this.discountPercentageField = value;
            }
        }

        /// <remarks/>
        public string PriceAmount
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
        public decimal PriceAmountNum
        {
            get
            {
                decimal dPriceAmountNum = -1;

                if (!string.IsNullOrEmpty(this.priceAmountField))
                    decimal.TryParse(this.priceAmountField, NumberStyles.Any, CultureInfo.InvariantCulture, out dPriceAmountNum);

                return dPriceAmountNum;
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

        #endregion

        #region Short Tags

        public int j148
        {
            get { return this.PriceTypeCode; }
            set { PriceTypeCode = value; }
        }

        /// <remarks/>
        public string j149
        {
            get { return ClassOfTrade; }
            set { ClassOfTrade = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("discountcoded")]
        public OnixLegacyDiscountCoded[] discountcoded
        {
            get { return shortDiscountCodedField; }
            set { shortDiscountCodedField = value; }
        }

        /// <remarks/>
        public string j267
        {
            get { return DiscountPercent; }
            set { DiscountPercent = value; }
        }

        /// <remarks/>
        public string j151
        {
            get { return this.priceAmountField; }
            set { this.priceAmountField = value; }
        }

        /// <remarks/>
        public string j152
        {
            get { return CurrencyCode; }
            set { CurrencyCode = value; }
        }

        /// <remarks/>
        public string j161
        {
            get { return this.priceEffectiveFromField; }
            set { this.priceEffectiveFromField = value; }
        }

        /// <remarks/>
        public string j162
        {
            get { return this.priceEffectiveUntilField; }
            set { this.priceEffectiveUntilField = value; }
        }

        /// <remarks/>
        public string j262
        {
            get { return PriceTypeDescription; }
            set { PriceTypeDescription = value; }
        }

        /// <remarks/>
        public string j239
        {
            get { return PricePer; }
            set { PricePer = value; }
        }

        /// <remarks/>
        public int j263
        {
            get { return this.minimumOrderQuantityField; }
            set { this.minimumOrderQuantityField = value; }
        }

        /// <remarks/>
        public string j266
        {
            get { return PriceStatus; }
            set { PriceStatus = value; }
        }

        /// <remarks/>
        public string b251
        {
            get { return CountryCode; }
            set { CountryCode = value; }
        }

        #endregion

    }
}
