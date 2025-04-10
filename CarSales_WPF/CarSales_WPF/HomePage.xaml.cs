using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace CarSales_WPF
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private string selectedFilePath;

        public HomePage()
        {
            InitializeComponent();
        }

        private void DropArea_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*",
                Title = "Select an XML File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LoadXMLFile(openFileDialog.FileName);
            }
        }

        private void DropArea_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && System.IO.Path.GetExtension(files[0]).ToLower() == ".xml")
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            e.Handled = true;
        }

        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string filePath = files[0];
                    LoadXMLFile(filePath);
                }
            }
        }

        private void LoadXMLFile(string filePath)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(filePath);
                FileNameBlock.Text = System.IO.Path.GetFileName(filePath);
                SubmitPanel.Visibility = Visibility.Visible;
                selectedFilePath = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load XML: {ex.Message}");
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                try
                {
                    XDocument xmlDoc = XDocument.Load(selectedFilePath);

                    var cars = xmlDoc.Descendants("Car")
                                     .Select(x => new Car
                                     {
                                         ModelName = (string)x.Element("ModelName"),
                                         DateOfSale = DateTime.Parse((string)x.Element("DateOfSale")),
                                         Price = (double)x.Element("Price"),
                                         DPH = (double)x.Element("DPH")
                                     }).ToList();

                    NavigationService.Navigate(new DatabasePage(cars));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load XML: {ex.Message}");
                }
            }
        }
    }
}
