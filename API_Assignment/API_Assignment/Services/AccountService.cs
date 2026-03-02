using API_Assignment.DTOs.UserDTOs;
using API_Assignment.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Assignment.Services
{
    public class AccountService : IAccountService
    {
        private readonly UOW _uow;
        private readonly IConfiguration _config;
        public AccountService(UOW uow, IConfiguration config)
        {
            _uow = uow;
            _config = config;
        }

        public LoginResponseDto Login(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.UserName))
                throw new ArgumentNullException(nameof(loginUserDto.UserName), "UserName can not left empty");

            if (string.IsNullOrEmpty(loginUserDto.Password))
                throw new ArgumentNullException(nameof(loginUserDto.Password), "Password can not left empty");

            var role = loginUserDto.UserName.ToLower() == "admin" ? "Admin" : "User";

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginUserDto.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]!));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signingCredentials);

            var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new LoginResponseDto { Token = stringToken };

            //another way to minimize the size of the code

            //return new LoginResponseDto()
            //{
            //    Token = new JwtSecurityTokenHandler()
            //        .WriteToken(new JwtSecurityToken(
            //         claims: claims,
            //         expires: DateTime.Now.AddHours(2),
            //         signingCredentials: new SigningCredentials(
            //            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey)),
            //            SecurityAlgorithms.HmacSha256)
            //         ))
            //};
        }

    }
}
