using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixBarcode
    {
        public OnixBarcode()
        {
            BarcodeType = PositionOnProduct = "";
        }

        private string barcodeTypeField;
        private string posOnProdField;

        #region Helper Methods

        public string BarcodeTypeAndPos
        {
            get
            {
                string sCombo = BarcodeType;

                if (!String.IsNullOrEmpty(PositionOnProduct))
                    sCombo += PositionOnProduct;

                return sCombo;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string BarcodeType
        {
            get
            {
                return this.barcodeTypeField;
            }
            set
            {
                this.barcodeTypeField = value;
            }
        }

        /// <remarks/>
        public string PositionOnProduct
        {
            get
            {
                return this.posOnProdField;
            }
            set
            {
                this.posOnProdField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x312
        {
            get { return BarcodeType; }
            set { BarcodeType = value; }
        }

        /// <remarks/>
        public string x313
        {
            get { return PositionOnProduct; }
            set { PositionOnProduct = value; }
        }

        #endregion
    }
}

