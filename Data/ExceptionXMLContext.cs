using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Data
{
    public class ExceptionXMLContext : IExceptionContext
    {
        public bool LogException(Exception e)
        {
            var id = DateTime.Now.ToShortTimeString();
            var innerException = string.Empty;
            var message = string.Empty;

            if (e.InnerException != null) { innerException = e.InnerException.ToString(); }
            if (e.Message != null) { message = e.Message; }

            if (File.Exists(XML.GetPath()))
            {
                var xDocument = XDocument.Load(XML.GetPath());
                var root = xDocument.Element("Exceptions");
                var rows = root.Descendants("Exception");
                var firstrow = rows.First();
                firstrow.AddBeforeSelf(
                    new XElement("Exception",
                    new XElement("date", id,
                    new XElement(nameof(e.Data), e.ToString(),
                    new XElement(nameof(e.HResult), e.HResult.ToString()),
                    new XElement(nameof(e.HelpLink), e.HelpLink),
                    new XElement(nameof(e.InnerException), innerException),
                    new XElement(nameof(e.Message), message),
                    new XElement(nameof(e.Source), e.Source),
                    new XElement(nameof(e.StackTrace), e.StackTrace),
                    new XElement(nameof(e.TargetSite), e.TargetSite.ToString())))));
                xDocument.Save(XML.GetPath());
                return true;
            }

            var settings = new XmlWriterSettings() {Indent = true};

            var xmlWriter = XmlWriter.Create(XML.GetPath(), settings);
            
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteComment("Automatically generated, do not modify.");

            xmlWriter.WriteStartElement("Exceptions");
            xmlWriter.WriteStartElement("Exception");
            xmlWriter.WriteAttributeString("date", id);
            xmlWriter.WriteAttributeString(nameof(e.Data), e.ToString());
            xmlWriter.WriteAttributeString(nameof(e.HResult), e.HResult.ToString());
            xmlWriter.WriteAttributeString(nameof(e.HelpLink), e.HelpLink);
            xmlWriter.WriteAttributeString(nameof(e.InnerException), innerException);
            xmlWriter.WriteAttributeString(nameof(e.Message), message);
            xmlWriter.WriteAttributeString(nameof(e.Source), e.Source);
            xmlWriter.WriteAttributeString(nameof(e.StackTrace), e.StackTrace);
            xmlWriter.WriteAttributeString(nameof(e.TargetSite), e.TargetSite.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Flush();
            xmlWriter.Close();

            return false;
        }
    }
}
