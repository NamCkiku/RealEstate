using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Web.Controllers
{
    public class RoomDetailController : Controller
    {
        // GET: RoomDetail
        public ActionResult Index(int id)
        {
            ViewBag.RoomId = id;
            return View();
        }
    }
}