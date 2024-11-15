using System;

namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacySubject
    {
        #region CONSTANTS

        public const int CONST_SUBJ_SCHEME_BISAC_CAT_ID = 10;
        public const int CONST_SUBJ_SCHEME_REGION_ID    = 11;
        public const int CONST_SUBJ_SCHEME_KEYWORDS     = 20;

        #endregion

        public OnixLegacySubject()
        {
            SubjectSchemeIdentifier = "";
            SubjectCode             = SubjectHeadingText = "";
        }

        private string subjectSchemeIdentifierField;
        private string subjectCodeField;
        private string subjectHeadingTextField;

        #region Reference Tags
        
        /// <remarks/>
        public string MainSubjectSchemeIdentifier
        {
            get
            {
                return this.subjectSchemeIdentifierField;
            }
            set
            {
                this.subjectSchemeIdentifierField = value;
            }
        }

        /// <remarks/>
        public string SubjectSchemeIdentifier
        {
            get
            {
                return this.subjectSchemeIdentifierField;
            }
            set
            {
                this.subjectSchemeIdentifierField = value;
            }
        }

        public uint SubjectSchemeIdentifierNum
        {
            get
            {
                uint nSubjSchemeIdNum = 0;

                if (!String.IsNullOrEmpty(SubjectSchemeIdentifier))
                    UInt32.TryParse(SubjectSchemeIdentifier, out nSubjSchemeIdNum);

                return nSubjSchemeIdNum;
            }
        }

        /// <remarks/>
        public string SubjectCode
        {
            get
            {
                return this.subjectCodeField;
            }
            set
            {
                this.subjectCodeField = value;
            }
        }

        public string SubjectHeadingText
        {
            get
            {
                return this.subjectHeadingTextField;
            }
            set
            {
                this.subjectHeadingTextField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b067
        {
            get { return SubjectSchemeIdentifier; }
            set { SubjectSchemeIdentifier = value; }
        }

        /// <remarks/>
        public string b069
        {
            get { return SubjectCode; }
            set { SubjectCode = value; }
        }

        /// <remarks/>
        public string b191
        {
            get { return SubjectSchemeIdentifier; }
            set { SubjectSchemeIdentifier = value; }
        }

        public string b070
        {
            get { return SubjectHeadingText; }
            set { SubjectHeadingText = value; }
        }

        #endregion
    }
}
