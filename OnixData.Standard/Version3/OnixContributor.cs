﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnixData.Standard.Version3.Names;

namespace OnixData.Standard.Version3
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

        private static readonly HashSet<string> CONST_ALL_ALLOWED_SUFFIX_LIST =
            new HashSet<string>()
{
"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"
, "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX"
, "XX", "XXI", "XXII", "XXIII", "2nd", "3rd", "AB", "ACE", "AG", "ASC"
, "B.V.", "BA", "BBA", "BE", "BS", "BSN", "CA", "CC", "CFP", "CM"
, "Co.", "Co., Ltd.", "Corp.", "CPA", "CQ", "DBE", "DC", "DD", "DDS", "DGA"
, "DMD", "DO", "DPM", "DSM", "DVM", "Esq", "fils", "FRS", "GmbH", "GOQ"
, "Inc", "JD", "Jr", "KBE", "KC", "KCB", "KG", "KK", "Lit B", "Lit D"
, "Litt B", "Litt D", "LLB", "LLC", "LLD", "LLM", "LM", "LOM", "LPN", "Ltd"
, "MA", "MBA", "MBE", "MD", "MFA", "MH", "MHA", "MLA", "MNA", "MP"
, "MPP", "MS", "MSc", "N.V.", "OBE", "OC", "OD", "OM", "OQ", "PC"
, "père", "PhD", "plc", "Pty", "Pty Ltd", "QC", "RCAF", "RCMP", "RCN", "Ret"
, "RN", "RS", "S.A.de C.V.", "S.p.A.", "S.r.l.", "SA", "SJ", "Sr", "USA", "USAF"
, "USCG", "USMC", "USN", "VC", "VMD", "Y.K."
};

        private static readonly HashSet<string> CONST_ALL_ALLOWED_FILTERED_SUFFIX_LIST =
            new HashSet<string>()
{
"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"
, "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX"
, "XX", "XXI", "XXII", "XXIII", "2ND", "3RD", "AB", "ACE", "AG", "ASC"
, "BV", "BA", "BBA", "BE", "BS", "BSN", "CA", "CC", "CFP", "CM"
, "CO", "CO,LTD", "CORP", "CPA", "CQ", "DBE", "DC", "DD", "DDS", "DGA"
, "DMD", "DO", "DPM", "DSM", "DVM", "ESQ", "FILS", "FRS", "GMBH", "GOQ"
, "INC", "JD", "JR", "KBE", "KC", "KCB", "KG", "KK", "LITB", "LITD"
, "LITTB", "LITTD", "LLB", "LLC", "LLD", "LLM", "LM", "LOM", "LPN", "LTD"
, "MA", "MBA", "MBE", "MD", "MFA", "MH", "MHA", "MLA", "MNA", "MP"
, "MPP", "MS", "MSC", "NV", "OBE", "OC", "OD", "OM", "OQ", "PC"
, "PÈRE", "PHD", "PLC", "PTY", "PTYLTD", "QC", "RCAF", "RCMP", "RCN", "RET"
, "RN", "RS", "SADECV", "SPA", "SRL", "SA", "SJ", "SR", "USA", "USAF"
, "USCG", "USMC", "USN", "VC", "VMD", "YK"
};

        #endregion

        public OnixContributor()
        {
            SequenceNumber    = "";
            ContributorRole   = PersonName = PersonNameInverted = "";
            NamesBeforeKey    = PrefixToKey = KeyNames = "";
            TitlesBeforeNames = SuffixToKey = TitlesAfterNames = LettersAfterNames = "";

            CorporateName      = BiographicalNote = "";
            onixNamesBeforeKey = onixKeyNames = onixLettersAndTitles = null;

            altNameField = shortAltNameField = new OnixAlternateName[0];
            nameIdField  = shortNameIdField = new OnixNameIdentifier[0];
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
        private string biographicalNoteField;

        private string onixNamesBeforeKey;
        private string onixKeyNames;
        private string onixLettersAndTitles;

        private OnixAlternateName[] altNameField;
        private OnixAlternateName[] shortAltNameField;

        private OnixNameIdentifier[] nameIdField;
        private OnixNameIdentifier[] shortNameIdField;

        #region ONIX Lists

        public OnixNameIdentifier[] OnixNameIdList
        {
            get
            {
                OnixNameIdentifier[] NameIdList = new OnixNameIdentifier[0];

                if (this.nameIdField != null)
                    NameIdList = this.nameIdField;
                else if (this.shortNameIdField != null)
                    NameIdList = this.shortNameIdField;
                else
                    NameIdList = new OnixNameIdentifier[0];

                return NameIdList;
            }
        }

        public OnixAlternateName[] OnixAltNameList
        {
            get
            {
                OnixAlternateName[] AltNameList = new OnixAlternateName[0];

                if (this.altNameField != null)
                    AltNameList = this.altNameField;
                else if (this.shortAltNameField != null)
                    AltNameList = this.shortAltNameField;
                else
                    AltNameList = new OnixAlternateName[0];

                return AltNameList;
            }
        }

        #endregion

        #region ONIX Helpers

        /// <remarks/>
        public OnixNameIdentifier GetFirstNameIdentifier(int pnNameIdTypeNum)
        {
            var firstNameID =
                OnixNameIdList
                .Where(x => (x.NameIDTypeNum == pnNameIdTypeNum) && (!String.IsNullOrEmpty(x.IDValue)))
                .FirstOrDefault();

            return (firstNameID != null) ? firstNameID : new OnixNameIdentifier();
        }

        public OnixNameIdentifier GetFirstProprietaryNameId()
        {
            return GetFirstNameIdentifier(OnixNameIdentifier.CONST_NAME_TYPE_ID_PROP);
        }

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

        public int SeqNum
        {
            get
            {
                int nSeqNum = 0;

                if (!String.IsNullOrEmpty(SequenceNumber))
                    Int32.TryParse(SequenceNumber, out nSeqNum);

                return nSeqNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string SequenceNumber
        {
            get { return this.sequenceNumberField; }
            set { this.sequenceNumberField = value; }
        }

        /// <remarks/>
        public string ContributorRole
        {
            get { return this.contributorRoleField; }
            set { this.contributorRoleField = value; }
        }

        /// <remarks/>
        public string TitlesBeforeNames
        {
            get { return this.titlesBeforeNamesField; }
            set { this.titlesBeforeNamesField = value; }
        }

        /// <remarks/>
        public string PersonName
        {
            get { return this.personNameField; }
            set { this.personNameField = value; }
        }

        /// <remarks/>
        public string PersonNameInverted
        {
            get { return this.personNameInvertedField; }
            set { this.personNameInvertedField = value; }
        }

        /// <remarks/>
        public string NamesBeforeKey
        {
            get { return this.namesBeforeKeyField; }
            set { this.namesBeforeKeyField = value; }
        }

        /// <remarks/>
        public string KeyNames
        {
            get { return this.keyNamesField; }
            set { this.keyNamesField = value; }
        }

        /// <remarks/>
        public string LettersAfterNames
        {
            get { return this.lettersAfterNamesField; }
            set { this.lettersAfterNamesField = value; }
        }

        /// <remarks/>
        public string TitlesAfterNames
        {
            get { return this.titlesAfterNamesField; }
            set { this.titlesAfterNamesField = value; }
        }

        public string BiographicalNote
        {
            get
            { return this.biographicalNoteField; }
            set
            { this.biographicalNoteField = value; }
        }

        /// <remarks/>
        public string CorporateName
        {
            get { return this.corporateNameField; }
            set { this.corporateNameField = value; }
        }

        /// <remarks/>
        public string PrefixToKey
        {
            get { return this.prefixToKeyNameField; }
            set { this.prefixToKeyNameField = value; }
        }

        /// <remarks/>
        public string SuffixToKey
        {
            get { return this.suffixToKeyNameField; }
            set { this.suffixToKeyNameField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AlternativeName")]
        public OnixAlternateName[] AlternativeName
        {
            get { return this.altNameField; }
            set { this.altNameField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NameIdentifier")]
        public OnixNameIdentifier[] NameIdentifier
        {
            get { return this.nameIdField; }
            set { this.nameIdField = value; }
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

        public string b044
        {
            get { return BiographicalNote; }
            set { BiographicalNote = value; }
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("alternativename")]
        public OnixAlternateName[] alternativename
        {
            get { return this.shortAltNameField; }
            set { this.shortAltNameField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("nameidentifier")]
        public OnixNameIdentifier[] nameidentifier
        {
            get { return this.shortNameIdField; }
            set { this.shortNameIdField = value; }
        }

        #endregion

        #region Support Methods

        private void DetermineContribFields()
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
            else if (!String.IsNullOrEmpty(this.PersonName))
            {
                string[] NameComponents = this.PersonName.Split(new char[1] { ' ' });

                if (NameComponents.Length == 1)
                    KeyNameBuilder.Append(NameComponents[NameComponents.Length - 1]);
                else if (NameComponents.Length > 1)
                {
                    bool   bLongSuffix      = false;
                    bool   bExtraLongSuffix = false;
                    string sPossibleSuffix  = "";

                    if (NameComponents.Length > 4)
                    {
                        sPossibleSuffix  = NameComponents[NameComponents.Length - 3] + " " + NameComponents[NameComponents.Length - 2] + " " + NameComponents[NameComponents.Length - 1];
                        bExtraLongSuffix = true;
                    }
                    else if (NameComponents.Length > 3)
                    {
                        sPossibleSuffix = NameComponents[NameComponents.Length-2] + " " + NameComponents[NameComponents.Length-1];
                        bLongSuffix     = true;
                    }
                    else
                    {
                        sPossibleSuffix = NameComponents[NameComponents.Length-1];
                        bLongSuffix     = bExtraLongSuffix = false;
                    }

                    int nKeyNameIndex = (NameComponents.Length - 1);
                    if (WithinAllowedSuffixDomain(sPossibleSuffix))
                    {
                        LettersTitlesBuilder.Append(sPossibleSuffix);

                        if (bExtraLongSuffix)
                            nKeyNameIndex = (NameComponents.Length - 4);
                        else if (bLongSuffix)
                            nKeyNameIndex = (NameComponents.Length - 3);
                        else
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

        public static bool WithinAllowedSuffixDomain(string psTestSuffix, bool pbTryFilteredVersion = true)
        {
            bool bSuffixMatch = false;

            if (!String.IsNullOrEmpty(psTestSuffix))
            {
                bSuffixMatch = CONST_ALL_ALLOWED_SUFFIX_LIST.Contains(psTestSuffix);

                if (!bSuffixMatch && pbTryFilteredVersion)
                {
                    string psFilteredSuffix = psTestSuffix.Replace(".", "").Replace(" ", "").ToUpper();

                    bSuffixMatch = CONST_ALL_ALLOWED_FILTERED_SUFFIX_LIST.Contains(psFilteredSuffix);
                }
            }

            return bSuffixMatch;
        }

        #endregion
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixAlternateName : OnixContributor
    {
        #region CONSTANTS

        public const int CONST_NAME_TYPE_UNKNOWN   = 0;
        public const int CONST_NAME_TYPE_PSEUDONYM = 1;
        public const int CONST_NAME_TYPE_AUTH_CNTL = 2;
        public const int CONST_NAME_TYPE_EARLIER   = 3;
        public const int CONST_NAME_TYPE_REAL      = 4;
        public const int CONST_NAME_TYPE_TRANSLIT  = 6;
        public const int CONST_NAME_TYPE_LATER     = 7;

        public readonly int[] CONST_SOUGHT_NAME_TYPES = { CONST_NAME_TYPE_PSEUDONYM, CONST_NAME_TYPE_REAL };

        #endregion

        public OnixAlternateName()
        {
            NameType = "";
        }

        private string nameTypeField;

        #region Helper Methods

        public bool IsSoughtNameType()
        {
            return CONST_SOUGHT_NAME_TYPES.Contains(NameTypeNum);
        }

        public int NameTypeNum
        {
            get
            {
                int nTypeNum = -1;

                if (!String.IsNullOrEmpty(NameType))
                    Int32.TryParse(NameType, out nTypeNum);

                return nTypeNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string NameType
        {
            get { return this.nameTypeField; }
            set { this.nameTypeField = value; }
        }

        #endregion

        #region Short Tags

        public string x414
        {
            get { return NameType; }
            set { NameType = value; }
        }

        #endregion
    }
}
