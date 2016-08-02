using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyPublisher
    {
        #region CONSTANTS

        public const int CONST_PUB_ROLE_PUBLISHER = 1;
        public const int CONST_PUB_ROLE_CO_PUB    = 2;

        #endregion

        public OnixLegacyPublisher()
        {
            NameCodeType     = PublishingRole = 0;
            NameCodeTypeName = "";
            NameCodeValue    = "";
            PublisherName    = "";
        }

        private int    nameCodeTypeField;
        private string nameCodeTypeNameField;
        private string nameCodeValueField;
        private int    publishingRoleField;
        private string publisherNameField;

        /// <remarks/>
        public int NameCodeType
        {
            get
            {
                return this.nameCodeTypeField;
            }
            set
            {
                this.nameCodeTypeField = value;
            }
        }

        /// <remarks/>
        public string NameCodeTypeName
        {
            get
            {
                return this.nameCodeTypeNameField;
            }
            set
            {
                this.nameCodeTypeNameField = value;
            }
        }

        /// <remarks/>
        public string NameCodeValue
        {
            get
            {
                return this.nameCodeValueField;
            }
            set
            {
                this.nameCodeValueField = value;
            }
        }

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
    }
}
