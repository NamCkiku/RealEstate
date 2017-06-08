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
    /// Class trả về dữ liệu như Tỉnh,huyện,xã,....
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  6/7/2017   created
    /// </Modified>
    /// <seealso cref="RealEstate.Api.Controllers.ApiControllerBase" />
    [RoutePrefix("api/management")]
    public class ManagementController : ApiControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        public ManagementController(ICountryService countryService, IProvinceService provinceService)
        {
            this._countryService = countryService;
            this._provinceService = provinceService;
        }

        /// <summary>
        /// Hàm API lấy ra danh sách các quốc gia trên thế giới.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getallcountry</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        [Route("getallcountry")]
        [CacheOutput(ClientTimeSpan = 100)]
        public HttpResponseMessage GetAllCountry(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listCountry = _countryService.GetAllCountry().OrderByDescending(x => x.SortOrder).ToList();

                var listCountryVm = Mapper.Map<List<CountryViewModel>>(listCountry);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCountryVm);

                return response;
            });
        }
        /// <summary>
        /// Hàm lấy ra danh sách các Tỉnh/Thành phố trên đất nước Việt Nam
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getallprovince</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        [Route("getallprovince")]
        [CacheOutput(ClientTimeSpan = 100)]
        public HttpResponseMessage GetAllProvince(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listProvince = _provinceService.GetAll().OrderByDescending(x => x.SortOrder).ToList();
                var listProvinceVm = Mapper.Map<List<ProvinceViewModel>>(listProvince);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listProvinceVm);
                return response;
            });
        }
    }
}
