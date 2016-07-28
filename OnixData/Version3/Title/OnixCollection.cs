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
        public OnixCollection()
        {
            CollectionType = -1;
            TitleDetail    = new OnixTitleDetail();
        }

        private int             collectionTypeField;
        private OnixTitleDetail titleDetailField;

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
    }
}
