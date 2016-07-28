using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3
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

        #endregion

        public OnixMeasure()
        {
            MeasureType = Measurement = -1;

            MeasureUnitCode = "";
        }

        private int    measureTypeField;
        private int    measurementField;
        private string measureUnitCodeField;

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
        public int Measurement
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
    }
}
