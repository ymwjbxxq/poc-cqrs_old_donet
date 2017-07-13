using System;
using System.Threading.Tasks;
using QuerySide.shouldbenuget;
using Storage.Events;
using Storage.Read.Repos;

namespace QuerySide.Handlers
{
    public class MoneyAdded2EventHandler : IDomainEventHandler<MoneyAddedEvent>
    {
        private readonly IPocketRepo _pocketRepo;
        public MoneyAdded2EventHandler(IPocketRepo pocketRepo)
        {
            _pocketRepo = pocketRepo;
        }

        public async Task Handle(MoneyAddedEvent request)
        {
            Console.WriteLine("MoneyAddedEventHandler2 respond to the same event");
        }
    }
}
