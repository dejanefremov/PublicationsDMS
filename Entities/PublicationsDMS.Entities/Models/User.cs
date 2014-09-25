using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Entities.Models
{
    public class User
    {
        public int UserID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public bool IsAdministrator
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }
    }
}
