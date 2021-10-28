using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Supply;

namespace OnixData.Version3.Publishing
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixPublisher
    {
        #region CONSTANTS

        public const int CONST_PUB_ROLE_PUBLISHER = 1;
        public const int CONST_PUB_ROLE_CO_PUB    = 2;
        public const int CONST_PUB_ROLE_IN_ASSOC  = 7;		

        #endregion

        public OnixPublisher()
        {
            PublishingRole = 0;
            PublisherName  = "";

            websiteField      = new OnixWebsite[0];
            shortWebsiteField = new OnixWebsite[0];
        }

        private int    publishingRoleField;
        private string publisherNameField;

        private OnixWebsite[] websiteField;
        private OnixWebsite[] shortWebsiteField;

        #region Helper Methods

        public OnixWebsite[] OnixWebsiteList
        {
            get
            {
                OnixWebsite[] websiteList = null;

                if (this.websiteField != null)
                    websiteList = this.websiteField;
                else if (this.shortWebsiteField != null)
                    websiteList = this.shortWebsiteField;
                else
                    websiteList = new OnixWebsite[0];

                return websiteList;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public int PublishingRole
        {
            get
            {
                return this.publishingRoleField;
            }
            set
            {
                this.publishingRoleField = value;
            }
        }

        /// <remarks/>
        public string PublisherName
        {
            get
            {
                return this.publisherNameField;
            }
            set
            {
                this.publisherNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Website")]
        public OnixWebsite[] Website
        {
            get
            {
                return this.websiteField;
            }
            set
            {
                this.websiteField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b291
        {
            get { return PublishingRole; }
            set { PublishingRole = value; }
        }

        /// <remarks/>
        public string b081
        {
            get { return PublisherName; }
            set { PublisherName = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("website")]
        public OnixWebsite[] website
        {
            get
            {
                return this.shortWebsiteField;
            }
            set
            {
                this.shortWebsiteField = value;
            }
        }

        #endregion
    }
}
