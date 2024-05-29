using CarInventory.Domain.Entities;
using CarInventory.Domain.Interfaces;
using CarInventory.Infrastructure.Data;
using CarInventory.Infrastructure.Repositories.Shared;

namespace CarInventory.Infrastructure.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(CarInventoryDbContext context) : base(context)
        {
        }
    }
}
