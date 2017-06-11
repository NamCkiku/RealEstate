using RealEstate.Entities.ModelView;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Administrator.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;
        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            this._roomTypeService = roomTypeService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadAllRoomType(SearchEntity filter)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                var data = _roomTypeService.GetAll();
                jsonResult = Json(new { success = true, lstData = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                throw ex;
            }
            return jsonResult;
        }
    }
}