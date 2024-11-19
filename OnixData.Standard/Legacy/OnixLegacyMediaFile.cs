namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyMediaFile
    {
        public OnixLegacyMediaFile()
        {
            MediaFileTypeCode     = -1;
            MediaFileFormatCode   = -1;
            MediaFileLinkTypeCode = -1;
            MediaFileLink         = "";
        }

        private int    mediaFileTypeCodeField;
        private int    mediaFileFormatCodeField;
        private int    mediaFileLinkTypeCodeField;
        private string mediaFileLinkField;

        #region Reference Tags

        /// <remarks/>
        public int MediaFileTypeCode
        {
            get
            {
                return this.mediaFileTypeCodeField;
            }
            set
            {
                this.mediaFileTypeCodeField = value;
            }
        }

        /// <remarks/>
        public int MediaFileFormatCode
        {
            get
            {
                return this.mediaFileFormatCodeField;
            }
            set
            {
                this.mediaFileFormatCodeField = value;
            }
        }

        /// <remarks/>
        public int MediaFileLinkTypeCode
        {
            get
            {
                return this.mediaFileLinkTypeCodeField;
            }
            set
            {
                this.mediaFileLinkTypeCodeField = value;
            }
        }

        /// <remarks/>
        public string MediaFileLink
        {
            get
            {
                return this.mediaFileLinkField;
            }
            set
            {
                this.mediaFileLinkField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int f114
        {
            get { return MediaFileTypeCode; }
            set { MediaFileTypeCode = value; }
        }

        /// <remarks/>
        public int f115
        {
            get { return MediaFileFormatCode; }
            set { MediaFileFormatCode = value; }
        }

        /// <remarks/>
        public int f116
        {
            get { return MediaFileLinkTypeCode; }
            set { MediaFileLinkTypeCode = value; }
        }

        /// <remarks/>
        public string f117
        {
            get { return MediaFileLink; }
            set { MediaFileLink = value; }
        }

        #endregion
    }
}
