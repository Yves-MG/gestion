using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Application.Dtos.Users.Request;
using Gestion.Application.Dtos.Users.Response;
using Gestion.Core.Entities;
using Gestion.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, ITokenService tokenService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<ServiceResult<AuthResp>> LoginAsync(Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.UserName);
            if (user == null)
                return (ServiceResult<AuthResp>)ServiceResult<AuthResp>.Failure("User not found.");

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            if (result.Succeeded)
            {
                var role = await _userManager.GetRolesAsync(user);
                var token = await _tokenService.GenerateToken(user); // Fix: await the task to get the string result
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _userManager.UpdateAsync(user);

                return ServiceResult<AuthResp>.Success("Connexion  réussie", new AuthResp
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                    Roles = user.Roles
                });
            }
            else
                return (ServiceResult<AuthResp>)ServiceResult<AuthResp>.Failure("User not found.");
        }

        public async Task<ServiceResult<AuthResp>> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);
            if (user == null)
            {
                return (ServiceResult<AuthResp>)ServiceResult<AuthResp>.Failure("Le refresh token est invalide ou expiré.");
            }

            var token = await _tokenService.GenerateToken(user); // Fix: await the task to get the string result
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return ServiceResult<AuthResp>.Success("succes", new AuthResp
            {
                Token = token,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
                // Roles = user.Roles.Split(',').ToList()
            });
        }

        public async Task<ServiceResult<AuthResp>> RegisterAsync(Register register)
        {
            if (await _userManager.FindByEmailAsync(register.Email) != null)

                return (ServiceResult<AuthResp>)ServiceResult<AuthResp>.Failure("User already exists.");

            var user = new User
            {
                Email = register.Email,
                UserName = register.UserName,
                FirstName = register.FirstName,
                LastName = register.LastName
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return (ServiceResult<AuthResp>)ServiceResult<AuthResp>.Failure("Failed to create user.");


            if (register.Roles != null && register.Roles.Any())
            {
                foreach (var role in register.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                        return (ServiceResult<AuthResp>)ServiceResult<AuthResp>.Failure("Role does not exist.");
                    var roleResult = await _userManager.AddToRoleAsync(user, role);
                    if (!roleResult.Succeeded)
                        return (ServiceResult<AuthResp>)ServiceResult<AuthResp>.Failure("Failed to assign role.");
                }
            }
            // Générer le JWT et le Refresh Token
            var token = await _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return ServiceResult<AuthResp>.Success("Inscription réussie", new AuthResp
            {
                Token = token,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                Roles = user.Roles
            });

        }
    }
}
