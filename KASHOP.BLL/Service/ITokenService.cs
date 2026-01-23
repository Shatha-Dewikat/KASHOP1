using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace KASHOP.BLL.Service
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(ApplicationUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
