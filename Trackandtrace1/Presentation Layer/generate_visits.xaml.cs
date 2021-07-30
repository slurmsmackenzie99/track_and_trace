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
using System.Windows.Shapes;
using TrackApplication.Data_Layer;

namespace TrackApplication
{
    /// <summary>
    /// Interaction logic for generate_visits.xaml
    /// </summary>
    public partial class generate_visits : Window
    {
        public generate_visits()
        {
            InitializeComponent();
        }
        Visit visit3 = new Visit();
        Visit visit4 = new Visit();

        private void Find_loc_btn_Click(object sender, RoutedEventArgs e) //location find
        {
            string[] found_line = CsvIO.findCsvLine(loc_srch_TextBox.Text, 1, "locations.csv");

            if (found_line[0] == "Error 1! Record Not Found. Please try again.")
            {
                MessageBox.Show("Input existing location.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                loc_srch_TextBox.Text = String.Empty;

            }
            else
            {
                visit3.Visit_location_name = found_line[1];

                MessageBoxResult result2 = MessageBox.Show("Is that the correct location? \n" + visit3.Visit_location_name, "Location Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result2 == MessageBoxResult.Yes)
                {
                    save_btn.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    loc_srch_TextBox.Text = String.Empty;
                }
            }
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Error! Date not selected. Try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string date = DatePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
                MessageBoxResult result = MessageBox.Show("Is that the correct date? \n" + date, "Date Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    visit3.Event_date = date;

                }
                else
                {
                    MessageBox.Show("Please try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    date = String.Empty;
                }
            }

            if (hour_text_box.Text == "Hour" || minute_text_box.Text == "Minute" || string.IsNullOrWhiteSpace(minute_text_box.Text) || string.IsNullOrWhiteSpace(hour_text_box.Text))
            {
                MessageBox.Show("Error! Time not selected. Try Again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                bool success1 = Int32.TryParse(hour_text_box.Text, out int hours);
                bool success2 = Int32.TryParse(minute_text_box.Text, out int minutes);

                if (success1 && success2 && hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                {
                    visit3.Event_time = hours + ":" + minutes;
                    if (minutes < 10)
                    {
                        visit3.Event_time = String.Empty;
                        string minutes_0 = string.Format("0{0}", minutes);
                        visit3.Event_time = hours + ":" + minutes_0;
                    }
                    MessageBoxResult result_time = MessageBox.Show("Is that the correct Time? \n" + visit3.Event_time, "Time Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result_time == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Time 'From' saved, now input time 'Until'");
                        save_btn_2.IsEnabled = true;
                    }
                    else
                    {
                        visit3.Event_time = String.Empty;
                        MessageBox.Show("Please input time again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Invalid Time Input! Please Try Again! ", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        private void Save_btn_2_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker_2.SelectedDate == null || DatePicker_2.SelectedDate < DatePicker.SelectedDate)
            {
                MessageBox.Show("Error! Date not selected or invalid. Try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string date = DatePicker_2.SelectedDate.Value.ToString("MM/dd/yyyy");
                MessageBoxResult result = MessageBox.Show("Is that the correct date? \n" + date, "Date Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    visit4.Event_date = date;

                }
                else
                {
                    MessageBox.Show("Please try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    date = String.Empty;
                }

            }

            if (hour_text_box_2.Text == "Hour" || minute_text_box_2.Text == "Minute" || string.IsNullOrWhiteSpace(minute_text_box_2.Text) || string.IsNullOrWhiteSpace(hour_text_box_2.Text))
            {
                MessageBox.Show("Error! Time not selected. Try Again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                bool success1 = Int32.TryParse(hour_text_box_2.Text, out int hours);
                bool success2 = Int32.TryParse(minute_text_box_2.Text, out int minutes);

                if (success1 && success2 && hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                {
                    visit4.Event_time = hours + ":" + minutes;

                    if (minutes < 10)
                    {
                        visit4.Event_time = String.Empty;
                        string minutes_0 = string.Format("0{0}", minutes);
                        visit4.Event_time = hours + ":" + minutes_0;
                    }


                    MessageBoxResult result_time = MessageBox.Show("Is that the correct Time? \n" + visit4.Event_time, "Time Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result_time == MessageBoxResult.Yes)
                    {

                        MessageBox.Show("Time saving complete! Now you can click on Generate Button!");
                        generate_btn.IsEnabled = true;


                    }
                    else
                    {
                        visit4.Event_time = String.Empty;
                        MessageBox.Show("Please input time again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Invalid Time Input! Please Try Again! ", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Generate_btn_Click(object sender, RoutedEventArgs e)
        {
            //save the result in list 
            string date_from = DatePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
            string date_to = DatePicker_2.SelectedDate.Value.ToString("MM/dd/yyyy");
            List<string> newlist = Functions.Get_Numbers_Visit(visit3.Visit_location_name, 4, date_from, date_to, visit3.Event_time, visit4.Event_time);
            string s = String.Join(",", newlist);
            //if list is empty tell the user
            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("No contacts found!", "Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("List generated successfuly!\nPhone numbers of users visited '" + visit3.Visit_location_name + "' : " + s);

            }
        }

        private void Hour_text_box_MouseEnter(object sender, MouseEventArgs e)
        {
            if (hour_text_box.Text == "Hour")
            {
                hour_text_box.Text = String.Empty;
            }
        }

        private void Minute_text_box_MouseEnter(object sender, MouseEventArgs e)
        {
            if (minute_text_box.Text == "Minute")
            {
                minute_text_box.Text = String.Empty;
            }
        }

        private void Hour_text_box_2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (hour_text_box_2.Text == "Hour")
            {
                hour_text_box_2.Text = String.Empty;
            }
        }

        private void Minute_text_box_2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (minute_text_box_2.Text == "Minute")
            {
                minute_text_box_2.Text = String.Empty;
            }
        }


    }

}
