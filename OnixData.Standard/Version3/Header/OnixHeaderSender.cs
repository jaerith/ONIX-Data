using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Header
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixHeaderSender
    {
        public OnixHeaderSender()
        {
            SenderName = ContactName = EmailAddress = "";
        }

        private string senderNameField;
        private string contactNameField;
        private string emailAddressField;

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

        #endregion
    }
}
