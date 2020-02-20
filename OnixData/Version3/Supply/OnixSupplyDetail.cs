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
        public OnixSupplyDetail()
        {
            ProductAvailability = PackQuantity = UnpricedItemType = "";

            Price             = new OnixPrice[0];
            Supplier          = new OnixSupplier[0];
            ReturnsConditions = new OnixReturnsConditions[0];
            SupplyDate        = new OnixSupplyDate[0];
        }

        private string productAvailabilityField;
        private string packQuantityField;
        private string unpricedItemTypeField;

        private OnixReturnsConditions[] returnsConditionsField;
        private OnixReturnsConditions[] shortReturnsConditionsField;

        private OnixPrice[] priceField;
        private OnixPrice[] shortPriceField;

        private OnixSupplier[] supplierField;
        private OnixSupplier[] shortSupplierField;

        private OnixSupplyDate[] supplyDateField;
        private OnixSupplyDate[] shortSupplyDateField;

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

        public OnixSupplyDate[] OnixSupplyDateList
        {
            get
            {
                OnixSupplyDate[] SupplyDates = null;

                if (this.supplyDateField != null)
                    SupplyDates = this.supplyDateField;
                else if (this.shortSupplyDateField != null)
                    SupplyDates = this.shortSupplyDateField;
                else
                    SupplyDates = new OnixSupplyDate[0];

                return SupplyDates;
            }
        }

        #endregion

        #region Helper Methods

        public bool HasUSDPrice()
        {
            bool bHasUSDPrice = false;

            if ((this.OnixPriceList != null) && (this.OnixPriceList.Length > 0))
            {
                bHasUSDPrice =
                    this.OnixPriceList.Any(x => (x.PriceType == OnixPrice.CONST_PRICE_TYPE_RRP_EXCL) && (x.CurrencyCode == "USD"));
            }

            return bHasUSDPrice;
        }

        public bool HasReturnCodeTypeBisac()
        {
            bool bHasRetCodeBisac = false;

            if ((this.OnixReturnsConditionsList != null) && (this.OnixReturnsConditionsList.Length > 0))
            {
                bHasRetCodeBisac =
                    this.OnixReturnsConditionsList.Any(x => (x.ReturnsCodeTypeNum == OnixReturnsConditions.CONST_RET_CODE_TYPE_BISAC));
            }

            return bHasRetCodeBisac;
        }

        public int PackQuantityNum
        {
            get
            {
                int nQuantity = -1;

                if (!String.IsNullOrEmpty(PackQuantity))
                    Int32.TryParse(PackQuantity, out nQuantity);

                return nQuantity;
            }
        }

        public string ReturnCodeBisac
        {
            get
            {
                string sRetCodeBisac = "";

                if ((this.OnixReturnsConditionsList != null) && (this.OnixReturnsConditionsList.Length > 0))
                {
                    var RetConditions =
                        this.OnixReturnsConditionsList.Where(x => (x.ReturnsCodeTypeNum == OnixReturnsConditions.CONST_RET_CODE_TYPE_BISAC))
                                                      .FirstOrDefault();

                    if (RetConditions != null)
                        sRetCodeBisac = RetConditions.ReturnsCode;
                }

                return sRetCodeBisac;
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
        public string PackQuantity
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SupplyDate", IsNullable = false)]
        public OnixSupplyDate[] SupplyDate
        {
            get
            {
                return this.supplyDateField;
            }
            set
            {
                this.supplyDateField = value;
            }
        }

        /// <remarks/>
        public string UnpricedItemType
        {
            get
            {
                return this.unpricedItemTypeField;
            }
            set
            {
                this.unpricedItemTypeField = value;
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
        public string j145
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("supplydate", IsNullable = false)]
        public OnixSupplyDate[] supplydate
        {
            get { return this.shortSupplyDateField; }
            set { this.shortSupplyDateField = value; }
        }

        /// <remarks/>
        public string j192
        {
            get { return UnpricedItemType; }
            set { UnpricedItemType = value; }
        }

        #endregion
    }
}
