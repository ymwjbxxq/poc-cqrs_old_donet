using CommandAndQuery.Queries;
using System.Threading.Tasks;
using Storage.Read.Repos;

namespace QuerySide.Queries
{
    public class GetTotalMoneyQuery : IGetTotalMoneyQuery, IQueryHandler<int>
    {
        private int _pocketId;
        private readonly IPocketRepo _pocketRepo;

        public GetTotalMoneyQuery(IPocketRepo pocketRepo)
        {
            _pocketRepo = pocketRepo;
        }

        public async Task<int> Execute()
        {
            var pocket = await _pocketRepo.GetById(_pocketId);
            return pocket.Money;
        }

        public IQueryHandler<int> Init(int pocketId)
        {
            _pocketId = pocketId;
            return this;
        }
    }
}
