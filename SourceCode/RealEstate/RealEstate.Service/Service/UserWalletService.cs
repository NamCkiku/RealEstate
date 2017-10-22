using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Entities.ModelView;
using System.Reflection;
using RealEstate.Repository.IRepositories;

namespace RealEstate.Service.Service
{
    public class UserWalletService : BaseService<UserWallet>, IUserWalletService
    {
        private readonly IUserWalletRepository _userWalletRepository;
        public UserWalletService(IRepository<UserWallet> repository, IUserWalletRepository userWalletRepository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._userWalletRepository = userWalletRepository;
        }

        public UserWalletEntity GetWalletByUserID(string userId)
        {
            UserWalletEntity user = new UserWalletEntity();
            try
            {
                user = _userWalletRepository.GetWalletByUserID(userId);
            }
            catch (Exception ex)
            {
                string FunctionName = MethodInfo.GetCurrentMethod().Name;
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
            }
            return user;
        }
    }
}
