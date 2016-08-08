using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyImprint
    {
        #region CONSTANTS
        public const int CONST_IMPRINT_ROLE_PROP = 2;
        #endregion

        public OnixLegacyImprint()
        {
            NameCodeType     = -1;
            NameCodeTypeName = "";
            NameCodeValue    = 0;
            ImprintName      = "";
        }

        private int    nameCodeTypeField;
        private string nameCodeTypeNameField;
        private uint   nameCodeValueField;
        private string imprintNameField;

        #region Reference Tags

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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b241
        {
            get { return NameCodeType; }
            set { NameCodeType = value; }
        }

        /// <remarks/>
        public string b242
        {
            get { return NameCodeTypeName; }
            set { NameCodeTypeName = value; }
        }

        /// <remarks/>
        public uint b243
        {
            get { return NameCodeValue; }
            set { NameCodeValue = value; }
        }

        /// <remarks/>
        public string b079
        {
            get { return ImprintName; }
            set { ImprintName = value; }
        }

        #endregion
    }
}
