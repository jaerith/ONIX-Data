using System;
using System.IO;
using System.Xml;

namespace OnixData
{
    public class OnixXmlTextReader : XmlTextReader
    {
        public OnixXmlTextReader(string psXmlBody) : base(new StringReader(psXmlBody)) { }

        public OnixXmlTextReader(FileInfo poFileInfo) : base(poFileInfo.FullName) { }

        public override string NamespaceURI
        {
            get { return ""; }
        }
    }
}
