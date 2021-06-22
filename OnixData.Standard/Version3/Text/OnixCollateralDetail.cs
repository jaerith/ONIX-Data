using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Text
{
    public partial class OnixCollateralDetail
    {
        public OnixCollateralDetail()
        {
            textContentField = shortTextContentField = new OnixTextContent[0];
            contentDateField = shortContentDateField = new OnixContentDate[0];
        }

        private OnixTextContent[] textContentField;
        private OnixTextContent[] shortTextContentField;
        private OnixContentDate[] contentDateField;
        private OnixContentDate[] shortContentDateField;

        #region ONIX Lists

        public OnixTextContent[] OnixTextContentList
        {
            get
            {
                OnixTextContent[] TextContents = null;

                if (this.textContentField != null)
                    TextContents = this.textContentField;
                else if (this.shortTextContentField != null)
                    TextContents = this.shortTextContentField;
                else
                    TextContents = new OnixTextContent[0];

                return TextContents;
            }
        }

        public OnixContentDate[] OnixContentDateList
        {
            get
            {
                OnixContentDate[] ContentDates = null;

                if (this.contentDateField != null)
                    ContentDates = this.contentDateField;
                else if (this.shortContentDateField != null)
                    ContentDates = this.shortContentDateField;
                else
                    ContentDates = new OnixContentDate[0];

                return ContentDates;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TextContent", IsNullable = false)]
        public OnixTextContent[] TextContent
        {
            get
            {
                return this.textContentField;
            }
            set
            {
                this.textContentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ContentDate", IsNullable = false)]
        public OnixContentDate[] ContentDate
        {
            get
            {
                return this.contentDateField;
            }
            set
            {
                this.contentDateField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("textcontent", IsNullable = false)]
        public OnixTextContent[] textcontent
        {
            get
            {
                return this.shortTextContentField;
            }
            set
            {
                this.shortTextContentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contentdate", IsNullable = false)]
        public OnixContentDate[] contentdate
        {
            get
            {
                return this.shortContentDateField;
            }
            set
            {
                this.shortContentDateField = value;
            }
        }

        #endregion
    }

}
