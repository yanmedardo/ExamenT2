using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace CalidadT2.Service
{

    public interface ICookieAuthService
    {
        void SetHttpContext(HttpContext httpContext);
        public void Login(ClaimsPrincipal claim);
        public Claim GetClaim();
    }

    public class CookieAuthService : ICookieAuthService
    {
        private HttpContext httpContext;

        public void SetHttpContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public void Login(ClaimsPrincipal claim)
        {
            httpContext.SignInAsync(claim);
        }

        public Claim GetClaim()
        {
            return httpContext.User.Claims.FirstOrDefault();
        }
    }
}
