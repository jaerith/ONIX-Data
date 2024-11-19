namespace OnixData.Standard.Version3.Epub
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixEpubUsageLimit
    {
        public OnixEpubUsageLimit()
        {
            Quantity = EpubUsageUnit = "";
        }

        private string quantityField;
        private string epubUsageUnitField;

        #region Reference Tags

        public string Quantity
        {
            get { return this.quantityField; }
            set { this.quantityField = value; }
        }

        public string EpubUsageUnit
        {
            get { return this.epubUsageUnitField; }
            set { this.epubUsageUnitField = value; }
        }

        #endregion

        #region Short Tags

        public string x320
        {
            get { return Quantity; }
            set { Quantity = value; }
        }

        public string x321
        {
            get { return EpubUsageUnit; }
            set { EpubUsageUnit = value; }
        }

        #endregion
    }
}