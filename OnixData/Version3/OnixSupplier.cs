using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3
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
    }
}
