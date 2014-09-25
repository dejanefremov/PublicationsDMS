using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicationsDMS.Web.Api.Models
{
    public class ShareNodeUserList
    {
        public int NodeID
        {
            get;
            set;
        }

        public List<int> UserIDs
        {
            get;
            set;
        }
    }
}