using System;

namespace OnixData.Standard.Version3.Publishing
{
    public class OnixImprintIdentifier
    {
        #region CONSTANTS

        public const int CONST_IMPRINT_ROLE_PROP = 1;

        #endregion

        public OnixImprintIdentifier()
        {
            ImprintIDType = "";
            IDTypeName    = "";
            IDValue       = "";
        }

        private string imprintIdTypeField;
        private string idTypeNameField;
        private string idValueField;

        #region Reference Tags

        /// <remarks/>
        public string ImprintIDType
        {
            get
            {
                return this.imprintIdTypeField;
            }
            set
            {
                this.imprintIdTypeField = value;
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

        #region Helper Methods

        public int GetImprintIDTypeNum()
        {
            int nIdTypeNum = -1;

            if (!String.IsNullOrEmpty(this.ImprintIDType))
            {
                Int32.TryParse(this.ImprintIDType, out nIdTypeNum);
            }

            return nIdTypeNum;
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x445
        {
            get { return ImprintIDType; }
            set { ImprintIDType = value; }
        }

        /// <remarks/>
        public string b233
        {
            get { return IDTypeName; }
            set { IDTypeName = value; }
        }

        /// <remarks/>
        public string b244
        {
            get { return IDValue; }
            set { IDValue = value; }
        }

        #endregion

    }
}
