namespace Storage.Events
{
    public interface IEvent
    {
        int AggregateId { get; }
        bool HasBeenSent { get; set; }
    }
}
