using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixExtent
    {
        public OnixExtent()
        {
            ExtentType = ExtentValue = ExtentUnit = -1;
        }

        private int extentTypeField;
        private int extentValueField;
        private int extentUnitField;

        /// <remarks/>
        public int ExtentType
        {
            get
            {
                return this.extentTypeField;
            }
            set
            {
                this.extentTypeField = value;
            }
        }

        /// <remarks/>
        public int ExtentValue
        {
            get
            {
                return this.extentValueField;
            }
            set
            {
                this.extentValueField = value;
            }
        }

        /// <remarks/>
        public int ExtentUnit
        {
            get
            {
                return this.extentUnitField;
            }
            set
            {
                this.extentUnitField = value;
            }
        }
    }
}
