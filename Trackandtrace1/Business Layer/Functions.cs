using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackApplication.Data_Layer
{
    class Functions
    {
        //convert time to string
        public static int Time_to_int(string raw_time)
        {
            int time_ready;
            string trim = raw_time.Replace(":", "");
            time_ready = Int32.Parse(trim);
            return time_ready;
        }
        
        public static List<string> Get_Numbers_Visit (string search_term, int search_field, string date_from_1, string date_to_1, string time_from, string time_to)
        {
            var Lines_containing = new List<string>(); 
            var all_numbers = new List<string>();

            string[] lines = System.IO.File.ReadAllLines("visit.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] AllFields = lines[i].Split(','); //split all lines to fields

                if (recordMatches_d(search_term, AllFields, search_field))
                {
                    Lines_containing.Add(lines[i]); //if line contain search term add to list lines_containing
                }
            }
            foreach (string a in Lines_containing)
            {
                string[] fields = a.Split(',');


                int time_from_int = Time_to_int(time_from); //convert time to int
                int time_untill_int = Time_to_int(time_to); 
                DateTime date_from = DateTime.ParseExact(date_from_1, "MM/dd/yyyy", null); //convert the date to valid date obj
                DateTime date_utill = DateTime.ParseExact(date_to_1, "MM/dd/yyyy", null); 
                DateTime date_csv = DateTime.ParseExact(fields[5], "MM/dd/yyyy", null); 
                if (date_csv >= date_from && date_csv <= date_utill)
                {
                    if (date_csv == date_from) //if the dates are equal
                    {
                        int time_csv = Time_to_int(fields[6]); //get the 6th field, time

                        if (time_from_int < time_csv) //if csv is later than from
                        {
                            all_numbers.Add(fields[2]);
                        }
                    }

                    else if (date_csv == date_utill) //if the date is same check time
                    {
                        int time_csv = Time_to_int(fields[6]);

                        if (time_untill_int > time_csv)
                        {
                            all_numbers.Add(fields[2]);

                        }
                    }
                    else
                    {
                        all_numbers.Add(fields[2]);

                    }
                }
            }
            return all_numbers;
        }


        public static List<string> Get_Numbers_Contact (string search_term, int search_field, int search_field2, string date_to_compare, string time_to_compare)
        {
            var Lines_containing = new List<string>();
            var all_numbers = new List<string>();

            string[] lines = System.IO.File.ReadAllLines("contact.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] AllFields = lines[i].Split(',');

                if (recordMatches(search_term, AllFields, search_field, search_field2))
                {
                    Lines_containing.Add(lines[i]);
                }
            }
            foreach (string a in Lines_containing)
            {
                string[] fields = a.Split(',');
                int time_event = Time_to_int(time_to_compare);
                DateTime date1 = DateTime.ParseExact(fields[5], "MM/dd/yyyy", null); //date from csv
                DateTime date2 = DateTime.ParseExact(date_to_compare, "MM/dd/yyyy", null); //date from datepicker 

                if (date1 > date2)
                {


                    if (search_term != fields[search_field])  //if the number is not the same > add
                    {
                        all_numbers.Add(fields[search_field]);

                    }

                    if (search_term != fields[search_field2])
                    {
                        all_numbers.Add(fields[search_field2]);
                    }
                }

                if (date1 == date2)
                {
                    int time_csv = Time_to_int(fields[6]);

                    if (search_term != fields[search_field] && time_event < time_csv)
                    {
                        all_numbers.Add(fields[search_field]);

                    }

                    if (search_term != fields[search_field2] && time_event < time_csv)
                    {
                        all_numbers.Add(fields[search_field2]);
                    }


                }
            }
            return all_numbers;
        }

        public static bool recordMatches(string search_term, string[] record, int search_field, int search_field2) //same funciton with 2 seach fields 
        {
            if (record[search_field].Equals(search_term))
            {
                return true;
            }
            if (record[search_field2].Equals(search_term))
            {
                return true;
            }
            return false;
        }

        public static bool recordMatches_d(string search_term, string[] record, int search_field)
        {
            if (record[search_field].Equals(search_term))
            {
                return true;
            }

            return false;
        }
    }
}
