using System;
using System.Collections.Generic;
using System.Linq;

namespace OnixData.Standard.Version3.Publishing
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixSalesRights
    {
        #region CONSTANTS

        public const int CONST_SALES_WITH_EXCL_RIGHTS         = 1;
        public const int CONST_SALES_WITH_NON_EXCL_RIGHTS     = 2;
        public const int CONST_NOT_FOR_SALE_GENERAL           = 3;
        public const int CONST_NOT_FOR_SALE_PUB_EXL_RIGHTS    = 4;
        public const int CONST_NOT_FOR_SALE_PUB_NO_EXL_RIGHTS = 5;
        public const int CONST_NOT_FOR_SALE_PUB_NO_RIGHTS     = 6;
        public const int CONST_SALES_WITH_EXCL_RIGHTS_SRA     = 7;
        public const int CONST_SALES_WITH_NOEX_RIGHTS_SRA     = 8;

        public const int  CONST_MISSING_NUM_VALUE = -1;
        public const char CONST_LIST_DELIM        = ' ';

        public static readonly int[] SALES_WITH_RIGHTS_COLL =
            new int[] { CONST_SALES_WITH_EXCL_RIGHTS, CONST_SALES_WITH_NON_EXCL_RIGHTS, CONST_SALES_WITH_EXCL_RIGHTS_SRA, CONST_SALES_WITH_NOEX_RIGHTS_SRA };

        public static readonly int[] NO_SALES_RIGHTS_COLL =
            new int[] { CONST_NOT_FOR_SALE_GENERAL, CONST_NOT_FOR_SALE_PUB_EXL_RIGHTS, CONST_NOT_FOR_SALE_PUB_NO_EXL_RIGHTS, CONST_NOT_FOR_SALE_PUB_NO_RIGHTS };

        #endregion

        public OnixSalesRights()
        {
            SalesRightsType = "";

            this.rightsCountryIncList = new List<string>();
            this.rightsRegionIncList  = new List<string>();

            this.rightsCountryExcList = new List<string>();
            this.rightsRegionExcList  = new List<string>();

            Territory = new OnixTerritory();
        }

        private string salesRightsTypeField;

        private List<string> rightsCountryIncList;
        private List<string> rightsRegionIncList;

        private List<string> rightsCountryExcList;
        private List<string> rightsRegionExcList;

        private OnixTerritory territoryField;

        #region Helper Methods

        public bool HasSalesRights
        {
            get 
            {
                return SALES_WITH_RIGHTS_COLL.Contains(SalesRightTypeNum);
            }
        }

        public bool HasNotForSalesRights
        {
            get
            {
                return NO_SALES_RIGHTS_COLL.Contains(SalesRightTypeNum);
            }
        }

        public List<string> RightsExcludedCountryList
        {
            get
            {
                if (this.rightsCountryExcList.Count() <= 0)
                {
                    if (this.Territory.CountriesExcluded.Count() > 0)
                    {
                        this.rightsCountryExcList.AddRange(this.Territory.CountriesExcluded.Split(CONST_LIST_DELIM));
                    }
                }

                return this.rightsCountryExcList;
            }
        }

        public List<string> RightsExcludedRegionList
        {
            get
            {
                if (this.rightsRegionExcList.Count() <= 0)
                {
                    if (this.Territory.RegionsExcluded.Count() > 0)
                    {
                        this.rightsRegionExcList.AddRange(this.Territory.RegionsExcluded.Split(CONST_LIST_DELIM));
                    }
                }

                return this.rightsRegionExcList;
            }
        }

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
