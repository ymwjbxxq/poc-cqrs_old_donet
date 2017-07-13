using Storage.Events;

namespace Storage.Write.Entities
{
    public class Pocket : AggregateRoot
    {
        public int Id { get; }

        public int Money { get; private set; }

        public Pocket(int id)
        {
            Id = id;
        }       

        public void AddMoney(int money)
        {
            Money = money;
            ApplyChange(new MoneyAddedEvent(Id, money));
        }

        public void RemoveSentEvents()
        {
            RemoveChange();
        }
    }
}
