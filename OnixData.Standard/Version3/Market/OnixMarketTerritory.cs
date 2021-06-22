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
        #region CONSTANTS

        private const char CONST_LIST_DELIM = ' ';

        public const int CONST_SR_TYPE_FOR_SALE_WITH_EXCL_RIGHTS    = 1;
        public const int CONST_SR_TYPE_FOR_SALE_WITH_NONEXCL_RIGHTS = 2;
        public const int CONST_SR_TYPE_NOT_FOR_SALE                 = 3;

        #endregion

        public OnixMarketTerritory()
        {
            countriesIncludedField = "";
            countriesIncludedList  = new List<string>();
        }

        private string       countriesIncludedField;
        private List<string> countriesIncludedList;

        #region Reference Tags

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

                if (!String.IsNullOrEmpty(this.countriesIncludedField))
                {
                    if (this.countriesIncludedField.Contains(CONST_LIST_DELIM))
                        this.countriesIncludedList = new List<string>(this.countriesIncludedField.Split(CONST_LIST_DELIM));
                    else
                        this.countriesIncludedList = new List<string>() { this.countriesIncludedField };
                }
            }
        }

        public List<string> CountriesIncludedList
        {
            get
            {
                return this.countriesIncludedList;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x449
        {
            get { return CountriesIncluded; }
            set { CountriesIncluded = value; }
        }        

        #endregion
    }
}
