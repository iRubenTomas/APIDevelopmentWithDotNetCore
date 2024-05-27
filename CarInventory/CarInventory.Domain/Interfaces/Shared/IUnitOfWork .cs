namespace CarInventory.Domain.Interfaces.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository Cars { get; }
        ICustomerRepository Customers { get; }
        ISalesRecordRepository SalesRecords { get; }
        Task<int> CompleteAsync();
    }
}
