namespace OnixData.Standard.Version3.Header
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixHeaderSender
    {
        public OnixHeaderSender()
        {
            SenderName = ContactName = EmailAddress = "";

            SenderIdentifier = senderidentifier = new OnixHeaderSenderIdentifier[0];
        }

        private string senderNameField;
        private string contactNameField;
        private string emailAddressField;

        private OnixHeaderSenderIdentifier[] senderIdentifierField;
        private OnixHeaderSenderIdentifier[] shortSenderIdentifierField;

        #region Helper Methods

        public OnixHeaderSenderIdentifier[] OnixSenderIdentifierList
        {
            get
            {
                OnixHeaderSenderIdentifier[] SenderIdList = null;

                if (this.senderIdentifierField != null)
                    SenderIdList = this.senderIdentifierField;
                else if (this.shortSenderIdentifierField != null)
                    SenderIdList = this.shortSenderIdentifierField;
                else
                    SenderIdList = new OnixHeaderSenderIdentifier[0];

                return SenderIdList;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string SenderName
        {
            get
            {
                return this.senderNameField;
            }
            set
            {
                this.senderNameField = value;
            }
        }

        /// <remarks/>
        public string ContactName
        {
            get
            {
                return this.contactNameField;
            }
            set
            {
                this.contactNameField = value;
            }
        }

        /// <remarks/>
        public string EmailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SenderIdentifier")]
        public OnixHeaderSenderIdentifier[] SenderIdentifier
        {
            get { return this.senderIdentifierField; }
            set { this.senderIdentifierField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x298
        {
            get { return SenderName; }
            set { SenderName = value; }
        }

        /// <remarks/>
        public string x299
        {
            get { return ContactName; }
            set { ContactName = value; }
        }

        /// <remarks/>
        public string j272
        {
            get { return EmailAddress; }
            set { EmailAddress = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("senderidentifier")]
        public OnixHeaderSenderIdentifier[] senderidentifier
        {
            get { return this.shortSenderIdentifierField; }
            set { this.shortSenderIdentifierField = value; }
        }

        #endregion
    }
}
