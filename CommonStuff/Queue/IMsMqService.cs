using System.Messaging;
using System.Threading.Tasks;

namespace CommonStuff.Queue
{
    public interface IMsMqService
    {
        Task<bool> Push(MessageEvent messageEvent);

        MessageQueue GetMessageQueue();
    }
}
