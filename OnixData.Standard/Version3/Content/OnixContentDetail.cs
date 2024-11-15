namespace OnixData.Standard.Version3.Content
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixContentDetail
    {
        public OnixContentDetail()
        {
            contentItemField = shortContentItemField = new OnixContentItem[0];
        }

        private OnixContentItem[] contentItemField;
        private OnixContentItem[] shortContentItemField;

        #region ONIX Lists

        public OnixContentItem[] OnixContentItemList
        {
            get
            {
                OnixContentItem[] OnixCtntList = null;

                if (this.contentItemField != null)
                    OnixCtntList = this.contentItemField;
                else if (this.shortContentItemField != null)
                    OnixCtntList = this.shortContentItemField;
                else
                    OnixCtntList = new OnixContentItem[0];

                return OnixCtntList;
            }
        }

        #endregion

        #region Helper Methods

        public OnixContentItem PrimaryContentItem
        {
            get
            {
                var PrimaryItem = new OnixContentItem();

                if ((OnixContentItemList != null) && (OnixContentItemList.Length > 0))
                    PrimaryItem = OnixContentItemList[0];

                return PrimaryItem;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ContentItem")]
        public OnixContentItem[] ContentItem
        {
            get { return this.contentItemField; }
            set { this.contentItemField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contentitem")]
        public OnixContentItem[] contentitem
        {
            get { return this.shortContentItemField; }
            set { shortContentItemField = value; }
        }

        #endregion
    }
}
