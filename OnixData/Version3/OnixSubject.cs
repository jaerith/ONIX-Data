using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixSubject
    {
        #region CONSTANTS

        public const string CONST_MAIN_SUBJ_INDICATOR = "MAIN";

        public const int CONST_SUBJ_SCHEME_BISAC_CAT_ID = 10;
        public const int CONST_SUBJ_SCHEME_REGION_ID    = 11;

        #endregion

        public OnixSubject()
        {
            MainSubject = SubjectCode = SubjectSchemeIdentifier = "";

            SubjectSchemeVersion = -1;
        }

        private string mainSubjectField;
        private string subjectSchemeIdentifierField;
        private int    subjectSchemeVersionField;
        private string subjectCodeField;


        #region Helper Methods

        public bool IsMainSubject()
        {
            return (!String.IsNullOrEmpty(MainSubject) && (MainSubject == CONST_MAIN_SUBJ_INDICATOR));
        }

        public int SubjectSchemeIdentifierNum
        {
            get
            {
                int nSubjSchemeIdNum = 0;

                if (!String.IsNullOrEmpty(SubjectSchemeIdentifier))
                    Int32.TryParse(SubjectSchemeIdentifier, out nSubjSchemeIdNum);

                return nSubjSchemeIdNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string MainSubject
        {
            get
            {
                return this.mainSubjectField;
            }
            set
            {
                this.mainSubjectField = value;
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

        /// <remarks/>
        public int SubjectSchemeVersion
        {
            get
            {
                return this.subjectSchemeVersionField;
            }
            set
            {
                this.subjectSchemeVersionField = value;
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x425
        {
            get { return MainSubject; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    MainSubject = CONST_MAIN_SUBJ_INDICATOR;
                else
                    MainSubject = value;
            }
        }

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
        public int b068
        {
            get { return SubjectSchemeVersion; }
            set { SubjectSchemeVersion = value; }
        }

        #endregion
    }
}
