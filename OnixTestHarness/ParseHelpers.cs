using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace OnixTestHarness
{
    internal static class ParseHelpers
    {
        private static JavaScriptSerializer json;
        private static JavaScriptSerializer JSON { get { return json ?? (json = new JavaScriptSerializer()); } }

        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T ParseXML<T>(this string @this, string sRoot = "ONIXMessage") where T : class
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.ValidationType   = ValidationType.Schema;
            settings.DtdProcessing    = DtdProcessing.Parse;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            // settings.ValidationFlags &= ~(XmlSchemaValidationFlags.ProcessInlineSchema);
            // settings.ValidationFlags &= ~(XmlSchemaValidationFlags.ReportValidationWarnings);

            var reader = XmlReader.Create(@this.Trim().ToStream(), settings);
            return new XmlSerializer(typeof(T), new XmlRootAttribute(sRoot)).Deserialize(reader) as T;
        }

        public static T ParseJSON<T>(this string @this) where T : class
        {
            return JSON.Deserialize<T>(@this.Trim());
        }
    }
}
