namespace OnixData.Standard.Version3.Publishing
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixTerritory
    {
        #region CONSTANTS

        public const string CONST_SALES_WITH_EXCL_RIGHTS     = "01";
        public const string CONST_SALES_WITH_NON_EXCL_RIGHTS = "02";
        public const string CONST_NOT_FOR_SALE               = "03";

        #endregion

        public OnixTerritory()
        {
            CountriesIncluded = RegionsIncluded = "";
            CountriesExcluded = RegionsExcluded = "";
        }

        private string countriesIncludedField;
        private string regionsIncludedField;
        private string countriesExludedField;
        private string regionsExludedField;

        #region Reference Tags

        public string CountriesIncluded
        {
            get { return this.countriesIncludedField; }
            set { this.countriesIncludedField = value; }
        }

        public string RegionsIncluded
        {
            get { return this.regionsIncludedField; }
            set { this.regionsIncludedField = value; }
        }

        public string CountriesExcluded
        {
            get { return this.countriesExludedField; }
            set { this.countriesExludedField = value; }
        }

        public string RegionsExcluded
        {
            get { return this.regionsExludedField; }
            set { this.regionsExludedField = value; }
        }

        #endregion

        #region Short Tags

        public string x449
        {
            get { return CountriesIncluded; }
            set { CountriesIncluded = value; }
        }

        public string x450
        {
            get { return RegionsIncluded; }
            set { RegionsIncluded = value; }
        }

        public string x451
        {
            get { return CountriesExcluded; }
            set { CountriesExcluded = value; }
        }

        public string x452
        {
            get { return RegionsExcluded; }
            set { RegionsExcluded = value; }
        }

        #endregion
    }
}
