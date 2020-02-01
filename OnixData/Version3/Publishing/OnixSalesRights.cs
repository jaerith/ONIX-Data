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

        public const int  CONST_MISSING_NUM_VALUE = -1;
        public const char CONST_LIST_DELIM        = ' ';

        #endregion

        public OnixSalesRights()
        {
            SalesRightsType = "";

            this.rightsCountryIncList = new List<string>();
            this.rightsRegionIncList  = new List<string>();

            Territory = new OnixTerritory();
        }

        private string salesRightsTypeField;

        private List<string> rightsCountryIncList;
        private List<string> rightsRegionIncList;

        private OnixTerritory territoryField;

        #region Helper Methods

        public List<string> RightsIncludedCountryList
        {
            get
            {
                if (this.rightsCountryIncList.Count() <= 0)
                {
                    if (this.Territory.CountriesIncluded.Count() > 0)
                    {
                        this.rightsCountryIncList.AddRange(this.Territory.CountriesIncluded.Split(CONST_LIST_DELIM));
                    }
                }

                return this.rightsCountryIncList;
            }
        }

        public List<string> RightsIncludedRegionList
        {
            get
            {
                if (this.rightsRegionIncList.Count() <= 0)
                {
                    if (this.Territory.RegionsIncluded.Count() > 0)
                    {
                        this.rightsRegionIncList.AddRange(this.Territory.RegionsIncluded.Split(CONST_LIST_DELIM));
                    }
                }

                return this.rightsRegionIncList;
            }
        }

        public int SalesRightTypeNum
        {
            get
            {
                int nTypeNum = CONST_MISSING_NUM_VALUE;

                if (!String.IsNullOrEmpty(SalesRightsType))
                    Int32.TryParse(SalesRightsType, out nTypeNum);

                return nTypeNum;
            }
        }

        #endregion

        #region Reference Tags

        public string SalesRightsType
        {
            get { return this.salesRightsTypeField; }
            set { this.salesRightsTypeField = value; }
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
