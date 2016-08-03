using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

using OnixData.Legacy;

namespace OnixData
{
    public class OnixLegacyParser : IDisposable
    {
        private XmlReader         LegacyOnixReader  = null;
        private OnixLegacyMessage LegacyOnixMessage = null;

        public OnixLegacyParser(FileInfo LegacyOnixFilepath, bool ReportValidationWarnings, bool LoadEntireFileIntoMemory = true)
        {
            if (!File.Exists(LegacyOnixFilepath.FullName))
                throw new Exception("ERROR!  File(" + LegacyOnixFilepath + ") does not exist.");

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel  = ConformanceLevel.Document;
            settings.ValidationType    = ValidationType.Schema;
            settings.DtdProcessing     = DtdProcessing.Parse;
            settings.ValidationFlags  |= XmlSchemaValidationFlags.ProcessInlineSchema;

            if (ReportValidationWarnings)
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            else
                settings.ValidationFlags &= ~(XmlSchemaValidationFlags.ReportValidationWarnings);

            this.LegacyOnixReader = XmlReader.Create(LegacyOnixFilepath.FullName, settings);

            if (LoadEntireFileIntoMemory)
            {
                this.LegacyOnixMessage =
                    new XmlSerializer(typeof(OnixLegacyMessage), new XmlRootAttribute("ONIXMessage")).Deserialize(this.LegacyOnixReader) as OnixLegacyMessage;
            }
        }

        public OnixLegacyMessage Message
        {
            get
            {
                return LegacyOnixMessage;
            }
        }

        public void Dispose()
        {
            if (this.LegacyOnixReader != null)
                this.LegacyOnixReader.Close();
        }
    }
}
