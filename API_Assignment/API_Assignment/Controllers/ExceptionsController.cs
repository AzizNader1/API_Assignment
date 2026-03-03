using API_Assignment.DTOs.AttendanceDTOs;
using API_Assignment.DTOs.ExceptionDTOs;
using API_Assignment.DTOs.LoanDTOs;
using API_Assignment.Services;
using Microsoft.AspNetCore.Authorization;
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
        public ExceptionsController(IExceptionService exceptionService,
            IAttendanceService attendanceService,
            ILoanService loanService)
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
                if (!ModelState.IsValid)
                    return BadRequest("You should enter all the required values");

                if (addExceptionDto.UserName.Equals("string") || string.IsNullOrEmpty(addExceptionDto.UserName))
                    return BadRequest("You should enter a vaild username");

                _exceptionService.AddException(addExceptionDto);
                return Ok("Exception added successfully.");
            }
            catch (System.Exception ex)
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
                return Ok(exceptions.Count() == 0 ? "there is no exception avaliable" : exceptions);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error retrieving exceptions: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ReturnMyExceptions([FromQuery] GetExceptionDto getExceptionDto)
        {
            try
            {
                if (getExceptionDto.UserName.Equals("string") || string.IsNullOrEmpty(getExceptionDto.UserName))
                    return BadRequest("UserName is required");

                var exceptions = _exceptionService.GetExceptionsByUserName(getExceptionDto);
                return Ok(exceptions.Count() == 0 ? "you have not exception yet" : exceptions);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error retrieving exceptions: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddAttendance([FromForm] AddAttendanceDto addAttendanceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(addAttendanceDto.UserName) || addAttendanceDto.UserName.Equals("string"))
                    return BadRequest("You should enter a vaild and correct username");

                _attendanceService.AddAttendance(addAttendanceDto);
                return Ok("Attendance added successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error retrieving attendances:  {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddLoans([FromForm] AddLoanDto addLoanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(addLoanDto.UserName) || addLoanDto.UserName.Equals("string"))
                    return BadRequest("You should enter a vaild and correct username");

                _loanService.AddLoan(addLoanDto);
                return Ok("Loan added successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error retrieving loans:  {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ReturnMyLoans([FromQuery] string userName)
        {
            try
            {

                var loans = _loanService.GetLoansByUserName(userName);
                return Ok(loans.Count() == 0 ? "You have not loans yet" : loans);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error retrieving loans:  {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ReturnMyApprovals()
        {
            if (!User.IsInRole("Admin"))
                return Forbid("Admins only can access this method");

            var approvals = new
            {
                PendingExceptions = _exceptionService.GetPendingExceptions(),
                PendingLoans = _loanService.GetPendingLoans(),
                PendingAttendances = _attendanceService.GetPendingAttendances()
            };
            return Ok(approvals == null ? "there is no avaliable pendings to approve" : approvals);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult ApproveExceptions([FromBody] UpdateExceptionDto updateExceptionDto)
        {
            if (!User.IsInRole("Admin"))
                return Forbid("Admins only can access this method");

            if (updateExceptionDto.ExceptionId == 0)
                return BadRequest("ExceptionId is required");

            _exceptionService.UpdateExceptionStatus(updateExceptionDto.ExceptionId, updateExceptionDto.Status);
            return Ok("Exception Status Change Successfully");
        }

    }
}