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

        #region Reference Tags

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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b256
        {
            get { return IllustrationType; }
            set { IllustrationType = value; }
        }

        /// <remarks/>
        public string b361
        {
            get { return IllustrationTypeDescription; }
            set { IllustrationTypeDescription = value; }
        }

        #endregion
    }
}
