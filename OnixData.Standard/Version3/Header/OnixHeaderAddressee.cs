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
        }

        private string addresseeNameField;

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

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x300
        {
            get { return AddresseeName; }
            set { AddresseeName = value; }
        }        

        #endregion
    }
}
