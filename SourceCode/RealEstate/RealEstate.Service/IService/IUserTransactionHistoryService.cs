using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Service.BaseService;
using System.Collections.Generic;

namespace RealEstate.Service.IService
{
    public interface IUserTransactionHistoryService : IBaseService<UserTransactionHistory>
    {
        IEnumerable<UserTransactionHistoryEntity> GetTransactionHistoryByUserId(string userId, int page, int pageSize, out int totalRow);
    }
}
