using System.Threading.Tasks;

namespace QuerySide.shouldbenuget
{
    public interface IDomainEventHandler<T> 
    {
        Task Handle(T @event);
    }
}
