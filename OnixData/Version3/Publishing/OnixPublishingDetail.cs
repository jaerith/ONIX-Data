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
        private OnixPublisher[] publisherField;

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
            get { return Imprint; }
            set { Imprint = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("publisher")]
        public OnixPublisher[] publisher
        {
            get { return Publisher; }
            set { Publisher = value; }
        }

        #endregion
    }
}
