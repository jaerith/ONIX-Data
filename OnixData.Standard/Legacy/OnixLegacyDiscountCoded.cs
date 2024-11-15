namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyDiscountCoded
    {
        private int    discountCodeTypeField;
        private string discountCodeTypeNameField;
        private string discountCodeField;

        public OnixLegacyDiscountCoded()
        {
            DiscountCodeType     = -1;
            DiscountCodeTypeName = DiscountCode = "";
        }

        #region Reference Tags

        /// <remarks/>
        public int DiscountCodeType
        {
            get
            {
                return this.discountCodeTypeField;
            }
            set
            {
                this.discountCodeTypeField = value;
            }
        }

        /// <remarks/>
        public string DiscountCodeTypeName
        {
            get
            {
                return this.discountCodeTypeNameField;
            }
            set
            {
                this.discountCodeTypeNameField = value;
            }
        }

        /// <remarks/>
        public string DiscountCode
        {
            get
            {
                return this.discountCodeField;
            }
            set
            {
                this.discountCodeField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int j363
        {
            get { return DiscountCodeType; }
            set { DiscountCodeType = value; }
        }

        /// <remarks/>
        public string j378
        {
            get { return DiscountCodeTypeName; }
            set { DiscountCodeTypeName = value; }
        }

        /// <remarks/>
        public string j364
        {
            get { return DiscountCode; }
            set { DiscountCode = value; }
        }

        #endregion
    }

}
