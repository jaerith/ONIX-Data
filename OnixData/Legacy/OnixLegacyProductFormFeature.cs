using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyProductFormFeature
    {
        public OnixLegacyProductFormFeature()
        {
            ProductFormFeatureType  = -1;
            ProductFormFeatureValue = "";
        }

        private int    productFormFeatureTypeField;
        private string productFormFeatureValueField;

        #region Reference Tags

        /// <remarks/>
        public int ProductFormFeatureType
        {
            get
            {
                return this.productFormFeatureTypeField;
            }
            set
            {
                this.productFormFeatureTypeField = value;
            }
        }

        /// <remarks/>
        public string ProductFormFeatureValue
        {
            get
            {
                return this.productFormFeatureValueField;
            }
            set
            {
                this.productFormFeatureValueField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b334
        {
            get { return ProductFormFeatureType; }
            set { ProductFormFeatureType = value; }
        }

        /// <remarks/>
        public string b335
        {
            get { return ProductFormFeatureValue; }
            set { ProductFormFeatureValue = value; }
        }

        #endregion
    }
}
