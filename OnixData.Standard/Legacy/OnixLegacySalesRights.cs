using System;
using System.Collections.Generic;
using System.Linq;

namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacySalesRights
    {
        #region CONSTANTS

        public const char CONST_LIST_DELIM = ' ';

        public const int  CONST_MISSING_NUM_VALUE = -1;

        public const int CONST_SR_TYPE_FOR_SALE_WITH_EXCL_RIGHTS    = 1;
        public const int CONST_SR_TYPE_FOR_SALE_WITH_NONEXCL_RIGHTS = 2;
        public const int CONST_SR_TYPE_NOT_FOR_SALE                 = 3;

        #endregion

        public OnixLegacySalesRights()
        {
            salesRightsTypeField = "";
            rightsCountryField   = shortRightsCountryField = null;
            rightsTerritoryField = shortRightsTerritoryField = null;
            rightsTerritoryList  = new List<string>();
            rightsCountryList    = new List<string>();
        }

        private string       salesRightsTypeField;

        private string[]     rightsCountryField;
        private string[]     shortRightsCountryField;    
        private List<string> rightsCountryList;

        private string[]     rightsTerritoryField;
        private string[]     shortRightsTerritoryField;
        private List<string> rightsTerritoryList;

        #region Helpers

        public string[] OnixRightsCountryField
        {
            get
            {
                string[] asOnixRightsCountryList = new string[0];

                if ((this.shortRightsCountryField != null) && (this.shortRightsCountryField.Length > 0))
                    asOnixRightsCountryList = this.shortRightsCountryField;
                else if ((this.rightsCountryField != null) && (this.rightsCountryField.Length > 0))
                    asOnixRightsCountryList = this.rightsCountryField;

                return asOnixRightsCountryList;
            }
        }

        public string[] OnixRightsTerritoryField
        {
            get
            {
                string[] asOnixRightsTerritoryList = new string[0];

                if ((this.shortRightsTerritoryField != null) && (this.shortRightsTerritoryField.Length > 0))
                    asOnixRightsTerritoryList = this.shortRightsTerritoryField;
                else if ((this.rightsTerritoryField != null) && (this.rightsTerritoryField.Length > 0))
                    asOnixRightsTerritoryList = this.rightsTerritoryField;

                return asOnixRightsTerritoryList;
            }
        }

        public List<string> RightsCountryList
        {
            get
            {
                if (this.rightsCountryList.Count() <= 0)
                {
                    if (this.OnixRightsCountryField.Count() > 0)
                    {
                        foreach (string sTmpCountryMiniList in this.OnixRightsCountryField)
                        {
                            List<string> TmpCountryList = new List<string>();

                            if (sTmpCountryMiniList.Contains(CONST_LIST_DELIM))
                                TmpCountryList = new List<string>(sTmpCountryMiniList.Split(CONST_LIST_DELIM));
                            else
                                TmpCountryList = new List<string>() { sTmpCountryMiniList };

                            this.rightsCountryList.AddRange(TmpCountryList);
                        }
                    }
                }

                return this.rightsCountryList;
            }
        }

        public List<string> RightsTerritoryList
        {
            get
            {
                if (this.rightsTerritoryList.Count() <= 0)
                {
                    if (this.OnixRightsTerritoryField.Count() > 0)
                    {
                        foreach (string sTmpTerritoryMiniList in this.OnixRightsTerritoryField)
                        {
                            List<string> TmpTerritoryList = new List<string>();

                            if (sTmpTerritoryMiniList.Contains(CONST_LIST_DELIM))
                                TmpTerritoryList = new List<string>(sTmpTerritoryMiniList.Split(CONST_LIST_DELIM));
                            else
                                TmpTerritoryList = new List<string>() { sTmpTerritoryMiniList };

                            this.rightsTerritoryList.AddRange(TmpTerritoryList);
                        }
                    }
                }

                return this.rightsTerritoryList;
            }
        }

        public int SalesRightsTypeNum
        {
            get
            {
                int nSRTypeNum = CONST_MISSING_NUM_VALUE;

                if (!String.IsNullOrEmpty(this.salesRightsTypeField))
                    Int32.TryParse(this.salesRightsTypeField, out nSRTypeNum);

                return nSRTypeNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string SalesRightsType
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
        [System.Xml.Serialization.XmlElementAttribute("RightsCountry")]
        public string[] RightsCountry
        {
            get
            {
                return this.rightsCountryField;
            }
            set
            {
                this.rightsCountryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RightsTerritory")]
        public string[] RightsTerritory
        {
            get
            {
                return this.rightsTerritoryField;
            }
            set
            {
                this.rightsTerritoryField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b089
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

        [System.Xml.Serialization.XmlElementAttribute("b090")]
        public string[] b090
        {
            get
            {
                return shortRightsCountryField;
            }
            set
            {
                shortRightsCountryField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("b388")]
        public string[] b388
        {
            get
            {
                return shortRightsTerritoryField;
            }
            set
            {
                shortRightsTerritoryField = value;
            }
        }

        #endregion
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyNotForSale
    {
        #region CONSTANTS

        private const char CONST_LIST_DELIM = ' ';

        #endregion

        public OnixLegacyNotForSale()
        {
            rightsTerritoryField = rightsCountryField = "";
            rightsTerritoryList  = rightsCountryList = new List<string>();
        }

        private string       rightsCountryField;
        private List<string> rightsCountryList;
        private string       rightsTerritoryField;
        private List<string> rightsTerritoryList;

        #region Reference Tags

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
