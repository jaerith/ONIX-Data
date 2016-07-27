using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
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
    }
}
