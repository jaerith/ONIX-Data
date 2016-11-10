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
            TitleType    = -1;
            TitleElement = new OnixTitleElement();
        }

        private int              titleTypeField;
        private OnixTitleElement titleElementField;

        #region Reference Tags

        /// <remarks/>
        public int TitleType
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
        public int b202
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
