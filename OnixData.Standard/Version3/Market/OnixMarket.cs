using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Market
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixMarket
    {
        public OnixMarket()
        {
            Territory = new OnixMarketTerritory();
        }

        private OnixMarketTerritory territoryField;

        #region Reference Tags

        /// <remarks/>
        public OnixMarketTerritory Territory
        {
            get
            {
                return this.territoryField;
            }
            set
            {
                this.territoryField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public OnixMarketTerritory territory
        {
            get { return Territory; }
            set { Territory = value; }
        }

        #endregion
    }
}
