using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Market
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
    }
}
