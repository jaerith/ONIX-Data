using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyHeader
    {
        private string fromCompanyField;
        private string fromPersonField;
        private uint   sentDateField;

        public OnixLegacyHeader()
        {
            fromCompanyField = "";
            fromPersonField  = "";
            sentDateField    = 0;
        }

        /// <remarks/>
        public string FromCompany
        {
            get
            {
                return this.fromCompanyField;
            }
            set
            {
                this.fromCompanyField = value;
            }
        }

        /// <remarks/>
        public string FromPerson
        {
            get
            {
                return this.fromPersonField;
            }
            set
            {
                this.fromPersonField = value;
            }
        }

        /// <remarks/>
        public uint SentDate
        {
            get
            {
                return this.sentDateField;
            }
            set
            {
                this.sentDateField = value;
            }
        }
    }
}
