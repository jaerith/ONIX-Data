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

            SupplierIdentifier = new OnixLegacySupplierId[0];
            Price              = new OnixLegacyPrice[0];
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
        private OnixLegacyPrice[]      priceField;

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
    }
}
