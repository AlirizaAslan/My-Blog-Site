using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Extensions
{
    public static class LoggedlnUserExtension
    {//static olduğu hiç her seferinde new yapmayacaz

        public static Guid GetLoggerInUserId(this ClaimsPrincipal principal)
        {
            return Guid.Parse( principal.FindFirstValue( ClaimTypes.NameIdentifier));
        }

        public static string GetLoggerInUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }

    }
}
