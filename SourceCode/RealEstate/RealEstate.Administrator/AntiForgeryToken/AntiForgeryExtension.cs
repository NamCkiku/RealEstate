using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Administrator.AntiForgeryToken
{
    public class AntiForgeryExtension
    {
        public static string GetTokenHeaderValue()
        {
            string cookieToken, formToken;
            System.Web.Helpers.AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }
    }
}