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
    public class ProvinceService : BaseService<Province>, IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;
        public ProvinceService(IRepository<Province> repository, IProvinceRepository provinceRepository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._provinceRepository = provinceRepository;
        }

        public IEnumerable<Province> GetAllProvince()
        {
            List<Province> lstprovince = new List<Province>();
            try
            {
                lstprovince = _provinceRepository.GetAllProvince().ToList();
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
            }
            return lstprovince;
        }
    }
}
