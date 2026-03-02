using API_Assignment.DTOs.UserDTOs;
using API_Assignment.Services.UserDTOs;
using API_Assignment.UnitOfWork;

namespace API_Assignment.Services
{
    public class UserService : IUserService
    {
        private readonly UOW _uow;
        public UserService(UOW uow)
        {
            _uow = uow;
        }

        public GetUserDto GetUser(LoginUserDto loginUserDto)
        {
            if(loginUserDto == null)
                throw new ArgumentNullException(nameof(loginUserDto));

            return new GetUserDto();
        }
    }
}
