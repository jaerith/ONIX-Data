namespace OnixData.Standard.Version3.Related
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixRelatedMaterial
    {
        public OnixRelatedMaterial()
        {
            relatedWorkField    = shortRelatedWorkField    = new OnixRelatedWork[0];
            relatedProductField = shortRelatedProductField = new OnixRelatedProduct[0];
        }

        private OnixRelatedWork[]    relatedWorkField;
        private OnixRelatedWork[]    shortRelatedWorkField;
        private OnixRelatedProduct[] relatedProductField;
        private OnixRelatedProduct[] shortRelatedProductField;

        #region ONIX Lists

        public OnixRelatedWork[] OnixRelatedWorkList
        {
            get
            {
                OnixRelatedWork[] RelatedWorks = null;

                if (this.relatedWorkField != null)
                    RelatedWorks = this.relatedWorkField;
                else if (this.shortRelatedWorkField != null)
                    RelatedWorks = this.shortRelatedWorkField;
                else
                    RelatedWorks = new OnixRelatedWork[0];

                return RelatedWorks;
            }
        }

        public OnixRelatedProduct[] OnixRelatedProductList
        {
            get
            {
                OnixRelatedProduct[] RelatedProducts = null;

                if (this.relatedProductField != null)
                    RelatedProducts = this.relatedProductField;
                else if (this.shortRelatedProductField != null)
                    RelatedProducts = this.shortRelatedProductField;
                else
                    RelatedProducts = new OnixRelatedProduct[0];

                return RelatedProducts;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedWork")]
        public OnixRelatedWork[] RelatedWork
        {
            get
            {
                return this.relatedWorkField;
            }
            set
            {
                this.relatedWorkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedProduct")]
        public OnixRelatedProduct[] RelatedProduct
        {
            get
            {
                return this.relatedProductField;
            }
            set
            {
                this.relatedProductField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("relatedwork")]
        public OnixRelatedWork[] relatedwork
        {
            get { return shortRelatedWorkField; }
            set { shortRelatedWorkField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("relatedproduct")]
        public OnixRelatedProduct[] relatedproduct
        {
            get { return shortRelatedProductField; }
            set { shortRelatedProductField = value; }
        }

        #endregion
    }
}
