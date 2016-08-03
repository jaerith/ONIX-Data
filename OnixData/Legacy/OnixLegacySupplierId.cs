using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacySupplierId
    {
        #region CONSTANTS

        public const int CONST_SUPPL_ID_TYPE_PROP   = 2;
        public const int CONST_SUPPL_ID_TYPE_BV     = 4;
        public const int CONST_SUPPL_ID_TYPE_GER_PI = 5;
        public const int CONST_SUPPL_ID_TYPE_GLN    = 6;
        public const int CONST_SUPPL_ID_TYPE_SAN    = 7;
        
        #endregion

        public OnixLegacySupplierId()
        {
            SupplierIDType = -1;
            IDTypeName     = IDValue = "";
        }

        private int    supplierIDTypeField;
        private string idTypeNameField;
        private string idValueField;

        /// <remarks/>
        public int SupplierIDType
        {
            get
            {
                return this.supplierIDTypeField;
            }
            set
            {
                this.supplierIDTypeField = value;
            }
        }

        /// <remarks/>
        public string IDTypeName
        {
            get
            {
                return this.idTypeNameField;
            }
            set
            {
                this.idTypeNameField = value;
            }
        }

        /// <remarks/>
        public string IDValue
        {
            get
            {
                return this.idValueField;
            }
            set
            {
                this.idValueField = value;
            }
        }
    }
}
