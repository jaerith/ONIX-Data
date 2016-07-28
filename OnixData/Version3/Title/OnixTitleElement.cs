using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnixData.Version3.Title
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OnixTitleElement
    {
        public OnixTitleElement()
        {
            TitleElementLevel = -1;
            TitleText         = "";
        }

        private int    titleElementLevelField;
        private string titleTextField;

        /// <remarks/>
        public int TitleElementLevel
        {
            get
            {
                return this.titleElementLevelField;
            }
            set
            {
                this.titleElementLevelField = value;
            }
        }

        /// <remarks/>
        public string TitleText
        {
            get
            {
                return this.titleTextField;
            }
            set
            {
                this.titleTextField = value;
            }
        }
    }
}
