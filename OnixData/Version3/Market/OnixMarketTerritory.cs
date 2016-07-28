using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Market
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixMarketTerritory
    {
        public OnixMarketTerritory()
        {
            CountriesIncluded = "";
        }

        private string countriesIncludedField;

        /// <remarks/>
        public string CountriesIncluded
        {
            get
            {
                return this.countriesIncludedField;
            }
            set
            {
                this.countriesIncludedField = value;
            }
        }
    }
}
