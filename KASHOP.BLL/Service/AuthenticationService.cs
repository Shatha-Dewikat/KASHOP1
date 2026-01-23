using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace KASHOP.BLL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);

                if (user is null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "invalid email"
                    };
                }

                var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (!result)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "invalid password"
                    };
                }
                var accessToken = await _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                await _userManager.UpdateAsync(user);

                return new LoginResponse()
                {
                    Success = true,
                    Message = "Login successfully",
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "An unexpected error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }




        // REGISTER
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                var user = registerRequest.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                var _emailSender = new EmailSender();
                if (!result.Succeeded)
                {
                    return new RegisterResponse()
                    {
                        Success = false,
                        Message = "User Creation Failed",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                await _userManager.AddToRoleAsync(user, "User");
                await _emailSender.SendEmailAsync(
    user.Email,
    "welcome",
    $"<h1>welcome ... {user.UserName}</h1>"
);

                return new RegisterResponse()
                {
                    Success = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse()
                {
                    Success = false,
                    Message = "An unexpected error",
                    Errors = new List<string> { ex.Message }
                };
            }


        }

        public async Task<LoginResponse> RefreshTokenAsync(TokenApiModel request)
        {
            if (request.AccessToken is null || request.RefreshToken is null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid client request"
                };
            }

            var accessToken = request.AccessToken;
            var refreshToken = request.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid token"
                };
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid client request"
                };
            }

            var newAccessToken = await _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return new LoginResponse
            {
                Success = true,
                Message = "Token refreshed successfully",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }


    }


}
