using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <summary>
    /// 
    /// This class will serve as the serialization envelope for ONIX files that are written 
    /// using the legacy standard (i.e., Version 2.1).
    /// 
    /// </summary>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    // [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
    public partial class OnixLegacyMessage
    {
        public OnixLegacyMessage()
        {
            Header  = new OnixLegacyHeader();
            Product = new OnixLegacyProduct[0];
        }

        private OnixLegacyHeader    headerField;
        private OnixLegacyProduct[] productField;

        /// <remarks/>
        public OnixLegacyHeader Header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        /// <remarks/>
        public OnixLegacyHeader header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Product")]
        public OnixLegacyProduct[] Product
        {
            get
            {
                return this.productField;
            }
            set
            {
                this.productField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("product")]
        public OnixLegacyProduct[] product
        {
            set
            {
                this.productField = value;
            }
        }
    }
}
