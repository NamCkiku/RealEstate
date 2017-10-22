using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;

namespace RealEstate.Service.Service
{
    public class WalletTransactionTypesService : BaseService<WalletTransactionTypes>, IWalletTransactionTypesService
    {
        public WalletTransactionTypesService(IRepository<WalletTransactionTypes> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
