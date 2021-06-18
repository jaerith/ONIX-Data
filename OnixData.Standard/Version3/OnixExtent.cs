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
        #region CONSTANTS

        public const int CONST_EXT_TYPE_MAIN_PPG_CNT = 0;
        public const int CONST_EXT_TYPE_WORD_NUM     = 2;
        public const int CONST_EXT_TYPE_FT_PG_CNT    = 3;
        public const int CONST_EXT_TYPE_BK_PG_CT     = 4;
        public const int CONST_EXT_TYPE_TOTAL_PG_CT  = 5;
        public const int CONST_EXT_TYPE_PROD_PG_CT   = 6;
        public const int CONST_EXT_TYPE_ABS_PG_CT    = 7;
        public const int CONST_EXT_TYPE_PT_CRT_PG_CT = 8;
        public const int CONST_EXT_TYPE_DUR_TIME     = 9;
        public const int CONST_EXT_TYPE_PG_CT_DG_PRD = 10;
        public const int CONST_EXT_TYPE_CNT_PG_CT    = 11;
        public const int CONST_EXT_TYPE_FILESIZE     = 22;

        public const int CONST_UNIT_TYPE_WORDS   = 2;
        public const int CONST_UNIT_TYPE_PAGES   = 3;
        public const int CONST_UNIT_TYPE_HOURS   = 4;
        public const int CONST_UNIT_TYPE_MINUTES = 5;
        public const int CONST_UNIT_TYPE_SECONDS = 6;
        public const int CONST_UNIT_TYPE_BYTES   = 17;
        public const int CONST_UNIT_TYPE_KBYTES  = 18;
        public const int CONST_UNIT_TYPE_MBYTES  = 19;

        public readonly int[] CONST_SOUGHT_PAGE_COUNT_TYPES
            = {
                CONST_EXT_TYPE_MAIN_PPG_CNT, CONST_EXT_TYPE_TOTAL_PG_CT, CONST_EXT_TYPE_PROD_PG_CT,
                CONST_EXT_TYPE_ABS_PG_CT, CONST_EXT_TYPE_PT_CRT_PG_CT, CONST_EXT_TYPE_PG_CT_DG_PRD,
                CONST_EXT_TYPE_CNT_PG_CT
              };

        public readonly int[] CONST_SOUGHT_EXTENT_TYPES
            = {
                CONST_EXT_TYPE_WORD_NUM, CONST_EXT_TYPE_DUR_TIME,
                CONST_EXT_TYPE_FILESIZE
              };

        #endregion

        public OnixExtent()
        {
            ExtentType  = ExtentUnit = -1;
            ExtentValue = "";
        }

        private int    extentTypeField;
        private string extentValueField;
        private int    extentUnitField;

        #region Helper Methods

        public bool HasSoughtPageCountType()
        {
            return CONST_SOUGHT_PAGE_COUNT_TYPES.Contains(this.ExtentType);
        }

        public bool HasSoughtExtentTypeCode()
        {
            return CONST_SOUGHT_EXTENT_TYPES.Contains(this.ExtentType);
        }

        #endregion

        #region Helper Methods

        public int ExtentValueNum
        {
            get
            {
                int nExtValue = -1;

                if (!String.IsNullOrEmpty(ExtentValue))
                    Int32.TryParse(ExtentValue, out nExtValue);

                return nExtValue;
            }
        }

        #endregion

        #region Reference Tags

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
        public string ExtentValue
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b218
        {
            get { return ExtentType; }
            set { ExtentType = value; }
        }

        /// <remarks/>
        public string b219
        {
            get { return ExtentValue; }
            set { ExtentValue = value; }
        }

        /// <remarks/>
        public int b220
        {
            get { return ExtentUnit; }
            set { ExtentUnit = value; }
        }

        #endregion
    }
}
