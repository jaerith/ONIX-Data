﻿namespace OnixData.Standard.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyOtherText
    {
        #region CONSTANTS

        public const int CONST_OTEXT_TYPE_MAIN_DESC   = 1;
        public const int CONST_OTEXT_TYPE_ANNOTATION  = 2;
        public const int CONST_OTEXT_TYPE_REV_QUOTE   = 8;
        public const int CONST_OTEXT_TYPE_SERIES_DESC = 43;

        #endregion

        public OnixLegacyOtherText()
        {
            TextTypeCode = -1;
            Text         = TextFormat = "";
        }

        private int    textTypeCodeField;
        private string textFormatField;
        private string textField;

        #region Reference Tags

        /// <remarks/>
        public int TextTypeCode
        {
            get
            {
                return this.textTypeCodeField;
            }
            set
            {
                this.textTypeCodeField = value;
            }
        }

        /// <remarks/>
        public string TextFormat
        {
            get
            {
                return this.textFormatField;
            }
            set
            {
                this.textFormatField = value;
            }
        }

        /// <remarks/>
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public int d102
        {
            get { return TextTypeCode; }
            set { TextTypeCode = value; }
        }

        public string d103
        {
            get { return TextFormat; }
            set { TextFormat = value; }
        }

        /// <remarks/>
        public string d104
        {
            get { return Text; }
            set { Text = value; }
        }

        #endregion
    }
}
