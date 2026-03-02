using API_Assignment.Data;

namespace API_Assignment.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public async void DeleteAsync(int id)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id)!);
            _context.SaveChanges();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return _context.Set<T>().ToList()!;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return _context.Set<T>().Find(id)!;
        }

        public async Task<T> GetByNameAsync(string name)
        {
            return _context.Set<T>().Find(name)!;
        }

        public async void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
