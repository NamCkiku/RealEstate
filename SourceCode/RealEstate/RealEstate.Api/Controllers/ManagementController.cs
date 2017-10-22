using AutoMapper;
using RealEstate.Api.Models.ViewModel;
using RealEstate.Common.CachingProvider;
using RealEstate.Common.Core;
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
    // [Authorize]
    public class ManagementController : ApiControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;
        private readonly IUserService _userService;
        private readonly IUserTransactionHistoryService _userTransactionHistoryService;
        public ManagementController(IErrorService errorService,
            ICountryService countryService,
            IProvinceService provinceService,
            IDistrictService districtService,
            IUserService userService,
            IUserTransactionHistoryService userTransactionHistoryService,
        IWardService wardService) : base(errorService)
        {
            this._countryService = countryService;
            this._provinceService = provinceService;
            this._districtService = districtService;
            this._wardService = wardService;
            this._userService = userService;
            this._userTransactionHistoryService = userTransactionHistoryService;
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
                        var listProvince = _provinceService.GetAllProvince().ToList();
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
                        var listDistrict = _districtService.GetAllDistrict().ToList();
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
                        var listWard = _wardService.GetAllWard().ToList();
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


        /// <summary>
        /// Hàm API lấy ra danh sách các xã phường theo district id.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getallward</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        [Route("getallwardbydistrictid/{id:int}")]
        public HttpResponseMessage GetAllWardByDistricId(HttpRequestMessage request, int id)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            List<WardViewModel> lstWardVM = null;
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var listWard = _wardService.GetAllWardByDistrictId(id).ToList();
                    lstWardVM = Mapper.Map<List<WardViewModel>>(listWard);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, lstWardVM);

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

        #region user

        /// <summary>
        /// Hàm API lấy ra danh sách lịch sử đăng nhập của user
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getallhistorylogin</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  17/09/2017   created
        /// </Modified>
        [Route("getallhistorylogin")]
        public HttpResponseMessage GetAllHistoryLogin(HttpRequestMessage request, string userID, int page, int pageSize)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    int totalRow = 0;
                    var listHistory = _userService.GetAllHistoryLogin(userID, page, pageSize, out totalRow).ToList();

                    var listHistoryVm = Mapper.Map<List<AuditlogViewModel>>(listHistory);
                    var paginationSet = new PaginationSet<AuditlogViewModel>()
                    {
                        Items = listHistoryVm,
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
        /// Hàm API lấy ra thông tin của user theo id
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getallhistorylogin</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  17/09/2017   created
        /// </Modified>
        [Route("getuserbyid")]
        public HttpResponseMessage GetInfomationById(HttpRequestMessage request, string userID)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    var user = _userService.GetInfomationUserById(userID);
                    var userVm = Mapper.Map<AppUserViewModel>(user);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, userVm);

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
        /// Hàm API lấy ra danh sách lịch sử đăng nhập của user
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/management/getallhistorylogin</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  17/09/2017   created
        /// </Modified>
        [Route("gettransactionhistory")]
        public HttpResponseMessage GetTransactionHistory(HttpRequestMessage request, string userID, int page, int pageSize)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                responeResult = CreateHttpResponse(request, () =>
                {
                    int totalRow = 0;
                    var listHistory = _userTransactionHistoryService.GetTransactionHistoryByUserId(userID, page, pageSize, out totalRow).ToList();

                    var listHistoryVm = Mapper.Map<List<UserTransactionHistoryViewModel>>(listHistory);
                    var paginationSet = new PaginationSet<UserTransactionHistoryViewModel>()
                    {
                        Items = listHistoryVm,
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
