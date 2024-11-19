using System;

namespace OnixData.Standard.Version3.ProductPart
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixProductPart
    {
        #region CONSTANTS

        public const string CONST_MAIN_PRD_PART_INDICATOR = "MAIN";

        #endregion

        public OnixProductPart()
        {
            PrimaryPart = ProductForm = NumberOfItemsOfThisForm = "";
        }

        private string primaryPartField;
        private string productFormField;
        private string numOfItemsOfThisFormField;

        #region Reference Tags

        /// <remarks/>
        public string PrimaryPart
        {
            get { return this.primaryPartField; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    value = CONST_MAIN_PRD_PART_INDICATOR;
                else
                    this.primaryPartField = value;
            }
        }

        /// <remarks/>
        public string ProductForm
        {
            get { return this.productFormField; }
            set { this.productFormField = value; }
        }

        /// <remarks/>
        public string NumberOfItemsOfThisForm
        {
            get { return this.numOfItemsOfThisFormField; }
            set { this.numOfItemsOfThisFormField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x457
        {
            get { return PrimaryPart; }
            set { PrimaryPart = value; }
        }

        /// <remarks/>
        public string b012
        {
            get { return ProductForm; }
            set { ProductForm = value; }
        }

        /// <remarks/>
        public string x322
        {
            get { return NumberOfItemsOfThisForm; }
            set { NumberOfItemsOfThisForm = value; }
        }

        #endregion
    }
}

