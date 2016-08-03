using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
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
    }
}
