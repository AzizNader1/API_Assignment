using API_Assignment.DTOs.UserDTOs;

namespace API_Assignment.Services
{
    public interface IAccountService
    {
        Task<LoginResponseDto> Login(LoginUserDto loginUserDto);
        Task<LoginResponseDto> Register(RegisterDto registerDto);
    }
}
