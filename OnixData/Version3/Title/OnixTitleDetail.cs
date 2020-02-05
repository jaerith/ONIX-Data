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
            TitleType    = "";
            TitleElement = new OnixTitleElement();
        }

        private string              titleTypeField;
        private OnixTitleElement titleElementField;

        #region Helper Methods

        public string FullName
        {
            get
            {
                string sFullName = "";

                if ((TitleElement != null) && !String.IsNullOrEmpty(TitleElement.Title))
                    sFullName = TitleElement.Title;

                return sFullName;
            }
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

            if (TitleElement != null)
                bIsCollName = (TitleElement.TitleElementLevel == OnixTitleElement.CONST_TITLE_TYPE_COLLECTION);

            return bIsCollName;
        }

        public bool IsProductName()
        {
            bool bIsProdName = false;

            if (TitleElement != null)
                bIsProdName = (TitleElement.TitleElementLevel == OnixTitleElement.CONST_TITLE_TYPE_PRODUCT);

            return bIsProdName;
        }

        public bool IsSubCollectionName()
        {
            bool bIsSubCollName = false;

            if (TitleElement != null)
                bIsSubCollName = (TitleElement.TitleElementLevel == OnixTitleElement.CONST_TITLE_TYPE_SUB_COLL);

            return bIsSubCollName;
        }

        public bool IsSubItemName()
        {
            bool bIsSubItemName = false;

            if (TitleElement != null)
                bIsSubItemName = (TitleElement.TitleElementLevel == OnixTitleElement.CONST_TITLE_TYPE_SUB_ITEM);

            return bIsSubItemName;
        }

        public string Prefix
        {
            get
            {
                string sPrefix = "";

                if ((TitleElement != null) && !String.IsNullOrEmpty(TitleElement.TitlePrefix))
                    sPrefix = TitleElement.TitlePrefix;

                return sPrefix;
            }
        }

        public string TitleWithoutPrefix
        {
            get
            {
                string sTitleWithoutPrefix = "";

                if ((TitleElement != null) && !String.IsNullOrEmpty(TitleElement.TitleWithoutPrefix))
                    sTitleWithoutPrefix = TitleElement.TitleWithoutPrefix;

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
            get
            {
                return this.titleTypeField;
            }
            set
            {
                this.titleTypeField = value;
            }
        }

        /// <remarks/>
        public OnixTitleElement TitleElement
        {
            get
            {
                return this.titleElementField;
            }
            set
            {
                this.titleElementField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b202
        {
            get { return TitleType; }
            set { TitleType = value; }
        }

        /// <remarks/>
        public OnixTitleElement titleelement
        {
            get { return TitleElement; }
            set { TitleElement = value; }
        }

        #endregion
    }
}
