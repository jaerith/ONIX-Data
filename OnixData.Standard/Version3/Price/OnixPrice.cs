using System;
using System.Globalization;
using System.Linq;
using OnixData.Standard.Version3.Publishing;

namespace OnixData.Standard.Version3.Price
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixPrice
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

        public OnixPrice()
        {
            PriceType    = -1;
            PriceAmount  = "";
            Tax          = new OnixPriceTax();
            CurrencyCode = UnpricedItemType = "";

            Territory = new OnixTerritory();

            discountField      = shortDiscountField      = new OnixDiscount[0];
            discountCodedField = shortDiscountCodedField = new OnixDiscountCoded[0];
        }

        private int          priceTypeField;
        private string       priceAmountField;
        private OnixPriceTax taxField;
        private string       currencyCodeField;
        private string       unpricedItemTypeField;

        private OnixDiscount[] discountField;
        private OnixDiscount[] shortDiscountField;

        private OnixDiscountCoded[] discountCodedField;
        private OnixDiscountCoded[] shortDiscountCodedField;

        private OnixTerritory territoryField;

        #region Helper Methods

        public string DiscountAmount
        {
            get
            {
                string DiscAmt = "";

                if ((this.OnixDiscountList != null) && (this.OnixDiscountList.Length > 0))
                {
                    OnixDiscount FoundDiscount =
                        OnixDiscountList.Where(x => !String.IsNullOrEmpty(x.DiscountAmount)).FirstOrDefault();

                    if ((FoundDiscount != null) && !String.IsNullOrEmpty(FoundDiscount.DiscountAmount))
                        DiscAmt = FoundDiscount.DiscountAmount;
                }

                return DiscAmt;
            }
        }

        public string DiscountPercent
        {
            get
            {
                string DiscPct = "";

                if ((this.OnixDiscountList != null) && (this.OnixDiscountList.Length > 0))
                {
                    OnixDiscount FoundDiscount =
                        OnixDiscountList.Where(x => !String.IsNullOrEmpty(x.DiscountPercent)).FirstOrDefault();

                    if ((FoundDiscount != null) && !String.IsNullOrEmpty(FoundDiscount.DiscountPercent))
                        DiscPct = FoundDiscount.DiscountPercent;
                }

                return DiscPct;
            }
        }

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

        public OnixDiscountCoded OnixFirstViableDiscountCoded
        {
            get
            {
                OnixDiscountCoded ViableDiscountCoded = null;

                if ((this.OnixDiscountCodedList != null) && (this.OnixDiscountCodedList.Length > 0))
                {
                    OnixDiscountCoded DiscountCodedCandidate =
                        OnixDiscountCodedList.Where(x => x.IsProprietaryType()).FirstOrDefault();

                    if ((DiscountCodedCandidate != null) && !String.IsNullOrEmpty(DiscountCodedCandidate.DiscountCode))
                        ViableDiscountCoded = DiscountCodedCandidate;
                }

                return ViableDiscountCoded;
            }
        }

        public decimal PriceAmountNum
        {
            get
            {
                decimal nPriceAmt = 0;

                if (!string.IsNullOrEmpty(PriceAmount))
                    decimal.TryParse(PriceAmount, NumberStyles.Any, CultureInfo.InvariantCulture, out nPriceAmt);

                return nPriceAmt;
            }
        }

        #endregion

        #region ONIX Lists

        public OnixDiscount[] OnixDiscountList
        {
            get
            {
                OnixDiscount[] DiscountList = null;

                if (discountField != null)
                    DiscountList = this.discountField;
                else if (shortDiscountField != null)
                    DiscountList = this.shortDiscountField;
                else
                    DiscountList = new OnixDiscount[0];

                return DiscountList;
            }
        }

        public OnixDiscountCoded[] OnixDiscountCodedList
        {
            get
            {
                OnixDiscountCoded[] DiscountCodedList = null;

                if (discountCodedField != null)
                    DiscountCodedList = this.discountCodedField;
                else if (shortDiscountCodedField != null)
                    DiscountCodedList = this.shortDiscountCodedField;
                else
                    DiscountCodedList = new OnixDiscountCoded[0];

                return DiscountCodedList;
            }
        }

        #endregion 

        #region Reference Tags

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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Discount")]
        public OnixDiscount[] Discount
        {
            get
            {
                return this.discountField;
            }
            set
            {
                this.discountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DiscountCoded")]
        public OnixDiscountCoded[] DiscountCoded
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
        public string UnpricedItemType
        {
            get
            {
                return this.unpricedItemTypeField;
            }
            set
            {
                this.unpricedItemTypeField = value;
            }
        }

        public OnixTerritory Territory
        {
            get { return this.territoryField; }
            set { this.territoryField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x462
        {
            get { return PriceType; }
            set { PriceType = value; }
        }

        /// <remarks/>
        public string j151
        {
            get { return PriceAmount; }
            set { PriceAmount = value; }
        }

        /// <remarks/>
        public OnixPriceTax tax
        {
            get { return Tax; }
            set { Tax = value; }
        }

        /// <remarks/>
        public string j152
        {
            get { return CurrencyCode; }
            set { CurrencyCode = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("discount")]
        public OnixDiscount[] discount
        {
            get { return shortDiscountField; }
            set { shortDiscountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("discountcoded")]
        public OnixDiscountCoded[] discountcoded
        {
            get { return shortDiscountCodedField; }
            set { shortDiscountCodedField = value; }
        }

        /// <remarks/>
        public string j192
        {
            get { return UnpricedItemType; }
            set { UnpricedItemType = value; }
        }

        public OnixTerritory territory
        {
            get { return Territory; }
            set { Territory = value; }
        }

        #endregion
    }
}
