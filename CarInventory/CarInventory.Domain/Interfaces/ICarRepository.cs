using CarInventory.Domain.Entities;
using CarInventory.Domain.Interfaces.Shared;

namespace CarInventory.Domain.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
    }
}
