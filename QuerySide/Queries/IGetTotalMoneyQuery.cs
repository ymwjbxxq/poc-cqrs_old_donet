using CommandAndQuery.Queries;

namespace QuerySide.Queries
{
    public interface IGetTotalMoneyQuery
    {
        IQueryHandler<int> Init(int pocketId);
    }
}