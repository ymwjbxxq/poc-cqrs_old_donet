using System;
using System.Threading.Tasks;
using QuerySide.shouldbenuget;
using Storage.Events;
using Storage.Read.Repos;

namespace QuerySide.Handlers
{
    public class AnotherEventHandler : IDomainEventHandler<AnotherEvent>
    {
        private readonly IPocketRepo _pocketRepo;
        public AnotherEventHandler(IPocketRepo pocketRepo)
        {
            _pocketRepo = pocketRepo;
        }

        public async Task Handle(AnotherEvent request)
        {
            Console.WriteLine("AnotherEventHandler");
        }
    }
}
