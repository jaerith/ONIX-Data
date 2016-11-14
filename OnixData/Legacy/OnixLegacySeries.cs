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
            Title              = new OnixLegacyTitle();
        }

        private int    numberWithinSeriesField;
        private string titleOfSeriesField;

        private OnixLegacyTitle titleField;

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

        /// <remarks/>
        public OnixLegacyTitle Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
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

        /// <remarks/>
        public OnixLegacyTitle title
        {
            get { return Title; }
            set { Title = value; }
        }

        #endregion
    }
}
