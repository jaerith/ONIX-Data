namespace OnixData.Standard.Legacy
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
            Product = null;
            product = null;
        }

        private OnixLegacyHeader    headerField;

        private OnixLegacyProduct[] productField;
        private OnixLegacyProduct[] altProductField;

        #region ONIX Lists

        public OnixLegacyProduct[] OnixProducts
        {
            get
            {
                OnixLegacyProduct[] productList = null;

                if (Product != null)
                    productList = Product;
                else if (product != null)
                    productList = product;
                else
                    productList = new OnixLegacyProduct[0];

                return productList;
            }
        }

        #endregion

        #region Reference Tags

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

        #endregion

        #region Short Tags

        /// <remarks/>
        public OnixLegacyHeader header
        {
            get
            {
                return Header;
            }
            set
            {
                Header = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("product")]
        public OnixLegacyProduct[] product
        {
            get
            {
                return altProductField;
            }
            set
            {
                altProductField = value;
            }
        }

        #endregion
    }
}
