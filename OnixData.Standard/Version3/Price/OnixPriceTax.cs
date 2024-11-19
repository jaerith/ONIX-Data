using System;

namespace OnixData.Standard.Version3.Price
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixPriceTax
    {
        public OnixPriceTax()
        {
            TaxType     = -1;
            TaxRateCode = TaxRatePercent = String.Empty;
        }            

        private int    taxTypeField;
        private string taxRateCodeField;
        private string taxRatePercentField;
        
        #region Helper Methods

        public decimal TaxRatePercentNum
        {
            get
            {
                decimal nPercent = 0;

                if (!String.IsNullOrEmpty(TaxRatePercent))
                    Decimal.TryParse(TaxRatePercent, out nPercent);

                return nPercent;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public int TaxType
        {
            get
            {
                return this.taxTypeField;
            }
            set
            {
                this.taxTypeField = value;
            }
        }

        /// <remarks/>
        public string TaxRateCode
        {
            get
            {
                return this.taxRateCodeField;
            }
            set
            {
                this.taxRateCodeField = value;
            }
        }
        
        public string TaxRatePercent
        {
            get
            {
                return this.taxRatePercentField;
            }
            set
            {
                this.taxRatePercentField = value;
            }
        }
        
        /// <remarks/>
        public string x472
        {
            get { return TaxRatePercent; }
            set { TaxRatePercent = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x470
        {
            get { return TaxType; }
            set { TaxType = value; }
        }

        /// <remarks/>
        public string x471
        {
            get { return TaxRateCode; }
            set { TaxRateCode = value; }
        }

        #endregion
    }
}
