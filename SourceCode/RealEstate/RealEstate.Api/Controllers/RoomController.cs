using AutoMapper;
using RealEstate.Api.Infrastructure.Extensions;
using RealEstate.Api.Models.ViewModel;
using RealEstate.Common.Core;
using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebApi.OutputCache.V2;

namespace RealEstate.Api.Controllers
{
    /// <summary>
    /// Class trả về API cho Phòng
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  6/19/2017   created
    /// </Modified>
    /// <seealso cref="RealEstate.Api.Controllers.ApiControllerBase" />
    /// 
    [RoutePrefix("api/room")]
    public class RoomController : ApiControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMoreInfomationService _moreInfomationService;
        public RoomController(IErrorService errorService, IRoomService roomService, IMoreInfomationService moreInfomationService) : base(errorService)
        {
            this._roomService = roomService;
            this._moreInfomationService = moreInfomationService;
        }
        #region Phòng

        /// <summary>
        /// Hàm trả về danh sách phòng không có vip.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/19/2017   created
        /// </Modified>
        [Route("getallroom")]
        [CacheOutput(ClientTimeSpan = 100)]
        [HttpGet]
        public HttpResponseMessage GetAllRoom(HttpRequestMessage request, int top = 10)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var listRoom = _roomService.GetAllListRoom(top).OrderByDescending(x => x.DisplayOrder).ToList();

