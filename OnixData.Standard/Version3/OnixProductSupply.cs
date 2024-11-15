using OnixData.Standard.Version3.Market;
using OnixData.Standard.Version3.Supply;

namespace OnixData.Standard.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixProductSupply
    {
        public OnixProductSupply()
        {
            Market                 = new OnixMarket();
            MarketPublishingDetail = new OnixMarketPublishingDetail();

            supplyDetailField = shortSupplyDetailField = new OnixSupplyDetail[0];
        }

        private OnixMarket                 marketField;
        private OnixMarketPublishingDetail marketPublishingDetailField;
        private OnixSupplyDetail[]         supplyDetailField;
        private OnixSupplyDetail[]         shortSupplyDetailField;

        #region Helper Methods

        public OnixSupplyDetail GetSupplyDetailWithUSDPrice(bool pbCheckHasReturnCodeTypeBisac = false)
        {
            OnixSupplyDetail USDSupplyDetail = new OnixSupplyDetail();

            foreach (OnixSupplyDetail TmpSupplyDetail in OnixSupplyDetailList)
            {
                if (pbCheckHasReturnCodeTypeBisac)
                {
                    if (TmpSupplyDetail.HasUSDPrice() && TmpSupplyDetail.HasReturnCodeTypeBisac())
                    {
                        USDSupplyDetail = TmpSupplyDetail;
                        break;
                    }
                }
                else if (TmpSupplyDetail.HasUSDPrice())
                {
                    USDSupplyDetail = TmpSupplyDetail;
                    break;
                }
            }

            return USDSupplyDetail;
        }

        public OnixSupplyDetail[] OnixSupplyDetailList
        {
            get
            {
                OnixSupplyDetail[] SupplyDetailList = new OnixSupplyDetail[0];

                if (this.supplyDetailField != null)
                    SupplyDetailList = this.supplyDetailField;
                else if (this.shortSupplyDetailField != null)
                    SupplyDetailList = this.shortSupplyDetailField;
                else
                    SupplyDetailList = new OnixSupplyDetail[0];

                return SupplyDetailList;
            }
        }

        public OnixSupplyDetail USDSupplyDetail
        {
            get
            {
                OnixSupplyDetail usdSupplyDetail = new OnixSupplyDetail();

                foreach (OnixSupplyDetail tmpSupplyDetail in OnixSupplyDetailList)
                {
                    if (tmpSupplyDetail.HasUSDPrice())
                    {
                        usdSupplyDetail = tmpSupplyDetail;
                        break;
                    }
                }

                return usdSupplyDetail;
            }
        }

        #endregion

        #region Reference Tags

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
        [System.Xml.Serialization.XmlElementAttribute("SupplyDetail")]
        public OnixSupplyDetail[] SupplyDetail
        {
            get { return this.supplyDetailField; }
            set { this.supplyDetailField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public OnixMarket market
        {
            get { return Market; }
            set { Market = value; }
        }

        /// <remarks/>
        public OnixMarketPublishingDetail marketpublishingdetail
        {
            get { return MarketPublishingDetail; }
            set { MarketPublishingDetail = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("supplydetail")]
        public OnixSupplyDetail[] supplydetail
        {
            get { return this.shortSupplyDetailField; }
            set { this.shortSupplyDetailField = value; }
        }

        #endregion
    }
}
