using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnixData.Standard.Version3.Text;

namespace OnixData.Version3.Text
{
    public partial class OnixCollateralDetail
    {
        public OnixCollateralDetail()
        {
            textContentField = shortTextContentField = Array.Empty<OnixTextContent>();
            contentDateField = shortContentDateField = Array.Empty<OnixContentDate>();
            supportingResourceField = shortSupportingResourceField = Array.Empty<OnixSupportingResource>();
        }

        private OnixTextContent[] textContentField;
        private OnixTextContent[] shortTextContentField;
        
        private OnixContentDate[] contentDateField;
        private OnixContentDate[] shortContentDateField;
        
        private OnixSupportingResource[] supportingResourceField;
        private OnixSupportingResource[] shortSupportingResourceField;

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
        
        public OnixSupportingResource[] OnixSupportingResourceList
        {
            get
            {
                OnixSupportingResource[] supportingResources = null;

                if (this.supportingResourceField != null)
                    supportingResources = this.supportingResourceField;
                else if (this.shortSupportingResourceField != null)
                    supportingResources = this.shortSupportingResourceField;
                else
                    supportingResources = new OnixSupportingResource[0];

                return supportingResources;
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SupportingResource", IsNullable = true)]
        public OnixSupportingResource[] SupportingResource
        {
            get => supportingResourceField;
            set => supportingResourceField = value;
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("supportingresource", IsNullable = true)]
        public OnixSupportingResource[] supportingresource
        {
            get => shortSupportingResourceField;
            set => shortSupportingResourceField = value;
        }

        #endregion
    }

}
