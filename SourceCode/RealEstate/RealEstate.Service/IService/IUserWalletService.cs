using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Service.BaseService;

namespace RealEstate.Service.IService
{
    public interface IUserWalletService : IBaseService<UserWallet>
    {
        UserWalletEntity GetWalletByUserID(string userId);
    }
}
