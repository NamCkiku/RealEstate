using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Administrator.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IErrorService errorService) : base(errorService)
        {
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RoomType()
        {
            return View();
        }
        public ActionResult Room()
        {
            return View();
        }
    }
}