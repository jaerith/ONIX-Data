namespace OnixData.Standard.Version3.Related
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixRelatedWork
    {
        #region CONSTANTS

        public const int CONST_WORK_REL_CD_MANIFESTION_OF = 1;
        public const int CONST_WORK_REL_CD_DERIVED_FROM   = 2;

        #endregion

        public OnixRelatedWork()
        {
            WorkRelationCode = -1;

            workIdentifierField = shortWorkIdentifierField = new OnixWorkIdentifier[0];
        }

        private int workRelationCodeField;

        private OnixWorkIdentifier[] workIdentifierField;
        private OnixWorkIdentifier[] shortWorkIdentifierField;

        #region ONIX Lists

        public OnixWorkIdentifier[] OnixWorkIdList
        {
            get
            {
                OnixWorkIdentifier[] WorkIds = null;

                if (this.workIdentifierField != null)
                    WorkIds = this.workIdentifierField;
                else if (this.shortWorkIdentifierField != null)
                    WorkIds = this.shortWorkIdentifierField;
                else
                    WorkIds = new OnixWorkIdentifier[0];

                return WorkIds;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public int WorkRelationCode
        {
            get
            {
                return this.workRelationCodeField;
            }
            set
            {
                this.workRelationCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("WorkIdentifier")]
        public OnixWorkIdentifier[] WorkIdentifier
        {
            get
            {
                return this.workIdentifierField;
            }
            set
            {
                this.workIdentifierField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x454
        {
            get { return WorkRelationCode; }
            set { WorkRelationCode = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("workidentifier")]
        public OnixWorkIdentifier[] workidentifier
        {
            get { return shortWorkIdentifierField; }
            set { shortWorkIdentifierField = value; }
        }

        #endregion
    }
}
