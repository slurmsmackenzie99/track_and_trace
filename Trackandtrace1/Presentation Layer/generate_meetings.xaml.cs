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

    /// Interaction logic for generate_meetings.xaml
    public partial class generate_meetings : Window
    {
        public generate_meetings()
        {
            InitializeComponent();
        }
        Contact user3 = new Contact();
        private void FindUser_btn_Click(object sender, RoutedEventArgs e)
        {
            //Making a string with an error message
            string error = "Error 1! Record Not Found. Please try again.";
            //switch control statement
            switch (select_cmBox.SelectedIndex)
            {
                case 0: //if phoneNumber selected
                    if (phone_inp_txtBox != null && !string.IsNullOrWhiteSpace(phone_inp_txtBox.Text))
                    {
                        string[] namePhone = CsvIO.findCsvLine(phone_inp_txtBox.Text, 2, "users.csv");
                        if (namePhone[0] == error)
                        { goto case 4; }
                        user3.userName = namePhone[1];
                        user3.userPhone = namePhone[2];
                        goto case 5;
                    }
                    else
                    {
                        goto case 3;
                    }

                case 1: 
                    if (name_inp_txtBox != null && !string.IsNullOrWhiteSpace(name_inp_txtBox.Text))
                    {
                        string[] name_phone = CsvIO.findCsvLine(name_inp_txtBox.Text, 1, "users.csv");
                        if (name_phone[0] == error)
                        { goto case 4; }
                        user3.userName = name_phone[1];
                        user3.userPhone = name_phone[2];
                        goto case 5;
                    }
                    else
                    {
                        goto case 3;
                    }

                case 3: 
                    MessageBox.Show("Please input search term", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case 4: //if user empty
                    MessageBox.Show("User not found in the database.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    user3.userName = String.Empty;
                    user3.userPhone = String.Empty;
                    break;

                case 5: //final check
                    MessageBoxResult result = MessageBox.Show("Is that the correct user? \n" + user3.userName + " " + user3.userPhone, "User Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        user3.userName = String.Empty;
                        user3.userPhone = String.Empty;
                        phone_inp_txtBox.Text = String.Empty;
                        name_inp_txtBox.Text = String.Empty;
                    }

                    if (result == MessageBoxResult.Yes)
                    {

                        findUser_btn.IsEnabled = false;
                        select_cmBox.IsEnabled = false;
                        DatePicker.IsEnabled = true;
                        hour_text_box.IsEnabled = true;
                        minute_text_box.IsEnabled = true;
                        generate_btn.IsEnabled = true;

                    }
                    break;
            }
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

        private void Generate_btn_Click(object sender, RoutedEventArgs e)
        {
            //handler for errors
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Date not selected", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string date = DatePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
                MessageBoxResult result = MessageBox.Show("Is that the correct date? \n" + date, "Date Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    user3.Event_date = date;

                }
                else
                {
                    MessageBox.Show("Please input the values again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    date = String.Empty;
                }

            }

            if (hour_text_box.Text == "Hour" || minute_text_box.Text == "Minute" || string.IsNullOrWhiteSpace(minute_text_box.Text) || string.IsNullOrWhiteSpace(hour_text_box.Text))
            {
                MessageBox.Show("Time was not selected", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                bool success1 = Int32.TryParse(hour_text_box.Text, out int hours);
                bool success2 = Int32.TryParse(minute_text_box.Text, out int minutes);
                
                if (success1 && success2 && hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                {

                    user3.Event_time = hours + ":" + minutes;

                    if (minutes < 10)
                    {
                        user3.Event_time = String.Empty;
                        string minutes_0 = string.Format("0{0}", minutes);
                        user3.Event_time = hours + ":" + minutes_0;
                    }


                    MessageBoxResult result_time = MessageBox.Show("Is that the correct Time? \n" + user3.Event_time, "Time Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result_time == MessageBoxResult.Yes)
                    {


                        string after_date = DatePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
                        List<string> newlist = Functions.Get_Numbers_Contact(user3.userPhone, 2, 4, after_date, user3.Event_time);
                        string s = String.Join(",", newlist);
                        //if function does not do anything, prompt the user
                        if (string.IsNullOrEmpty(s))
                        {
                            MessageBox.Show("No contacts found", "Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("List generated successfuly\n Phone numbers of the individuals in contact with " + user3.userName + " : " + s);

                        }
                    }
                    else
                    {
                        user3.Event_time = String.Empty;
                        MessageBox.Show("Please input time again", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Invalid time input. Try again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
