namespace OnixData.Standard.Legacy
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

        #region Reference Tags

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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b241
        {
            get { return NameCodeType; }
            set { NameCodeType = value; }
        }

        /// <remarks/>
        public string b242
        {
            get { return NameCodeTypeName; }
            set { NameCodeTypeName = value; }
        }

        /// <remarks/>
        public string b243
        {
            get { return NameCodeValue; }
            set { NameCodeValue = value; }
        }

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
