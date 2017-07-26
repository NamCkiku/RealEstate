using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstate.Entities.Entites;
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
    public class WardRepositoryTest
    {
        IDbFactory dbFactory;
        IWardRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new WardRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void Ward_Repository_GetAllWard()
        {
            var list = objRepository.GetAllWard().ToList();

            Assert.IsNotNull(list);
            Assert.AreNotEqual(2, list);
        }

        [TestMethod]
        public void Ward_Repository_GetAllWardByDistrictId()
        {
            Ward db = new Ward();
            db.DistrictID = 1;

            var list = objRepository.GetAllWardByDistrictId(db.DistrictID).ToList();

            Assert.IsNotNull(list);
            Assert.AreNotEqual(2, list);
        }
    }
}
