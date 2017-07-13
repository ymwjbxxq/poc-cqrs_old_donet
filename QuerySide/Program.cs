using CommandAndQuery.Commands;
using QuerySide.Queries;
using QuerySide.Setup.Castle;
using System;
using System.Linq;
using System.Threading.Tasks;
using CommonStuff.Queue;
using QuerySide.shouldbenuget;
using System.Messaging;
using Newtonsoft.Json;

namespace QuerySide
{
    class Program
    {
        private static IDomainEventProcessor _domainEventProcessor;
        private static IServiceLocator _serviceLocator;

        static void Main(string[] args)
        {
            WindsorConfig.Boot();
            _serviceLocator = ServiceLocator.Current;

            SyncFromQueue();

            ShowMenu();

            var query = _serviceLocator.Resolve<IGetTotalMoneyQuery>();
            _domainEventProcessor = _serviceLocator.Resolve<IDomainEventProcessor>();

            var consoleKey = ConsoleKey.R;
            while (consoleKey != ConsoleKey.E)
            {
                consoleKey = Console.ReadKey(true).Key;
                switch (consoleKey)
                {
                    case ConsoleKey.R:                        
                        Task.Run(async () => {
                            var response = await query.Init(1).Execute();
                            Console.WriteLine("You have a total of: {0}", response);
                        }).ConfigureAwait(false);
                        
                        break;
                }
            }
        }

        private static void SyncFromQueue()
        {
            var queue = _serviceLocator.Resolve<IMsMqService>();

            var messageQueue = queue.GetMessageQueue();
            //Set the call back
            messageQueue.ReceiveCompleted += queue_ReceiveCompleted;
            //Set the formatter
            //messageQueue.Formatter = new JsonFormatter(new[] { typeof(Employee) });
            //Start the begin Receive
            messageQueue.BeginReceive();
        }

        private static void queue_ReceiveCompleted(object source, ReceiveCompletedEventArgs e)
        {
            var externalQueue = (MessageQueue)source;
            //Create an instance of the queue
            var message = externalQueue.EndReceive(e.AsyncResult);
            var messageEvent = JsonConvert.DeserializeObject<MessageEvent>(message.Body.ToString());


            //find the type
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes()).First(x => x.Name == messageEvent.EventName); ;

            var @event = JsonConvert.DeserializeObject(messageEvent.EventData, type);

            _domainEventProcessor.GetType().GetMethod("Handle")
                .MakeGenericMethod(@event.GetType())
                .Invoke(_domainEventProcessor, new[] { @event });


            // Listen for the next message.
            externalQueue.BeginReceive();
            return;
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Press R to refresh the query result");
            Console.WriteLine("Press E to exit the application");
        }
    }
}
