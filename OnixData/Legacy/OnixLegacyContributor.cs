using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyContributor
    {
        public OnixLegacyContributor()
        {
            SequenceNumber     = -1;
            ContributorRole    = "";
            PersonName         = "";
            PersonNameInverted = "";
            NamesBeforeKey     = "";
            KeyNames           = "";
        }

        private int sequenceNumberField;

        private string contributorRoleField;

        private string personNameField;

        private string personNameInvertedField;

        private string namesBeforeKeyField;

        private string keyNamesField;

        /// <remarks/>
        public int SequenceNumber
        {
            get
            {
                return this.sequenceNumberField;
            }
            set
            {
                this.sequenceNumberField = value;
            }
        }

        /// <remarks/>
        public string ContributorRole
        {
            get
            {
                return this.contributorRoleField;
            }
            set
            {
                this.contributorRoleField = value;
            }
        }

        /// <remarks/>
        public string PersonName
        {
            get
            {
                return this.personNameField;
            }
            set
            {
                this.personNameField = value;
            }
        }

        /// <remarks/>
        public string PersonNameInverted
        {
            get
            {
                return this.personNameInvertedField;
            }
            set
            {
                this.personNameInvertedField = value;
            }
        }

        /// <remarks/>
        public string NamesBeforeKey
        {
            get
            {
                return this.namesBeforeKeyField;
            }
            set
            {
                this.namesBeforeKeyField = value;
            }
        }

        /// <remarks/>
        public string KeyNames
        {
            get
            {
                return this.keyNamesField;
            }
            set
            {
                this.keyNamesField = value;
            }
        }
    }
}
