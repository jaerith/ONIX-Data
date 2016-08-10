using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixAudienceRange
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

        public OnixAudienceRange()
        {
            AudienceRangeQualifier = -1;

            audienceRangePrecisionField = shortAudienceRangePrecisionField = new int[0];
            audienceRangeValueField     = shortAudienceRangeValueField     = new string[0];
        }

        private int      audienceRangeQualifierField;
        private int[]    audienceRangePrecisionField;
        private int[]    shortAudienceRangePrecisionField;
        private string[] audienceRangeValueField;
        private string[] shortAudienceRangeValueField;

        #region ONIX Helper Methods

        public string USAgeFrom
        {
            get
            {
                string FoundAgeFrom = FindAudRangeValue(CONST_AUD_RANGE_TYPE_INTEREST_AGE, CONST_AUD_RANGE_PRCN_FROM);
                if (String.IsNullOrEmpty(FoundAgeFrom))
                    FoundAgeFrom = FindAudRangeValue(CONST_AUD_RANGE_TYPE_READING_AGE, CONST_AUD_RANGE_PRCN_FROM);

                return FoundAgeFrom;
            }
        }

        public string USAgeTo
        {
            get
            {
                string FoundAgeTo = FindAudRangeValue(CONST_AUD_RANGE_TYPE_INTEREST_AGE, CONST_AUD_RANGE_PRCN_TO);
                if (String.IsNullOrEmpty(FoundAgeTo))
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

        #region ONIX Lists

        public int[] OnixAudRangePrecisionList
        {
            get
            {
                int[] PrecisionList = null;

                if (this.audienceRangePrecisionField != null)
                    PrecisionList = this.audienceRangePrecisionField;
                else if (this.shortAudienceRangePrecisionField != null)
                    PrecisionList = this.shortAudienceRangePrecisionField;
                else
                    PrecisionList = new int[0];

                return PrecisionList;
            }
        }

        public string[] OnixAudRangeValueList
        {
            get
            {
                string[] ValueList = null;

                if (this.audienceRangeValueField != null)
                    ValueList = this.audienceRangeValueField;
                else if (this.shortAudienceRangeValueField != null)
                    ValueList = this.shortAudienceRangeValueField;
                else
                    ValueList = new string[0];

                return ValueList;
            }
        }

        #endregion

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
            get { return shortAudienceRangePrecisionField; }
            set { shortAudienceRangePrecisionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b076")]
        public string[] b076
        {
            get { return shortAudienceRangeValueField; }
            set { shortAudienceRangeValueField = value; }
        }

        #endregion

        #region Support Methods

        private string FindAudRangeValue(int pnRangeType, int pnPrecisionType)
        {
            string FoundRangeValue = "";

            if ((AudienceRangeQualifier == pnRangeType) && (OnixAudRangePrecisionList != null))
            {
                int nFoundIndex = Array.IndexOf(OnixAudRangePrecisionList, pnPrecisionType);

                if ((nFoundIndex >= 0) && (OnixAudRangeValueList != null) && (OnixAudRangeValueList.Length >= nFoundIndex))
                    FoundRangeValue = OnixAudRangeValueList[nFoundIndex];
            }

            return FoundRangeValue;
        }

        #endregion
    }
}
