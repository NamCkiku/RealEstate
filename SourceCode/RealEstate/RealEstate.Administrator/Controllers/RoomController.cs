using AutoMapper;
using RealEstate.Administrator.Models.ViewModel;
using RealEstate.Common.Core;
using RealEstate.Entities.ModelView;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Administrator.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;
        public RoomController(IErrorService errorService, IRoomService roomService) : base(errorService)
        {
            this._roomService = roomService;
        }
        public JsonResult LoadAllRoomPaging(SearchRoomEntity filter, int page, int pageSize)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                int totalRow = 0;
                var data = _roomService.GetAllListRoomPaging(filter, page, pageSize, out totalRow);
                var listRoomVm = Mapper.Map<List<RoomViewModel>>(data);
                var paginationSet = new PaginationSet<RoomViewModel>()
                {
                    Items = listRoomVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                jsonResult = Json(new { success = true, lstData = paginationSet }, JsonRequestBehavior.AllowGet);
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