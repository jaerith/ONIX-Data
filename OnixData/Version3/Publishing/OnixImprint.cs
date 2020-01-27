using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Publishing
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixImprint
    {
        public OnixImprint()
        {
            ImprintName = "";
        }

        private string imprintNameField;

        private OnixImprintIdentifier[] imprintIdentifierField;
        private OnixImprintIdentifier[] shortImprintIdentifierField;

        #region ONIX Lists

        public OnixImprintIdentifier[] OnixImprintIdentifierList
        {
            get
            {
                OnixImprintIdentifier[] ImprintIdList = null;

                if (this.imprintIdentifierField != null)
                    ImprintIdList = this.imprintIdentifierField;
                else if (this.shortImprintIdentifierField != null)
                    ImprintIdList = this.shortImprintIdentifierField;
                else
                    ImprintIdList = new OnixImprintIdentifier[0];

                return ImprintIdList;
            }
        }

        #endregion

        #region Helper Methods

        public bool IsProprietaryName()
        {
            OnixImprintIdentifier[] ImprintIdList = this.OnixImprintIdentifierList;

            bool bIsProprietary = false;

            if ((ImprintIdList != null) && (ImprintIdList.Length > 0))
            {
                OnixImprintIdentifier FoundImprint =
                    ImprintIdList.Where(x => x.GetImprintIDTypeNum() == OnixImprintIdentifier.CONST_IMPRINT_ROLE_PROP).LastOrDefault();

                if ((FoundImprint != null) && !String.IsNullOrEmpty(FoundImprint.ImprintIDType))
                    bIsProprietary = true;
            }

            return bIsProprietary;
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ImprintIdentifier")]
        public OnixImprintIdentifier[] ImprintIdentifier
        {
            get { return this.imprintIdentifierField; }
            set { this.imprintIdentifierField = value; }
        }

        /// <remarks/>
        public string ImprintName
        {
            get
            {
                return this.imprintNameField;
            }
            set
            {
                this.imprintNameField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("imprintidentifier")]
        public OnixImprintIdentifier[] imprintidentifier
        {
            get { return this.shortImprintIdentifierField; }
            set { this.shortImprintIdentifierField = value; }
        }

        /// <remarks/>
        public string b079
        {
            get { return ImprintName; }
            set { ImprintName = value; }
        }

        #endregion
    }
}
