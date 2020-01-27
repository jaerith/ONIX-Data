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
            Imprint        = new OnixImprint[0];
            Publisher      = new OnixPublisher[0];
            PublishingDate = new OnixPubDate[0];
        }


        private OnixImprint[]   imprintField;
        private OnixImprint[]   shortImprintField;

        private OnixPubDate[]   pubDateField;
        private OnixPubDate[]   shortPubDateField;

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

        public OnixPubDate[] OnixPublishingDateList
        {
            get
            {
                OnixPubDate[] PubDates = null;

                if (this.pubDateField != null)
                    PubDates = this.pubDateField;
                else if (this.shortPubDateField != null)
                    PubDates = this.shortPubDateField;
                else
                    PubDates = new OnixPubDate[0];

                return PubDates;
            }
        }

        #endregion

        #region Helper Methods

        public string PublicationDate
        {
            get
            {
                OnixPubDate[] PubDtList = this.OnixPublishingDateList;

                string sPubDate = "";

                if ((PubDtList != null) && (PubDtList.Length > 0))
                {                    
                    OnixPubDate FoundPubDate =
                        PubDtList.Where(x => x.PublishingDateRole == OnixPubDate.CONST_PUB_DT_ROLE_NORMAL).LastOrDefault();

                    if ((FoundPubDate != null) && !String.IsNullOrEmpty(FoundPubDate.Date))
                        sPubDate = FoundPubDate.Date;
                }

                return sPubDate;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Imprint")]
        public OnixImprint[] Imprint
        {
            get { return this.imprintField; }
            set { this.imprintField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Publisher")]
        public OnixPublisher[] Publisher
        {
            get { return this.publisherField; }
            set { this.publisherField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PublishingDate")]
        public OnixPubDate[] PublishingDate
        {
            get { return this.pubDateField; }
            set { this.pubDateField = value; }
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("publishingdate")]
        public OnixPubDate[] publishingdate
        {
            get { return this.shortPubDateField; }
            set { this.shortPubDateField = value; }
        }

        #endregion
    }
}
