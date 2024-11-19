using System;
using System.Linq;

namespace OnixData.Standard.Version3.Title
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixCollectionSequence
    {
        #region CONSTANTS

        public const int CONST_COLL_SEQ_TYPE_PROP       = 1;
        public const int CONST_COLL_SEQ_TYPE_TTL_ORDER  = 2;
        public const int CONST_COLL_SEQ_TYPE_PUB_ORDER  = 3;
        public const int CONST_COLL_SEQ_TYPE_NAR_ORDER  = 4;
        public const int CONST_COLL_SEQ_TYPE_ORIG_ORDER = 5;
        public const int CONST_COLL_SEQ_TYPE_SGR_ORDER  = 6;
        public const int CONST_COLL_SEQ_TYPE_SGD_ORDER  = 7;

        public readonly int[] CONST_COLL_SEQ_TYPES_PRIMARY =
            new int[] { CONST_COLL_SEQ_TYPE_TTL_ORDER , CONST_COLL_SEQ_TYPE_PUB_ORDER, CONST_COLL_SEQ_TYPE_NAR_ORDER,
                        CONST_COLL_SEQ_TYPE_ORIG_ORDER, CONST_COLL_SEQ_TYPE_SGR_ORDER, CONST_COLL_SEQ_TYPE_SGD_ORDER };

        #endregion

        public OnixCollectionSequence()
        {
            CollectionSequenceType = CollectionSequenceTypeName = CollectionSequenceNumber = "";
        }
      
        private string collSeqTypeField;
        private string collSeqTypeNameField;
        private string collSeqField;

        #region Helper Methods

        public int CollectionSequenceTypeNum
        {
            get
            {
                int nCollSeqType = -1;

                if (!String.IsNullOrEmpty(CollectionSequenceType))
                    Int32.TryParse(CollectionSequenceType, out nCollSeqType);

                return nCollSeqType;
            }
        }

        public int CollectionSequenceNum
        {
            get
            {
                int nCollSeq = -1;

                if (!String.IsNullOrEmpty(CollectionSequenceNumber))
                    Int32.TryParse(CollectionSequenceNumber, out nCollSeq);

                return nCollSeq;
            }
        }

        public bool IsTitleSeq()
        {
            return ((CollectionSequenceTypeNum == CONST_COLL_SEQ_TYPE_PROP) || (CollectionSequenceTypeNum == CONST_COLL_SEQ_TYPE_TTL_ORDER));
        }

        public bool IsSeriesSeq()
        {
            return CONST_COLL_SEQ_TYPES_PRIMARY.Contains(CollectionSequenceTypeNum);
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string CollectionSequenceType
        {
            get
            {
                return this.collSeqTypeField;
            }
            set
            {
                this.collSeqTypeField = value;
            }
        }

        /// <remarks/>
        public string CollectionSequenceTypeName
        {
            get
            {
                return this.collSeqTypeNameField;
            }
            set
            {
                this.collSeqTypeNameField = value;
            }
        }

        /// <remarks/>
        public string CollectionSequenceNumber
        {
            get
            {
                return this.collSeqField;
            }
            set
            {
                this.collSeqField = value;
            }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x479
        {
            get
            {
                return CollectionSequenceType;
            }
            set
            {
                CollectionSequenceType = value;
            }
        }

        /// <remarks/>
        public string x480
        {
            get
            {
                return CollectionSequenceTypeName;
            }
            set
            {
                CollectionSequenceTypeName = value;
            }
        }

        /// <remarks/>
        public string x481
        {
            get
            {
                return CollectionSequenceNumber;
            }
            set
            {
                CollectionSequenceNumber = value;
            }
        }

        #endregion
    }}
