namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixLegacyAudience
    {
        #region CONSTANTS

        public const int CONST_AUD_TYPE_ONIX = 1;
        public const int CONST_AUD_TYPE_PROP = 2;
        public const int CONST_AUD_TYPE_MPAA = 3;
        public const int CONST_AUD_TYPE_BBFC = 4;

        #endregion

        public OnixLegacyAudience()
        {
            audienceCodeTypeField = -1;

            audienceCodeTypeNameField = audienceCodeValueField = "";
        }

        private int    audienceCodeTypeField;
        private string audienceCodeTypeNameField;
        private string audienceCodeValueField;

        #region Reference Tags

        /// <remarks/>
        public int AudienceCodeType
        {
            get { return this.audienceCodeTypeField; }
            set { this.audienceCodeTypeField = value; }
        }

        /// <remarks/>
        public string AudienceCodeTypeName
        {
            get { return this.audienceCodeTypeNameField; }
            set { this.audienceCodeTypeNameField = value; }
        }

        /// <remarks/>
        public string AudienceCodeValue
        {
            get { return this.audienceCodeValueField; }
            set { this.audienceCodeValueField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b204
        {
            get { return AudienceCodeType; }
            set { AudienceCodeType = value; }
        }

        /// <remarks/>
        public string b205
        {
            get { return AudienceCodeTypeName; }
            set { AudienceCodeTypeName = value; }
        }

        /// <remarks/>
        public string b206
        {
            get { return AudienceCodeValue; }
            set { AudienceCodeValue = value; }
        }

        #endregion

    }
}
