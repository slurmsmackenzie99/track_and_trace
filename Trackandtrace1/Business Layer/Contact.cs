using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackApplication
{
    class Contact
    {
        string user_name;
        string user_phone;
        string event_date;
        string event_time;
        string event_id;

        public string userName
        {
            set { user_name = value; }
            get { return user_name; }
        }

        public string userPhone
        {
            set { user_phone = value; }
            get { return user_phone; }
        }

        public string Event_date
        {
            set { event_date = value; }
            get { return event_date; }
        }

        public string Event_time
        {
            set { event_time = value; }
            get { return event_time; }
        }


        public string Event_id
        {
            set { event_id = value; }
            get { return event_id; }
        }
    }
}
