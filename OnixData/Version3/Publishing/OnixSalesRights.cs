using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Publishing
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixSalesRights
    {
        #region CONSTANTS

        public const string CONST_SALES_WITH_EXCL_RIGHTS     = "01";
        public const string CONST_SALES_WITH_NON_EXCL_RIGHTS = "02";
        public const string CONST_NOT_FOR_SALE               = "03";

        #endregion

        public OnixSalesRights()
        {
            SalesRightsType = "";

            Territory = new OnixTerritory();
        }

        private string salesRightsField;

        private OnixTerritory territoryField;

        #region Reference Tags

        public string SalesRightsType
        {
            get { return this.salesRightsField; }
            set { this.salesRightsField = value; }
        }

        public OnixTerritory Territory
        {
            get { return this.territoryField; }
            set { this.territoryField = value; }
        }

        #endregion

        #region Short Tags

        public string b089
        {
            get { return SalesRightsType; }
            set { SalesRightsType = value; }
        }

        public OnixTerritory territory
        {
            get { return Territory; }
            set { Territory = value; }
        }

        #endregion
    }
}
