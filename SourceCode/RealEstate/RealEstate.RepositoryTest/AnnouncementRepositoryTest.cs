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
    public class AnnouncementRepositoryTest
    {
        IDbFactory dbFactory;
        IAnnouncementRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new AnnouncementRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }


    }
}
