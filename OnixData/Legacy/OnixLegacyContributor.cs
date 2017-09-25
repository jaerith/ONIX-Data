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

        public OnixLegacyContributor()
        {
            SequenceNumber     = "";
            ContributorRole    = "";
            PersonName         = "";
            PersonNameInverted = "";
            NamesBeforeKey     = PrefixToKey;
            KeyNames           = "";
            TitlesBeforeNames  = SuffixToKey = TitlesAfterNames = LettersAfterNames = "";
            CorporateName      = "";

            onixNamesBeforeKey = onixKeyNames = onixLettersAndTitles = null;
        }

        private string sequenceNumberField;
        private string contributorRoleField;
        private string titlesBeforeNamesField;
        private string personNameField;
        private string personNameInvertedField;
        private string namesBeforeKeyField;
        private string prefixToKeyNameField;
        private string keyNamesField;
        private string suffixToKeyNameField;
        private string lettersAfterNamesField;
        private string titlesAfterNamesField;
        private string corporateNameField;

        private string onixNamesBeforeKey;
        private string onixKeyNames;
        private string onixLettersAndTitles;

        #region ONIX Helpers

        /// <remarks/>
        public string OnixNamesBeforeKey
        {
            get
            {
                if (String.IsNullOrEmpty(onixNamesBeforeKey))
                    DetermineContribFields();

                return onixNamesBeforeKey;
            }
        }

        /// <remarks/>
        public string OnixKeyNames
        {
            get
            {
                if (String.IsNullOrEmpty(onixKeyNames))
                    DetermineContribFields();

                return onixKeyNames;
            }
        }

        /// <remarks/>
        public string OnixLettersAndTitles
        {
            get
            {
                if (String.IsNullOrEmpty(onixLettersAndTitles))
                    DetermineContribFields();

                return onixLettersAndTitles;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string SequenceNumber
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
        public string TitlesBeforeNames
        {
            get
            {
                return this.titlesBeforeNamesField;
            }
            set
            {
                this.titlesBeforeNamesField = value;
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

        /// <remarks/>
        public string LettersAfterNames
        {
            get
            {
                return this.lettersAfterNamesField;
            }
            set
            {
                this.lettersAfterNamesField = value;
            }
        }

        /// <remarks/>
        public string TitlesAfterNames
        {
            get
            {
                return this.titlesAfterNamesField;
            }
            set
            {
                this.titlesAfterNamesField = value;
            }
        }

        /// <remarks/>
        public string CorporateName
        {
            get
            {
                return this.corporateNameField;
            }
            set
            {
                this.corporateNameField = value;
            }
        }

        /// <remarks/>
        public string PrefixToKey
        {
            get
            {
                return this.prefixToKeyNameField;
            }
            set
            {
                this.prefixToKeyNameField = value;
            }
        }

        /// <remarks/>
        public string SuffixToKey
        {
            get
            {
                return this.suffixToKeyNameField;
            }
            set
            {
                this.suffixToKeyNameField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b034
        {
            get { return SequenceNumber; }
            set { SequenceNumber = value; }
        }

        /// <remarks/>
        public string b035
        {
            get { return ContributorRole; }
            set { ContributorRole = value; }
        }

        /// <remarks/>
        public string b036
        {
            get { return PersonName; }
            set { PersonName = value; }
        }

        /// <remarks/>
        public string b037
        {
            get { return PersonNameInverted; }
            set { PersonNameInverted = value; }
        }

        /// <remarks/>
        public string b038
        {
            get { return TitlesBeforeNames; }
            set { TitlesBeforeNames = value; }
        }

        /// <remarks/>
        public string b039
        {
            get { return NamesBeforeKey; }
            set { NamesBeforeKey = value; }
        }

        /// <remarks/>
        public string b040
        {
            get { return KeyNames; }
            set { KeyNames = value; }
        }

        /// <remarks/>
        public string b042
        {
            get { return LettersAfterNames; }
            set { LettersAfterNames = value; }
        }

        /// <remarks/>
        public string b043
        {
            get { return TitlesAfterNames; }
            set { TitlesAfterNames = value; }
        }

        /// <remarks/>
        public string b047
        {
            get { return CorporateName; }
            set { CorporateName = value; }
        }

        /// <remarks/>
        public string b247
        {
            get { return PrefixToKey; }
            set { PrefixToKey = value; }
        }

        /// <remarks/>
        public string b248
        {
            get { return SuffixToKey; }
            set { SuffixToKey = value; }
        }

        #endregion

        #region Support Methods

        public void DetermineContribFields()
        {
            StringBuilder KeyNamePrefixBuilder = new StringBuilder();
            StringBuilder KeyNameBuilder       = new StringBuilder();
            StringBuilder LettersTitlesBuilder = new StringBuilder();

            if (!String.IsNullOrEmpty(this.KeyNames))
            {
                KeyNameBuilder.Append(this.KeyNames);

                if (!String.IsNullOrEmpty(this.NamesBeforeKey))
                    KeyNamePrefixBuilder.Append(this.NamesBeforeKey);
            }
            else if (!String.IsNullOrEmpty(this.PersonName))
            {
                string[] NameComponents = this.PersonName.Split(new char[1] { ' ' });

                if (NameComponents.Length == 1)
                    KeyNameBuilder.Append(NameComponents[NameComponents.Length - 1]);
                else if (NameComponents.Length > 1)
                {
                    int nKeyNameIndex = (NameComponents.Length - 1);
                    if (NameComponents[nKeyNameIndex].Contains(".") || (NameComponents[nKeyNameIndex] == NameComponents[nKeyNameIndex].ToUpper()))
                    {
                        LettersTitlesBuilder.Append(NameComponents[nKeyNameIndex]);
                        nKeyNameIndex = (NameComponents.Length - 2);
                    }

                    KeyNameBuilder.Append(NameComponents[nKeyNameIndex]);

                    for (int i = 0; i < nKeyNameIndex; ++i)
                    {
                        if (KeyNamePrefixBuilder.Length > 0)
                            KeyNamePrefixBuilder.Append(" ");

                        KeyNamePrefixBuilder.Append(NameComponents[i]);
                    }
                }
            }
            else if (!String.IsNullOrEmpty(this.PersonNameInverted))
            {
                string[] NameComponents = this.PersonNameInverted.Split(new char[1] { ',' });

                if (NameComponents.Length > 0)
                {
                    KeyNameBuilder.Append(NameComponents[0]);

                    if (NameComponents.Length > 1)
                    {
                        string sKeynamePrefixBuffer = NameComponents[1];

                        string[] AltNameComponents = sKeynamePrefixBuffer.Split(new char[1] { ' ' });

                        for (int i = 0; i < AltNameComponents.Length; ++i)
                        {
                            if (KeyNamePrefixBuilder.Length > 0)
                                KeyNamePrefixBuilder.Append(" ");

                            KeyNamePrefixBuilder.Append(AltNameComponents[i]);
                        }
                    }
                }
            }
            else if (!String.IsNullOrEmpty(this.CorporateName))
            {
                KeyNameBuilder.Append(CorporateName);
            }

            if (!String.IsNullOrEmpty(LettersAfterNames))
            {
                LettersTitlesBuilder.Clear();
                LettersTitlesBuilder.Append(LettersAfterNames);
            }

            if (!String.IsNullOrEmpty(TitlesAfterNames))
            {
                if (LettersTitlesBuilder.Length > 0)
                    LettersTitlesBuilder.Append(" ");

                LettersTitlesBuilder.Append(TitlesAfterNames);
            }

            if (!String.IsNullOrEmpty(PrefixToKey))
                KeyNamePrefixBuilder.Append(" " + PrefixToKey);

            if (!String.IsNullOrEmpty(SuffixToKey))
                LettersTitlesBuilder.Insert(0, SuffixToKey + " ");

            onixNamesBeforeKey   = KeyNamePrefixBuilder.ToString().Trim();
            onixKeyNames         = KeyNameBuilder.ToString().Trim();
            onixLettersAndTitles = LettersTitlesBuilder.ToString().Trim();
        }

        #endregion
    }
}
