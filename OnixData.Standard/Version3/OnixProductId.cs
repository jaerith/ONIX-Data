namespace OnixData.Standard.Version3
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixProductId
    {
        #region CONSTANTS

        public const int CONST_PRODUCT_TYPE_PROP   = 1;
        public const int CONST_PRODUCT_TYPE_ISBN   = 2;
        public const int CONST_PRODUCT_TYPE_EAN    = 3;
        public const int CONST_PRODUCT_TYPE_UPC    = 4;
        public const int CONST_PRODUCT_TYPE_ISMN   = 5;
        public const int CONST_PRODUCT_TYPE_DOI    = 6;
        public const int CONST_PRODUCT_TYPE_LCCN   = 13;
        public const int CONST_PRODUCT_TYPE_GTIN   = 14;
        public const int CONST_PRODUCT_TYPE_ISBN13 = 15;

        #endregion

        public OnixProductId()
        {
            ProductIDType = -1;
            IDValue       = "";
        }

        private int    productIDTypeField;
        private string iDValueField;

        #region Reference Tags

        /// <remarks/>
        public int ProductIDType
        {
            get
            {
                return this.productIDTypeField;
            }
            set
            {
                this.productIDTypeField = value;
            }
        }

        /// <remarks/>
        public string IDValue
        {
            get
            {
                return this.iDValueField;
            }
            set
            {
                this.iDValueField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b221
        {
            get
            {
                return ProductIDType;
            }
            set
            {
                ProductIDType = value;
            }
        }

        /// <remarks/>
        public string b244
        {
            get
            {
                return IDValue;
            }
            set
            {
                IDValue = value;
            }
        }

        #endregion

    }
}
