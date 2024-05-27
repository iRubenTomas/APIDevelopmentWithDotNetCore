using CarInventory.Domain.Entities.Sales;
using CarInventory.Domain.Interfaces;
using CarInventory.Infrastructure.Data;
using CarInventory.Infrastructure.Repositories.Shared;

namespace CarInventory.Infrastructure.Repositories
{
    public class SalesRecordRepository : GenericRepository<SalesRecord>, ISalesRecordRepository
    {
        public SalesRecordRepository(CarInventoryDbContext context) : base(context)
        {
        }
    }
}
