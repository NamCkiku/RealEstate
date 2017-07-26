using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RealEstate.Administrator.Controllers;
using RealEstate.Repository.Infrastructure;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.ControllerTest
{
    [TestClass]
    public class RoomControllerTest
    {
        private Mock<IRoomService> _mockRoomService;
        private Mock<IErrorService> _mockErrorService;
        private RoomController _roomController;

        [TestInitialize]
        public void Initialize()
        {
            _mockRoomService = new Mock<IRoomService>();
            _mockErrorService = new Mock<IErrorService>();
            _roomController = new RoomController(_mockErrorService.Object, _mockRoomService.Object);
        }

        //[TestMethod]
        //public void Room_Controller_InsertRoom()
        //{
        //    _mockRoomService.Setup(m=>m.InsertRoom)
        //}
    }
}
