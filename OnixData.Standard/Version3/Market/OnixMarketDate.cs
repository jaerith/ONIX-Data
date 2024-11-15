namespace OnixData.Standard.Version3.Market
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixMarketDate
    {
        public OnixMarketDate()
        {
            MarketDateRole = DateFormat = -1;

            Date = 0;
        }

        private int  marketDateRoleField;
        private int  dateFormatField;
        private uint dateField;

        #region Reference Tags

        /// <remarks/>
        public int MarketDateRole
        {
            get
            {
                return this.marketDateRoleField;
            }
            set
            {
                this.marketDateRoleField = value;
            }
        }

        /// <remarks/>
        public int DateFormat
        {
            get
            {
                return this.dateFormatField;
            }
            set
            {
                this.dateFormatField = value;
            }
        }

        /// <remarks/>
        public uint Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int j408
        {
            get { return MarketDateRole; }
            set { MarketDateRole = value; }
        }

        /// <remarks/>
        public int j260
        {
            get { return DateFormat; }
            set { DateFormat = value; }
        }

        /// <remarks/>
        public uint b306
        {
            get { return Date; }
            set { Date = value; }
        }

        #endregion
    }
}
