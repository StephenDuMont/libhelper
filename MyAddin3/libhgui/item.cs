using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace libhgui
{
    public class item
    {
        XmlElement elem;
        public string name;
        public string include;
        public string release;
        public string debug;
        public string release64;
        public string debug64;
        public string lRelease;
        public string lDebug;
        public string lRelease64;
        public string lDebug64;

        public item(XmlDocument doc)
        {
            elem = doc.CreateElement("lib");

            name = "new";
            include = release = release64 = debug = debug64 = "";
            elem.AppendChild(doc.CreateElement("include"));
            elem.AppendChild(doc.CreateElement("debug"));
            elem.AppendChild(doc.CreateElement("release"));

            elem.AppendChild(doc.CreateElement("debugx64"));            
            elem.AppendChild(doc.CreateElement("releasex64"));

            elem.AppendChild(doc.CreateElement("ldebug")); 
            elem.AppendChild(doc.CreateElement("lrelease"));

            elem.AppendChild(doc.CreateElement("ldebugx64"));
            elem.AppendChild(doc.CreateElement("lreleasex64"));

            new item(toXML());
            doc.DocumentElement.AppendChild(elem);
        }
        public item(XmlElement e)
        {
            elem = e;
            name = e.GetAttribute("name");
            include=e["include"].InnerXml;

            release = e["release"].InnerXml;
            debug = e["debug"].InnerXml;
            debug64 = e["debugx64"].InnerXml;
            release64 = e["releasex64"].InnerXml;

            lRelease = e["lrelease"].InnerXml;
            lDebug = e["ldebug"].InnerXml;
            lDebug64 = e["ldebugx64"].InnerXml;
            lRelease64 = e["lreleasex64"].InnerXml;
                        
        }
        public XmlElement toXML()
        {
            elem.SetAttribute("name",name);
            elem["include"].InnerXml = include;

            elem["release"].InnerXml=release;
            elem["debug"].InnerXml = debug;
            elem["debugx64"].InnerXml=debug64;
            elem["releasex64"].InnerXml=release64;

            elem["lrelease"].InnerXml = lRelease;
            elem["ldebug"].InnerXml = lDebug;
            elem["ldebugx64"].InnerXml = lDebug64;
            elem["lreleasex64"].InnerXml = lRelease64;
            
            return elem;
        }
        public override string   ToString()
        {
         	 return name;
        }
    }
}
