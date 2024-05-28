
using MediatR;

namespace CarInventory.Domain.Interfaces.Shared
{
    public interface ICacheableQuery<TResponse> : IRequest<TResponse>
    {
    }
}
