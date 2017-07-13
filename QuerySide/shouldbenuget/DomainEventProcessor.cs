using System.Threading.Tasks;
using Castle.Windsor;

namespace QuerySide.shouldbenuget
{
    public class DomainEventProcessor : IDomainEventProcessor
    {
        private readonly IWindsorContainer _container;

        public DomainEventProcessor(IWindsorContainer container)
        {
            _container = container;
        }

        public async Task Handle<T>(T domainEvent) where T: class
        {
            var argumentsAsAnonymousType = typeof(IDomainEventHandler<T>);
            var concretes = _container.ResolveAll(argumentsAsAnonymousType);

            foreach (var concrete in concretes)
            {
                var method = concrete as IDomainEventHandler<T>;
                //execute
                await method.Handle(domainEvent);
            }
        }
    }
}
