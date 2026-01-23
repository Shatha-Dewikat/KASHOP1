using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public interface IAuthenticationService
    {
      
        Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<LoginResponse> RefreshTokenAsync(TokenApiModel request);

    }
}
