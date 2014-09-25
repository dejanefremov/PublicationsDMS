using Microsoft.Owin.Security.OAuth;
using PublicationsDMS.Entities.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PublicationsDMS.Web.Api.Providers
{
    public class PublicationsAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IUserService userService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService)) as IUserService;
            PublicationsDMS.Entities.Models.User user = null;

            if (!string.IsNullOrEmpty(context.UserName) && !string.IsNullOrEmpty(context.Password))
            {
                user = userService.Login(context.UserName, context.Password);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("name", user.Name));
                    identity.AddClaim(new Claim("email", user.Email));
                    identity.AddClaim(new Claim("userid", user.UserID.ToString()));
                    identity.AddClaim(new Claim("isadmin", user.IsAdministrator.ToString()));

                    context.Validated(identity);
                }
            }

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }

            return Task.FromResult<object>(null);
        }
    }
}