using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Web_ProjectName.Lib
{
    public static class WebEncodersHelper
    {
        public static string Base64UrlEncode(string token)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(token);
            return WebEncoders.Base64UrlEncode(bytes);
        }
        public static string Base64UrlDecode(string token)
        {
            var decode = WebEncoders.Base64UrlDecode(token);
            return Encoding.UTF8.GetString(decode);
        }
    }
}
