using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.Service
{
    public class UserTransactionHistoryService : BaseService<UserTransactionHistory>, IUserTransactionHistoryService
    {
        private readonly IUserTransactionHistoryRepository _userTransactionHistoryRepository;
        public UserTransactionHistoryService(IRepository<UserTransactionHistory> repository, IUserTransactionHistoryRepository userTransactionHistoryRepository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._userTransactionHistoryRepository = userTransactionHistoryRepository;
        }

        public IEnumerable<UserTransactionHistoryEntity> GetTransactionHistoryByUserId(string userId, int page, int pageSize, out int totalRow)
        {
            List<UserTransactionHistoryEntity> lstHistory = new List<UserTransactionHistoryEntity>();
            try
            {
                totalRow = 0;
                lstHistory = _userTransactionHistoryRepository.GetTransactionHistoryByUserId(userId, page, pageSize, out totalRow).ToList();
            }
            catch (Exception ex)
            {
                string FunctionName = MethodInfo.GetCurrentMethod().Name;
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
                totalRow = 0;
            }
            return lstHistory;
        }
    }
}
