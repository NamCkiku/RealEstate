using AutoMapper;
using RealEstate.Api.Models.ViewModel;
using RealEstate.Common.Core;
using RealEstate.Entities.ModelView;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        #endregion
    }
}
