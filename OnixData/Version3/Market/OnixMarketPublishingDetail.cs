using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Market
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixMarketPublishingDetail
    {
        public OnixMarketPublishingDetail()
        {
            MarketPublishingStatus = -1;
            MarketDate             = new OnixMarketDate();
        }

        private int            marketPublishingStatusField;
        private OnixMarketDate marketDateField;

        /// <remarks/>
        public int MarketPublishingStatus
        {
            get
            {
                return this.marketPublishingStatusField;
            }
            set
            {
                this.marketPublishingStatusField = value;
            }
        }

        /// <remarks/>
        public OnixMarketDate MarketDate
        {
            get
            {
                return this.marketDateField;
            }
            set
            {
                this.marketDateField = value;
            }
        }
    }
}
