using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Supply
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixSupplier
    {
        #region CONSTANTS

        public const int CONST_SUPPL_ID_TYPE_PROP   = 2;
        public const int CONST_SUPPL_ID_TYPE_BV     = 4;
        public const int CONST_SUPPL_ID_TYPE_GER_PI = 5;
        public const int CONST_SUPPL_ID_TYPE_GLN    = 6;
        public const int CONST_SUPPL_ID_TYPE_SAN    = 7;
        
        #endregion

        public OnixSupplier()
        {
            SupplierRole = SupplierName = "";

            supplierIdField      = null;
            shortSupplierIdField = null;

            websiteField = null;
            shortWebsiteField = null;
        }

        private string supplierRoleField;
        private string supplierNameField;

        private OnixSupplierId[] supplierIdField;
        private OnixSupplierId[] shortSupplierIdField;

        private OnixWebsite[] websiteField;
        private OnixWebsite[] shortWebsiteField;

        #region Helper Methods

        public OnixSupplierId[] OnixSupplierIdList
        {
            get
            {
                OnixSupplierId[] SupplierIdList = null;

                if (this.supplierIdField != null)
                    SupplierIdList = this.supplierIdField;
                else if (this.shortSupplierIdField != null)
                    SupplierIdList = this.shortSupplierIdField;
                else
                    SupplierIdList = new OnixSupplierId[0];

                return SupplierIdList;
            }
        }
        
        public OnixWebsite[] OnixWebsiteList
        {
            get
            {
                OnixWebsite[] websiteList = null;

                if (this.websiteField != null)
                    websiteList = this.websiteField;
                else if (this.shortSupplierIdField != null)
                    websiteList = this.shortWebsiteField;
                else
                    websiteList = new OnixWebsite[0];

                return websiteList;
            }
        }

        public int SupplierRoleNum
        {
            get
            {
                int nRoleNum = 0;

                if (!String.IsNullOrEmpty(SupplierRole))
                    Int32.TryParse(SupplierRole, out nRoleNum);

                return nRoleNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string SupplierRole
        {
            get
            {
                return this.supplierRoleField;
            }
            set
            {
                this.supplierRoleField = value;
            }
        }

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
        [System.Xml.Serialization.XmlElementAttribute("SupplierId")]
        public OnixSupplierId[] SupplierId
        {
            get
            {
                return this.supplierIdField;
            }
            set
            {
                this.supplierIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Website")]
        public OnixWebsite[] Website
        {
            get
            {
                return this.websiteField;
            }
            set
            {
                this.websiteField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string j292
        {
            get { return SupplierRole; }
            set { SupplierRole = value; }
        }

        /// <remarks/>
        public string j137
        {
            get { return SupplierName; }
            set { SupplierName = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("supplierid")]
        public OnixSupplierId[] supplierid
        {
            get
            {
                return this.shortSupplierIdField;
            }
            set
            {
                this.shortSupplierIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("website")]
        public OnixWebsite[] website
        {
            get
            {
                return this.shortWebsiteField;
            }
            set
            {
                this.shortWebsiteField = value;
            }
        }

        #endregion
    }
}
