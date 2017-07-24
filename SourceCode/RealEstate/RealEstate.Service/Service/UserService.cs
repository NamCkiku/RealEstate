using RealEstate.Entities.Entites;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System.Reflection;
using RealEstate.Entities.ModelView;

namespace RealEstate.Service.Service
{
    public class UserService : BaseService<AppUser>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IRepository<AppUser> repository, IUserRepository userRepository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._userRepository = userRepository;
        }

        public async Task<IEnumerable<AppUserEntity>> GetAllUserIsBirthDay()
        {
            IEnumerable<AppUserEntity> lstuser = new List<AppUserEntity>();
            try
            {
                lstuser = await _userRepository.GetAllUserIsBirthDay();
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
            }
            return lstuser;
        }
    }
}
