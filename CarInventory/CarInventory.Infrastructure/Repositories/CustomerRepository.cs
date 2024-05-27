using CarInventory.Domain.Entities.Customers;
using CarInventory.Domain.Interfaces;
using CarInventory.Infrastructure.Data;
using CarInventory.Infrastructure.Repositories.Shared;

namespace CarInventory.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CarInventoryDbContext context) : base(context)
        {
        }
    }
}
