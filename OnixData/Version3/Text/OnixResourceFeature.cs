namespace OnixData.Version3.Text
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixResourceFeature
    {
        private int resourceFeatureTypeField;
        private string featureValueField;
        private string featureNoteField;
        
        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceFeatureType", IsNullable = false)]
        public int ResourceFeatureType
        {
            get => resourceFeatureTypeField;
            set => resourceFeatureTypeField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FeatureValue", IsNullable = true)]
        public string FeatureValue
        {
            get => featureValueField;
            set => featureValueField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FeatureNote", IsNullable = true)]
        public string FeatureNote
        {
            get => featureNoteField;
            set => featureNoteField = value;
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x438", IsNullable = false)]
        public int x438
        {
            get => ResourceFeatureType;
            set => ResourceFeatureType = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x439", IsNullable = true)]
        public string x439
        {
            get => FeatureValue;
            set => FeatureValue = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x440", IsNullable = true)]
        public string x440
        {
            get => FeatureNote;
            set => FeatureNote = value;
        }

        #endregion
    }
}