using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Title
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixCollection
    {
        #region CONSTANTS

        public const int CONST_COLL_TYPE_SERIES = 10;
        public const int CONST_COLL_TYPE_AGGR   = 20;

        #endregion

        public OnixCollection()
        {
            collTypeField    = "";
            collSeqField     = shortCollSeqField     = new OnixCollectionSequence[0];
            titleDetailField = shortTitleDetailField = new OnixTitleDetail[0];
        }

        private string collTypeField;

        private OnixCollectionSequence[] collSeqField;
        private OnixCollectionSequence[] shortCollSeqField;

        private OnixTitleDetail[] titleDetailField;
        private OnixTitleDetail[] shortTitleDetailField;

        #region ONIX Lists

        public OnixCollectionSequence[] OnixCollectionSequenceList
        {
            get
            {
                OnixCollectionSequence[] CollSeqList = null;

                if (this.collSeqField != null)
                    CollSeqList = this.collSeqField;
                else if (this.shortCollSeqField != null)
                    CollSeqList = this.shortCollSeqField;
                else
                    CollSeqList = new OnixCollectionSequence[0];

                return CollSeqList;
            }
        }

        public OnixTitleDetail[] OnixTitleDetailList
        {
            get
            {
                OnixTitleDetail[] TitleDetailList = null;

                if (this.titleDetailField != null)
                    TitleDetailList = this.titleDetailField;
                else if (this.shortTitleDetailField != null)
                    TitleDetailList = this.shortTitleDetailField;
                else
                    TitleDetailList = new OnixTitleDetail[0];

                return TitleDetailList;
            }
        }

        #endregion

        #region Helper Methods

        public int CollectionTypeNum
        {
            get
            {
                int nCollTypeNum = -1;

                if (!String.IsNullOrEmpty(CollectionType))
                    Int32.TryParse(CollectionType, out nCollTypeNum);

                return nCollTypeNum;
            }
        }

        public string CollectionName
        {
            get
            {
                string sCollName = "";

                var TitleDetailList = this.OnixTitleDetailList;

                if ((TitleDetailList != null) && (TitleDetailList.Length > 0))
                {
                    string sFullName          = "";
                    string sPrefixName        = "";
                    string sWithoutPrefixName = "";

                    OnixTitleDetail FullNameTitleDetail =
                        TitleDetailList.Where(x => x.IsCollectionName() && x.HasQualifiedTitle() && !String.IsNullOrEmpty(x.FullName)).LastOrDefault();

                    OnixTitleDetail PrefixTitleDetail =
                        TitleDetailList.Where(x => x.IsCollectionName() && x.HasQualifiedTitle() && !String.IsNullOrEmpty(x.Prefix)).LastOrDefault();

                    OnixTitleDetail WithoutPrefixTitleDetail =
                        TitleDetailList.Where(x => x.IsCollectionName() && x.HasQualifiedTitle() && !String.IsNullOrEmpty(x.TitleWithoutPrefix)).LastOrDefault();

                    if (FullNameTitleDetail != null)
                        sFullName = FullNameTitleDetail.FullName;

                    if (PrefixTitleDetail != null)
                        sPrefixName = PrefixTitleDetail.Prefix;

                    if (WithoutPrefixTitleDetail != null)
                        sWithoutPrefixName = WithoutPrefixTitleDetail.TitleWithoutPrefix;

                    if (!String.IsNullOrEmpty(sFullName))
                        sCollName = sFullName;
                    else if (!String.IsNullOrEmpty(sWithoutPrefixName))
                    {
                        if (!String.IsNullOrEmpty(sPrefixName))
                            sCollName = sPrefixName + " : ";

                        sCollName += sWithoutPrefixName;
                    }
                }

                return sCollName;
            }
        }

        public bool IsSeriesType()
        {
            return (CollectionTypeNum == CONST_COLL_TYPE_SERIES);
        }

        public string TitleSequence
        {
            get
            {
                string sSeqNum = "";

                if ((this.OnixCollectionSequenceList != null) && (this.OnixCollectionSequenceList.Length > 0))
                {
                    OnixCollectionSequence TitleCollSeq =
                        this.OnixCollectionSequenceList.Where(x => x.IsTitleSeq()).LastOrDefault();

                    if ((TitleCollSeq != null) && (TitleCollSeq.CollectionSequenceNum > 0))
                        sSeqNum = TitleCollSeq.CollectionSequence;
                }

                return sSeqNum;
            }
        }

        #endregion

        #region Reference Tags

        /// <remarks/>
        public string CollectionType
        {
            get { return this.collTypeField; }
            set { this.collTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CollectionSequence")]
        public OnixCollectionSequence[] CollectionSequence
        {
            get { return this.collSeqField; }
            set { this.collSeqField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TitleDetail")]
        public OnixTitleDetail[] TitleDetail
        {
            get { return this.titleDetailField; }
            set { this.titleDetailField = value; }
        }

        #endregion

        #region Short Tags

        /// <remarks/>
        public string x329
        {
            get { return CollectionType; }
            set { CollectionType = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("collectionsequence")]
        public OnixCollectionSequence[] collectionsequence
        {
            get { return this.shortCollSeqField; }
            set { this.shortCollSeqField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("titledetail")]
        public OnixTitleDetail[] titledetail
        {
            get { return this.shortTitleDetailField; }
            set { this.shortTitleDetailField = value; }
        }

        #endregion
    }
}
