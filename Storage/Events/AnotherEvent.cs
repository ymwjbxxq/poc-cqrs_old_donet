namespace Storage.Events
{
    public class AnotherEvent : IEvent
    {
        public int Money { get; }
        public int AggregateId { get; }
        public bool HasBeenSent { get; set; }

        public AnotherEvent(int pocketId, int money)
        {
            Money = money;
            AggregateId = pocketId;
            HasBeenSent = false;
        }
    }
}
