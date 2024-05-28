namespace CarInventory.Domain.Interfaces.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository Cars { get; }
        Task<int> CompleteAsync();
    }
}
