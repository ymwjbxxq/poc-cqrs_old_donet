using System.Threading.Tasks;
using Storage.Write.Entities;

namespace Storage.Write.Repos
{
    public interface IPocketRepo
    {
        Task<Pocket> GetById(int id);
        Task Save(Pocket pocket);
    }
}