using OnixData.Standard.Version3.Header;

namespace OnixData.Standard.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    // [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class OnixMessage
    {
        public OnixMessage()
        {
            Header = new OnixHeader();

            productField = shortProductField = new OnixProduct[0];
        }

        private OnixHeader    headerField;

        private OnixProduct[] productField;
        private OnixProduct[] shortProductField;

        #region ONIX Lists

        public OnixProduct[] OnixProducts
        {
            get
            {
                OnixProduct[] productList = null;

                if (Product != null)
                    productList = Product;
                else if (product != null)
                    productList = product;
                else
                    productList = new OnixProduct[0];

                return productList;
            }
        }

        #endregion

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
