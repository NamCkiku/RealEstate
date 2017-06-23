using AutoMapper;
using RealEstate.Administrator.Infrastructure.Extensions;
using RealEstate.Administrator.Models.ViewModel;
using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Administrator.Controllers
{
    public class RoomTypeController : BaseController
    {
        private readonly IRoomTypeService _roomTypeService;
        public RoomTypeController(IErrorService errorService, IRoomTypeService roomTypeService) : base(errorService)
        {
            this._roomTypeService = roomTypeService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadAllRoomTypePaging(SearchRoomTypeEntity filter)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                var data = _roomTypeService.GetAllRoomTypePaging(filter);
                var listRoomTypeVm = Mapper.Map<List<RoomTypeViewModel>>(data);
                jsonResult = Json(new { success = true, lstData = listRoomTypeVm }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                Common.Logs.LogCommon.WriteLogError(ex.Message);
                LogError(ex);
            }
            return jsonResult;
        }
        public JsonResult LoadAllRoomType()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                var data = _roomTypeService.GetAll().OrderByDescending(x => x.DisplayOrder);
                var listRoomTypeVm = Mapper.Map<List<RoomTypeViewModel>>(data);
                jsonResult = Json(new { success = true, lstData = listRoomTypeVm }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                Common.Logs.LogCommon.WriteLogError(ex.Message);
                LogError(ex);
            }
            return jsonResult;
        }
        public JsonResult InsertRoomType(RoomTypeViewModel roomtypevm)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                if (ModelState.IsValid)
                {
                    var roomType = new RoomType();
                    roomType.UpdateRoomType(roomtypevm);
                    roomType.CreatedDate = DateTime.Now;
                    roomType.CreatedBy = User.Identity.Name;
                    var result = _roomTypeService.Insert(roomType);
                    _roomTypeService.SaveChanges();
                    if (result != null)
                    {
                        var responseData = Mapper.Map<RoomType, RoomTypeViewModel>(result);
                        jsonResult = Json(new { success = true, objData = responseData }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                Common.Logs.LogCommon.WriteLogError(ex.Message);
                LogError(ex);
            }
            return jsonResult;
        }
        public JsonResult UpdateRoomType(RoomTypeViewModel roomtypevm)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                if (ModelState.IsValid)
                {
                    var roomType = _roomTypeService.GetById(roomtypevm.ID);
                    roomType.UpdateRoomType(roomtypevm);
                    roomType.UpdatedDate = DateTime.Now;
                    roomType.UpdatedBy = User.Identity.Name;
                    _roomTypeService.Update(roomType);
                    _roomTypeService.SaveChanges();
                    jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                Common.Logs.LogCommon.WriteLogError(ex.Message);
                LogError(ex);
            }
            return jsonResult;
        }
    }
}