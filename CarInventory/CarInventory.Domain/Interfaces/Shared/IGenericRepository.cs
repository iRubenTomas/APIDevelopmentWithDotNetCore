namespace CarInventory.Domain.Interfaces.Shared
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PaginatedList<T>> GetPaginatedAsync(int pageIndex, int pageSize);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
