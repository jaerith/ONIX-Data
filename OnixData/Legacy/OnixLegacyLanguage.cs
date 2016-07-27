using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
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
    }
}
