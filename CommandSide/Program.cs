using CommandAndQuery.Commands;
using CommandSide.Setup.Castle;
using CommandSide.Tasks.Commands;
using System;
using System.Threading.Tasks;

namespace CommandSide
{
    class Program
    {
        static void Main(string[] args)
        {
            WindsorConfig.Boot();
            var serviceLocator = ServiceLocator.Current;

            ShowMenu();

            var commandProcessor = serviceLocator.Resolve<ICommandProcessor>();

            var consoleKey = ConsoleKey.A;
            while(consoleKey != ConsoleKey.E)
            {
                consoleKey = Console.ReadKey(true).Key;
                switch(consoleKey)
                {
                    case ConsoleKey.A:
                        var money = new Random().Next(10, 100);                       
                        Task.Run(async () => await commandProcessor.Process(new AddMoneyToPocketCommand(money))).ConfigureAwait(false);
                        break;
                }
            }            
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Press A to add some money to the pocket");
            Console.WriteLine("Press E to close the application");
        }
    }
}
