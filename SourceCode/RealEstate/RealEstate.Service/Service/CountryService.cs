using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.Service
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(IRepository<Country> repository, ICountryRepository countryRepository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._countryRepository = countryRepository;
        }

        public IEnumerable<Country> GetAllCountry()
        {
            return _countryRepository.GetAllCountry();
        }
    }
}
