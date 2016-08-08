using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Publishing
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixPublisher
    {
        #region CONSTANTS

        public const int CONST_PUB_ROLE_PUBLISHER = 1;
        public const int CONST_PUB_ROLE_CO_PUB    = 2;

        #endregion

        public OnixPublisher()
        {
            PublishingRole = 0;
            PublisherName  = "";
        }

        private int    publishingRoleField;
        private string publisherNameField;

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

        #endregion
    }
}
