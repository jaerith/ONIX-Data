using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixLegacyProductClassification
    {
        #region CONSTANTS

        public const string CONST_PRD_CLASS_TYPE_WCO    = "01"; // Harmonized System World Customs Organization Harmonized Commodity Coding & Description System 
        public const string CONST_PRD_CLASS_TYPE_UNSPSC = "02"; // UN Standard Product & Service Classification 
        public const string CONST_PRD_CLASS_TYPE_HMCE   = "03"; // UK Customs & Excise classifications, based on the Harmonized System 
        public const string CONST_PRD_CLASS_TYPE_WAREN  = "04"; // Warenverzeichnis für die Außenhandelsstatistik German export trade classification, based on the Harmonised System
        public const string CONST_PRD_CLASS_TYPE_TARIC  = "05"; // EU TARIC codes, an extended version of the Harmonized System
        public const string CONST_PRD_CLASS_TYPE_PROP   = "07";

        #endregion

        public OnixLegacyProductClassification()
        {
            prdClassTypeField = prdClassCodeField = "";
        }

        private string prdClassTypeField;
        private string prdClassCodeField;

        #region Reference Tags

        /// <remarks/>
        public string ProductClassificationType
        {
            get
            {
                return this.prdClassTypeField;
            }
            set
            {
                this.prdClassTypeField = value;
            }
        }

        /// <remarks/>
        public string ProductClassificationCode
        {
            get
            {
                return this.prdClassCodeField;
            }
            set
            {
                this.prdClassCodeField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string b274
        {
            get { return ProductClassificationType; }
            set { ProductClassificationType = value; }
        }

        /// <remarks/>
        public string b275
        {
            get { return ProductClassificationCode; }
            set { ProductClassificationCode = value; }
        }

        #endregion

    }
}
