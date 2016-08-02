using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Related
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
            WorkIdentifier   = new OnixWorkIdentifier[0];

        }

        private int workRelationCodeField;

        private OnixWorkIdentifier[] workIdentifierField;

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
    }
}
