using API_Assignment.Data;

namespace API_Assignment.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id)!);
            _context.SaveChanges();
        }

        public List<T> GetAllAsync()
        {
            return _context.Set<T>().ToList()!;
        }

        public T GetByIdAsync(int id)
        {
            return _context.Set<T>().Find(id)!;
        }

        public T GetByNameAsync(string name)
        {
            return _context.Set<T>().Find(name)!;
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
