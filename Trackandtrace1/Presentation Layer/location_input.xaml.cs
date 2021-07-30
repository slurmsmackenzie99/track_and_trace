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
using System.Windows.Shapes;
using TrackApplication.Data_Layer;

namespace TrackApplication
{
    /// <summary>
    /// Interaction logic for location_input.xaml
    /// </summary>
    public partial class location_input : Window
    {
        public location_input()
        {
            InitializeComponent();
        }
        private void OkLoc_btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(location_input_textbox.Text) || string.IsNullOrWhiteSpace(location_input_textbox.Text))
            {
                MessageBox.Show("Error! Empty field detected.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Location l1 = new Location();
                l1.Location_name = location_input_textbox.Text;
                MessageBox.Show("Location: " + l1.Location_name + " added successfully!");
                CsvIO.importCSV("locations.csv", "id,locationName", l1.Location_name);
            }
           
        }

        private void Show_all_locs_btn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
