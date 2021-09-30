using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ASWAWrapper.Common.Helpers
{
    public class XMLConverter
    {
        public string Serialize<T>(T model) where T : class
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            string xml = string.Empty;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using (var ms = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.NewLineChars = Environment.NewLine;
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.Indent = true;

                var xw = XmlWriter.Create(ms, settings);
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(new XmlWriterEE(xw), model, ns);
                xw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms, Encoding.Default))
                {
                    xml = sr.ReadToEnd();
                }
                return xml;
            }
        }

        public T Deserializer<T>(string xml) where T : class
        {
            T instance;
            try
            {
                ReplaceValidXmlChars(ref xml);
                using (var ms = new MemoryStream(Encoding.Default.GetBytes(xml)/*Encoding.GetEncoding(1251).GetBytes(xml)*/))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    instance = (T)serializer.Deserialize(ms);
                }
            }

            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);

                    using (XmlReader reader = new XmlNodeReader(doc))
                    {
                        var serializer = new XmlSerializer(typeof(T));
                        instance = (T)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    throw ;
                }
            }

            return instance;
        }

        public static void ReplaceValidXmlChars(ref string text)
        {
            text = text.Replace(@"&", "&amp;");
        }
    }

    public class XmlWriterEE : XmlWriter
    {
        private XmlWriter baseWriter;

        public XmlWriterEE(XmlWriter w)
        {
            baseWriter = w;
        }

        public override void WriteEndElement() { baseWriter.WriteFullEndElement(); }

        public override void WriteFullEndElement()
        {
            baseWriter.WriteFullEndElement();
        }

        public override void Close()
        {
            baseWriter.Close();
        }

        public override void Flush()
        {
            baseWriter.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return (baseWriter.LookupPrefix(ns));
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            baseWriter.WriteBase64(buffer, index, count);
        }

        public override void WriteCData(string text)
        {
            baseWriter.WriteCData(text);
        }

        public override void WriteCharEntity(char ch)
        {
            baseWriter.WriteCharEntity(ch);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            baseWriter.WriteChars(buffer, index, count);
        }

        public override void WriteComment(string text)
        {
            baseWriter.WriteComment(text);
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            baseWriter.WriteDocType(name, pubid, sysid, subset);
        }

        public override void WriteEndAttribute()
        {
            baseWriter.WriteEndAttribute();
        }

        public override void WriteEndDocument()
        {
            baseWriter.WriteEndDocument();
        }

        public override void WriteEntityRef(string name)
        {
            baseWriter.WriteEntityRef(name);
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            baseWriter.WriteProcessingInstruction(name, text);
        }

        public override void WriteRaw(string data)
        {
            baseWriter.WriteRaw(data);
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            baseWriter.WriteRaw(buffer, index, count);
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            baseWriter.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteStartDocument(bool standalone)
        {
            baseWriter.WriteStartDocument(standalone);
        }

        public override void WriteStartDocument()
        {
            baseWriter.WriteStartDocument();
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            baseWriter.WriteStartElement(prefix, localName, ns);
        }

        public override WriteState WriteState
        {
            get { return baseWriter.WriteState; }
        }

        public override void WriteString(string text)
        {
            baseWriter.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            baseWriter.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteWhitespace(string ws)
        {
            baseWriter.WriteWhitespace(ws);
        }
    }
}
