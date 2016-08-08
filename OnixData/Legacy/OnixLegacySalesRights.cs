using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacySalesRights
    {
        #region CONSTANTS

        private const char CONST_LIST_DELIM = ' ';

        public const int CONST_SR_TYPE_FOR_SALE_WITH_EXCL_RIGHTS    = 1;
        public const int CONST_SR_TYPE_FOR_SALE_WITH_NONEXCL_RIGHTS = 2;
        public const int CONST_SR_TYPE_NOT_FOR_SALE                 = 3;

        #endregion

        public OnixLegacySalesRights()
        {
            salesRightsTypeField = -1;
            rightsTerritoryField = rightsCountryField = "";
            rightsTerritoryList  = rightsCountryList  = new List<string>();
        }

        private int          salesRightsTypeField;
        private string       rightsCountryField;
        private List<string> rightsCountryList;
        private string       rightsTerritoryField;
        private List<string> rightsTerritoryList;

        #region Reference Tags

        /// <remarks/>
        public int SalesRightsType
        {
            get
            {
                return this.salesRightsTypeField;
            }
            set
            {
                this.salesRightsTypeField = value;
            }
        }

        /// <remarks/>
        public string RightsCountry
        {
            get
            {
                return this.rightsCountryField;
            }
            set
            {
                this.rightsCountryField = value;

                if (!String.IsNullOrEmpty(this.rightsCountryField))
                {
                    if (this.rightsCountryField.Contains(CONST_LIST_DELIM))
                        this.rightsCountryList = new List<string>(this.rightsCountryField.Split(CONST_LIST_DELIM));
                    else
                        this.rightsCountryList = new List<string>() { this.rightsCountryField };
                }
            }
        }

        public List<string> RightsCountryList
        {
            get
            {
                return this.rightsCountryList;
            }
        }

        /// <remarks/>
        public string RightsTerritory
        {
            get
            {
                return this.rightsTerritoryField;
            }
            set
            {
                this.rightsTerritoryField = value;

                if (!String.IsNullOrEmpty(this.rightsTerritoryField))
                {
                    if (this.rightsTerritoryField.Contains(CONST_LIST_DELIM))
                        this.rightsTerritoryList = new List<string>(this.rightsTerritoryField.Split(CONST_LIST_DELIM));
                    else
                        this.rightsTerritoryList = new List<string>() { this.rightsTerritoryField };
                }
            }
        }

        public List<string> RightsTerritoryList
        {
            get
            {
                return this.rightsTerritoryList;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b089
        {
            get
            {
                return SalesRightsType;
            }
            set
            {
                SalesRightsType = value;
            }
        }

        /// <remarks/>
        public string b090
        {
            get
            {
                return RightsCountry;
            }
            set
            {
                RightsCountry = value;
            }
        }

        /// <remarks/>
        public string b388
        {
            get
            {
                return RightsTerritory;
            }
            set
            {
                RightsTerritory = value;
            }
        }

        #endregion
    }
}
