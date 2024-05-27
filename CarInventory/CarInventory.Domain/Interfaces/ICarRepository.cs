using CarInventory.Domain.Entities.Cars;
using CarInventory.Domain.Interfaces.Shared;

namespace CarInventory.Domain.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
    }
}
