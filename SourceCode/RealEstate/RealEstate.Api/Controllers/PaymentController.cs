using RealEstate.Api.Models.ViewModel;
using RealEstate.Api.NganLuongAPI;
using RealEstate.Common.Helper;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RealEstate.Api.Controllers
{
    /// <summary>
    /// Class dùng để thanh toán tiền,....
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  6/7/2017   created
    /// </Modified>
    /// <seealso cref="RealEstate.Api.Controllers.ApiControllerBase" />
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiControllerBase
    {
        private string merchantId = ConfigHelper.GetByKey("MerchantId");
        private string merchantPassword = ConfigHelper.GetByKey("MerchantPassword");
        private string merchantEmail = ConfigHelper.GetByKey("MerchantEmail");

        public PaymentController(IErrorService errorService) : base(errorService)
        {

        }
        /// <summary>
        /// Hàm API thanh toán bằng ngân hàng
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/payment/paymentbank</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  17/09/2017   created
        /// </Modified>
        [Route("paymentbank")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage PaymentBank(HttpRequestMessage request, PaymentViewModel paymentviewmodel)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                if (ModelState.IsValid)
                {
                    responeResult = CreateHttpResponse(request, () =>
                    {
                        HttpResponseMessage response = null;

                        RequestCheckoutInfo info = new RequestCheckoutInfo();
                        info.Merchant_id = merchantId;
                        info.Merchant_password = merchantPassword;
                        info.Receiver_email = merchantEmail;



                        info.cur_code = "vnd";
                        info.bank_code = paymentviewmodel.BankCode;

                        info.Order_code = paymentviewmodel.OrderCode.ToString();
                        info.Total_amount = paymentviewmodel.TotalAmount.ToString();
                        info.fee_shipping = "0";
                        info.Discount_amount = "0";
                        info.order_description = "Nạp tiền vào tài khoản tại Bizland.vn";
                        info.return_url = "";
                        info.cancel_url = "";

                        info.Buyer_fullname = paymentviewmodel.UserName;
                        info.Buyer_email = paymentviewmodel.Email;
                        info.Buyer_mobile = paymentviewmodel.PhoneNumber.ToString();

                        APICheckoutV3 objNLChecout = new APICheckoutV3();
                        ResponseInfo result = objNLChecout.GetUrlCheckout(info, paymentviewmodel.PaymentMethod);
                        if (result.Error_code == "00")
                        {
                            response = request.CreateResponse(HttpStatusCode.OK, result);
                        }
                        else
                        {
                            response = request.CreateResponse(HttpStatusCode.BadRequest, result.Description);
                        }

                        return response;
                    });
                }
                else
                {
                    responeResult = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }

        /// <summary>
        /// Hàm API thanh toán thẻ điện thoại
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Link API:api/payment/paymentcash</returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  17/09/2017   created
        /// </Modified>
        [Route("paymentcash")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage PaymentCash(HttpRequestMessage request, PaymentCashViewModel paymentviewmodel)
        {
            HttpResponseMessage responeResult = new HttpResponseMessage();
            try
            {
                if (ModelState.IsValid)
                {
                    responeResult = CreateHttpResponse(request, () =>
                    {

                        HttpResponseMessage response = null;

                        RequestInfo info = new RequestInfo();
                        info.Merchant_id = merchantId;
                        //Email tài khoản nhận tiền khi nạp
                        info.Merchant_acount = merchantEmail;
                        info.Merchant_password = merchantPassword;

                        //Nhà mạng
                        info.CardType = paymentviewmodel.CardType;
                        info.Pincard = paymentviewmodel.Pincard;

                        info.Refcode = (new Random().Next(0, 10000)).ToString();
                        info.SerialCard = paymentviewmodel.SerialCard;

                        ResponseCashInfo resutl = NLCardLib.CardChage(info);
                        if (resutl.Errorcode.Equals("00"))
                        {
                            response = request.CreateResponse(HttpStatusCode.OK, resutl);
                        }
                        else
                        {
                            response = request.CreateResponse(HttpStatusCode.BadRequest, resutl);
                        }
                        return response;
                    });
                }
                else
                {
                    responeResult = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
            }
            return responeResult;
        }
    }
}
