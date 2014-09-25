using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;
using PublicationsDMS.Web.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PublicationsDMS.Web.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class UserAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            if (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated 
                && HttpContext.Current.User.GetType() != typeof(PublicationsPrincipal))
            {
                System.Security.Claims.ClaimsIdentity claimsIdentity = ((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);

                string name = claimsIdentity.FindFirst("name").Value;
                string email = claimsIdentity.FindFirst("email").Value;
                int userID = Convert.ToInt32(claimsIdentity.FindFirst("userid").Value);
                bool isAdministrator = Convert.ToBoolean(claimsIdentity.FindFirst("isadmin").Value);

                HttpContext.Current.User =
                    System.Threading.Thread.CurrentPrincipal = new PublicationsPrincipal(new PublicationsIdentity(name, userID, email, isAdministrator));
            }
        }
    }
}