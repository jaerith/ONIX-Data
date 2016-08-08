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
            MeasureType = -1;

            Measurement = 0;

            MeasureUnitCode = "";
        }

        private int     measureTypeField;
        private decimal measurementField;
        private string  measureUnitCodeField;

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
