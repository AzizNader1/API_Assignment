using API_Assignment.DTOs.AttendanceDTOs;
using API_Assignment.DTOs.ExceptionDTOs;
using API_Assignment.DTOs.LoanDTOs;
using API_Assignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Assignment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExceptionsController : ControllerBase
    {
        private readonly IExceptionService _exceptionService;
        private readonly IAttendanceService _attendanceService;
        private readonly ILoanService _loanService;
        public ExceptionsController(IExceptionService exceptionService, IAttendanceService attendanceService, ILoanService loanService)
        {
            _exceptionService = exceptionService;
            _attendanceService = attendanceService;
            _loanService = loanService;
        }

        [HttpPost]
        public IActionResult AddException([FromForm] AddExceptionDto addExceptionDto)
        {
            try
            {
                _exceptionService.AddException(addExceptionDto);
                return Ok("Exception added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding exception: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ReturnExceptions()
        {
            try
            {
                var exceptions = _exceptionService.GetAllExceptions();
                return Ok(exceptions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving exceptions: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ReturnMyExceptions([FromForm] GetExceptionDto getExceptionDto)
        {
            try
            {
                var exceptions = _exceptionService.GetExceptionsByUserName(getExceptionDto);
                return Ok(exceptions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving exceptions: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddAttendance([FromForm] AddAttendanceDto addAttendanceDto)
        {
            try
            {
                _attendanceService.AddAttendance(addAttendanceDto);
                return Ok("Attendance added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving attendances:  {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddLoans([FromForm] AddLoanDto addLoanDto)
        {
            try
            {
                _loanService.AddLoan(addLoanDto);
                return Ok("Loan added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving loans:  {ex.Message}");
            }
        }
    }
}