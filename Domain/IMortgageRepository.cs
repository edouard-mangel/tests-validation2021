namespace Domain
{
    public interface IMortgageRepository
    {
        void Save(Mortgage m);
        Mortgage ById(int id);
    }
}