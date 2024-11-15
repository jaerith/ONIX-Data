using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Header
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixHeaderAddressee
    {
        public OnixHeaderAddressee()
        {
            AddresseeName = "";

            AddresseeIdentifier = addresseeidentifier = new OnixHeaderAddresseeIdentifier[0];
        }

        private string addresseeNameField;

        private OnixHeaderAddresseeIdentifier[] addresseeIdentifierField;
        private OnixHeaderAddresseeIdentifier[] shortAddresseeIdentifierField;

        #region Helper Methods

        public OnixHeaderAddresseeIdentifier[] OnixAddresseeIdentifierList
        {
            get
            {
                OnixHeaderAddresseeIdentifier[] AddresseeIdList = null;

                if (this.addresseeIdentifierField != null)
                    AddresseeIdList = this.addresseeIdentifierField;
                else if (this.shortAddresseeIdentifierField != null)
                    AddresseeIdList = this.shortAddresseeIdentifierField;
                else
                    AddresseeIdList = new OnixHeaderAddresseeIdentifier[0];

                return AddresseeIdList;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string AddresseeName
        {
            get
            {
                return this.addresseeNameField;
            }
            set
            {
                this.addresseeNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AddresseeIdentifier")]
        public OnixHeaderAddresseeIdentifier[] AddresseeIdentifier
        {
            get { return this.addresseeIdentifierField; }
            set { this.addresseeIdentifierField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x300
        {
            get { return AddresseeName; }
            set { AddresseeName = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("addresseeidentifier")]
        public OnixHeaderAddresseeIdentifier[] addresseeidentifier
        {
            get { return this.shortAddresseeIdentifierField; }
            set { this.shortAddresseeIdentifierField = value; }
        }

        #endregion
    }
}
