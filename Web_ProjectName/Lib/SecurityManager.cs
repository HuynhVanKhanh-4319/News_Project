using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using static System.String;

namespace Web_ProjectName.Lib
{
    public static class SecurityManager
    {
        public class M_AccountSecurity
        {
            public string accountId { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
            public string name { get; set; }
            public string accessToken { get; set; }
            public string accountType { get; set; }
            public string avatar { get; set; }
            public string supplierId { get; set; }
            public string supplierName { get; set; }
            public string refCode { get; set; }
            public bool stayLoggedIn { get; set; }
            public string accessLogId { get; set; }
            public int timeOut { get; set; } = 30;
            public List<string> role { get; set; }
        }
        private static IEnumerable<Claim> getUserClaims(M_AccountSecurity account)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("AccountId", account.accountId),
                new Claim(ClaimTypes.NameIdentifier, account.userName),
                new Claim(ClaimTypes.Name, account.name),
                new Claim("Avatar", account.avatar),
                new Claim("Password", IsNullOrEmpty(account.password) ? "" : account.password),
                new Claim("AccountType", IsNullOrEmpty(account.accountType) ? "" : account.accountType),
                new Claim("AccessLogId", IsNullOrEmpty(account.accessLogId) ? "" : account.accessLogId),
                new Claim("AccessToken", account.accessToken),
                new Claim("SupplierId", IsNullOrEmpty(account.supplierId) ? "" : account.supplierId),
                new Claim("SupplierName", IsNullOrEmpty(account.supplierName) ? "" : account.supplierName),
                new Claim("RefCode", IsNullOrEmpty(account.refCode) ? "" : account.refCode),
            };
            account.role?.ForEach((x) => { claims.Add(new Claim(ClaimTypes.Role, x.ToString())); });
            return claims;
        }
        public static async Task SignIn(HttpContext httpContext, M_AccountSecurity account, string scheme)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(getUserClaims(account), scheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(
                scheme: scheme,
                principal: claimsPrincipal,
                properties: new AuthenticationProperties
                {
                    IsPersistent = account.stayLoggedIn,
                    ExpiresUtc = DateTime.UtcNow.AddHours(7).AddMinutes(account.timeOut)
                });
        }
        public static async Task SignOut(HttpContext httpContext, string scheme)
        {
            if (httpContext.Request.Cookies.Count > 0)
            {
                httpContext.Response.Cookies.Delete("Auth");
                //foreach (var cookie in httpContext.Request.Cookies.Keys)
                //    httpContext.Response.Cookies.Delete(cookie);
            }
            await httpContext.SignOutAsync(scheme);
            httpContext.Session.Clear();
        }
    }
}
