using API_Assignment.DTOs.UserDTOs;
using API_Assignment.Services.UserDTOs;

namespace API_Assignment.Services
{
    public interface IUserService
    {
        GetUserDto GetUser(LoginUserDto loginUserDto);
    }
}
