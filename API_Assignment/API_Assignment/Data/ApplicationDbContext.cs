using Microsoft.EntityFrameworkCore;
using API_Assignment.Models;

namespace API_Assignment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public DbSet<Models.Exception> Exceptions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
