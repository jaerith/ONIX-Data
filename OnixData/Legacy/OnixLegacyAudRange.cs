using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyAudRange
    {
        #region CONSTANTS

        public const int CONST_AUD_RANGE_TYPE_US_GRADES    = 11;
        public const int CONST_AUD_RANGE_TYPE_UK_GRADES    = 12;
        public const int CONST_AUD_RANGE_TYPE_INTEREST_AGE = 17;
        public const int CONST_AUD_RANGE_TYPE_READING_AGE  = 18;
        public const int CONST_AUD_RANGE_TYPE_SP_GRADES    = 19;
        public const int CONST_AUD_RANGE_TYPE_NW_GRADES    = 20;

        public const int CONST_AUD_RANGE_PRCN_EXACT = 1;
        public const int CONST_AUD_RANGE_PRCN_FROM  = 3;
        public const int CONST_AUD_RANGE_PRCN_TO    = 4;

        #endregion

        private int      audienceRangeQualifierField;
        private int[]    audienceRangePrecisionField;
        private string[] audienceRangeValueField;

        public OnixLegacyAudRange()
        {
            AudienceRangeQualifier = -1;
            AudienceRangePrecision = new int[0];
            AudienceRangeValue     = new string[0];
        }

        #region Reference Tags

        /// <remarks/>
        public int AudienceRangeQualifier
        {
            get
            {
                return this.audienceRangeQualifierField;
            }
            set
            {
                this.audienceRangeQualifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AudienceRangePrecision")]
        public int[] AudienceRangePrecision
        {
            get
            {
                return this.audienceRangePrecisionField;
            }
            set
            {
                this.audienceRangePrecisionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AudienceRangeValue")]
        public string[] AudienceRangeValue
        {
            get
            {
                return this.audienceRangeValueField;
            }
            set
            {
                this.audienceRangeValueField = value;
            }
        }

        public string USAgeFrom
        {
            get
            {                
                string FoundAgeFrom = FindAudRangeValue(CONST_AUD_RANGE_TYPE_INTEREST_AGE, CONST_AUD_RANGE_PRCN_FROM);
                if (!String.IsNullOrEmpty(FoundAgeFrom))
                    FoundAgeFrom = FindAudRangeValue(CONST_AUD_RANGE_TYPE_READING_AGE, CONST_AUD_RANGE_PRCN_FROM);

                return FoundAgeFrom;
            }
        }

        public string USAgeTo
        {
            get
            {
                string FoundAgeTo = FindAudRangeValue(CONST_AUD_RANGE_TYPE_INTEREST_AGE, CONST_AUD_RANGE_PRCN_TO);
                if (!String.IsNullOrEmpty(FoundAgeTo))
                    FoundAgeTo = FindAudRangeValue(CONST_AUD_RANGE_TYPE_READING_AGE, CONST_AUD_RANGE_PRCN_TO);

                return FoundAgeTo;
            }
        }

        public string USGradeExact
        {
            get
            {
                return FindAudRangeValue(CONST_AUD_RANGE_TYPE_US_GRADES, CONST_AUD_RANGE_PRCN_EXACT);
            }
        }

        public string USGradeFrom
        {
            get
            {
                return FindAudRangeValue(CONST_AUD_RANGE_TYPE_US_GRADES, CONST_AUD_RANGE_PRCN_FROM);
            }
        }

        public string USGradeTo
        {
            get
            {
                return FindAudRangeValue(CONST_AUD_RANGE_TYPE_US_GRADES, CONST_AUD_RANGE_PRCN_TO);
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b074
        {
            get { return AudienceRangeQualifier; }
            set { AudienceRangeQualifier = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b075")]
        public int[] b075
        {
            get { return AudienceRangePrecision; }
            set { AudienceRangePrecision = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b076")]
        public string[] b076
        {
            get { return AudienceRangeValue; }
            set { AudienceRangeValue = value; }
        }

        #endregion

        #region Support Methods

        private string FindAudRangeValue(int pnRangeType, int pnPrecisionType)
        {
            string FoundRangeValue = "";

            if ((AudienceRangeQualifier == pnRangeType) && (AudienceRangePrecision != null))
            {
                int nFoundIndex = Array.IndexOf(AudienceRangePrecision, pnPrecisionType);

                if ((nFoundIndex >= 0) && (AudienceRangeValue != null) && (AudienceRangeValue.Length >= nFoundIndex))
                    FoundRangeValue = AudienceRangeValue[nFoundIndex];
            }

            return FoundRangeValue;
        }

        #endregion
    }
}
