using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Serialization; 

namespace libhgui
{
    /// <summary>   
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static string fname;
        public static XmlDocument dout=new XmlDocument();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 1)
            {
                fname = e.Args[0].Replace("\\\\", "\\");
            dout.Load(fname);

            }
                
            else
            {
                MessageBox.Show("null project mode");
                dout.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\"/>");
                XmlElement elem= dout.DocumentElement.AppendChild(dout.CreateElement("ImportGroup")) as XmlElement;
                //elem.AppendChild(dout.CreateAttribute("Label"));
                //elem.Attributes.Xmlns = false;
                elem.SetAttribute("Label", "PropertySheets");
                elem=dout.DocumentElement.AppendChild(dout.CreateElement("PropertyGroup"))as XmlElement;
               // elem.AppendChild(dout.CreateAttribute("Label"));
                elem.SetAttribute("Label","UserMacros");
                
            }
        }
        
        public static void makeOutDocEl(string incdir, string liblinks,string adddep, string cond)
        {
            if (adddep != "")
                adddep += ";";
            XmlElement elem = dout.DocumentElement.AppendChild(dout.CreateElement("ItemDefinitionGroup")) as XmlElement;
            elem.SetAttribute("Condition", cond);
            elem.AppendChild(dout.CreateElement("CLCompile")).AppendChild(dout.CreateElement("AdditionalIncludeDirectories")).InnerText = incdir;
            elem=elem.AppendChild(dout.CreateElement("Link")) as XmlElement;
                elem.AppendChild(dout.CreateElement("AdditionalLibraryDirectories")).InnerText = liblinks;
            elem.AppendChild(dout.CreateElement("AdditionalDependencies")).InnerText=adddep+
            "kernel32.lib;user32.lib;gdi32.lib;winspool.lib;comdlg32.lib;advapi32.lib;shell32.lib;ole32.lib;oleaut32.lib;uuid.lib;odbc32.lib;odbccp32.lib;%(AdditionalDependencies)";
            
        }
        public static void setOutDocEl(string incdir, string liblinks, string adddep, string cond)
        {
            XmlElement elem = dout.SelectNodes("ItemDefinitionGroup[@Condition=\"" + cond + "\"]").Item(0) as XmlElement;
            if (elem == null)
                makeOutDocEl(incdir, liblinks, adddep, cond);
            else
            {
                if (adddep != "")
                    adddep += ";";
                elem["AdditionalIncludeDirectories"].InnerText = incdir;
                elem = elem.AppendChild(dout.CreateElement("Link")) as XmlElement;
                elem["AdditionalLibraryDirectories"].InnerText = liblinks;
                elem["AdditionalDependencies"].InnerText = adddep +
                "kernel32.lib;user32.lib;gdi32.lib;winspool.lib;comdlg32.lib;advapi32.lib;shell32.lib;ole32.lib;oleaut32.lib;uuid.lib;odbc32.lib;odbccp32.lib;%(AdditionalDependencies)";

            }
        }
        public static void save()
        {
            if (fname == "")
                MessageBox.Show(dout.InnerXml);
            else
                dout.Save(fname);
        }
    }
}
