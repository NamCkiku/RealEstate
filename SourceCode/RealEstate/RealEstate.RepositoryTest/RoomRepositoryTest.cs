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
    public class RoomRepositoryTest
    {
        IDbFactory dbFactory;
        IRoomRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new RoomRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        //[TestMethod]
        //public void Room_Repository_GetAllRoom()
        //{
        //    var list = objRepository.GetAllRoom().ToList();

        //    Assert.IsNotNull(list);
        //    Assert.AreNotEqual(1, list.Count());
        //}
    }
}
