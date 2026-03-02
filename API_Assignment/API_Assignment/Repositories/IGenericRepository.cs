namespace API_Assignment.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAllAsync();
        T GetByIdAsync(int id);
        T GetByNameAsync(string name);
        void AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(int id);
    }
}
