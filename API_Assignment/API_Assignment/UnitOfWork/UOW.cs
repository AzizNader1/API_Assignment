using API_Assignment.Data;
using API_Assignment.Models;
using API_Assignment.Repositories;

namespace API_Assignment.UnitOfWork
{
    public class UOW
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Attendance> _attendanceRepository;
        private GenericRepository<Loan> _loanRepository;
        private GenericRepository<Models.Exception> _exceptionRepository;

        public UOW(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<Attendance> AttendanceRepository
        {
            get
            {
                if (_attendanceRepository == null)
                {
                    _attendanceRepository = new GenericRepository<Attendance>(_context);
                }
                return _attendanceRepository;
            }
        }

        public GenericRepository<Loan> LoanRepository
        {
            get
            {
                if (_loanRepository == null)
                {
                    _loanRepository = new GenericRepository<Loan>(_context);
                }
                return _loanRepository;
            }
        }

        public GenericRepository<Models.Exception> ExceptionRepository
        {
            get
            {
                if (_exceptionRepository == null)
                {
                    _exceptionRepository = new GenericRepository<Models.Exception>(_context);
                }
                return _exceptionRepository;
            }
        }
    }
}
