using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyIllustrations
    {
        public OnixLegacyIllustrations()
        {
            IllustrationType            = -1;
            IllustrationTypeDescription = "";
        }

        private int    illustrationTypeField;
        private string illustrationTypeDescriptionField;

        /// <remarks/>
        public int IllustrationType
        {
            get
            {
                return this.illustrationTypeField;
            }
            set
            {
                this.illustrationTypeField = value;
            }
        }

        /// <remarks/>
        public string IllustrationTypeDescription
        {
            get
            {
                return this.illustrationTypeDescriptionField;
            }
            set
            {
                this.illustrationTypeDescriptionField = value;
            }
        }
    }
}
