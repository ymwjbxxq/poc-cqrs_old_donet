using CommandAndQuery.Commands;
using CommandSide.Tasks.Commands;
using System.Threading.Tasks;
using System;
using System.Linq;
using Storage.Write.Entities;
using Storage.Write.FakeDb;
using Storage.Write.Repos;

namespace CommandSide.Tasks.Handlers
{
    public class AddMoneyToPocketHandler : ICommandHandler<AddMoneyToPocketCommand>
    {
        private readonly IPocketRepo _pocketRepo;

        public AddMoneyToPocketHandler(IPocketRepo pocketRepo)
        {
            _pocketRepo = pocketRepo;
        }

        public async Task Handle(AddMoneyToPocketCommand command)
        {
            var pocket = await _pocketRepo.GetById(1);
            if (pocket != null)
            {
                pocket.AddMoney(command.Money);
            }
            else
            {
                //this side should not be here, create should be another separate command called from a different action
               pocket = new Pocket(1);
               pocket.AddMoney(command.Money);
            }

            await _pocketRepo.Save(pocket);
            Console.WriteLine("MoneyAdded {0} your total in your pocket is {1}", 
                                        command.Money, 
                                        WriteFakeDb.Instance.Pockets.Where(x => x.Id == 1).Sum(x => x.Money));
        }
    }
}
