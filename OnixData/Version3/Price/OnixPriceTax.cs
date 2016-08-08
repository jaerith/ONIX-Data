using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Price
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixPriceTax
    {
        public OnixPriceTax()
        {
            TaxType     = -1;
            TaxRateCode = "";
        }            

        private int    taxTypeField;
        private string taxRateCodeField;

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
