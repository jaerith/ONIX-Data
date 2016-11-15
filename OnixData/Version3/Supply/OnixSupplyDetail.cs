using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Version3.Price;

namespace OnixData.Version3.Supply
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixSupplyDetail
    {
        #region CONSTANTS

        public const int CONST_RET_CODE_TYPE_BISAC = 2;

        #endregion	
	
        public OnixSupplyDetail()
        {
            ProductAvailability = "";
            PackQuantity        = -1;

            Price             = new OnixPrice[0];
            Supplier          = new OnixSupplier[0];
            ReturnsConditions = new OnixReturnsConditions[0];
        }

        private string productAvailabilityField;
        private int    packQuantityField;

        private OnixReturnsConditions[] returnsConditionsField;
        private OnixReturnsConditions[] shortReturnsConditionsField;
        private OnixPrice[]             priceField;
        private OnixPrice[]             shortPriceField;
        private OnixSupplier[]          supplierField;
        private OnixSupplier[]          shortSupplierField;

        #region ONIX Lists

        public OnixPrice[] OnixPriceList
        {
            get
            {
                OnixPrice[] Prices = null;

                if (this.priceField != null)
                    Prices = this.priceField;
                else if (this.shortPriceField != null)
                    Prices = this.shortPriceField;
                else
                    Prices = new OnixPrice[0];

                return Prices;
            }
        }

        public OnixReturnsConditions[] OnixReturnsConditionsList
        {
            get
            {
                OnixReturnsConditions[] ReturnsConditions = null;

                if (this.returnsConditionsField != null)
                    ReturnsConditions = this.returnsConditionsField;
                else if (this.shortReturnsConditionsField != null)
                    ReturnsConditions = this.shortReturnsConditionsField;
                else
                    ReturnsConditions = new OnixReturnsConditions[0];

                return ReturnsConditions;
            }
        }

        public OnixSupplier[] OnixSupplierList
        {
            get
            {
                OnixSupplier[] Suppliers = null;

                if (this.supplierField != null)
                    Suppliers = this.supplierField;
                else if (this.shortSupplierField != null)
                    Suppliers = this.shortSupplierField;
                else
                    Suppliers = new OnixSupplier[0];

                return Suppliers;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string ProductAvailability
        {
            get
            {
                return this.productAvailabilityField;
            }
            set
            {
                this.productAvailabilityField = value;
            }
        }

        /// <remarks/>
        public int PackQuantity
        {
            get
            {
                return this.packQuantityField;
            }
            set
            {
                this.packQuantityField = value;
            }
        }        

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Price", IsNullable = false)]
        public OnixPrice[] Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReturnsConditions", IsNullable = false)]
        public OnixReturnsConditions[] ReturnsConditions
        {
            get
            {
                return this.returnsConditionsField;
            }
            set
            {
                this.returnsConditionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Supplier", IsNullable = false)]
        public OnixSupplier[] Supplier
        {
            get
            {
                return this.supplierField;
            }
            set
            {
                this.supplierField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string j396
        {
            get { return ProductAvailability; }
            set { ProductAvailability = value; }
        }

        /// <remarks/>
        public int j145
        {
            get { return PackQuantity; }
            set { PackQuantity = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("price", IsNullable = false)]
        public OnixPrice[] price
        {
            get { return shortPriceField; }
            set { shortPriceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("returnsconditions", IsNullable = false)]
        public OnixReturnsConditions[] returnsconditions
        {
            get { return shortReturnsConditionsField; }
            set { shortReturnsConditionsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("supplier", IsNullable = false)]
        public OnixSupplier[] supplier
        {
            get { return shortSupplierField; }
            set { shortSupplierField = value; }
        }

        #endregion
    }
}
