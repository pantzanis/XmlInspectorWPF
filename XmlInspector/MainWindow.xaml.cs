using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace XmlInspector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
        //    {
        //        lstNames.Items.Add(txtName.Text);
        //        txtName.Clear();
        //    }
        //}

        private void ParseLoadedFile(string fileName)
        {
            if (fileName.EndsWith(".xml") || fileName.EndsWith(".behdat"))
            {
                txtEditor.Text = "The results are: \n";
                //Create the XmlDocument.
                XmlDocument doc = new XmlDocument();
                System.Diagnostics.Debug.WriteLine("The Path if the selected file is: ", fileName);
                doc.Load(fileName);

                //Display all the book titles.
                XmlNodeList elemList = doc.GetElementsByTagName("title");
                for (int i = 0; i < elemList.Count; i++)
                {
                    System.Diagnostics.Debug.WriteLine(elemList[i]!.InnerXml);
                    txtEditor.Text += elemList[i]!.InnerXml + "\n";
                }
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            }
            else
            {
                txtEditor.Text = "Please select a .xml or a .behdat file";
            }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|Behdat files (*.behdat)|*.behdat|Xml files (*.xml)|*.xml";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == true) ParseLoadedFile(openFileDialog.FileName);
        }

        private void File_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.
                System.Diagnostics.Debug.WriteLine("event fired");
                foreach (string filename in files)
                {
                    ParseLoadedFile(filename);
                }
            }
        }

        private void Preview_Drag_Over(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
