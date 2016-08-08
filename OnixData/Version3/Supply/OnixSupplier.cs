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
        public OnixSupplier()
        {
            SupplierRole = -1;
            SupplierName = "";
        }

        private int    supplierRoleField;
        private string supplierNameField;

        #region Reference Tags

        /// <remarks/>
        public int SupplierRole
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int j292
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

        #endregion
    }
}
