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
    public class DistrictRepositoryTest
    {
        IDbFactory dbFactory;
        IDistrictRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new DistrictRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void District_Repository_GetAllProvince()
        {
            var list = objRepository.GetAllDistrict().ToList();

            Assert.IsNotNull(list);
            Assert.AreNotEqual(3, list.Count());
        }
    }
}
