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
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;
        public ManagementController(IErrorService errorService,
            ICountryService countryService,
            IProvinceService provinceService,
            IDistrictService districtService,
            IWardService wardService) : base(errorService)
        {
            this._countryService = countryService;
            this._provinceService = provinceService;
            this._districtService = districtService;
            this._wardService = wardService;
        }
        #region Địa Chính Việt Nam
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
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                 {
                     var listCountry = _countryService.GetAllCountry().OrderByDescending(x => x.SortOrder).ToList();

                     var listCountryVm = Mapper.Map<List<CountryViewModel>>(listCountry);

                     HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCountryVm);

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
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var listProvince = _provinceService.GetAll().OrderByDescending(x => x.SortOrder).ToList();
                    var listProvinceVm = Mapper.Map<List<ProvinceViewModel>>(listProvince);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listProvinceVm);
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
        /// Hàm API lấy ra danh sách các huyện.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getalldistrict</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        [Route("getalldistrict")]
        [CacheOutput(ClientTimeSpan = 100)]
        public HttpResponseMessage GetAllDistrict(HttpRequestMessage request)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var listDistrict = _districtService.GetAll().OrderByDescending(x => x.SortOrder).ToList();

                    var listDistrictVm = Mapper.Map<List<DistrictViewModel>>(listDistrict);

                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDistrictVm);

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
        /// Hàm API lấy ra danh sách các xã phường.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getallward</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        [Route("getallward")]
        [CacheOutput(ClientTimeSpan = 100)]
        public HttpResponseMessage GetAllWard(HttpRequestMessage request)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var listWard = _wardService.GetAll().OrderByDescending(x => x.SortOrder).ToList();

                    var listWardVm = Mapper.Map<List<WardViewModel>>(listWard);

                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listWardVm);

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
