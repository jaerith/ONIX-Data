using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
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
            SubjectSchemeIdentifier = -1;
            SubjectCode             = SubjectHeadingText = "";
        }

        private int    subjectSchemeIdentifierField;
        private string subjectCodeField;
        private string subjectHeadingTextField;

        #region Reference Tags
        
        /// <remarks/>
        public int MainSubjectSchemeIdentifier
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
        public int SubjectSchemeIdentifier
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
        public int b067
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
        public int b191
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
