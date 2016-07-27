using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyPublisher
    {
        public OnixLegacyPublisher()
        {
            NameCodeType     = -1;
            NameCodeTypeName = "";
            NameCodeValue    = 0;
            PublisherName    = "";
        }

        private int    nameCodeTypeField;
        private string nameCodeTypeNameField;
        private uint   nameCodeValueField;
        private string publisherNameField;

        /// <remarks/>
        public int NameCodeType
        {
            get
            {
                return this.nameCodeTypeField;
            }
            set
            {
                this.nameCodeTypeField = value;
            }
        }

        /// <remarks/>
        public string NameCodeTypeName
        {
            get
            {
                return this.nameCodeTypeNameField;
            }
            set
            {
                this.nameCodeTypeNameField = value;
            }
        }

        /// <remarks/>
        public uint NameCodeValue
        {
            get
            {
                return this.nameCodeValueField;
            }
            set
            {
                this.nameCodeValueField = value;
            }
        }

        /// <remarks/>
        public string PublisherName
        {
            get
            {
                return this.publisherNameField;
            }
            set
            {
                this.publisherNameField = value;
            }
        }
    }
}
