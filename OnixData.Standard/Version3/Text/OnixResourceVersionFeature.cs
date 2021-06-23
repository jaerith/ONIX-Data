using System;

namespace OnixData.Standard.Version3.Text
{
    public class OnixResourceVersionFeature
    {
        public OnixResourceVersionFeature()
        {
            featureNoteField = shortFeatureNoteField = Array.Empty<string>();
        }
        
        private int resourceVersionFeatureTypeField;
        private string featureValueField;

        private string[] featureNoteField;
        private string[] shortFeatureNoteField;
        
        #region ONIX Lists
        
        public string[] FeatureNotesList
        {
            get
            {
                string[] featureNotesList = null;

                if (this.featureNoteField != null)
                    featureNotesList = this.featureNoteField;
                else if (this.shortFeatureNoteField != null)
                    featureNotesList = this.shortFeatureNoteField;
                else
                    featureNotesList = new string[0];

                return featureNotesList;
            }
        }

        #endregion
        
        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceVersionFeatureType", IsNullable = false)]
        public int ResourceVersionFeatureType
        {
            get => resourceVersionFeatureTypeField;
            set => resourceVersionFeatureTypeField = value;
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
        public string[] FeatureNote
        {
            get => featureNoteField;
            set => featureNoteField = value;
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x442", IsNullable = false)]
        public int x442
        {
            get => ResourceVersionFeatureType;
            set => ResourceVersionFeatureType = value;
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
        public string[] x440
        {
            get => shortFeatureNoteField;
            set => shortFeatureNoteField = value;
        }
        
        #endregion
    }
}