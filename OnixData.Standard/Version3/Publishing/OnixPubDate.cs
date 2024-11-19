using System;

namespace OnixData.Standard.Version3.Publishing
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class OnixPubDate
    {
        #region CONSTANTS

        public const int CONST_PUB_DT_ROLE_NORMAL       = 1;
        public const int CONST_PUB_DT_ROLE_ON_SALE      = 2;
        public const int CONST_PUB_DT_ROLE_TRADE_ANN    = 10;
        public const int CONST_PUB_DT_ROLE_PUB_FIRST    = 11;
        public const int CONST_PUB_DT_ROLE_LAST_REPRINT = 12;
        public const int CONST_PUB_DT_ROLE_OOP_FIRST    = 13;
        public const int CONST_PUB_DT_ROLE_LAST_REISSUE = 16;
        public const int CONST_PUB_DT_ROLE_PRINT_CTRPRT = 19;

        #endregion

        public OnixPubDate()
        {
            PublishingDateRole = Date = "";
        }

        private string dateField;
        private string pubDateRoleField;

        #region Helper Methods

        public int PubDateRoleNum
        {
            get
            {
                int nPubDateRoleNum = -1;

                if (!String.IsNullOrEmpty(PublishingDateRole))
                    Int32.TryParse(PublishingDateRole, out nPubDateRoleNum);

                return nPubDateRoleNum;
            }
        }

        #endregion

        #region Reference Tags

        public string Date
        {
            get { return this.dateField; }
            set { this.dateField = value; }
        }

        /// <remarks/>
        public string PublishingDateRole
        {
            get { return this.pubDateRoleField; }
            set { this.pubDateRoleField = value; }
        }

        #endregion

        #region Short Tags

        public string b306
        {
            get { return Date; }
            set { Date = value; }
        }        

        /// <remarks/>
        public string x448
        {
            get { return PublishingDateRole; }
            set { PublishingDateRole = value; }
        }

        #endregion
    }
}
