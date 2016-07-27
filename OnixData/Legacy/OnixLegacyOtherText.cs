using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyOtherText
    {
        public OnixLegacyOtherText()
        {
            TextTypeCode = -1;
            Text         = "";
        }

        private int    textTypeCodeField;
        private string textField;

        /// <remarks/>
        public int TextTypeCode
        {
            get
            {
                return this.textTypeCodeField;
            }
            set
            {
                this.textTypeCodeField = value;
            }
        }

        /// <remarks/>
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }
}
