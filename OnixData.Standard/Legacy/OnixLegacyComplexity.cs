namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyComplexity
    {
        #region CONSTANTS

        public const int CONST_COMPLEXITY_CODE = 1;
        public const int CONST_COMPLEXITY_NUM  = 2;
        
        #endregion

        public OnixLegacyComplexity()
        {
            complexitySchemeIdentifierField = -1;
            complexityCodeField             = "";
        }

        private int    complexitySchemeIdentifierField;
        private string complexityCodeField;

        #region Reference Tags

        /// <remarks/>
        public int ComplexitySchemeIdentifier
        {
            get
            {
                return this.complexitySchemeIdentifierField;
            }
            set
            {
                this.complexitySchemeIdentifierField = value;
            }
        }

        /// <remarks/>
        public string ComplexityCode
        {
            get
            {
                return this.complexityCodeField;
            }
            set
            {
                this.complexityCodeField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b077
        {
            get { return ComplexitySchemeIdentifier; }
            set { ComplexitySchemeIdentifier = value; }
        }

        /// <remarks/>
        public string b078
        {
            get { return ComplexityCode; }
            set { ComplexityCode = value; }
        }

        #endregion
    }
}
