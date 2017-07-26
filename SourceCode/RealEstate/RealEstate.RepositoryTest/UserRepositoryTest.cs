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
    public class UserRepositoryTest
    {
        IDbFactory dbFactory;
        IUserRepository objRepository;
        IUnitOfWork unitOfWork;
        
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new UserRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void User_Repository_GetAllUserIsBirthDay()
        {
            var list = objRepository.GetAllUserIsBirthDay();

            Assert.AreNotEqual(1, list);
            Assert.IsNotNull(list);
        }

      
    }
}
