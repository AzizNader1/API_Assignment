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

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id)!);
            _context.SaveChanges();
        }

        public List<T> GetAllEntities()
        {
            return _context.Set<T>().ToList()!;
        }

        public T GetEntityById(int id)
        {
            return _context.Set<T>().Find(id)!;
        }

        public T GetEntityByName(string name)
        {
            return _context.Set<T>().Find(name)!;
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
