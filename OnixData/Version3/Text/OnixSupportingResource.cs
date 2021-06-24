using System;
using OnixData.Version3.Publishing;

namespace OnixData.Version3.Text
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixSupportingResource
    {
        public OnixSupportingResource()
        {
            contentAudienceField = shortContentAudienceField = Array.Empty<int>();
            resourceFeatureField = shortResourceFeatureField = Array.Empty<OnixResourceFeature>();
            resourceVersionField = shortResourceVersionField = Array.Empty<OnixResourceVersion>();
        }
        private int resourceContentTypeField;

        private int[] contentAudienceField;
        private int[] shortContentAudienceField;

        private OnixTerritory territoryField;
        private int resourceModeField;
        
        private OnixResourceFeature[] resourceFeatureField;
        private OnixResourceFeature[] shortResourceFeatureField;
        
        private OnixResourceVersion[] resourceVersionField;
        private OnixResourceVersion[] shortResourceVersionField;
        
        #region ONIX Lists
        
        public int[] ContentAudienceList
        {
            get
            {
                int[] audienceField = null;

                if (this.contentAudienceField != null)
                    audienceField = this.contentAudienceField;
                else if (this.shortContentAudienceField != null)
                    audienceField = this.shortContentAudienceField;
                else
                    audienceField = new int[0];

                return audienceField;
            }
        }

        public OnixResourceFeature[] OnixResourceFeatureList
        {
            get
            {
                OnixResourceFeature[] resourceFeature = null;

                if (this.resourceFeatureField != null)
                    resourceFeature = this.resourceFeatureField;
                else if (this.shortResourceFeatureField != null)
                    resourceFeature = this.shortResourceFeatureField;
                else
                    resourceFeature = new OnixResourceFeature[0];

                return resourceFeature;
            }
        }
        
        public OnixResourceVersion[] OnixResourceVersionList
        {
            get
            {
                OnixResourceVersion[] resourceVersions = null;

                if (this.resourceVersionField != null)
                    resourceVersions = this.resourceVersionField;
                else if (this.shortResourceVersionField != null)
                    resourceVersions = this.shortResourceVersionField;
                else
                    resourceVersions = new OnixResourceVersion[0];

                return resourceVersions;
            }
        }

        #endregion
        
        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceContentType", IsNullable = false)]
        public int ResourceContentType
        {
            get => resourceContentTypeField;
            set => resourceContentTypeField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ContentAudience", IsNullable = false)]
        public int[] ContentAudience
        {
            get => contentAudienceField;
            set => contentAudienceField = value;
        }
        
        [System.Xml.Serialization.XmlElementAttribute("Territory", IsNullable = true)]
        public OnixTerritory Territory
        {
            get => territoryField;
            set => territoryField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceMode", IsNullable = false)]
        public int ResourceMode
        {
            get => resourceModeField;
            set => resourceModeField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceFeature", IsNullable = true)]
        public OnixResourceFeature[] ResourceFeature
        {
            get => resourceFeatureField;
            set => resourceFeatureField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceVersion", IsNullable = false)]
        public OnixResourceVersion[] ResourceVersion
        {
            get => resourceVersionField;
            set => resourceVersionField = value;
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x436", IsNullable = false)]
        public int x436
        {
            get => ResourceContentType;
            set => ResourceContentType = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x427", IsNullable = false)]
        public int[] x427
        {
            get => shortContentAudienceField;
            set => shortContentAudienceField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("territory", IsNullable = false)]
        public OnixTerritory territory
        {
            get => Territory;
            set => Territory = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("x437", IsNullable = false)]
        public int x437
        {
            get => ResourceMode;
            set => ResourceMode = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("resourcefeature", IsNullable = true)]
        public OnixResourceFeature[] resourcefeature
        {
            get => shortResourceFeatureField;
            set => shortResourceFeatureField = value;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("resourceversion", IsNullable = false)]
        public OnixResourceVersion[] resourceversion
        {
            get => shortResourceVersionField;
            set => shortResourceVersionField = value;
        }
        
        #endregion
    }
}