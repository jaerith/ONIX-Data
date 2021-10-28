namespace OnixData.Version3.Supply
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixWebsite
    {
        public OnixWebsite()
        {
            WebsiteRole = string.Empty;

            websiteDescriptionField      = null;
            shortWebsiteDescriptionField = null;

            websiteLinkField      = null;
            shortWebsiteLinkField = null;
        }

        private string websiteRoleField;
        
        private string[] websiteDescriptionField;
        private string[] shortWebsiteDescriptionField;
        
        private string[] websiteLinkField;
        private string[] shortWebsiteLinkField;

        #region ONIX Lists
        
        public string[] WebsiteDescriptionList
        {
            get
            {
                string[] websiteDescriptionList = null;

                if (this.websiteDescriptionField != null)
                    websiteDescriptionList = this.websiteDescriptionField;
                else if (this.shortWebsiteDescriptionField != null)
                    websiteDescriptionList = this.shortWebsiteDescriptionField;
                else
                    websiteDescriptionList = new string[0];

                return websiteDescriptionList;
            }
        }
        
        public string[] WebsiteLinkList
        {
            get
            {
                string[] websiteLinkList = null;

                if (this.websiteLinkField != null)
                    websiteLinkList = this.websiteLinkField;
                else if (this.shortWebsiteLinkField != null)
                    websiteLinkList = this.shortWebsiteLinkField;
                else
                    websiteLinkList = new string[0];

                return websiteLinkList;
            }
        }

        #endregion

        #region Helper Methods

        

        #endregion

        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("WebsiteRole", IsNullable = true)]
        public string WebsiteRole
        {
            get
            {
                return this.websiteRoleField;
            }
            set
            {
                this.websiteRoleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("WebsiteDescription", IsNullable = true)]
        public string[] WebsiteDescription
        {
            get
            {
                return this.websiteDescriptionField;
            }
            set
            {
                this.websiteDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("WebsiteLink", IsNullable = false)]
        public string[] WebsiteLink
        {
            get
            {
                return this.websiteLinkField;
            }
            set
            {
                this.websiteLinkField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b367
        {
            get { return WebsiteRole; }
            set { WebsiteRole = value; }
        }
        
        /// <remarks/>
        public string[] b294
        {
            get { return shortWebsiteDescriptionField; }
            set { shortWebsiteDescriptionField = value; }
        }
        
        /// <remarks/>
        public string[] b295
        {
            get { return shortWebsiteLinkField; }
            set { shortWebsiteLinkField = value; }
        }

        #endregion
    }
}