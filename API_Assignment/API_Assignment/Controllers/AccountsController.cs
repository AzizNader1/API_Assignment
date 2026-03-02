using API_Assignment.DTOs.UserDTOs;
using API_Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Assignment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var response = _accountService.Login(loginUserDto);
                return Ok(response);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}