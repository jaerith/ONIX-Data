using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Legacy
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixLegacyHeader
    {
        public OnixLegacyHeader()
        {
            fromCompanyField = "";
            fromPersonField  = "";
            fromEmailField   = "";
            sentDateField    = 0;
            messageNoteField = "";

            defaultLanguageOfTextField = defaultCurrencyCodeField = "";
            defaultLinearUnitField     = defaultWeightUnitField   = "";
            defaultClassOfTradeField   = "";
        }

        private string fromCompanyField;
        private string fromPersonField;
        private string fromEmailField;
        private uint   sentDateField;
        private string messageNoteField;
        private string defaultLanguageOfTextField;
        private string defaultPriceTypeCodeField;
        private string defaultCurrencyCodeField;
        private string defaultLinearUnitField;
        private string defaultWeightUnitField;
        private string defaultClassOfTradeField;

        /// <remarks/>
        public string FromCompany
        {
            get
            {
                return this.fromCompanyField;
            }
            set
            {
                this.fromCompanyField = value;
            }
        }

        /// <remarks/>
        public string FromPerson
        {
            get
            {
                return this.fromPersonField;
            }
            set
            {
                this.fromPersonField = value;
            }
        }

        /// <remarks/>
        public string FromEmail
        {
            get
            {
                return this.fromEmailField;
            }
            set
            {
                this.fromEmailField = value;
            }
        }

        /// <remarks/>
        public uint SentDate
        {
            get
            {
                return this.sentDateField;
            }
            set
            {
                this.sentDateField = value;
            }
        }

        /// <remarks/>
        public string MessageNote
        {
            get
            {
                return this.messageNoteField;
            }
            set
            {
                this.messageNoteField = value;
            }
        }

        /// <remarks/>
        public string DefaultLanguageOfText
        {
            get
            {
                return this.defaultLanguageOfTextField;
            }
            set
            {
                this.defaultLanguageOfTextField = value;
            }
        }

        /// <remarks/>
        public string DefaultPriceTypeCode
        {
            get
            {
                return this.defaultPriceTypeCodeField;
            }
            set
            {
                this.defaultPriceTypeCodeField = value;
            }
        }

        /// <remarks/>
        public string DefaultCurrencyCode
        {
            get
            {
                return this.defaultCurrencyCodeField;
            }
            set
            {
                this.defaultCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        public string DefaultLinearUnit
        {
            get
            {
                return this.defaultLinearUnitField;
            }
            set
            {
                this.defaultLinearUnitField = value;
            }
        }

        /// <remarks/>
        public string DefaultWeightUnit
        {
            get
            {
                return this.defaultWeightUnitField;
            }
            set
            {
                this.defaultWeightUnitField = value;
            }
        }

        /// <remarks/>
        public string DefaultClassOfTrade
        {
            get
            {
                return this.defaultClassOfTradeField;
            }
            set
            {
                this.defaultClassOfTradeField = value;
            }
        }

    }
}



