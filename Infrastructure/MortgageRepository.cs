using Domain;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MortgageRepository : IMortgageRepository
    {
        public Task<Mortgage> ById(int id)
        {
            // todo connect Database. 
            throw new NotImplementedException();
        }

        public Task Save(Mortgage m)
        {
            // todo connect Database. 
            throw new NotImplementedException();
        }
    }
}
