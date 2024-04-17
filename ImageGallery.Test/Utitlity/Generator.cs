using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace ImageGallery.Test.Utitlity
{
    public static class Generator
    {
        public static HttpContextAccessor GenerateHttpContextAccessor()
        {
            HttpContextAccessor httpContextAccessor;

            var identity = new GenericIdentity("test@gmail.com", "test");
            var contextUser = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext();
            httpContext.User = contextUser;
            httpContextAccessor = new HttpContextAccessor { HttpContext = httpContext };

            return httpContextAccessor;
        }
    }
}
