using Microsoft.Owin.Security;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Web.Api.Models;
using PublicationsDMS.Web.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

namespace PublicationsDMS.Web.Api.Authentication
{
    public class AuthController : ApiController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public bool Post(LoginUser loginUser)
        {
            if (!string.IsNullOrEmpty(loginUser.Email) && !string.IsNullOrEmpty(loginUser.Password))
            {
                var user = _userService.Login(loginUser.Email, loginUser.Password);
                if (user != null)
                {
                    var authentication = Request.GetOwinContext().Authentication;
                    var identity = new ClaimsIdentity();
                    identity.AddClaim(new Claim("name", user.Name));
                    identity.AddClaim(new Claim("email", user.Email));
                    identity.AddClaim(new Claim("userid", user.UserID.ToString()));
                    identity.AddClaim(new Claim("isadmin", user.IsAdministrator.ToString()));

                    



                    AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                    var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
                    ticket.Properties.IssuedUtc = currentUtc;
                    ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));
                    var token = Startup.OAuthServerOptions.AccessTokenFormat.Protect(ticket);

                    authentication.SignIn(identity);

                    return true;
                }
            }

            return false;
        }



        public bool Get()
        {
            return true;
        }
    }
}
