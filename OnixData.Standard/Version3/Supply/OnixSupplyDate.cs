using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Supply
{
     /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixSupplyDate
    {
        #region CONSTANTS

        public const int CONST_SUPPL_DT_TYPE_EMBARGO = 2;
        public const int CONST_SUPPL_DT_TYPE_AVAIL   = 8;
        public const int CONST_SUPPL_ID_TYPE_LDFR    = 18;
        public const int CONST_SUPPL_ID_TYPE_RESV    = 25;
        public const int CONST_SUPPL_ID_TYPE_LAST_RD = 29;
        public const int CONST_SUPPL_ID_TYPE_LAST_TM = 30;
        public const int CONST_SUPPL_ID_TYPE_WAREHS  = 34;
        public const int CONST_SUPPL_ID_TYPE_SUPL_ST = 50;
        public const int CONST_SUPPL_ID_TYPE_SUPL_ED = 51;

        #endregion

        public OnixSupplyDate()
        {
            SupplyDateRole = DateFormat = "";
        }

        private string supplyDateRoleField;
        private string dateFormatField;
        private string dateValueField;

        #region Helper Methods

        public int SupplyDateRoleNum
        {
            get
            {
                int nDtRoleNum = -1;

                if (!String.IsNullOrEmpty(SupplyDateRole))
                    Int32.TryParse(SupplyDateRole, out nDtRoleNum);

                return nDtRoleNum;
            }
        }

        public bool IsLasteDateForReturns()
        {
            return (SupplyDateRoleNum == CONST_SUPPL_ID_TYPE_LDFR);
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string SupplyDateRole
        {
            get
            {
                return this.supplyDateRoleField;
            }
            set
            {
                this.supplyDateRoleField = value;
            }
        }

        /// <remarks/>
        public string DateFormat
        {
            get
            {
                return this.dateFormatField;
            }
            set
            {
                this.dateFormatField = value;
            }
        }

        /// <remarks/>
        public string Date
        {
            get
            {
                return this.dateValueField;
            }
            set
            {
                this.dateValueField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x461
        {
            get { return SupplyDateRole; }
            set { SupplyDateRole = value; }
        }

        /// <remarks/>
        public string j260
        {
            get { return DateFormat; }
            set { DateFormat = value; }
        }

        /// <remarks/>
        public string b306
        {
            get { return Date; }
            set { Date = value; }
        }

        #endregion
    }
}
