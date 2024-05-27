using CarInventory.Domain.Entities.Customers;
using CarInventory.Domain.Interfaces.Shared;

namespace CarInventory.Domain.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
    }
}
