using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Header
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixHeaderAddresseeIdentifier
    {
        public OnixHeaderAddresseeIdentifier()
        {
            AddresseeIDType = IDTypeName = IDValue = String.Empty;
        }

        private string addresseeIdTypeField;
        private string idTypeNameField;
        private string idValueField;

        #region Reference Tags

        /// <remarks/>
        public string AddresseeIDType
        {
            get
            {
                return this.addresseeIdTypeField;
            }
            set
            {
                this.addresseeIdTypeField = value;
            }
        }

        /// <remarks/>
        public string IDTypeName
        {
            get
            {
                return this.idTypeNameField;
            }
            set
            {
                this.idTypeNameField = value;
            }
        }

        /// <remarks/>
        public string IDValue
        {
            get
            {
                return this.idValueField;
            }
            set
            {
                this.idValueField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string m380
        {
            get { return AddresseeIDType; }
            set { AddresseeIDType = value; }
        }

        /// <remarks/>
        public string b233
        {
            get { return IDTypeName; }
            set { IDTypeName = value; }
        }

        /// <remarks/>
        public string b244
        {
            get { return IDValue; }
            set { IDValue = value; }
        }

        #endregion
    }
}
