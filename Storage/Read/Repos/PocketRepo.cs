using System.Linq;
using System.Threading.Tasks;
using Storage.Read.Entities;
using Storage.Read.FakeDb;

namespace Storage.Read.Repos
{
    // This simulate an ORM but it is very bad
    public class PocketRepo : IPocketRepo
    {
        public async Task<Pocket> GetById(int id)
        {
            return await Task.Run(() =>
            {
                var pocketFakeTable  = ReadFakeDb.Instance.Pockets.FirstOrDefault((x => x.Id == id));

                return pocketFakeTable != null
                    ? new Pocket
                            {
                                Id = pocketFakeTable.Id,
                                Money = pocketFakeTable.Money
                            }
                    : null;
            });
        }

        public async Task Save(Pocket pocket)
        {
            await Task.Run(() =>
            {
                var pocketFakeTable = ReadFakeDb.Instance.Pockets.FirstOrDefault(x => x.Id == pocket.Id);

                if(pocketFakeTable != null)
                {
                    var oldMoney = pocketFakeTable.Money;
                    ReadFakeDb.Instance.Pockets.Remove(pocketFakeTable);
                    //update on the write side
                    ReadFakeDb.Instance.Pockets.Add(new PocketFakeTable
                    {
                        Id = pocket.Id,
                        Money = pocket.Money + oldMoney
                    });
                }
            });
            
        }
    }
}
