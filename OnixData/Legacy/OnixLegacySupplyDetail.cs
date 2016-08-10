using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacySupplyDetail
    {
        public OnixLegacySupplyDetail()
        {
            SupplierName        = "";
            ReturnsCodeType     = -1;
            ReturnsCode         = "";
            LastDateForReturns  = "";
            AvailabilityCode    = "";
            ProductAvailability = -1;
            ExpectedShipDate    = "";
            OnSaleDate          = "";
            PackQuantity        = -1;

            supplierIdentifierField = shortSupplierIdentifierField = new OnixLegacySupplierId[0];
            priceField              = shortPriceField              = new OnixLegacyPrice[0];
        }

        private string supplierNameField;
        private int    returnsCodeTypeField;
        private string returnsCodeField;
        private string lastDateForReturnsField;
        private string availabilityCodeField;
        private int    productAvailabilityField;
        private string expectedShipDateField;
        private string onSaleDateField;
        private int    packQuantityField;

        private OnixLegacySupplierId[] supplierIdentifierField;
        private OnixLegacySupplierId[] shortSupplierIdentifierField;
        private OnixLegacyPrice[]      priceField;
        private OnixLegacyPrice[]      shortPriceField;

        #region ONIX Lists

        public OnixLegacySupplierId[] OnixSupplierIdList
        {
            get
            {
                OnixLegacySupplierId[] SupplierIds = null;

                if (supplierIdentifierField != null)
                    SupplierIds = this.supplierIdentifierField;
                else if (shortSupplierIdentifierField != null)
                    SupplierIds = this.shortSupplierIdentifierField;
                else
                    SupplierIds = new OnixLegacySupplierId[0];

                return SupplierIds;
            }
        }

        public OnixLegacyPrice[] OnixPriceList
        {
            get
            {
                OnixLegacyPrice[] Prices = null;

                if (priceField != null)
                    Prices = this.priceField;
                else if (shortPriceField != null)
                    Prices = this.shortPriceField;
                else
                    Prices = new OnixLegacyPrice[0];

                return Prices;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string SupplierName
        {
            get
            {
                return this.supplierNameField;
            }
            set
            {
                this.supplierNameField = value;
            }
        }

        /// <remarks/>
        public int ReturnsCodeType
        {
            get
            {
                return this.returnsCodeTypeField;
            }
            set
            {
                this.returnsCodeTypeField = value;
            }
        }

        /// <remarks/>
        public string ReturnsCode
        {
            get
            {
                return this.returnsCodeField;
            }
            set
            {
                this.returnsCodeField = value;
            }
        }

        /// <remarks/>
        public string LastDateForReturns
        {
            get
            {
                return this.lastDateForReturnsField;
            }
            set
            {
                this.lastDateForReturnsField = value;
            }
        }

        /// <remarks/>
        public string AvailabilityCode
        {
            get
            {
                return this.availabilityCodeField;
            }
            set
            {
                this.availabilityCodeField = value;
            }
        }

        /// <remarks/>
        public int ProductAvailability
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
        public string ExpectedShipDate
        {
            get
            {
                return this.expectedShipDateField;
            }
            set
            {
                this.expectedShipDateField = value;
            }
        }

        /// <remarks/>
        public string OnSaleDate
        {
            get
            {
                return this.onSaleDateField;
            }
            set
            {
                this.onSaleDateField = value;
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
       
        [System.Xml.Serialization.XmlElementAttribute("SupplierIdentifier")]
        public OnixLegacySupplierId[] SupplierIdentifier
        {
            get
            {
                return this.supplierIdentifierField;
            }
            set
            {
                this.supplierIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Price")]
        public OnixLegacyPrice[] Price
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public string j137
        {
            get { return SupplierName; }
            set { SupplierName = value; }
        }

        /// <remarks/>
        public int j268
        {
            get
            { return ReturnsCodeType; }
            set { ReturnsCodeType = value; }
        }

        /// <remarks/>
        public string j269
        {
            get { return ReturnsCode; }
            set { ReturnsCode = value; }
        }

        /// <remarks/>
        public string j387
        {
            get { return LastDateForReturns; }
            set { LastDateForReturns = value; }
        }

        /// <remarks/>
        public string j141
        {
            get { return AvailabilityCode; }
            set { AvailabilityCode = value; }
        }

        /// <remarks/>
        public int j396
        {
            get { return ProductAvailability; }
            set { ProductAvailability = value; }
        }

        /// <remarks/>
        public string j142
        {
            get { return ExpectedShipDate; }
            set { ExpectedShipDate = value; }
        }

        /// <remarks/>
        public string j143
        {
            get { return OnSaleDate; }
            set { OnSaleDate = value; }
        }

        /// <remarks/>
        public int j145
        {
            get { return PackQuantity; }
            set { PackQuantity = value; }
        }

        [System.Xml.Serialization.XmlElementAttribute("supplieridentifier")]
        public OnixLegacySupplierId[] supplieridentifier
        {
            get { return this.shortSupplierIdentifierField; }
            set { this.shortSupplierIdentifierField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("price")]
        public OnixLegacyPrice[] price
        {
            get { return this.shortPriceField; }
            set { this.shortPriceField = value; }
        }

        #endregion
    }
}
