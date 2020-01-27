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

        public const int CONST_ROLE_LANG_OF_TEXT = 1;

        #endregion

        public OnixLanguage()
        {
            LanguageRole = "-1";
            LanguageCode = "";
        }

        private string langRole;
        private string langCode;

        #region Helper Methods

        public int GetLanguageRoleNum()
        {
            int nLangRole = -1;

            if (!String.IsNullOrEmpty(this.LanguageRole))
            {
                Int32.TryParse(this.LanguageRole, out nLangRole);
            }

            return nLangRole;
        }

        public bool IsLanguageOfText()
        {
            return (GetLanguageRoleNum() == CONST_ROLE_LANG_OF_TEXT);
        }

        #endregion

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
