using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Language
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLanguage
    {
        #region CONSTANTS

        public const string CONST_ROLE_LANG_OF_TEXT = "01";

        #endregion

        public OnixLanguage()
        {
            LanguageRole = "-1";
            LanguageCode = "";
        }

        private string langRole;
        private string langCode;

        #region Reference Tags

        /// <remarks/>
        public string LanguageRole
        {
            get
            {
                return this.langRole;
            }
            set
            {
                this.langRole = value;
            }
        }

        /// <remarks/>
        public string LanguageCode
        {
            get
            {
                return this.langCode;
            }
            set
            {
                this.langCode = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b253
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