                    var listRoomVm = Mapper.Map<List<RoomViewModel>>(listRoom);

                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listRoomVm);

                    return response;
                });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }
        /// <summary>
        /// Lấy ra tất cả danh sách phòng của User.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/19/2017   created
        /// </Modified>
        [Route("getallroombyuser")]
        [CacheOutput(ClientTimeSpan = 100)]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAllRoomByUser(HttpRequestMessage request, string userID, int page, int pageSize)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    int totalRow = 0;
                    var listRoom = _roomService.GetAllListRoomByUser(userID, page, pageSize, out totalRow).OrderByDescending(x => x.DisplayOrder).ToList();

                    var listRoomVm = Mapper.Map<List<RoomViewModel>>(listRoom);
                    var paginationSet = new PaginationSet<RoomViewModel>()
                    {
                        Items = listRoomVm,
                        Page = page,
                        TotalCount = totalRow,
                        TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                    };
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                    return response;
                });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }

        /// <summary>
        /// Hàm trả về danh sách phòng có vip.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/19/2017   created
        /// </Modified>
        [Route("getallroomvip")]
        [CacheOutput(ClientTimeSpan = 100)]
        [HttpGet]
        public HttpResponseMessage GetAllRoomVip(HttpRequestMessage request, int vipID, int top = 10)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var listRoom = _roomService.GetAllListRoomVip(top, vipID).OrderByDescending(x => x.DisplayOrder).ToList();

                    var listRoomVm = Mapper.Map<List<RoomViewModel>>(listRoom);

                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listRoomVm);

                    return response;
                });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }

        /// <summary>
        /// Hàm trả về danh sách phòng có vip.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/19/2017   created
        /// </Modified>
        [Route("getallroomfullsearch")]
        [CacheOutput(ClientTimeSpan = 100)]
        [HttpGet]
        public HttpResponseMessage GetAllRoomFullSearch(HttpRequestMessage request, [FromUri] SearchRoomEntity filter, int page, int pageSize, string sort)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    int totalRow = 0;
                    var listRoom = _roomService.GetAllListRoomFullSearchStoreProc(filter, page, pageSize, out totalRow, sort).OrderByDescending(x => x.DisplayOrder).ToList();

                    var listRoomVm = Mapper.Map<List<RoomViewModel>>(listRoom);

                    var paginationSet = new PaginationSet<RoomViewModel>()
                    {
                        Items = listRoomVm,
                        Page = page,
                        TotalCount = totalRow,
                        TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                    };
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                    return response;
                });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }


        /// <summary>
        /// Hàm thêm thông tin phòng.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/20/2017   created
        /// </Modified>
        [Route("insertroom")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage InsertRoom(HttpRequestMessage request, RoomViewModel roomViewModel)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                 {
                     HttpResponseMessage response = null;
                     if (!ModelState.IsValid)
                     {
                         request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                     }
                     else
                     {
                         if (roomViewModel != null)
                         {
                             var newRoom = new Room();
                             newRoom.UpdateRoom(roomViewModel);
                             newRoom.CreatedBy = User.Identity.Name;
                             if (roomViewModel.MoreInfomations != null)
                             {
                                 var moreInfomation = new MoreInfomation();
                                 moreInfomation.BedroomNumber = roomViewModel.MoreInfomations.BedroomNumber;
                                 moreInfomation.Compass = roomViewModel.MoreInfomations.Compass;
                                 moreInfomation.Convenient = roomViewModel.MoreInfomations.Convenient;
                                 moreInfomation.ElectricPrice = roomViewModel.MoreInfomations.ElectricPrice;
                                 moreInfomation.WaterPrice = roomViewModel.MoreInfomations.WaterPrice;
                                 moreInfomation.ToiletNumber = roomViewModel.MoreInfomations.ToiletNumber;
                                 moreInfomation.FloorNumber = roomViewModel.MoreInfomations.FloorNumber;
                                 var moreIfResult = _moreInfomationService.Insert(moreInfomation);
                                 _moreInfomationService.SaveChanges();
                                 newRoom.MoreInfomationID = moreIfResult.MoreInfomationID;
                             }
                             var room = _roomService.InsertRoom(newRoom);
                             _roomService.SaveChanges();
                             var responseData = Mapper.Map<Room, RoomViewModel>(room);
                             response = request.CreateResponse(HttpStatusCode.Created, responseData);
                         }
                         else
                         {
                             request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                         }
                     }
                     return response;
                 });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }


        /// <summary>
        /// Hàm sửa thông tin phòng.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/20/2017   created
        /// </Modified>
        [Route("updateroom")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage UpdateRoom(HttpRequestMessage request, RoomViewModel roomViewModel)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                 {
                     HttpResponseMessage response = null;
                     if (!ModelState.IsValid)
                     {
                         request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                     }
                     else
                     {
                         if (roomViewModel != null)
                         {
                             var dbRoom = _roomService.GetById(roomViewModel.ID);
                             dbRoom.UpdateRoom(roomViewModel);
                             dbRoom.UpdatedBy = User.Identity.Name;
                             dbRoom.UpdatedDate = DateTime.Now;
                             if (dbRoom.MoreInfomationID != null)
                             {
                                 var moreInfomation = _moreInfomationService.GetById(dbRoom.MoreInfomationID.GetValueOrDefault());
                                 moreInfomation.BedroomNumber = roomViewModel.MoreInfomations.BedroomNumber;
                                 moreInfomation.Compass = roomViewModel.MoreInfomations.Compass;
                                 moreInfomation.Convenient = roomViewModel.MoreInfomations.Convenient;
                                 moreInfomation.ElectricPrice = roomViewModel.MoreInfomations.ElectricPrice;
                                 moreInfomation.WaterPrice = roomViewModel.MoreInfomations.WaterPrice;
                                 moreInfomation.ToiletNumber = roomViewModel.MoreInfomations.ToiletNumber;
                                 moreInfomation.FloorNumber = roomViewModel.MoreInfomations.FloorNumber;
                                 _moreInfomationService.Update(moreInfomation);
                                 _moreInfomationService.SaveChanges();
                             }
                             _roomService.UpdateRoom(dbRoom);
                             _roomService.SaveChanges();
                             var responseData = Mapper.Map<Room, RoomViewModel>(dbRoom);
                             response = request.CreateResponse(HttpStatusCode.Created, responseData);
                         }
                         else
                         {
                             request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                         }
                     }
                     return response;
                 });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }

        /// <summary>
        /// Hàm xóa thông tin phòng.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/20/2017   created
        [Route("deleteroom")]
        [HttpDelete]
        [Authorize]
        public HttpResponseMessage DeleteRoom(HttpRequestMessage request, int id)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                 {
                     HttpResponseMessage response = null;
                     if (!ModelState.IsValid)
                     {
                         response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                     }
                     else
                     {
                         var oldroom = _roomService.Delete(id);
                         if (oldroom != null && oldroom.MoreInfomationID != null)
                         {
                             _moreInfomationService.Delete(oldroom.MoreInfomationID.GetValueOrDefault());
                             _moreInfomationService.SaveChanges();
                         }
                         _roomService.SaveChanges();

                         var responseData = Mapper.Map<Room, RoomViewModel>(oldroom);
                         response = request.CreateResponse(HttpStatusCode.Created, responseData);
                     }

                     return response;
                 });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }

        /// <summary>
        /// Hàm xóa nhiều thông tin phòng.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/20/2017   created
        [Route("deletemultiroom")]
        [HttpDelete]
        [Authorize]
        public HttpResponseMessage DeleteMultiRoom(HttpRequestMessage request, string checkedrooms)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                 {
                     HttpResponseMessage response = null;
                     if (!ModelState.IsValid)
                     {
                         response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                     }
                     else
                     {
                         var listRoom = new JavaScriptSerializer().Deserialize<List<int>>(checkedrooms);
                         foreach (var item in listRoom)
                         {
                             var result = _roomService.Delete(item);
                             if (result != null && result.MoreInfomationID != null)
                             {
                                 _moreInfomationService.Delete(result.MoreInfomationID.GetValueOrDefault());
                                 _moreInfomationService.SaveChanges();
                             }
                         }
                         _roomService.SaveChanges();

                         response = request.CreateResponse(HttpStatusCode.OK, listRoom.Count);
                     }

                     return response;
                 });
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }

        #endregion

    }
}
