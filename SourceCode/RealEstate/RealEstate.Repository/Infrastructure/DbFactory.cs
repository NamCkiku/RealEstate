using RealEstate.Entities.Entites;

namespace RealEstate.Repository.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private RealEstateDbContext dbContext;

        public RealEstateDbContext Init()
        {
            return dbContext ?? (dbContext = new RealEstateDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}