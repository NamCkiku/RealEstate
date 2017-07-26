using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using RealEstate.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.RepositoryTest
{
    [TestClass]
    public class CountryRepositoryTest
    {
        IDbFactory dbFactory;
        ICountryRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new CountryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void Country_Repository_GetAllCountry()
        {
            var list = objRepository.GetAllCountry().ToList();

            Assert.IsNotNull(list);
            Assert.AreNotEqual(2, list);           
        }

        [TestMethod]
        public void Country_Repository_GetAllCountryDapper()
        {
            var list = objRepository.GetAllCountryDapper().ToList();

            Assert.IsNotNull(list);
            Assert.AreNotEqual(2, list);
        }
    }
}
