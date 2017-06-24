using AutoMapper;
using RealEstate.Api.Models.ViewModel;
using RealEstate.Common.CachingProvider;
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
            List<CountryViewModel> lstCountryCache = null;
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                 {
                     var temp = MemoryCacheHelper.GetValue(MemoryCacheKey.Provinces);
                     if (temp != null)
                     {
                         lstCountryCache = (List<CountryViewModel>)temp;
                     }
                     else
                     {
                         var listCountry = _countryService.GetAllCountry().OrderByDescending(x => x.SortOrder).ToList();
                         lstCountryCache = Mapper.Map<List<CountryViewModel>>(listCountry);
                         MemoryCacheHelper.Add(MemoryCacheKey.Countries, lstCountryCache, DateTimeOffset.MaxValue);
                     }

                     HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, lstCountryCache);

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
            List<ProvinceViewModel> lstProvinceCache = null;
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var temp = MemoryCacheHelper.GetValue(MemoryCacheKey.Provinces);
                    if (temp != null)
                    {
                        lstProvinceCache = (List<ProvinceViewModel>)temp;
                    }
                    else
                    {
                        var listProvince = _provinceService.GetAll().OrderByDescending(x => x.SortOrder).ToList();
                        lstProvinceCache = Mapper.Map<List<ProvinceViewModel>>(listProvince);
                        MemoryCacheHelper.Add(MemoryCacheKey.Provinces, lstProvinceCache, DateTimeOffset.MaxValue);
                    }
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, lstProvinceCache);
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
            List<DistrictViewModel> lstDistrictCache = null;
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var temp = MemoryCacheHelper.GetValue(MemoryCacheKey.Districts);
                    if (temp != null)
                    {
                        lstDistrictCache = (List<DistrictViewModel>)temp;
                    }
                    else
                    {
                        var listDistrict = _districtService.GetAll().OrderByDescending(x => x.SortOrder).ToList();
                        lstDistrictCache = Mapper.Map<List<DistrictViewModel>>(listDistrict);
                        MemoryCacheHelper.Add(MemoryCacheKey.Districts, lstDistrictCache, DateTimeOffset.MaxValue);
                    }
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, lstDistrictCache);

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
            List<WardViewModel> lstWardCache = null;
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var temp = MemoryCacheHelper.GetValue(MemoryCacheKey.Wards);
                    if (temp != null)
                    {
                        lstWardCache = (List<WardViewModel>)temp;
                    }
                    else
                    {
                        var listWard = _wardService.GetAll().OrderByDescending(x => x.SortOrder).ToList();
                        lstWardCache = Mapper.Map<List<WardViewModel>>(listWard);
                        MemoryCacheHelper.Add(MemoryCacheKey.Wards, lstWardCache, DateTimeOffset.MaxValue);
                    }
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, lstWardCache);

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
