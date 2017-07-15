using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using RealEstate.Entities.Entites;
using RealEstate.Service.IService;
using RealEstate.Api.Infrastructure.Core;
using System.Web;
using RealEstate.Common.Enumerations;

namespace RealEstate.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<UserManager<AppUser>>();

            AppUser user;
            try
            {
                user = await userManager.FindAsync(context.UserName, context.Password);

            }
            catch
            {
                // Could not retrieve the user due to error.
                context.SetError("server_error", "Lỗi trong quá trình xử lý.");
                context.Rejected();
                return;
            }
            if (user == null)
            {
                context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.");
                context.Rejected();
            }
            else
            {
                ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                string avatar = string.IsNullOrEmpty(user.Avatar) ? "" : user.Avatar;
                string email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;
                identity.AddClaim(new Claim("userID", user.Id));
                identity.AddClaim(new Claim("fullName", user.FullName));
                identity.AddClaim(new Claim("avatar", avatar));
                identity.AddClaim(new Claim("email", email));
                identity.AddClaim(new Claim("username", user.UserName));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {"userID", user.Id},
                        {"fullName", user.FullName},
                        {"avatar", avatar },
                        {"email", email},
                        {"username", user.UserName},

                    });
                context.Validated(new AuthenticationTicket(identity, props));
                //Lưu lại lịch sử đăng nhập
                var auditlog = new AuditLog();
                auditlog.CreatedDate = DateTime.Now;
                auditlog.UserID = user.Id;
                auditlog.CreatedBy = user.UserName;
                auditlog.IPAddress = HttpContext.Current.Request.UserHostAddress;
                auditlog.LogType = (int)AuditLogType.Login;
                auditlog.Description = "Đăng nhập thành công tại IP" + auditlog.IPAddress;
                var service = ServiceFactory.Get<IAuditLogService>();
                if (service != null)
                {
                    service.Insert(auditlog);
                    service.SaveChanges();
                }
            }
            //ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
            //   OAuthDefaults.AuthenticationType);
            //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            //    CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName);
            //AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}