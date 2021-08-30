using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Title
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixTitleDetail
    {
        #region CONSTANTS

        public const int CONST_TITLE_TYPE_UN_TITLE   = 0;
        public const int CONST_TITLE_TYPE_DIST_TITLE = 1;

        #endregion

        public OnixTitleDetail()
        {
            TitleType         = "";
            titleElementField = shortTitleElementField = new OnixTitleElement[0];
        }

        private string             titleTypeField;
        private OnixTitleElement[] titleElementField;
        private OnixTitleElement[] shortTitleElementField;

        #region ONIX Lists

        public OnixTitleElement[] OnixTitleElementList
        {
            get
            {
                OnixTitleElement[] TitleElemList = null;

                if (this.titleElementField != null)
                    TitleElemList = this.titleElementField;
                else if (this.shortTitleElementField != null)
                    TitleElemList = this.shortTitleElementField;
                else
                    TitleElemList = new OnixTitleElement[0];

                return TitleElemList;
            }
        }

        #endregion

        #region Helper Methods

        public string AssembledSeriesName
        {
            get
            {
                StringBuilder SeriesNameBuilder = new StringBuilder();

                var SeriesNameParts =
                    this.OnixTitleElementList.Where(x => x.IsQualifiedSeriesType()).OrderBy(x => x.TitleElementLevel);

                if (SeriesNameParts != null)
                {
                    foreach (var TitleElement in SeriesNameParts)
                    {
                        if (SeriesNameBuilder.Length > 0)
                            SeriesNameBuilder.Append(": ");

                        SeriesNameBuilder.Append(TitleElement.Title);
                    }
                }

                if (SeriesNameBuilder.Length <= 0)
                {
                    var MasterBrandTitle =
                        this.OnixTitleElementList.Where(x => x.IsMasterBrandType()).FirstOrDefault();

                    if ((MasterBrandTitle != null) && !String.IsNullOrEmpty(MasterBrandTitle.Title))
                    {
                        SeriesNameBuilder.Append(MasterBrandTitle.Title);
                    }
                }

                return SeriesNameBuilder.ToString();
            }
        }

        public OnixTitleElement FirstCollectionTitleElement
        {
            get
            {
                OnixTitleElement CollTitleElement = null;

                if ((OnixTitleElementList != null) && (OnixTitleElementList.Length > 0))
                {
                    var FoundElement =
                        OnixTitleElementList.Where(x => x.IsElementLevelCollection()).FirstOrDefault();

                    if (FoundElement != null)
                        CollTitleElement = FoundElement;
                }

                return CollTitleElement;
            }
        }

        public OnixTitleElement FirstProductTitleElement
        {
            get
            {
                OnixTitleElement PrdTitleElement = null;

                if ((OnixTitleElementList != null) && (OnixTitleElementList.Length > 0))
                {
                    var FoundElement =
                        OnixTitleElementList.Where(x => x.IsElementLevelProduct()).FirstOrDefault();

                    if (FoundElement != null)
                        PrdTitleElement = FoundElement;
                }

                return PrdTitleElement;
            }
        }

        public OnixTitleElement FirstTitleElement
        {
            get
            {
                OnixTitleElement FirstElement = null;

                if ((OnixTitleElementList != null) && (OnixTitleElementList.Length > 0))
                    FirstElement = OnixTitleElementList[0];

                return FirstElement;
            }
        }

        public string FullName
        {
            get
            {
                string sFullName = "";

                if ((FirstTitleElement != null) && !String.IsNullOrEmpty(FirstTitleElement.Title))
                    sFullName = FirstTitleElement.Title;

                return sFullName;
            }
        }

        public bool HasDistinctiveTitle()
        {
            bool bHasQualifiedTitle = false;

            if (TitleTypeNum > 0)
            {
                if (TitleTypeNum == CONST_TITLE_TYPE_DIST_TITLE)
                    bHasQualifiedTitle = true;
            }

            return bHasQualifiedTitle;
        }

        public bool HasQualifiedTitle()
        {
            bool bHasQualifiedTitle = false;

            if (TitleTypeNum > 0)
            {
                if ((TitleTypeNum == CONST_TITLE_TYPE_UN_TITLE) || (TitleTypeNum == CONST_TITLE_TYPE_DIST_TITLE))
                    bHasQualifiedTitle = true;
            }

            return bHasQualifiedTitle;
        }

        public bool IsCollectionName()
        {
            bool bIsCollName = false;

            if (FirstTitleElement != null)
                bIsCollName = (FirstTitleElement.TitleElementLevel == OnixTitleElement.CONST_TITLE_TYPE_COLLECTION);

            return bIsCollName;
        }

        public bool IsProductName()
        {
            bool bIsProdName = false;

            if (FirstTitleElement != null)
                bIsProdName = (FirstTitleElement.TitleElementLevel == OnixTitleElement.CONST_TITLE_TYPE_PRODUCT);

            return bIsProdName;
        }

        public bool IsSubCollectionName()
        {
            bool bIsSubCollName = false;

            if (FirstTitleElement != null)
                bIsSubCollName = (FirstTitleElement.TitleElementLevel == OnixTitleElement.CONST_TITLE_TYPE_SUB_COLL);

            return bIsSubCollName;
        }

        public bool IsSubItemName()
        {
            bool bIsSubItemName = false;

            if (FirstTitleElement != null)
                bIsSubItemName = (FirstTitleElement.TitleElementLevel == OnixTitleElement.CONST_TITLE_TYPE_SUB_ITEM);

            return bIsSubItemName;
        }

        public string Prefix
        {
            get
            {
                string sPrefix = "";

                if ((FirstTitleElement != null) && !String.IsNullOrEmpty(FirstTitleElement.TitlePrefix))
                    sPrefix = FirstTitleElement.TitlePrefix;

                return sPrefix;
            }
        }

        public string TitleWithoutPrefix
        {
            get
            {
                string sTitleWithoutPrefix = "";

                if ((FirstTitleElement != null) && !String.IsNullOrEmpty(FirstTitleElement.TitleWithoutPrefix))
                    sTitleWithoutPrefix = FirstTitleElement.TitleWithoutPrefix;

                return sTitleWithoutPrefix;
            }
        }

        public int TitleTypeNum
        {
            get
            {
                int nTypeNum = -1;

                if (!String.IsNullOrEmpty(TitleType))
                    Int32.TryParse(TitleType, out nTypeNum);

                return nTypeNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string TitleType
        {
            get { return this.titleTypeField; }
            set { this.titleTypeField = value; }
        }

        [System.Xml.Serialization.XmlElementAttribute("TitleElement")]
        public OnixTitleElement[] TitleElement
        {
            get { return this.titleElementField; }
            set { this.titleElementField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b202
        {
            get { return TitleType; }
            set { TitleType = value; }
        }

        [System.Xml.Serialization.XmlElementAttribute("titleelement")]
        /// <remarks/>
        public OnixTitleElement[] titleelement
        {
            get { return this.shortTitleElementField; }
            set { shortTitleElementField = value; }
        }

        #endregion
    }
}