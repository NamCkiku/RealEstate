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
        public RoomController(IErrorService errorService, IRoomService roomService) : base(errorService)
        {
            this._roomService = roomService;
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
        public HttpResponseMessage GetAllRoomFullSearch(HttpRequestMessage request, SearchRoomEntity filter, int page, int pageSize, string sort)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    int totalRow = 0;
                    var listRoom = _roomService.GetAllListRoomFullSearch(filter, page, pageSize, out totalRow, sort).OrderByDescending(x => x.DisplayOrder).ToList();

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
                             if (roomViewModel.MoreInfomationID != null)
                             {

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
                             if (roomViewModel.MoreInfomationID != null)
                             {

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
        #endregion

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
                             _roomService.Delete(item);
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

    }
}
