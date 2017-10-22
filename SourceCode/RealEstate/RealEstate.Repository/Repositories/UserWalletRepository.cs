using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;

namespace RealEstate.Repository.Repositories
{
    public class UserWalletRepository : RepositoryBase<UserWallet>, IUserWalletRepository
    {
        public UserWalletRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
