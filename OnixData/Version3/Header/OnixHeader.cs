using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Header
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixHeader
    {
        public OnixHeader()
        {
            Sender       = new OnixHeaderSender();
            Addressee    = new OnixHeaderAddressee();
            SentDateTime = 0;
        }

        private OnixHeaderSender    senderField;
        private OnixHeaderAddressee addresseeField;
        private uint                sentDateTimeField;

        #region Reference Tags

        /// <remarks/>
        public OnixHeaderSender Sender
        {
            get
            {
                return this.senderField;
            }
            set
            {
                this.senderField = value;
            }
        }

        /// <remarks/>
        public OnixHeaderAddressee Addressee
        {
            get
            {
                return this.addresseeField;
            }
            set
            {
                this.addresseeField = value;
            }
        }

        /// <remarks/>
        public uint SentDateTime
        {
            get
            {
                return this.sentDateTimeField;
            }
            set
            {
                this.sentDateTimeField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public OnixHeaderSender sender
        {
            get { return Sender; }
            set { Sender = value; }
        }

        /// <remarks/>
        public OnixHeaderAddressee addressee
        {
            get { return Addressee; }
            set { Addressee = value; }
        }

        /// <remarks/>
        public uint x307
        {
            get { return SentDateTime; }
            set { SentDateTime = value; }
        }

        #endregion
    }
}
