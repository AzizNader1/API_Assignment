using API_Assignment.DTOs.UserDTOs;

namespace API_Assignment.Services
{
    public interface IAccountService
    {
        LoginResponseDto Login(LoginUserDto loginUserDto);

    }
}
