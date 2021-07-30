using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackApplication
{
    class User
    {
        string name, phone_number;
        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public string Phone_number
        {
            set { phone_number = value; }
            get { return phone_number; }
        }

        public string get_Details()
        {
            return "Name: " + Name + " Phone Number: " + Phone_number;
        }

    }
}
