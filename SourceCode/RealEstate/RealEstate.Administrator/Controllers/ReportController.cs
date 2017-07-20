using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Administrator.Controllers
{
    public class ReportController : BaseController
    {
        public ReportController(IErrorService errorService) : base(errorService)
        {
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Designer()
        {
            return View();
        }
    }
}