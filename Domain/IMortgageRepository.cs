using System.Threading.Tasks;

namespace Domain
{
    public interface IMortgageRepository
    {
        Task Save(Mortgage m);
        Task<Mortgage> ById(int id);
    }
}