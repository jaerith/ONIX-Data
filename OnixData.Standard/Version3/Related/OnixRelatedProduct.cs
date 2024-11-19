﻿using System;
using System.Linq;

namespace OnixData.Standard.Version3.Related
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixRelatedProduct
    {
        public OnixRelatedProduct()
        {
            ProductRelationCode = -1;

            productIdentifierField = shortProductIdentifierField = new OnixProductId[0];
        }

        private int productRelationCodeField;

        private OnixProductId[] productIdentifierField;
        private OnixProductId[] shortProductIdentifierField;

        #region Helper Methods

        public string EAN
        {
            get
            {
                OnixProductId[] ProductIdList = OnixProductIdList;

                string sEAN = "";
                if ((ProductIdList != null) && (ProductIdList.Length > 0))
                {
                    OnixProductId EanProductId =
                        ProductIdList.Where(x => (x.ProductIDType == OnixProductId.CONST_PRODUCT_TYPE_EAN) ||
                                                 (x.ProductIDType == OnixProductId.CONST_PRODUCT_TYPE_ISBN13)).FirstOrDefault();

                    if ((EanProductId != null) && !String.IsNullOrEmpty(EanProductId.IDValue))
                        sEAN = EanProductId.IDValue;
                }

                return sEAN;
            }
        }

        public string ISBN
        {
            get
            {
                OnixProductId[] ProductIdList = OnixProductIdList;

                string sISBN = "";
                if ((ProductIdList != null) && (ProductIdList.Length > 0))
                {
                    OnixProductId EanProductId =
                        ProductIdList.Where(x => (x.ProductIDType == OnixProductId.CONST_PRODUCT_TYPE_ISBN)).FirstOrDefault();

                    if ((EanProductId != null) && !String.IsNullOrEmpty(EanProductId.IDValue))
                        sISBN = EanProductId.IDValue;
                }

                return sISBN;
            }
        }

        #endregion

        #region ONIX Lists

        public OnixProductId[] OnixProductIdList
        {
            get
            {
                OnixProductId[] ProductIds = null;

                if (this.productIdentifierField != null)
                    ProductIds = this.productIdentifierField;
                else if (this.shortProductIdentifierField != null)
                    ProductIds = this.shortProductIdentifierField;
                else
                    ProductIds = new OnixProductId[0];

                return ProductIds;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public int ProductRelationCode
        {
            get
            {
                return this.productRelationCodeField;
            }
            set
            {
                this.productRelationCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductIdentifier")]
        public OnixProductId[] ProductIdentifier
        {
            get
            {
                return this.productIdentifierField;
            }
            set
            {
                this.productIdentifierField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x455
        {
            get { return ProductRelationCode; }
            set { ProductRelationCode = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("productidentifier")]
        public OnixProductId[] productidentifier
        {
            get { return shortProductIdentifierField; }
            set { shortProductIdentifierField = value; }
        }

        #endregion
    }
}
