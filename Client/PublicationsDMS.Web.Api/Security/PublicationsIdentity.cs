using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace PublicationsDMS.Web.Api.Security
{
    public class PublicationsIdentity : IIdentity
    {
        private string _name;
        private string _email;
        private int _userID;
        private bool _isAdministrator;

        public PublicationsIdentity(string name, int userID, string email, bool isAdministrator)
        {
            _userID = userID;
            _email = email;
            _name = name;
            _isAdministrator = isAdministrator;
        }

        public int UserID
        {
            get { return _userID; }
        }

        public string Email
        {
            get { return _email; }
        }

        public string AuthenticationType
        {
            get { return "Bearer"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsAdministrator
        {
            get { return _isAdministrator; }
        }
    }
}