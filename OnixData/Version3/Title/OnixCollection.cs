using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Title
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixCollection
    {
        #region CONSTANTS

        public const int CONST_COLL_TYPE_SERIES = 10;
        public const int CONST_COLL_TYPE_AGGR   = 20;

        #endregion

        public OnixCollection()
        {
            CollectionType = -1;
            TitleDetail    = new OnixTitleDetail();
        }

        private int             collectionTypeField;
        private OnixTitleDetail titleDetailField;

        #region Reference Tags

        /// <remarks/>
        public int CollectionType
        {
            get
            {
                return this.collectionTypeField;
            }
            set
            {
                this.collectionTypeField = value;
            }
        }

        /// <remarks/>
        public OnixTitleDetail TitleDetail
        {
            get
            {
                return this.titleDetailField;
            }
            set
            {
                this.titleDetailField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x329
        {
            get { return CollectionType; }
            set { CollectionType = value; }
        }

        /// <remarks/>
        public OnixTitleDetail titledetail
        {
            get { return TitleDetail; }
            set { TitleDetail = value; }
        }

        #endregion
    }
}
