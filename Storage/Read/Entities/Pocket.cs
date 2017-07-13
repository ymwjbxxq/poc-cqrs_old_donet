namespace Storage.Read.Entities
{
    public class Pocket
    {
        public int Id { get; set; }

        public int Money { get; set; }

        public void AddMoney(int money)
        {
            Money = money;
        }
    }
}
