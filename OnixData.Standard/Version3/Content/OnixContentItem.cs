namespace OnixData.Standard.Version3.Content
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixContentItem
    {
        #region CONSTANTS

        #endregion

        public OnixContentItem()
        {
            NumberOfPages = "";
        }

        private string numberOfPagesField;

        #region Reference Tags

        /// <remarks/>
        public string NumberOfPages
        {
            get
            {
                return this.numberOfPagesField;
            }
            set
            {
                this.numberOfPagesField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b061
        {
            get { return NumberOfPages; }
            set { NumberOfPages = value; }
        }

        #endregion
    }
}