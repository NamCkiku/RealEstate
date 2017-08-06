using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult _FooterPatialView()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _HeaderPatialView()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _BasePatialView()
        {
            return PartialView();
        }
    }
}