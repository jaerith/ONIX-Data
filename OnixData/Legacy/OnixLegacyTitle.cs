using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyTitle
    {
        #region CONSTANTS

        public const int CONST_TITLE_TYPE_UN_TITLE   = 0;
        public const int CONST_TITLE_TYPE_DIST_TITLE = 1;

        #endregion

        public OnixLegacyTitle()
        {
            TitleType = -1;
            TitleText = TitlePrefix = TitleWithoutPrefix = "";
            Subtitle  = "";
        }

        private int    titleTypeField;
        private string titleTextField;
        private string titlePrefixField;
        private string titleWithoutPrefixField;
        private string subtitleField;

        #region ONIX Helpers

        public string OnixTitle
        {
            get
            {
                StringBuilder TitleBuilder = new StringBuilder();

                if (this.Title != null)
                {
                    if (!String.IsNullOrEmpty(this.Title))
                        TitleBuilder.Append(this.Title);
                    else if (!String.IsNullOrEmpty(this.TitleWithoutPrefix))
                    {
                        if (!String.IsNullOrEmpty(this.TitlePrefix))
                            TitleBuilder.Append(this.TitlePrefix).Append(" ");

                        TitleBuilder.Append(this.TitleWithoutPrefix);
                    }
                    else if ((this.TitleType == OnixLegacyTitle.CONST_TITLE_TYPE_UN_TITLE) &&
                             (this.TitleType == OnixLegacyTitle.CONST_TITLE_TYPE_DIST_TITLE))
                    {
                        if (!String.IsNullOrEmpty(this.TitleText))
                            TitleBuilder.Append(this.TitleText);
                    }

                    if (!String.IsNullOrEmpty(this.Subtitle))
                        TitleBuilder.Append(": ").Append(this.Subtitle);
                }

                return TitleBuilder.ToString();
            }
        }

        #endregion

        #region Reference Tags

        public string Title
        {
            get
            {
                string sTitle = "";

                if (!String.IsNullOrEmpty(TitleText))
                    sTitle = TitleText;
                else if (!String.IsNullOrEmpty(TitleWithoutPrefix))
                    sTitle = TitlePrefix + " " + TitleWithoutPrefix;

                return sTitle;
            }
        }

        /// <remarks/>
        public int TitleType
        {
            get
            {
                return this.titleTypeField;
            }
            set
            {
                this.titleTypeField = value;
            }
        }

        /// <remarks/>
        public string TitleText
        {
            get
            {
                return this.titleTextField;
            }
            set
            {
                this.titleTextField = value;
            }
        }

        /// <remarks/>
        public string TitlePrefix
        {
            get
            {
                return this.titlePrefixField;
            }
            set
            {
                this.titlePrefixField = value;
            }
        }

        /// <remarks/>
        public string TitleWithoutPrefix
        {
            get
            {
                return this.titleWithoutPrefixField;
            }
            set
            {
                this.titleWithoutPrefixField = value;
            }
        }

        /// <remarks/>
        public string Subtitle
        {
            get
            {
                return this.subtitleField;
            }
            set
            {
                this.subtitleField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b202
        {
            get { return TitleType; }
            set { TitleType = value; }
        }

        /// <remarks/>
        public string b203
        {
            get { return TitleText; }
            set { TitleText = value; }
        }

        /// <remarks/>
        public string b030
        {
            get { return TitlePrefix; }
            set { TitlePrefix = value; }
        }

        /// <remarks/>
        public string b031
        {
            get { return TitleWithoutPrefix; }
            set { TitleWithoutPrefix = value; }
        }

        /// <remarks/>
        public string b029
        {
            get { return Subtitle; }
            set { Subtitle = value; }
        }

        #endregion
    }
}
