using System.Collections.Generic;
using System.Linq;
using Storage.Events;

namespace Storage.Write.Entities
{
    public abstract class AggregateRoot
    {
        private readonly List<IEvent> _changes;

        protected AggregateRoot()
        {
            _changes = new List<IEvent>();
        }

        protected virtual void ApplyChange(IEvent baseEvent)
        {
            _changes.Add(baseEvent);
        }

        public virtual IEnumerable<IEvent> GetUncommittedChanges()
        {
            return _changes.Where(x => !x.HasBeenSent);
        }

        protected virtual void RemoveChange()
        {
            _changes.RemoveAll(x => x.HasBeenSent);
        }
    }
}
