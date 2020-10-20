using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace DeliverIT.Business.Services
{
    public class AuthenticatedUser
    {
        private readonly IHttpContextAccessor _accessor;
        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IIdentity UsuarioLogado => _accessor.HttpContext.User.Identity;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
