using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixContributor
    {
        #region CONSTANTS

        public const string CONST_CONTRIB_ROLE_AUTHOR        = "A01";
        public const string CONST_CONTRIB_ROLE_GHOST_AUTHOR  = "A02";
        public const string CONST_CONTRIB_ROLE_SRNPLY_AUTHOR = "A03";
        public const string CONST_CONTRIB_ROLE_LIBRTO_AUTHOR = "A04";
        public const string CONST_CONTRIB_ROLE_LYRICS_AUTHOR = "A05";
        public const string CONST_CONTRIB_ROLE_EDITED_BY     = "B01";
        public const string CONST_CONTRIB_ROLE_REVISED_BY    = "B02";
        public const string CONST_CONTRIB_ROLE_RETOLD_BY     = "B03";
        public const string CONST_CONTRIB_ROLE_COMPILED_BY   = "C01";
        public const string CONST_CONTRIB_ROLE_SELECTED_BY   = "C02";
        public const string CONST_CONTRIB_ROLE_PRODUCER      = "D01";
        public const string CONST_CONTRIB_ROLE_DIRECTOR      = "D02";
        public const string CONST_CONTRIB_ROLE_ACTOR         = "E01";
        public const string CONST_CONTRIB_ROLE_DANCER        = "E02";
        public const string CONST_CONTRIB_ROLE_NARRATOR      = "E03";
        public const string CONST_CONTRIB_ROLE_COMMENTATOR   = "E04";
        public const string CONST_CONTRIB_ROLE_VOCAL_SOLO    = "E05";
        public const string CONST_CONTRIB_ROLE_FILMED_BY     = "F01";
        public const string CONST_CONTRIB_ROLE_OTHER         = "Z99";

        #endregion

        public OnixContributor()
        {
            SequenceNumber  = -1;
            ContributorRole = "";
            NamesBeforeKey  = KeyNames = "";
        }

        private int    sequenceNumberField;
        private string contributorRoleField;
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
