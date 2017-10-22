using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;

namespace RealEstate.Repository.Repositories
{
    public class WalletTransactionTypesRepository : RepositoryBase<WalletTransactionTypes>, IWalletTransactionTypesRepository
    {
        public WalletTransactionTypesRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
