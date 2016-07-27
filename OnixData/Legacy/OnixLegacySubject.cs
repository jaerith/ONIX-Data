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
        public OnixLegacySubject()
        {
            SubjectSchemeIdentifier = -1;
            SubjectCode             = "";
        }

        private int    subjectSchemeIdentifierField;
        private string subjectCodeField;

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
    }
}
