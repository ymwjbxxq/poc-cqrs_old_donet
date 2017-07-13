using System.Threading.Tasks;

namespace QuerySide.shouldbenuget
{
    public interface IDomainEventProcessor
    {
        Task Handle<T>(T domainEvent) where T : class;
    }
}
