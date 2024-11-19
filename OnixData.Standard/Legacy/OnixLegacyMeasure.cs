using System.Globalization;

namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyMeasure
    {
        #region CONSTANTS

        public const int CONST_MEASURE_TYPE_HEIGHT = 1;
        public const int CONST_MEASURE_TYPE_WIDTH  = 2;
        public const int CONST_MEASURE_TYPE_THICK  = 3;
        public const int CONST_MEASURE_TYPE_WEIGHT = 8;
        public const int CONST_MEASURE_TYPE_DIAMTR = 9;

        public const string CONST_MEASURE_UNIT_US_WEIGHT_LB  = "lb";
        public const string CONST_MEASURE_UNIT_US_WEIGHT_OZ  = "oz";
        public const string CONST_MEASURE_UNIT_METRIC_WEIGHT = "gr";

        public const string CONST_MEASURE_UNIT_US_LENGTH_IN  = "in";
        public const string CONST_MEASURE_UNIT_METRIC_LENGTH = "mm";

        public static readonly int[] MEASURE_TYPES_DIM = 
            new int[] { CONST_MEASURE_TYPE_HEIGHT, CONST_MEASURE_TYPE_WIDTH, CONST_MEASURE_TYPE_THICK, CONST_MEASURE_TYPE_DIAMTR };

        public static readonly int[] MEASURE_TYPES_WEIGHT = new int[] { CONST_MEASURE_TYPE_WEIGHT };

        public static readonly string[] MEASURE_WEIGHTS_US = new string[] { CONST_MEASURE_UNIT_US_WEIGHT_LB, CONST_MEASURE_UNIT_US_WEIGHT_OZ };

        #endregion

        public OnixLegacyMeasure()
        {
            MeasureTypeCode = -1;
            Measurement     = "";
            MeasureUnitCode = "";
        }

        private int     measureTypeCodeField;
        private string  measurementField;
        private string  measureUnitCodeField;

        #region Reference Tags

        /// <remarks/>
        public int MeasureTypeCode
        {
            get
            {
                return this.measureTypeCodeField;
            }
            set
            {
                this.measureTypeCodeField = value;
            }
        }

        /// <remarks/>
        public string Measurement
        {
            get
            {
                return this.measurementField;
            }
            set
            {
                this.measurementField = value;
            }
        }

        public decimal MeasurementNum
        {
            get
            {
                decimal nMeasurementVal = 0;

                if (!string.IsNullOrEmpty(Measurement))
                    decimal.TryParse(Measurement, NumberStyles.Any, CultureInfo.InvariantCulture, out nMeasurementVal);

                return nMeasurementVal;
            }
        }

        /// <remarks/>
        public string MeasureUnitCode
        {
            get
            {
                return this.measureUnitCodeField;
            }
            set
            {
                this.measureUnitCodeField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int c093
        {
            get { return MeasureTypeCode; }
            set { MeasureTypeCode = value; }
        }

        /// <remarks/>
        public string c094
        {
            get { return Measurement; }
            set { Measurement = value; }
        }

        /// <remarks/>
        public string c095
        {
            get { return MeasureUnitCode; }
            set { MeasureUnitCode = value; }
        }

        #endregion
    }
}
