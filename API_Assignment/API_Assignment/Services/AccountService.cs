using API_Assignment.DTOs.UserDTOs;
using API_Assignment.Helpers;
using API_Assignment.Models;
using API_Assignment.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Assignment.Services
{
    public class AccountService : IAccountService
    {
        private readonly UOW _uow;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AccountService(UOW uow, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _uow = uow;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<LoginResponseDto> Register(RegisterDto registerDto)
        {
            if (await _userManager.FindByNameAsync(registerDto.Username) is not null)
                return new LoginResponseDto { Message = "Username is already registered!" };

            var user = new User
            {
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new LoginResponseDto { Message = errors };
            }
            if (registerDto.Username.ToLower().Contains("admin"))
                await _userManager.AddToRoleAsync(user, "Admin");

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new LoginResponseDto
            {
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = registerDto.Username.Contains("Admin") ? ["Admin"] : ["User"],
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }

        public async Task<LoginResponseDto> Login(LoginUserDto loginUserDto)
        {
            var loginResponseDto = new LoginResponseDto();

            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginUserDto.Password))
            {
                loginResponseDto.Message = "UserName or Password is incorrect!";
                return loginResponseDto;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            loginResponseDto.IsAuthenticated = true;
            loginResponseDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            loginResponseDto.Username = user.UserName!;
            loginResponseDto.ExpiresOn = jwtSecurityToken.ValidTo;
            loginResponseDto.Roles = rolesList.ToList()!;

            return loginResponseDto;
        }

        public async Task<string> AddRoleAsync(AddToRoleDto addToRoleDto)
        {
            var user = await _userManager.FindByIdAsync(addToRoleDto.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(addToRoleDto.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, addToRoleDto.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, addToRoleDto.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
