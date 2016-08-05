using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacySeries
    {
        #region CONSTANTS

        public const int CONST_SUBJ_SCHEME_BISAC_CAT_ID = 10;
        public const int CONST_SUBJ_SCHEME_REGION_ID    = 11;

        #endregion

        public OnixLegacySeries()
        {
            NumberWithinSeries = -1;
            TitleOfSeries      = "";
        }

        private int    numberWithinSeriesField;
        private string titleOfSeriesField;

        #region Reference Tags

        /// <remarks/>
        public int NumberWithinSeries
        {
            get
            {
                return this.numberWithinSeriesField;
            }
            set
            {
                this.numberWithinSeriesField = value;
            }
        }

        /// <remarks/>
        public string TitleOfSeries
        {
            get
            {
                return this.titleOfSeriesField;
            }
            set
            {
                this.titleOfSeriesField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b019
        {
            get { return NumberWithinSeries; }
            set { NumberWithinSeries = value; }
        }

        /// <remarks/>
        public string b018
        {
            get { return TitleOfSeries; }
            set { TitleOfSeries = value; }
        }

        #endregion
    }
}
