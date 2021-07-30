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
    /// This file contains logic for the contact_record.xaml
    public partial class contact_record : Window
    {
        public contact_record()
        {
            InitializeComponent();
        }

        Contact userOne = new Contact();
        Contact userTwo = new Contact();

        private void Select_cmBox_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (select_cmBox_2.SelectedIndex)
            {
                case 0:
                    phoneNum_lbl_2.IsEnabled = true;
                    phone_inp_txtBox_2.IsEnabled = true;
                    fullName_lbl_2.IsEnabled = false;
                    name_inp_txtBox_2.IsEnabled = false;
                    name_inp_txtBox_2.Text = String.Empty;
                    findUser_btn_2.IsEnabled = true;
                    break;
                case 1:
                    fullName_lbl_2.IsEnabled = true;
                    name_inp_txtBox_2.IsEnabled = true;
                    phoneNum_lbl_2.IsEnabled = false;
                    phone_inp_txtBox_2.IsEnabled = false;
                    phone_inp_txtBox_2.Text = String.Empty;
                    findUser_btn_2.IsEnabled = true;
                    break;
            }
        }

        private void Select_cmBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //switch statement for decision which combo box is clicked
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

        //Event handler to locate Person one
        private void FindUser_btn_Click(object sender, RoutedEventArgs e)
        {
            //Error returned if record not found
            string error = "Error 1! Record Not Found. Please try again.";
            //switch statement to see which index is selected
            switch (select_cmBox.SelectedIndex)
            {
                case 0: 
                    if (phone_inp_txtBox != null && !string.IsNullOrWhiteSpace(phone_inp_txtBox.Text))
                    {
                        string[] namePhone = CsvIO.findCsvLine(phone_inp_txtBox.Text, 2, "users.csv");
                        if (namePhone[0] == error)
                        { goto case 4; }
                        userOne.userName = namePhone[1];
                        userOne.userPhone = namePhone[2];
                        goto case 5;
                    }
                    else
                    {
                        goto case 3;
                    }

                case 1: //name selected

                    if (name_inp_txtBox != null && !string.IsNullOrWhiteSpace(name_inp_txtBox.Text))
                    {
                        string[] namePhone = CsvIO.findCsvLine(name_inp_txtBox.Text, 1, "users.csv");
                        if (namePhone[0] == error)
                        { goto case 4; }
                        userOne.userName = namePhone[1];
                        userOne.userPhone = namePhone[2];
                        goto case 5;
                    }
                    else
                    {
                        goto case 3;
                    }

                    //if input is empty
                case 3:
                    MessageBox.Show("Empty input", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                    // if user is not found
                case 4: 
                    MessageBox.Show("Cannot locate user in the database. Try again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    userOne.userName = String.Empty;
                    userOne.userPhone = String.Empty;
                    break;

                    //get confirmation
                case 5:
                    MessageBoxResult result = MessageBox.Show("Is that the correct user? \n" + userOne.userName + " " + userOne.userPhone, "User Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        userOne.userName = String.Empty;
                        userOne.userPhone = String.Empty;
                        phone_inp_txtBox.Text = String.Empty;
                        name_inp_txtBox.Text = String.Empty;
                    }

                    if (result == MessageBoxResult.Yes)
                    {

                        findUser_btn.IsEnabled = false;
                        save_btn.IsEnabled = true;
                        select_cmBox.IsEnabled = false;
                    }
                    break;
            }
        }

        private void FindUser_btn_2_Click(object sender, RoutedEventArgs e)
        {
            string error = "Try again. Record cannot be found."; 

            switch (select_cmBox_2.SelectedIndex)
            {
                //check phone number
                case 0:
                    if (phone_inp_txtBox_2 != null && !string.IsNullOrWhiteSpace(phone_inp_txtBox_2.Text))
                    {
                        //find in the csv file
                        string[] namePhone = CsvIO.findCsvLine(phone_inp_txtBox_2.Text, 2, "users.csv");
                        if (namePhone[0] == error)
                        { goto case 4; }
                        userTwo.userName = namePhone[1];
                        userTwo.userPhone = namePhone[2];
                        goto case 5;
                    }
                    else
                    {
                        goto case 3;
                    }

                case 1: //name
                    if (name_inp_txtBox_2 != null && !string.IsNullOrWhiteSpace(name_inp_txtBox_2.Text))
                    {
                        string[] namePhone = CsvIO.findCsvLine(name_inp_txtBox_2.Text, 1, "users.csv");
                        if (namePhone[0] == error)
                        { goto case 4; }
                        userTwo.userName = namePhone[1];
                        userTwo.userPhone = namePhone[2];
                        goto case 5;
                    }
                    else
                    {
                        goto case 3;
                    }

                case 3: //input empty
                    MessageBox.Show("Input lacking. Please try again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case 4: //user not found
                    MessageBox.Show("Can't locate the person in the record. Try again", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    userTwo.userName = String.Empty;
                    userTwo.userPhone = String.Empty;
                    break;

                case 5: //final check
                    MessageBoxResult result = MessageBox.Show("Is this the correct user? \n" + userTwo.userName + " " + userTwo.userPhone, "User Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        userTwo.userName = String.Empty;
                        userTwo.userPhone = String.Empty;
                        phone_inp_txtBox_2.Text = String.Empty;
                        name_inp_txtBox_2.Text = String.Empty;
                    }

                    if (result == MessageBoxResult.Yes)
                    {
                        findUser_btn_2.IsEnabled = false;
                        save_btn.IsEnabled = true;
                        select_cmBox.IsEnabled = false;
                        DatePicker.IsEnabled = true;
                        Date_input_lbl.IsEnabled = true;
                        Time_pick_lbl.IsEnabled = true;
                    }
                    break;
            }
        }


        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {


            int caseSwitch = 1;
            switch (caseSwitch)
            {
                case 1: 

                    if (string.IsNullOrWhiteSpace(userOne.userName) || string.IsNullOrWhiteSpace(userOne.userName))
                    {
                        MessageBox.Show("Individual selection empty. Try Again", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    }
                    if (DatePicker.SelectedDate == null)
                    {
                        MessageBox.Show("Date not selected. Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    }
                    if (hour_text_box.Text == "Hour" || minute_text_box.Text == "Minute" || string.IsNullOrWhiteSpace(minute_text_box.Text) || string.IsNullOrWhiteSpace(hour_text_box.Text))
                    {
                        MessageBox.Show("Time not selected. Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }

                    else
                    { goto case 2; }

                case 2: 
                    bool success1 = Int32.TryParse(hour_text_box.Text, out int hours);
                    bool success2 = Int32.TryParse(minute_text_box.Text, out int minutes);

                    if (success1 && success2 && hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                    {
                        userOne.Event_time = hours + ":" + minutes;

                        if (minutes < 10)
                        {
                            userOne.Event_time = String.Empty;
                            string zeroMinutes = string.Format("0{0}", minutes);
                            userOne.Event_time = hours + ":" + zeroMinutes;
                        }
                        MessageBoxResult result_time = MessageBox.Show("Is that the correct time? \n" + userOne.Event_time, "Time Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result_time == MessageBoxResult.Yes)
                        {
                            goto case 3;
                        }
                        else
                        {
                            userOne.Event_time = String.Empty;
                            MessageBox.Show("Please input time again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Time Input. Try Again.", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }

                case 3: //date validation and input in CSV
                    string date = DatePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
                    MessageBoxResult result = MessageBox.Show("Is that the correct date? \n" + date, "Date Validation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        userOne.Event_date = date;
                        string header = "ContactId,UserName1,UserPhone1,UserName2,UserPhone2,ContactDate,ContactTime";
                        string toCsv = userOne.userName + "," + userOne.userPhone + "," + userTwo.userName + "," + userTwo.userPhone + "," + userOne.Event_date + "," + userOne.Event_time;
                        CsvIO.importCSV("contact.csv", header, toCsv);
                        MessageBox.Show("Contact saved successfuly!");

                        MessageBoxResult again = MessageBox.Show("Contact saved successfuly! \n Do you want to record another contact?", "Dialog", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (again == MessageBoxResult.Yes)
                        {
                            userOne.userName = String.Empty;
                            userOne.userPhone = String.Empty;
                            userTwo.userName = String.Empty;
                            userTwo.userPhone = String.Empty;
                            userOne.Event_date = String.Empty;
                            userOne.Event_time = String.Empty;
                            select_cmBox.IsEnabled = true;
                            phone_inp_txtBox.Text = String.Empty;
                            name_inp_txtBox.Text = String.Empty;
                            hour_text_box.Text = String.Empty;
                            minute_text_box.Text = String.Empty;
                            phone_inp_txtBox_2.Text = String.Empty;
                            name_inp_txtBox_2.Text = String.Empty;
                        }
                        else
                        {
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please try again!", "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
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
    }
}
