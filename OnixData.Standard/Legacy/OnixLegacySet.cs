namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacySet : OnixLegacyBaseProduct
    {
        public OnixLegacySet()
        {
            ItemNumberWithinSet = TitleOfSet = "";
        }

        private string itemNumberWithinSetField;
        private string titleOfSetField;

        #region Reference Tags

        /// <remarks/>
        public string TitleOfSet
        {
            get { return this.titleOfSetField; }
            set { this.titleOfSetField = value; }
        }

        /// <remarks/>
        public string ItemNumberWithinSet
        {
            get { return this.itemNumberWithinSetField; }
            set { this.itemNumberWithinSetField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b023
        {
            get { return TitleOfSet; }
            set { TitleOfSet = value; }
        }

        /// <remarks/>
        public string b026
        {
            get { return ItemNumberWithinSet; }
            set { ItemNumberWithinSet = value; }
        }

        #endregion
    }
}
