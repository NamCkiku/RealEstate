using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using RealEstate.Api.Infrastructure;
using RealEstate.Api.Models;
using RealEstate.Api.Models.ViewModel;
using RealEstate.Api.Providers;
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
        private readonly IUserWalletService _userWalletService;

        public AccountController(IErrorService errorService,
            IAuditLogService audilogService,
            IAuthService authService,
            IUserWalletService userWalletService,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager) : base(errorService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this._authService = authService;
            this._audilogService = audilogService;
            this._userWalletService = userWalletService;
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

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
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
                UpdatedDate = DateTime.Now,
                Coin = 0,
                RewardPoint = 0,
                RankStar = 0,
            };
            var result = await UserManager.CreateAsync(user, model.Password);

            #region Added Code for Send Email

            //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //code = HttpUtility.UrlEncode(code);
            //try
            //{
            //    string callbackUrl = Url.Link("DefaultApi", new { controller = "Account/ConfirmEmail", userId = user.Id, code = code });

            //    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            //}
            //catch (Exception ex)
            //{
            //    Common.Logs.LogCommon.WriteLogError(ex.Message);
            //}

            #endregion

            var auditlog = new AuditLog();
            auditlog.CreatedDate = DateTime.Now;
            auditlog.CreatedBy = model.Email;
            auditlog.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditlog.LogType = (int)AuditLogType.Register;
            auditlog.Device = ((int)AuthenticationSourceEnum.Web).ToString();
            if (result.Succeeded)
            {
                //Tạo ví cho người dùng
                auditlog.UserID = user.Id;
                auditlog.Description = "Đăng ký thành công tại IP" + auditlog.IPAddress;

                //Tạo ví cho người dùng
                var userWallet = new UserWallet();
                userWallet.Amount = 0;
                userWallet.WalletName = user.UserName;
                userWallet.PromotionAmount = 0;
                userWallet.UserID = user.Id;
                userWallet.IsLocked = false;
                userWallet.IsDeleted = false;
                userWallet.CreatedDate = DateTime.Now;
                userWallet.CreatedByUser = user.UserName;
                _userWalletService.Insert(userWallet);
                _userWalletService.SaveChanges();
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
        [Route("externallogin", Name = "ExternalLogin")]
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

            AppUser user = await _userManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity oAuthIdentity = await _userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await _userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            redirectUri = string.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
                                            redirectUri,
                                            externalLogin.ExternalAccessToken,
                                            externalLogin.LoginProvider,
                                            hasRegistered.ToString(),
                                            externalLogin.UserName);

            return Redirect(redirectUri);

        }


        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
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


        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = this.AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return this.BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return this.BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await this.UserManager.AddLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }
        // GET api/Account/UserInfo
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        //[Route("UserInfo")]
        //public UserInfoViewModel GetUserInfo()
        //{
        //    ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //    return new UserInfoViewModel
        //    {
        //        Email = User.Identity.GetUserName(),
        //        HasRegistered = externalLogin == null,
        //        LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
        //    };
        //}

        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage Logout(HttpRequestMessage request)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        }

        // POST api/Account/SetPassword
        [Route("setpassword")]
        public async Task<IHttpActionResult> SetPassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IdentityResult result = await this.UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }



        #region Confirm Email 

        //
        // GET: /Account/ConfirmEmail
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return NotFound();
            }

            code = HttpUtility.UrlDecode(code);

            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return Ok(result.Succeeded ? "ConfirmEmail" : "Error");
        }


        #region Reset Password

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToRoute("Default", new { controller = "User" });
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return InternalServerError();
        }

        #endregion

        #endregion

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

            //var clientId = GetQueryString(Request, "client_id");

            //if (string.IsNullOrWhiteSpace(clientId))
            //{
            //    return "client_Id is required";
            //}

            //var client = _authService.FindClient(clientId);

            //if (client == null)
            //{
            //    return string.Format("Client_id '{0}' is not registered in the system.", clientId);
            //}

            //if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            //{
            //    return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
            //}

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
                var appToken = "1974624196135004|B28bNJDT2gB3LPbQcevcVqn-_NE";
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


            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

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
