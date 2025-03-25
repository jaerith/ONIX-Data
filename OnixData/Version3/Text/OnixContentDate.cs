using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Text
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixContentDate
    {
        public OnixContentDate()
        {
            ContentDateRole = DateFormat = -1;

            Date = string.Empty;
        }

        private int    contentDateRoleField;
        private int    dateFormatField;
        private string dateField;

        #region Reference Tags

        /// <remarks/>
        public int ContentDateRole
        {
            get
            {
                return this.contentDateRoleField;
            }
            set
            {
                this.contentDateRoleField = value;
            }
        }

        /// <remarks/>
        public int DateFormat
        {
            get
            {
                return this.dateFormatField;
            }
            set
            {
                this.dateFormatField = value;
            }
        }

        /// <remarks/>
        public string Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int x429
        {
            get { return ContentDateRole; }
            set { ContentDateRole = value; }
        }

        /// <remarks/>
        public int j260
        {
            get { return DateFormat; }
            set { DateFormat = value; }
        }

        /// <remarks/>
        public string b306
        {
            get { return Date; }
            set { Date = value; }
        }

        #endregion
    }
}
