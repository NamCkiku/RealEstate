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
    public class WardService : BaseService<Ward>, IWardService
    {
        private readonly IWardRepository _wardRepository;
        public WardService(IRepository<Ward> repository, IWardRepository wardRepository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._wardRepository = wardRepository;
        }

        public IEnumerable<WardEntity> GetAllWard()
        {
            List<WardEntity> lstward = new List<WardEntity>();
            try
            {
                lstward = _wardRepository.GetAllWard().ToList();
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
            }
            return lstward;
        }

        public IEnumerable<WardEntity> GetAllWardByDistrictId(int id)
        {
            List<WardEntity> lstward = new List<WardEntity>();
            try
            {
                lstward = _wardRepository.GetAllWardByDistrictId(id).ToList();
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
            }
            return lstward;
        }
    }
}
