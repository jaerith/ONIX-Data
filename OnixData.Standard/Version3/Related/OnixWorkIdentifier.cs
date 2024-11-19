namespace OnixData.Standard.Version3.Related
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixWorkIdentifier
    {
        public OnixWorkIdentifier()
        {
            WorkIDType = -1;
            IDTypeName = IDValue = "";
        }

        private int    workIDTypeField;
        private string idTypeNameField;
        private string idValueField;

        #region Reference Tags

        /// <remarks/>
        public int WorkIDType
        {
            get
            {
                return this.workIDTypeField;
            }
            set
            {
                this.workIDTypeField = value;
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

        #endregion

        #region Short Tags

        /// <remarks/>
        public int b201
        {
            get {  return this.workIDTypeField; }
            set { this.workIDTypeField = value; }
        }

        /// <remarks/>
        public string b233
        {
            get { return this.idTypeNameField; }
            set { this.idTypeNameField = value; }
        }

        /// <remarks/>
        public string b244
        {
            get { return this.idValueField; }
            set { this.idValueField = value; }
        }

        #endregion
    }
}
