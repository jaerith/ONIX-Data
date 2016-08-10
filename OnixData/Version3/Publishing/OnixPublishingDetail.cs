using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Publishing
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixPublishingDetail
    {
        public OnixPublishingDetail()
        {
            Imprint   = new OnixImprint[0];
            Publisher = new OnixPublisher[0];
        }

        private OnixImprint[]   imprintField;
        private OnixImprint[]   shortImprintField;
        private OnixPublisher[] publisherField;
        private OnixPublisher[] shortPublisherField;

        #region ONIX Lists

        public OnixImprint[] OnixImprintList
        {
            get
            {
                OnixImprint[] Imprints = null;

                if (this.imprintField != null)
                    Imprints = this.imprintField;
                else if (this.shortImprintField != null)
                    Imprints = this.shortImprintField;
                else
                    Imprints = new OnixImprint[0];

                return Imprints;
            }
        }

        public OnixPublisher[] OnixPublisherList
        {
            get
            {
                OnixPublisher[] Publishers = null;

                if (this.publisherField != null)
                    Publishers = this.publisherField;
                else if (this.shortPublisherField != null)
                    Publishers = this.shortPublisherField;
                else
                    Publishers = new OnixPublisher[0];

                return Publishers;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Imprint")]
        public OnixImprint[] Imprint
        {
            get
            {
                return this.imprintField;
            }
            set
            {
                this.imprintField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Publisher")]
        public OnixPublisher[] Publisher
        {
            get
            {
                return this.publisherField;
            }
            set
            {
                this.publisherField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("imprint")]
        public OnixImprint[] imprint
        {
            get { return shortImprintField; }
            set { shortImprintField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("publisher")]
        public OnixPublisher[] publisher
        {
            get { return shortPublisherField; }
            set { shortPublisherField = value; }
        }

        #endregion
    }
}
