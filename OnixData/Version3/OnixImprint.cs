using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixImprint
    {
        public OnixImprint()
        {
            ImprintName = "";
        }

        private string imprintNameField;

        /// <remarks/>
        public string ImprintName
        {
            get
            {
                return this.imprintNameField;
            }
            set
            {
                this.imprintNameField = value;
            }
        }
    }
}
