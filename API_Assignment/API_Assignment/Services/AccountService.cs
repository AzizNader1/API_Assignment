using API_Assignment.DTOs.UserDTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Assignment.UnitOfWork;

namespace API_Assignment.Services
{
    public class AccountService : IAccountService
    {
        private readonly UOW _uow;
        public AccountService(UOW uow)
        {
            _uow = uow;
        }

        public LoginResponseDto Login(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
                throw new ArgumentNullException(nameof(loginUserDto),"the data can not be null");

            var userClaims = new List<Claim>()!;
            userClaims.Add(new Claim("UserName",loginUserDto.UserName.ToString()));
            userClaims.Add(new Claim("UserPassword",loginUserDto.Password.ToString()));
            userClaims.Add(new Claim("UserRole", "User"));

            string secretKey = "welcome to my world where you can convert your dreams into reality";
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var signingCredentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentails
                );

            var stringToken =  new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginResponseDto() { Token = stringToken };

            //another way to minimize the size of the code

            //return new LoginResponseDto()
            //{
            //    Token = new JwtSecurityTokenHandler()
            //        .WriteToken(new JwtSecurityToken(
            //         claims: userClaims,
            //         expires: DateTime.Now.AddDays(1),
            //         signingCredentials: new SigningCredentials(
            //            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            //            SecurityAlgorithms.HmacSha256)
            //         ))
            //};
        }

    }
}
