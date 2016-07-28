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
        public OnixTitleDetail()
        {
            TitleType    = -1;
            TitleElement = new OnixTitleElement();
        }

        private int              titleTypeField;
        private OnixTitleElement titleElementField;

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
    }
}
