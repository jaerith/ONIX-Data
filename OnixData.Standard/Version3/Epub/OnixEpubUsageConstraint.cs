namespace OnixData.Standard.Version3.Epub
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixEpubUsageConstraint
    {
        #region CONSTANTS

        public const int CONST_CONSTRAINT_TYPE_PREVIEW = 1;
        public const int CONST_CONSTRAINT_TYPE_PRINT   = 2;
        public const int CONST_CONSTRAINT_TYPE_COPY    = 3;
        public const int CONST_CONSTRAINT_TYPE_SHARE   = 4;
        public const int CONST_CONSTRAINT_TYPE_TTS     = 5;
        public const int CONST_CONSTRAINT_TYPE_LEND    = 6;
        public const int CONST_CONSTRAINT_TYPE_TIME    = 7;
        public const int CONST_CONSTRAINT_TYPE_RENEW   = 8;
        public const int CONST_CONSTRAINT_TYPE_MU      = 9;
        public const int CONST_CONSTRAINT_TYPE_PVW_LCL = 10;

        public const int CONST_CONSTRAINT_STS_UNLIMITED  = 1;
        public const int CONST_CONSTRAINT_STS_LIMITED    = 2;
        public const int CONST_CONSTRAINT_STS_PROHIBITED = 3;

        #endregion

        public OnixEpubUsageConstraint()
        {
            EpubUsageType = EpubUsageStatus = "";

            usageLimitField = shortUsageLimitField = new OnixEpubUsageLimit[0];
        }

        private string epubUsageTypeField;
        private string epubUsageStatusField;

        private OnixEpubUsageLimit[] usageLimitField;
        private OnixEpubUsageLimit[] shortUsageLimitField;

        #region ONIX Lists

        public OnixEpubUsageLimit[] OnixEpubUsageLimitList
        {
            get
            {
                OnixEpubUsageLimit[] Limits = new OnixEpubUsageLimit[0];

                if (this.usageLimitField != null)
                    Limits = this.usageLimitField;
                else if (this.shortUsageLimitField != null)
                    Limits = this.shortUsageLimitField;
                else
                    Limits = new OnixEpubUsageLimit[0];

                return Limits;
            }
        }

        #endregion

        #region Reference Tags

        public string EpubUsageType
        {
            get { return this.epubUsageTypeField; }
            set { this.epubUsageTypeField = value; }
        }

        public string EpubUsageStatus
        {
            get { return this.epubUsageStatusField; }
            set { this.epubUsageStatusField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EpubUsageLimit")]
        public OnixEpubUsageLimit[] EpubUsageLimit
        {
            get { return this.usageLimitField; }
            set { this.usageLimitField = value; }
        }

        #endregion

        #region Short Tags

        public string x318
        {
            get { return EpubUsageType; }
            set { EpubUsageType = value; }
        }

        public string x319
        {
            get { return EpubUsageStatus; }
            set { EpubUsageStatus = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("epubusagelimit")]
        public OnixEpubUsageLimit[] epubusagelimit
        {
            get { return this.shortUsageLimitField; }
            set { this.shortUsageLimitField = value; }
        }

        #endregion
    }
}
