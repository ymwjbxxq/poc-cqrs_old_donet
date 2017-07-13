using System;
using System.Threading.Tasks;
using QuerySide.shouldbenuget;
using Storage.Events;
using Storage.Read.Repos;

namespace QuerySide.Handlers
{
    public class MoneyAddedEventHandler : IDomainEventHandler<MoneyAddedEvent>
    {
        private readonly IPocketRepo _pocketRepo;
        public MoneyAddedEventHandler(IPocketRepo pocketRepo)
        {
            _pocketRepo = pocketRepo;
        }

        public async Task Handle(MoneyAddedEvent request)
        {
            Console.WriteLine("MoneyAddedEventHandler");
            var pocket = await _pocketRepo.GetById(1);
            pocket.AddMoney(request.Money);
            await _pocketRepo.Save(pocket);
        }
    }
}
