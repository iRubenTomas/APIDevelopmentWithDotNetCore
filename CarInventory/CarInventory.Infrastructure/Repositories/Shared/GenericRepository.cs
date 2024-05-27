using CarInventory.Domain.Entities;
using CarInventory.Domain.Interfaces.Shared;
using CarInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarInventory.Infrastructure.Repositories.Shared
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseIdentity
    {
        protected readonly CarInventoryDbContext _context;

        public GenericRepository(CarInventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<PaginatedList<T>> GetPaginatedAsync(int pageIndex, int pageSize)
        {
            var source = _context.Set<T>().OrderBy(e => e.CreatedAt).AsQueryable();
            return await PaginatedList<T>.CreateAsync(source, pageIndex, pageSize);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
