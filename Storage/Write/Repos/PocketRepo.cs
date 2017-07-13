using System;
using System.Linq;
using System.Threading.Tasks;
using CommonStuff.Queue;
using Newtonsoft.Json;
using Storage.Write.Entities;
using Storage.Write.FakeDb;

namespace Storage.Write.Repos
{
    // This simulate an ORM but it is very bad
    public class PocketRepo : IPocketRepo
    {
        private readonly IMsMqService _queue;

        public PocketRepo(IMsMqService queue)
        {
            _queue = queue;
        }

        public async Task<Pocket> GetById(int id)
        {
            return await Task.Run(() => {
                var pocketFakeTable = WriteFakeDb.Instance.Pockets.FirstOrDefault((x => x.Id == id));
                return (pocketFakeTable != null) 
                    ? new Pocket(pocketFakeTable.Id)
                    : null;
            });
        }

        public async Task Save(Pocket pocket)
        {
            var pocketFakeTable = WriteFakeDb.Instance.Pockets.FirstOrDefault((x => x.Id == pocket.Id));

            if (pocketFakeTable != null)
            {
                //update on the write side
                pocketFakeTable.Money += pocket.Money;
            }
            else
            {
                //insert
                WriteFakeDb.Instance.Pockets.Add(new PocketFakeTable
                {
                    Id = pocket.Id,
                    Money = pocket.Money
                });
            }

            //get all the events
            var events = pocket.GetUncommittedChanges().ToList();
            if (events.Any())
            {
                foreach (var @event in events)
                {
                    var isSent = await _queue.Push(new MessageEvent {
                        EventId = Guid.NewGuid(),
                        EventName = @event.GetType().Name,
                        EventData = JsonConvert.SerializeObject(@event)
                        });
                    
                   @event.HasBeenSent = isSent;
                }
                pocket.RemoveSentEvents();
            }
        }
    }
}
