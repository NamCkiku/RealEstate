using AutoMapper;
using RealEstate.Administrator.Models.ViewModel;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Administrator.Controllers
{
    public class ManagementController : BaseController
    {
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;
        public ManagementController(IErrorService errorService, ICountryService countryService,
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

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.ServerAndClient, Duration = int.MaxValue)]
        public JsonResult LoadAllProvince()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                var data = _provinceService.GetAllProvince().OrderByDescending(x => x.SortOrder);
                var listProvinceVm = Mapper.Map<List<ProvinceViewModel>>(data);
                jsonResult = Json(new { success = true, lstData = listProvinceVm }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                Common.Logs.LogCommon.WriteLogError(ex.Message);
                LogError(ex);
            }
            return jsonResult;
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.ServerAndClient, Duration = int.MaxValue)]
        public JsonResult LoadAllDistrict()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                var data = _districtService.GetAllDistrict().OrderByDescending(x => x.SortOrder);
                var listDistrictVm = Mapper.Map<List<DistrictViewModel>>(data);
                jsonResult = Json(new { success = true, lstData = listDistrictVm }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                Common.Logs.LogCommon.WriteLogError(ex.Message);
                LogError(ex);
            }
            return jsonResult;
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.ServerAndClient, Duration = int.MaxValue)]
        public JsonResult LoadAllWard()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                var data = _wardService.GetAllWard().OrderByDescending(x => x.SortOrder);
                var listWardVm = Mapper.Map<List<WardViewModel>>(data);
                jsonResult = Json(new { success = true, lstData = listWardVm }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                Common.Logs.LogCommon.WriteLogError(ex.Message);
                LogError(ex);
            }
            return jsonResult;
        }
        #endregion
    }
}