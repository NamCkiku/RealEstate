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
    public class ProvinceRepositoryTest
    {
        IDbFactory dbFactory;
        IProvinceRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new ProvinceRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void Province_Repository_GetAllProvince()
        {
            var list = objRepository.GetAllProvince().ToList();

            Assert.AreNotEqual(3, list.Count);
        }
    }
}
