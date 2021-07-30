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

namespace TrackApplication
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

        private void Add_loc_btn_Click(object sender, RoutedEventArgs e)
        {
            location_input loc_inp_win = new location_input();
            loc_inp_win.Show();
        }

        private void Add_individual_Click(object sender, RoutedEventArgs e)
        {
            individual_input ind_inp_win = new individual_input();
            ind_inp_win.Show();
        }

        private void Rec_visit_btn_Click(object sender, RoutedEventArgs e)
        {
            visit_record rec_vst_win = new visit_record();
            rec_vst_win.Show();
        }

        private void Rec_contact_btn_Click(object sender, RoutedEventArgs e)
        {
            contact_record con_rec_win = new contact_record();
            con_rec_win.Show();
        }

        private void Generate_number_btn_Click(object sender, RoutedEventArgs e)
        {
            generate_meetings gen_meet_win = new generate_meetings();
            gen_meet_win.Show();

        }

        private void Generate_loc_numbers_btn_Click(object sender, RoutedEventArgs e)
        {
            generate_visits gen_visit_win = new generate_visits();
            gen_visit_win.Show();
        }
    }
}

