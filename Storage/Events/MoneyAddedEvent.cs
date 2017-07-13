namespace Storage.Events
{
    public class MoneyAddedEvent : IEvent
    {
        public int Money { get; }
        public int AggregateId { get; }
        public bool HasBeenSent { get; set; }

        public MoneyAddedEvent(int pocketId, int money)
        {
            Money = money;
            AggregateId = pocketId;
            HasBeenSent = false;
        }
    }
}
