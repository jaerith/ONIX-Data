using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Related
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixWorkIdentifier
    {
        public OnixWorkIdentifier()
        {
            WorkIDType = -1;
            IDTypeName = IDValue = "";
        }

        private int    workIDTypeField;
        private string idTypeNameField;
        private string idValueField;

        /// <remarks/>
        public int WorkIDType
        {
            get
            {
                return this.workIDTypeField;
            }
            set
            {
                this.workIDTypeField = value;
            }
        }

        /// <remarks/>
        public string IDTypeName
        {
            get
            {
                return this.idTypeNameField;
            }
            set
            {
                this.idTypeNameField = value;
            }
        }

        /// <remarks/>
        public string IDValue
        {
            get
            {
                return this.idValueField;
            }
            set
            {
                this.idValueField = value;
            }
        }
    }
}
