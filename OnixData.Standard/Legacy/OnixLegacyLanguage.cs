namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyLanguage
    {
        public OnixLegacyLanguage()
        {
            LanguageRole = -1;
            LanguageCode = "";
        }

        private int    languageRoleField;
        private string languageCodeField;

        #region Reference Tags

        /// <remarks/>
        public int LanguageRole
        {
            get
            {
                return this.languageRoleField;
            }
            set
            {
                this.languageRoleField = value;
            }
        }

        /// <remarks/>
        public string LanguageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b253
        {
            get { return LanguageRole; }
            set { LanguageRole = value; }
        }

        /// <remarks/>
        public string b252
        {
            get { return LanguageCode; }
            set { LanguageCode = value; }
        }

        #endregion
    }
}
