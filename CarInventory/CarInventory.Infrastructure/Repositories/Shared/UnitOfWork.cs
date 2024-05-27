using CarInventory.Domain.Interfaces;
using CarInventory.Domain.Interfaces.Shared;
using CarInventory.Infrastructure.Data;

namespace CarInventory.Infrastructure.Repositories.Shared
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly CarInventoryDbContext _context;
        public UnitOfWork(CarInventoryDbContext context)
        {
            _context = context;
            Cars = new CarRepository(context);
            Customers = new CustomerRepository(context);
            SalesRecords = new SalesRecordRepository(context);
        }

        public ICarRepository Cars { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public ISalesRecordRepository SalesRecords { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
