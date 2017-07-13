using Newtonsoft.Json;
using System;
using System.Messaging;
using System.Threading.Tasks;

namespace CommonStuff.Queue
{
    public class MsMqService : IMsMqService
    {
        private const string QueuName = "CqrsQueue";
        private static MessageQueue _messageQueue;
        private static MessageQueue MessageQueue => _messageQueue ?? GetOrCreate();

        public async Task<bool> Push(MessageEvent messageEvent)
        {
            return await Task.Run(() => {
                try
                {
                    using(var messageQueue = MessageQueue)
                    {
                        var message = JsonConvert.SerializeObject(messageEvent);
                        messageQueue.Send(new Message(message));
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                    return false;
                }
            });
        }

        public MessageQueue GetMessageQueue()
        {
            return MessageQueue;
        }

        private static MessageQueue GetOrCreate()
        {
            MessageQueue queue;
            if(MessageQueue.Exists(@".\Private$\" + QueuName))
            {
                queue = new MessageQueue(@".\Private$\" + QueuName);
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\" + QueuName);
                queue = new MessageQueue(@".\Private$\" + QueuName);
            }

            queue.Formatter = new XmlMessageFormatter(new Type[1] { typeof(string) });

            return queue;
        }
    }
}
