﻿namespace OnixData.Standard.Version3.Text
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixContentDate
    {
        public OnixContentDate()
        {
            ContentDateRole = DateFormat = Date = - 1;
        }

        private int contentDateRoleField;
        private int dateFormatField;
        private int dateField;

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
        public int Date
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
        public int b306
        {
            get { return Date; }
            set { Date = value; }
        }

        #endregion
    }
}
