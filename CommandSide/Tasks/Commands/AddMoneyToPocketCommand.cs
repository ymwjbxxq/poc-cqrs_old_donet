using CommandAndQuery.Commands;

namespace CommandSide.Tasks.Commands
{
    public class AddMoneyToPocketCommand : ICommand
    {
        public AddMoneyToPocketCommand(int money)
        {
            Money = money;
        }

        public int Money { get; }
    }
}
