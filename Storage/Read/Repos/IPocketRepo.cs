using System.Threading.Tasks;
using Storage.Read.Entities;

namespace Storage.Read.Repos
{
    public interface IPocketRepo
    {
        Task<Pocket> GetById(int id);
        Task Save(Pocket pocket);
    }
}