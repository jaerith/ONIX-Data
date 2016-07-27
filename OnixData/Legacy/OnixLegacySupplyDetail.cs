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
            AvailabilityCode    = "";
            ProductAvailability = -1;
            OnSaleDate          = 0;
            PackQuantity        = -1;

            Price = new OnixLegacyPrice[0];
        }

        private string supplierNameField;
        private int    returnsCodeTypeField;
        private string returnsCodeField;
        private string availabilityCodeField;
        private int    productAvailabilityField;
        private uint   onSaleDateField;
        private int    packQuantityField;

        private OnixLegacyPrice[] priceField;

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
        public uint OnSaleDate
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
