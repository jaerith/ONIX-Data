using System;
using OnixData.Version3.Text;

namespace OnixData.Version3.Text
{
    public class OnixResourceVersion
    {
        public OnixResourceVersion()
        {
            resourceVersionFeatureField = shortResourceVersionFeature = Array.Empty<OnixResourceVersionFeature>();
            resourceLinkField = shortResourceLinkFeatureField = Array.Empty<string>();
            contentDateField = shortContentDateField = Array.Empty<OnixContentDate>();
        }
        private int resourceFormField;

        private OnixResourceVersionFeature[] resourceVersionFeatureField;
        private OnixResourceVersionFeature[] shortResourceVersionFeature;
        
        private string featureNoteField;
        
        private string[] resourceLinkField;
        private string[] shortResourceLinkFeatureField;
        
        private OnixContentDate[] contentDateField;
        private OnixContentDate[] shortContentDateField;

        #region ONIX Lists
        
        public OnixResourceVersionFeature[] OnixResourceVersionFeatureList
        {
            get
            {
                OnixResourceVersionFeature[] resourceVersionFeatures = null;

                if (this.resourceVersionFeatureField != null)
                    resourceVersionFeatures = this.resourceVersionFeatureField;
                else if (this.shortResourceVersionFeature != null)
                    resourceVersionFeatures = this.shortResourceVersionFeature;
                else
                    resourceVersionFeatures = new OnixResourceVersionFeature[0];

                return resourceVersionFeatures;
            }
        }
        
        public string[] ResourceLinksList
        {
            get
            {
                string[] resourceLinksList = null;

                if (this.resourceLinkField != null)
                    resourceLinksList = this.resourceLinkField;
                else if (this.shortResourceLinkFeatureField != null)
                    resourceLinksList = this.shortResourceLinkFeatureField;
                else
                    resourceLinksList = new string[0];

                return resourceLinksList;
            }
        }
        
        public OnixContentDate[] ContentDatesList
        {
            get
            {
                OnixContentDate[] contentDatesList = null;

                if (this.contentDateField != null)
                    contentDatesList = this.contentDateField;
                else if (this.shortContentDateField != null)
                    contentDatesList = this.shortContentDateField;
                else
                    contentDatesList = new OnixContentDate[0];

                return contentDatesList;
            }
        }

        #endregion
        
        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceForm", IsNullable = false)]
        public int ResourceForm
        {
            get => resourceFormField;
            set => resourceFormField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceVersionFeature", IsNullable = true)]
        public OnixResourceVersionFeature[] ResourceVersionFeature
        {
            get => resourceVersionFeatureField;
            set => resourceVersionFeatureField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FeatureNote", IsNullable = true)]
        public string FeatureNote
        {
            get => featureNoteField;
            set => featureNoteField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceLink", IsNullable = false)]
        public string[] ResourceLink
        {
            get => resourceLinkField;
            set => resourceLinkField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ContentDate", IsNullable = true)]
        public OnixContentDate[] ContentDate
        {
            get => contentDateField;
            set => contentDateField = value;
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x441", IsNullable = false)]
        public int x441
        {
            get => ResourceForm;
            set => ResourceForm = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("resourceversionfeature", IsNullable = true)]
        public OnixResourceVersionFeature[] resourceversionfeature
        {
            get => shortResourceVersionFeature;
            set => shortResourceVersionFeature = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x440", IsNullable = true)]
        public string x440
        {
            get => FeatureNote;
            set => FeatureNote = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x435", IsNullable = false)]
        public string[] x435
        {
            get => shortResourceLinkFeatureField;
            set => shortResourceLinkFeatureField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contentdate", IsNullable = true)]
        public OnixContentDate[] contentdate
        {
            get => shortContentDateField;
            set => shortContentDateField = value;
        }
        
        #endregion
    }
}