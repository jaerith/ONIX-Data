using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

using OnixData.Version3;

namespace OnixData
{
    public class OnixParser : IDisposable
    {
        private XmlReader   V3OnixReader  = null;
        private OnixMessage V3OnixMessage = null;

        public OnixParser(FileInfo LegacyOnixFilepath, bool ReportValidationWarnings, bool LoadEntireFileIntoMemory = true)
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

            this.V3OnixReader = XmlReader.Create(LegacyOnixFilepath.FullName, settings);

            if (LoadEntireFileIntoMemory)
            {
                this.V3OnixMessage =
                    new XmlSerializer(typeof(OnixMessage), new XmlRootAttribute("ONIXMessage")).Deserialize(this.V3OnixReader) as OnixMessage;
            }
        }

        public OnixMessage Message
        {
            get
            {
                return V3OnixMessage;
            }
        }

        public void Dispose()
        {
            if (this.V3OnixReader != null)
                this.V3OnixReader.Close();
        }
    }
}
