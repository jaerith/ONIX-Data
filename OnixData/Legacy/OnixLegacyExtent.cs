using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyExtent
    {
        #region CONSTANTS

        public const int CONST_EXTENT_TYPE_NUM_OF_WORDS = 2;
        public const int CONST_EXTENT_TYPE_DURATION     = 9;
        public const int CONST_EXTENT_TYPE_FILESIZE     = 22;

        public const int CONST_EXTENT_UNIT_WORDS    = 2;
        public const int CONST_EXTENT_UNIT_HOURS    = 9;
        public const int CONST_EXTENT_TYPE_MINUTES  = 5;
        public const int CONST_EXTENT_UNIT_SECONDS  = 2;
        public const int CONST_EXTENT_UNIT_HHH      = 14;
        public const int CONST_EXTENT_TYPE_HHHMM    = 15;
        public const int CONST_EXTENT_UNIT_HHHMMSS  = 16;
        public const int CONST_EXTENT_TYPE_KBytes   = 18;
        public const int CONST_EXTENT_UNIT_MBytes   = 19;

        #endregion

        public OnixLegacyExtent()
        {
            extentTypeField  = extentUnitField = -1;
            extentValueField = 0;
        }

        private int    extentTypeField;
        private double extentValueField;
        private int    extentUnitField;

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
        public double ExtentValue
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
