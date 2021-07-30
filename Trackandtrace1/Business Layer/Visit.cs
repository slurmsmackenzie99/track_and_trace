using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackApplication
{
    class Visit : Contact
    {
        string user_name_2;
        string user_phone_2;
        string visit_location_name;
        
        public string User_name_2
        {
            set { user_name_2 = value; }
            get { return user_name_2; }
        }

        public string User_phone_2
        {
            set { user_phone_2 = value; }
            get { return user_phone_2; }
        }

        public string Visit_location_name
        {
            set { visit_location_name = value; }
            get { return visit_location_name; }
        }

    }
}
