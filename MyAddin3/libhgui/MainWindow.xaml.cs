using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace libhgui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public XmlDocument doc = new XmlDocument();
        public item sellItem;
         outClass r=new outClass();
         outClass d = new outClass();
         outClass rx = new outClass();
         outClass dx = new outClass();

        public MainWindow()
        {
            InitializeComponent();
             doc.Load("../../data.xml");
             foreach (XmlElement ei in doc.GetElementsByTagName("lib"))
                 libli.Items.Add(new item(ei));
        }

        private void libli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sellItem= libli.SelectedItem as item;
            Title.Text = sellItem.name;
            libDebug.Text = sellItem.debug;
            libDebug64.Text = sellItem.debug64;
            libRelease.Text = sellItem.release;
            libRelease64.Text = sellItem.release64;
            include.Text = sellItem.include;

            linkDebug.Text = sellItem.lDebug;
            linkDebug64.Text = sellItem.lDebug64;
            linkRelease.Text = sellItem.lRelease;
            linkRelease64.Text = sellItem.lRelease64;
            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            fieldS();
            try
            {
                XmlNode f = doc.SelectNodes("/data/lib[@name=\"" + sellItem.name + "\"]").Item(0);
                f = (XmlNode)sellItem.toXML();
            }
            catch (Exception)
            {

                doc.AppendChild(sellItem.toXML());
            }
            doc.Save("../../data.xml");
        }
        private void fieldS()
        {
            sellItem.name = Title.Text;
            sellItem.debug = libDebug.Text;
            sellItem.debug64 = libDebug64.Text;
            sellItem.release = libRelease.Text;
            sellItem.release64 = libRelease64.Text;
            sellItem.include = include.Text;
            
            sellItem.lDebug = linkDebug.Text;
            sellItem.lRelease = linkRelease.Text;
            sellItem.lDebug64 = linkDebug64.Text;
            sellItem.lRelease64 = linkRelease64.Text;

        }

        private void NewI(object sender, RoutedEventArgs e)
        {
            sellItem= new item(doc);
            libli.Items.Add(sellItem);
            doc.Save("../../data.xml");

        }

        private void addClick(object sender, RoutedEventArgs e)
        {
            manifest.Items.Add(sellItem);
        }
        private void removeClick(object sender,RoutedEventArgs e)
        {
            manifest.Items.Remove(manifest.SelectedItem);
        }

        private void compileClick(object sender, RoutedEventArgs e)
        {
            foreach (item i in manifest.Items)
            {
                r.incdir.Add(i.include);
                d.incdir.Add(i.include);
                rx.incdir.Add(i.include);
                dx.incdir.Add(i.include);

                r.liblinks.Add(i.release);
                d.liblinks.Add(i.debug);
                rx.liblinks.Add(i.release64);
                dx.liblinks.Add(i.debug64);

                r.liblinks.Add(i.lRelease);
                d.liblinks.Add(i.lDebug);
                rx.liblinks.Add(i.lRelease64);
                dx.liblinks.Add(i.lDebug64);
                //r.incdir.Add()
            }   
            r.cond = "'$(Configuration)|$(Platform)'=='Release|Win32'";
            d.cond = "'$(Configuration)|$(Platform)'=='Debug|Win32'";
            rx.cond = "'$(Configuration)|$(Platform)'=='Release|x64'";
            dx.cond = "'$(Configuration)|$(Platform)'=='Debug|x64'";

            bigout(r);
            bigout(d);
            bigout(rx);
            bigout(dx);
            App.save();
        }
        private void bigout(outClass oo)
        {
         App.makeOutDocEl(
                string.Join(";",oo.incdir),
                string.Join(";",oo.liblinks),
                string.Join(";",oo.adddep),
                oo.cond
                );
        }
    }
}
