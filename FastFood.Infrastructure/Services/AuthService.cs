using FastFood.Core.DTOs.Auth;
using FastFood.Core.Entities;
using FastFood.Core.Interfaces;
using FastFood.Infrastructure.Data;
using FastFood.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;

namespace FastFood.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtTokenService jwtTokenService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Email already exists."
                };
            }

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(", ",
                        result.Errors.Select(e => e.Description))
                };
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(user, "User");

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateToken(user, roles);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Registration successful.",
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            bool passwordValid =
                await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateToken(user, roles);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Login successful.",
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync(string refreshToken)
        {
            return Task.CompletedTask;
        }
    }
}