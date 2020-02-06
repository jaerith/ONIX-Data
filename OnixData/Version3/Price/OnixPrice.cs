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
            PriceAmount  = 0;
            Tax          = new OnixPriceTax();
            CurrencyCode = "";

            discountCodedField = shortDiscountCodedField = new OnixDiscountCoded[0];
        }

        private int          priceTypeField;
        private decimal      priceAmountField;
        private OnixPriceTax taxField;
        private string       currencyCodeField;

        private OnixDiscountCoded[] discountCodedField;
        private OnixDiscountCoded[] shortDiscountCodedField;

        #region Helper Methods

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

        #endregion

        #region ONIX Lists

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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x462
        {
            get { return PriceType; }
            set { PriceType = value; }
        }

        /// <remarks/>
        public decimal j151
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
        [System.Xml.Serialization.XmlElementAttribute("discountcoded")]
        public OnixDiscountCoded[] discountcoded
        {
            get { return shortDiscountCodedField; }
            set { shortDiscountCodedField = value; }
        }

        #endregion
    }
}
