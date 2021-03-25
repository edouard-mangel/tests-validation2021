using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporary
{
    public class InMemoryMortgageRepository : IMortgageRepository
    {
        private readonly List<Mortgage> mortgages ;

        public InMemoryMortgageRepository()
        {
            this.mortgages = new List<Mortgage>();
        }

        public async Task<Mortgage> ById(int id)
        {
            return mortgages.FirstOrDefault(m => m.Id == id);
        }

        public async Task Save(Mortgage mortGage)
        {
            Random random = new Random(); 
            int newId = random.Next();
            
            mortGage.Id = newId;
            // todo connect Database. 
            mortgages.Add(mortGage);
        }
    }
}
