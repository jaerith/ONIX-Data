using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Market;
using OnixData.Version3.Supply;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixProductSupply
    {
        public OnixProductSupply()
        {
            Market                 = new OnixMarket();
            MarketPublishingDetail = new OnixMarketPublishingDetail();
            SupplyDetail           = new OnixSupplyDetail();
        }

        private OnixMarket                 marketField;
        private OnixMarketPublishingDetail marketPublishingDetailField;
        private OnixSupplyDetail           supplyDetailField;

        /// <remarks/>
        public OnixMarket Market
        {
            get
            {
                return this.marketField;
            }
            set
            {
                this.marketField = value;
            }
        }

        /// <remarks/>
        public OnixMarketPublishingDetail MarketPublishingDetail
        {
            get
            {
                return this.marketPublishingDetailField;
            }
            set
            {
                this.marketPublishingDetailField = value;
            }
        }

        /// <remarks/>
        public OnixSupplyDetail SupplyDetail
        {
            get
            {
                return this.supplyDetailField;
            }
            set
            {
                this.supplyDetailField = value;
            }
        }
    }
}
