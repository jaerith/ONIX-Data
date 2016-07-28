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
            Imprint   = new OnixImprint();
            Publisher = new OnixPublisher();
        }

        private OnixImprint   imprintField;
        private OnixPublisher publisherField;

        /// <remarks/>
        public OnixImprint Imprint
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
        public OnixPublisher Publisher
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
    }
}
