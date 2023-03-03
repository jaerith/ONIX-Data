using System;
using System.Xml;

namespace OnixData.Standard.Extensions.Models
{
    public class OnixXmlText
    {
        public string XmlText { get; set; }

        public OnixXmlText(string xmlText, bool shouldPrettyPrint = true)
        {
            XmlText = xmlText;

            if (shouldPrettyPrint)
            {
                XmlText = this.PrettyPrintXml();
            }
        }
    }
}
