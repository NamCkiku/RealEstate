using AutoMapper;
using RealEstate.Api.Models.ViewModel;
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
    /// Class trả về dữ liệu loại phòng....
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  6/18/2017   created
    /// </Modified>
    /// <seealso cref="RealEstate.Api.Controllers.ApiControllerBase" />
    [RoutePrefix("api/roomtype")]
    public class RoomTypeController : ApiControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;
        public RoomTypeController(IErrorService errorService, IRoomTypeService roomTypeService) : base(errorService)
        {
            this._roomTypeService = roomTypeService;
        }
        #region Loại Phòng
        /// <summary>
        /// Hàm API lấy ra danh sách các loại phòng
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getallroomtype</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/18/2017   created
        /// </Modified>
        [Route("getallroomtype")]
        [CacheOutput(ClientTimeSpan = 100)]
        public HttpResponseMessage GetAllRoomType(HttpRequestMessage request)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var listRoomType = _roomTypeService.GetAll().OrderByDescending(x => x.DisplayOrder).ToList();

                    var listRoomTypeVm = Mapper.Map<List<RoomTypeViewModel>>(listRoomType);

                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listRoomTypeVm);

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
