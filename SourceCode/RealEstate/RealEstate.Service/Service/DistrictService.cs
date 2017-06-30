using RealEstate.Entities.Entites;
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
    public class DistrictService : BaseService<District>, IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        public DistrictService(IRepository<District> repository, IDistrictRepository districtRepository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._districtRepository = districtRepository;
        }

        public IEnumerable<District> GetAllDistrict()
        {
            List<District> lstdistrict = new List<District>();
            try
            {
                lstdistrict = _districtRepository.GetAllDistrict().ToList();
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
            }
            return lstdistrict;
        }
    }
}
