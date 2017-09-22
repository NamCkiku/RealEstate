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

        public IEnumerable<AuditlogEntity> GetAllHistoryLogin(string userID, int page, int pageSize, out int totalRow)
        {
            List<AuditlogEntity> lstHistory = new List<AuditlogEntity>();
            try
            {
                totalRow = 0;
                lstHistory = _userRepository.GetAllHistoryLogin(userID, page, pageSize, out totalRow).ToList();
            }
            catch (Exception ex)
            {
                string FunctionName = MethodInfo.GetCurrentMethod().Name;
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
                totalRow = 0;
            }
            return lstHistory;
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

        public AppUserEntity GetInfomationUserById(string userId)
        {
            AppUserEntity user = new AppUserEntity();
            try
            {
                user = _userRepository.GetInfomationUserById(userId);
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
