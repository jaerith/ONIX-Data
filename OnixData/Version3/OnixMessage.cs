using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Header;

namespace OnixData.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    // [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class OnixMessage
    {
        public OnixMessage()
        {
            Header  = new OnixHeader();
            Product = new OnixProduct[0];
        }

        private OnixHeader    headerField;
        private OnixProduct[] productField;

        #region Reference Tags

        /// <remarks/>
        public OnixHeader Header
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
        public OnixProduct[] Product
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public OnixHeader header
        {
            get { return Header; }
            set { Header = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("product")]
        public OnixProduct[] product
        {
            get { return Product; }
            set { Product = value; }
        }

        #endregion
    }
}
