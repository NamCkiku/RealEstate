using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult PVProfileHistory()
        {
            return PartialView();
        }
        public PartialViewResult PVProfileUpdateUser()
        {
            return PartialView();
        }
        public PartialViewResult PVProfileChangePassword()
        {
            return PartialView();
        }
        public PartialViewResult PVProfileTransactionHistory()
        {
            return PartialView();
        }      
    }
}