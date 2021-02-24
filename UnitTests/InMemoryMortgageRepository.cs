using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class InMemoryMortgageRepository : IMortgageRepository
    {
        List<Mortgage> mortgages { get; } = new List<Mortgage>();

        public Mortgage ById(int id)
        {
            return mortgages.FirstOrDefault(m => m.Id == id);
        }

        public void Save(Mortgage mortGage)
        {
            Random random = new Random(); 
            int newId = random.Next();
            
            mortGage.Id = newId;
            // todo connect Database. 
            mortgages.Add(mortGage);
        }

    }
}
