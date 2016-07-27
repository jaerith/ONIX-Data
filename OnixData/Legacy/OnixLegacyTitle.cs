using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyTitle
    {
        public OnixLegacyTitle()
        {
            TitleType = -1;
            TitleText = "";
            Subtitle  = "";
        }

        private int    titleTypeField;
        private string titleTextField;
        private string subtitleField;

        /// <remarks/>
        public int TitleType
        {
            get
            {
                return this.titleTypeField;
            }
            set
            {
                this.titleTypeField = value;
            }
        }

        /// <remarks/>
        public string TitleText
        {
            get
            {
                return this.titleTextField;
            }
            set
            {
                this.titleTextField = value;
            }
        }

        /// <remarks/>
        public string Subtitle
        {
            get
            {
                return this.subtitleField;
            }
            set
            {
                this.subtitleField = value;
            }
        }
    }
}
