namespace RealEstate.Repository.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}