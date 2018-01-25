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
            IllustrationType            = "";
            IllustrationTypeDescription = "";
        }

        private string illustrationTypeField;
        private string illustrationTypeDescriptionField;

        #region Reference Tags

        /// <remarks/>
        public string IllustrationType
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

        public uint IllustrationTypeNum
        {
            get
            {
                uint nIllTypeNum = 0;

                if (!String.IsNullOrEmpty(IllustrationType))
                    UInt32.TryParse(IllustrationType, out nIllTypeNum);

                return nIllTypeNum;
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
        public string b256
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
