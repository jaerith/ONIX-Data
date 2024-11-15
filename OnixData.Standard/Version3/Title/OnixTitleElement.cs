using System;
using System.Linq;

namespace OnixData.Standard.Version3.Title
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixTitleElement
    {
        #region CONSTANTS

        public const int CONST_TITLE_TYPE_PRODUCT      = 1;
        public const int CONST_TITLE_TYPE_COLLECTION   = 2;
        public const int CONST_TITLE_TYPE_SUB_COLL     = 3;
        public const int CONST_TITLE_TYPE_SUB_ITEM     = 4;
        public const int CONST_TITLE_TYPE_MASTER_BRAND = 5;
        public const int CONST_TITLE_TYPE_SUB_SUB_COLL = 6;

        public readonly int[] CONST_TITLE_TYPES_SERIES =
            new[] { CONST_TITLE_TYPE_COLLECTION, CONST_TITLE_TYPE_SUB_COLL, CONST_TITLE_TYPE_SUB_SUB_COLL };

        #endregion

        public OnixTitleElement()
        {
            partNumberField   = "";
            TitleElementLevel = -1;
            TitleText         = TitlePrefix = TitleWithoutPrefix = "";
        }

        private int    titleElementLevelField;
        private string partNumberField;
        private string titleTextField;
        private string titlePrefixField;
        private string titleWithoutPrefixField;
        private string subtitleField;

        #region Helper Methods

        public int GetPartNum()
        {
            int nPartNum = -1;

            if (!String.IsNullOrEmpty(PartNumber))
                Int32.TryParse(PartNumber, out nPartNum);

            return nPartNum;
        }

        public bool IsElementLevelCollection()
        {
            return (titleElementLevelField == CONST_TITLE_TYPE_COLLECTION);
        }

        public bool IsElementLevelProduct()
        {
            return (titleElementLevelField == CONST_TITLE_TYPE_PRODUCT);
        }

        public bool IsMasterBrandType()
        {
            return (TitleElementLevel == CONST_TITLE_TYPE_MASTER_BRAND);
        }

        public bool IsQualifiedSeriesType()
        {
            return (CONST_TITLE_TYPES_SERIES.Contains(TitleElementLevel));
        }

        public string Title
        {
            get
            {
                string sTitle = "";

                if (!String.IsNullOrEmpty(TitleText))
                    sTitle = TitleText;
                else if (!String.IsNullOrEmpty(TitleWithoutPrefix))
                    sTitle = TitlePrefix + " " + TitleWithoutPrefix;

                sTitle = sTitle.Trim();

                return sTitle;
            }
        }

        #endregion

        #region Reference Tags

        public string PartNumber
        {
            get
            {
                return partNumberField;
            }
            set
            {
                partNumberField = value;
            }
        }

        /// <remarks/>
        public int TitleElementLevel
        {
            get
            {
                return this.titleElementLevelField;
            }
            set
            {
                this.titleElementLevelField = value;
            }
        }

        /// <remarks/>
        public string TitleText
        {
            get
            {
                return this.titleTextField;
            }
            set
            {
                this.titleTextField = value;
            }
        }

        public string Subtitle
        {
            get
            {
                return this.subtitleField;
            }
            set
            {
                this.subtitleField = value;
            }
        }

        /// <remarks/>
        public string TitlePrefix
        {
            get
            {
                return this.titlePrefixField;
            }
            set
            {
                this.titlePrefixField = value;
            }
        }

        /// <remarks/>
        public string TitleWithoutPrefix
        {
            get
            {
                return this.titleWithoutPrefixField;
            }
            set
            {
                this.titleWithoutPrefixField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x409
        {
            get
            {
                return TitleElementLevel;
            }
            set
            {
                TitleElementLevel = value;
            }
        }

        /// <remarks/>
        public string x410
        {
            get
            {
                return PartNumber;
            }
            set
            {
                PartNumber = value;
            }
        }        

        /// <remarks/>
        public string b203
        {
            get
            {
                return TitleText;
            }
            set
            {
                TitleText = value;
            }
        }

        /// <remarks/>
        public string b029
        {
            get
            {
                return Subtitle;
            }
            set
            {
                Subtitle = value;
            }
        }

        /// <remarks/>
        public string b030
        {
            get
            {
                return TitlePrefix;
            }
            set
            {
                TitlePrefix = value;
            }
        }

        /// <remarks/>
        public string b031
        {
            get
            {
                return TitleWithoutPrefix;
            }
            set
            {
                TitleWithoutPrefix = value;
            }
        }

        #endregion
    }}
