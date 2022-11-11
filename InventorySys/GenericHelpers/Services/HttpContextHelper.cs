using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GenericHelpers.Services
{
    public class HttpContextHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextHelper._httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext
        {
            get
            {
                return _httpContextAccessor.HttpContext ?? null;
            }
        }

        public static string GetRemoteIP
        {
            get
            {
                return HttpContextHelper.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        public static string GetUserAgent
        {
            get
            {
                return HttpContextHelper.HttpContext.Request.Headers["User-Agent"].ToString();
            }
        }

        public static ClaimsPrincipal GetUserFromHttContext
        {
            get
            {
                return HttpContextHelper.HttpContext.User;
            }
        }

        public static string GetClientIdFromClaim
        {
            get
            {
                try
                {
                    HttpContext httpContext = HttpContextHelper.HttpContext;
                    if (httpContext == null)
                        return (string)null;
                    ClaimsPrincipal user = httpContext.User;
                    if (user == null)
                        return (string)null;
                    IEnumerable<Claim> claims = user.Claims;
                    if (claims == null)
                        return (string)null;
                    return claims.Where<Claim>(s => s.Type == "client_id").FirstOrDefault<Claim>()?.Value;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public static long GetUserIdFromClaim
        {
            get
            {
                try
                {
                    HttpContext httpContext = HttpContextHelper.HttpContext;
                    if (httpContext == null)
                        return 0;
                    ClaimsPrincipal user = httpContext.User;
                    if (user == null)
                        return 0;
                    IEnumerable<Claim> claims = user.Claims;
                    if (claims == null)
                        return 0;
                    var userId = claims.Where<Claim>((Func<Claim, bool>)(s => s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" || s.Type == "sub")).FirstOrDefault<Claim>()?.Value;
                    if (userId == null)
                        return 0;
                    return long.Parse(userId);
                }
                catch (Exception ex)
                {

                    return 0;
                }
            }
        }

        public static string GetEmailFromClaim
        {
            get
            {
                HttpContext httpContext = HttpContextHelper.HttpContext;
                if (httpContext == null)
                    return (string)null;
                ClaimsPrincipal user = httpContext.User;
                if (user == null)
                    return (string)null;
                IEnumerable<Claim> claims = user.Claims;
                if (claims == null)
                    return (string)null;
                return claims.Where<Claim>((Func<Claim, bool>)(s => s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress" || s.Type == "email")).FirstOrDefault<Claim>()?.Value;
            }
        }
        public static string GetBearerTokenFromRequestHeader()
        {
            if (!HttpContextHelper.HttpContext.Request.Headers.ContainsKey("Authorization"))
                return (string)null;
            string str1 = (string)null;
            string str2 = HttpContextHelper.HttpContext.Request.Headers["Authorization"][0];
            if (str2.StartsWith("Bearer "))
                str1 = str2.Substring("Bearer ".Length);
            return str1;
        }

        public static string GetScheme
        {
            get
            {
                return HttpContextHelper.HttpContext.Request.Scheme;
            }
        }

        public static string GetCurrentCulture
        {
            get
            {
                return HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name.ToString();
            }
        }

        public static double ConvertDateTimeToTimestamp(DateTime value)
        {
            TimeSpan epoch = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //return the total seconds (which is a UNIX timestamp)
            return (double)epoch.TotalSeconds;
        }

        public static string GetNameFromClaim
        {
            get
            {
                HttpContext httpContext = HttpContextHelper.HttpContext;
                if (httpContext == null)
                    return (string)null;
                ClaimsPrincipal user = httpContext.User;
                if (user == null)
                    return (string)null;
                IEnumerable<Claim> claims = user.Claims;
                if (claims == null)
                    return (string)null;
                return claims.Where<Claim>((Func<Claim, bool>)(s => s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Name" || s.Type == "name")).FirstOrDefault<Claim>()?.Value;
            }
        }

        public static string GetUsernameFromClaim
        {
            get
            {
                HttpContext httpContext = HttpContextHelper.HttpContext;
                if (httpContext == null)
                    return (string)null;
                ClaimsPrincipal user = httpContext.User;
                if (user == null)
                    return (string)null;
                IEnumerable<Claim> claims = user.Claims;
                if (claims == null)
                    return (string)null;
                return claims.Where<Claim>((Func<Claim, bool>)(s => s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Username" || s.Type == "UserName")).FirstOrDefault<Claim>()?.Value;
            }
        }



    }
}
