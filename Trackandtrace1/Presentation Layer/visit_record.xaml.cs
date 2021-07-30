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
    /// Interaction logic for visit_record.xaml
    /// </summary>
    public partial class visit_record : Window
    {
        Visit visit1 = new Visit();

        public visit_record()
        {
            InitializeComponent();
        }

        private void Select_cmBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (select_cmBox.SelectedIndex)
            {
                case 0:
                    phoneNum_lbl.IsEnabled = true;
                    phone_inp_txtBox.IsEnabled = true;
                    fullName_lbl.IsEnabled = false;
                    name_inp_txtBox.IsEnabled = false;
                    name_inp_txtBox.Text = String.Empty;
                    findUser_btn.IsEnabled = true;
                    break;
                case 1:
                    fullName_lbl.IsEnabled = true;
                    name_inp_txtBox.IsEnabled = true;
                    phoneNum_lbl.IsEnabled = false;
                    phone_inp_txtBox.IsEnabled = false;
                    phone_inp_txtBox.Text = String.Empty;
                    findUser_btn.IsEnabled = true;
                    break;
            }
        }

        private void FindUser_btn_Click(object sender, RoutedEventArgs e)
        {

            string error = "Error 1! Record Not Found. Please try again.";

            switch (select_cmBox.SelectedIndex)
            {
                case 0: ///phone number
                    if (phone_inp_txtBox != null && !string.IsNullOrWhiteSpace(phone_inp_txtBox.Text))
                    {
                        string[] name_phone = CsvIO.findCsvLine(phone_inp_txtBox.Text, 2, "users.csv");
                        if (name_phone[0] == error)
                        { goto case 4; }
                        visit1.userName = name_phone[1];
                        visit1.userPhone = name_phone[2];
                        goto case 5;
                    }
                    else
                    {
                        goto case 3;
                    }

                case 1: //name

                    if (name_inp_txtBox != null && !string.IsNullOrWhiteSpace(name_inp_txtBox.Text))
                    {
                        string[] name_phone = CsvIO.findCsvLine(name_inp_txtBox.Text, 1, "users.csv");
                        if (name_phone[0] == error)
                        { goto case 4; }
                        visit1.userName = name_phone[1];
                        visit1.userPhone = name_phone[2];
                        goto case 5;
                    }
                    else
                    {
                        goto case 3;
                    }

                case 3: //input empty
                    MessageBox.Show("Please input search term!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case 4: //user not found
                    MessageBox.Show("User not found in the database! Please try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    visit1.userName = String.Empty;
                    visit1.userPhone = String.Empty;
                    break;

                case 5: //final check
                    MessageBoxResult result = MessageBox.Show("Is that the correct user? \n" + visit1.userName + " " + visit1.userPhone, "User Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        visit1.userName = String.Empty;
                        visit1.userPhone = String.Empty;
                        phone_inp_txtBox.Text = String.Empty;
                        name_inp_txtBox.Text = String.Empty;
                    }

                    if (result == MessageBoxResult.Yes)
                    {
                        findUser_btn.IsEnabled = false;
                        save_btn.IsEnabled = true;
                    }
                    break;
            }
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            int caseSwitch = 1;
            switch (caseSwitch)
            {

                case 1: //check for null and existing
                    if (string.IsNullOrWhiteSpace(loc_srch_TextBox.Text))
                    {
                        MessageBox.Show("Error! Location not selected. Try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }

                    if (DatePicker.SelectedDate == null)
                    {
                        MessageBox.Show("Error! Date not selected. Try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }

                    if (hour_text_box.Text == "Hour" || minute_text_box.Text == "Minute" || string.IsNullOrWhiteSpace(minute_text_box.Text) || string.IsNullOrWhiteSpace(hour_text_box.Text))
                    {
                        MessageBox.Show("Error! Time not selected. Try Again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }



                    else
                    {
                        goto case 5; //check time first
                    }


                case 2: //location input
                    string[] found_line = CsvIO.findCsvLine(loc_srch_TextBox.Text, 1, "locations.csv");
                    if (found_line[0] == "Error 1! Record Not Found. Please try again.")
                    {
                        MessageBoxResult result_loc = MessageBox.Show(loc_srch_TextBox.Text + " location not found in the database! Do you want to add as new location?", "Instructions", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result_loc == MessageBoxResult.Yes)
                        {
                            CsvIO.importCSV("locations.csv", "id,locationName", loc_srch_TextBox.Text);
                            MessageBox.Show("Location: " + loc_srch_TextBox.Text + "\n Added successful!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                            string[] newloc = CsvIO.findCsvLine(loc_srch_TextBox.Text, 1, "locations.csv");
                            visit1.Event_id = newloc[0];
                            visit1.Visit_location_name = newloc[1];
                            goto case 4;
                        }
                        else
                        {
                            MessageBox.Show("Input existing location.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                            loc_srch_TextBox.Text = String.Empty;
                            break;
                        }
                    }
                    else
                    {
                        visit1.Event_id = found_line[0];
                        visit1.Visit_location_name = found_line[1];
                        goto case 4;
                    }

                case 3://date check
                    string date = DatePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
                    MessageBoxResult result = MessageBox.Show("Is that the correct date? \n" + date, "Date Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        visit1.Event_date = date;
                        //Visit.addToCsvVisit(visit1.User_name, visit1.User_phone, visit1.Visit_location, visit1.Visit_date);
                        goto case 2;

                    }
                    else
                    {
                        MessageBox.Show("Please try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                
                case 4: //correct location check
                    MessageBoxResult result2 = MessageBox.Show("Is that the correct location? \n" + visit1.Visit_location_name, "Location Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        string header = "VisitId,UserName,UserPhone,LocationID,LocationName,Date,Time";
                        string toCsv = visit1.userName + "," + visit1.userPhone + "," + visit1.Event_id + "," + visit1.Visit_location_name + "," + visit1.Event_date + "," + visit1.Event_time;
                        CsvIO.importCSV("visit.csv", header, toCsv);
                        MessageBox.Show("Visit added successful!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        loc_srch_TextBox.Text = String.Empty;
                        break;
                    }
                case 5: //Time validation
                    bool success1 = Int32.TryParse(hour_text_box.Text, out int hours);
                    bool success2 = Int32.TryParse(minute_text_box.Text, out int minutes);

                    if (success1 && success2 && hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                    {
                        visit1.Event_time = hours + ":" + minutes;

                        if (minutes < 10)
                        {
                            visit1.Event_time = String.Empty;
                            string minutes_0 = string.Format("0{0}", minutes);
                            visit1.Event_time = hours + ":" + minutes_0;
                        }

                        MessageBoxResult result_time = MessageBox.Show("Is that the correct Time? \n" + visit1.Event_time, "Time Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result_time == MessageBoxResult.Yes)
                        {
                            goto case 3;
                        }
                        else
                        {
                            visit1.Event_time = String.Empty;
                            MessageBox.Show("Please input time again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Time Input! Please Try Again! ", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
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
    }
}
