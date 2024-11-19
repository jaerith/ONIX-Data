using System.Linq;

namespace OnixData.Standard.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixMeasure
    {
        #region CONSTANTS

        public const int CONST_MEASURE_TYPE_HEIGHT = 1;
        public const int CONST_MEASURE_TYPE_WIDTH  = 2;
        public const int CONST_MEASURE_TYPE_THICK  = 3;
        public const int CONST_MEASURE_TYPE_WEIGHT = 8;
        public const int CONST_MEASURE_TYPE_DIAMTR = 9;

        public const string CONST_MEASURE_METRIC_UNIT_CM = "cm";
        public const string CONST_MEASURE_METRIC_UNIT_GR = "gr";
        public const string CONST_MEASURE_METRIC_UNIT_MM = "mm";

        public const string CONST_MEASURE_USCS_UNIT_LB   = "lb";
        public const string CONST_MEASURE_USCS_UNIT_OZ   = "oz";
        public const string CONST_MEASURE_USCS_UNIT_IN   = "in";

        public readonly string[] CONST_METRIC_UNIT_TYPES
            = {
                CONST_MEASURE_METRIC_UNIT_CM, CONST_MEASURE_METRIC_UNIT_GR,
                CONST_MEASURE_METRIC_UNIT_MM
              };

        #endregion

        public OnixMeasure()
        {
            MeasureType = -1;

            Measurement = 0;

            MeasureUnitCode = "";
        }

        private int     measureTypeField;
        private decimal measurementField;
        private string  measureUnitCodeField;

        #region Helper Methods

        public bool IsMetricUnitType()
        {
            return CONST_METRIC_UNIT_TYPES.Contains(this.MeasureUnitCode);
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public int MeasureType
        {
            get
            {
                return this.measureTypeField;
            }
            set
            {
                this.measureTypeField = value;
            }
        }

        /// <remarks/>
        public decimal Measurement
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
        public int x315
        {
            get { return MeasureType; }
            set { MeasureType = value; }
        }

        /// <remarks/>
        public decimal c094
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
