using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using RealEstate.Api.Models;
using RealEstate.Api.Models.ViewModel;
using RealEstate.Api.Results;
using RealEstate.Common.Enumerations;
using RealEstate.Entities.Entites;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static RealEstate.Api.ApplicationUserStore;

namespace RealEstate.Api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiControllerBase
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private readonly IAuthService _authService;
        private readonly IAuditLogService _audilogService;

        public AccountController(IErrorService errorService, IAuditLogService audilogService, IAuthService authService, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(errorService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this._authService = authService;
            this._audilogService = audilogService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }


        [Route("user", Name = "GetUserById")]
        public HttpResponseMessage GetUser(HttpRequestMessage request, string Id)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var result = UserManager.FindById(Id);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;

        }

        [Route("user/{username}")]
        public HttpResponseMessage GetUserByName(HttpRequestMessage request, string username)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var result = UserManager.FindByNameAsync(username);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;

        }
        /// <summary>
        /// Hàm API update lại thông tin user
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/account/updateuser</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  17/09/2017   created
        /// </Modified>
        [Route("updateuser")]
        [HttpPost]
        public HttpResponseMessage UpdateInfomationUser(HttpRequestMessage request, AppUserViewModel model)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    responeResult = CreateHttpResponse(request, () =>
                    {
                        var user = UserManager.FindById(User.Identity.GetUserId());
                        if (user != null)
                        {
                            user.Email = model.Email;
                            user.FullName = model.FullName;
                            user.PhoneNumber = model.PhoneNumber;
                            user.Address = model.Address;
                            user.Avatar = model.Avatar;
                            user.BirthDay = model.BirthDay;
                            user.Gender = model.Gender;
                            user.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cập nhập thông tin không thành công");
                        }
                        var result = UserManager.Update(user);
                        var auditlog = new AuditLog();
                        auditlog.CreatedDate = DateTime.Now;
                        auditlog.CreatedBy = user.UserName;
                        auditlog.IPAddress = HttpContext.Current.Request.UserHostAddress;
                        auditlog.LogType = (int)AuditLogType.UpdateUser;
                        auditlog.Device = ((int)AuthenticationSourceEnum.Web).ToString();
                        if (result.Succeeded)
                        {
                            auditlog.UserID = user.Id;
                            auditlog.Description = "Cập nhập thông tin người dùng thành công tại IP" + auditlog.IPAddress;

                        }
                        else
                        {
                            auditlog.UserID = user.Id;
                            auditlog.Description = "Cập nhập thông tin người dùng không thành công tại IP" + auditlog.IPAddress;
                        }
                        _audilogService.Insert(auditlog);
                        _audilogService.SaveChanges();
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, result);

                        return response;
                    });
                }
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }

        /// <summary>
        /// Hàm API thay đổi mật khẩu
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/account/updateuser</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  24/09/2017   created
        /// </Modified>
        [Route("changepassword")]
        [HttpPost]
        public HttpResponseMessage ChangePassword(HttpRequestMessage request, ChangePasswordViewModel model)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    responeResult = CreateHttpResponse(request, () =>
                    {
                        var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                        var auditlog = new AuditLog();
                        auditlog.CreatedDate = DateTime.Now;
                        auditlog.CreatedBy = string.Empty;
                        auditlog.IPAddress = HttpContext.Current.Request.UserHostAddress;
                        auditlog.LogType = (int)AuditLogType.UpdateUser;
                        auditlog.Device = ((int)AuthenticationSourceEnum.Web).ToString();
                        if (result.Succeeded)
                        {
                            auditlog.UserID = User.Identity.GetUserId();
                            auditlog.Description = "Thay đổi mật khẩu thành công tại IP" + auditlog.IPAddress;

                        }
                        else
                        {
                            auditlog.UserID = User.Identity.GetUserId();
                            auditlog.Description = "Thay đổi mật khẩu không thành công tại IP" + auditlog.IPAddress;
                        }
                        _audilogService.Insert(auditlog);
                        _audilogService.SaveChanges();
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, result);

                        return response;
                    });
                }
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }
        //
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<HttpResponseMessage> Register(HttpRequestMessage request, RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                CreatedDate = DateTime.Now,
                Coin = 0,
                RewardPoint = 0,
                RankStar = 0,
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            var auditlog = new AuditLog();
            auditlog.CreatedDate = DateTime.Now;
            auditlog.CreatedBy = model.Email;
            auditlog.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditlog.LogType = (int)AuditLogType.Register;
            auditlog.Device = ((int)AuthenticationSourceEnum.Web).ToString();
            if (result.Succeeded)
            {
                auditlog.UserID = user.Id;
                auditlog.Description = "Đăng ký thành công tại IP" + auditlog.IPAddress;

            }
            else
            {
                auditlog.UserID = model.Email;
                auditlog.Description = "Đăng ký không thành công tại IP" + auditlog.IPAddress;
            }
            _audilogService.Insert(auditlog);
            _audilogService.SaveChanges();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }


        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("externallogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            string redirectUri = string.Empty;

            if (error != null)
            {
                return BadRequest(Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            var redirectUriValidationResult = ValidateClientAndRedirectUri(this.Request, ref redirectUri);

            if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
            {
                return BadRequest(redirectUriValidationResult);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            IdentityUser user = await _userManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            redirectUri = string.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
                                            redirectUri,
                                            externalLogin.ExternalAccessToken,
                                            externalLogin.LoginProvider,
                                            hasRegistered.ToString(),
                                            externalLogin.UserName);

            return Redirect(redirectUri);

        }

        [AllowAnonymous]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var verifiedAccessToken = await VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
            if (verifiedAccessToken == null)
            {
                return BadRequest("Invalid Provider or External Access Token");
            }

            AppUser user = await UserManager.FindAsync(new UserLoginInfo(model.Provider, verifiedAccessToken.user_id));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                return BadRequest("External user is already registered");
            }

            user = new AppUser() { UserName = model.UserName };

            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var info = new ExternalLoginInfo()
            {
                DefaultUserName = model.UserName,
                Login = new UserLoginInfo(model.Provider, verifiedAccessToken.user_id)
            };

            //result = await _repo.AddLoginAsync(user.Id, info.Login);
            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            //generate access token response
            var accessTokenResponse = GenerateLocalAccessTokenResponse(model.UserName);

            return Ok(accessTokenResponse);
        }

        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage Logout(HttpRequestMessage request)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private string ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput)
        {

            Uri redirectUri;

            var redirectUriString = GetQueryString(Request, "redirect_uri");

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                return "redirect_uri is required";
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri)
            {
                return "redirect_uri is invalid";
            }

            var clientId = GetQueryString(Request, "client_id");

            if (string.IsNullOrWhiteSpace(clientId))
            {
                return "client_Id is required";
            }

            var client = _authService.FindClient(clientId);

            if (client == null)
            {
                return string.Format("Client_id '{0}' is not registered in the system.", clientId);
            }

            if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
            }

            redirectUriOutput = redirectUri.AbsoluteUri;

            return string.Empty;

        }

        private string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();

            if (queryStrings == null) return null;

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;

            return match.Value;
        }

        private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
        {
            ParsedExternalAccessToken parsedToken = null;

            var verifyTokenEndPoint = "";

            if (provider == "Facebook")
            {
                //You can get it from here: https://developers.facebook.com/tools/accesstoken/
                //More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook
                var appToken = "xxxxxx";
                verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken);
            }
            else if (provider == "Google")
            {
                verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
            }
            else
            {
                return null;
            }

            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                parsedToken = new ParsedExternalAccessToken();

                if (provider == "Facebook")
                {
                    parsedToken.user_id = jObj["data"]["user_id"];
                    parsedToken.app_id = jObj["data"]["app_id"];

                    if (!string.Equals(Startup.facebookAuthOptions.AppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
                else if (provider == "Google")
                {
                    parsedToken.user_id = jObj["user_id"];
                    parsedToken.app_id = jObj["audience"];

                    if (!string.Equals(Startup.googleAuthOptions.ClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }

                }

            }

            return parsedToken;
        }

        private JObject GenerateLocalAccessTokenResponse(string userName)
        {

            var tokenExpiration = TimeSpan.FromDays(1);

            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);

            var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            JObject tokenResponse = new JObject(
                                        new JProperty("userName", userName),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
        );

            return tokenResponse;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }
            public string ExternalAccessToken { get; set; }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name),
                    ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
                };
            }
        }

        #endregion
    }
}
