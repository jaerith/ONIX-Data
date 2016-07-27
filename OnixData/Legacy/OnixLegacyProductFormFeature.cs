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
    }
}
