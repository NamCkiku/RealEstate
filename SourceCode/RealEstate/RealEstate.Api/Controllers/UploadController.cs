using RealEstate.Api.Models.ViewModel;
using RealEstate.Common.Resource;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RealEstate.Common.Helper;
namespace RealEstate.Api.Controllers
{
    [RoutePrefix("api/upload")]
    //[Authorize]
    public class UploadController : ApiControllerBase
    {
        public UploadController(IErrorService errorService) : base(errorService)
        {
        }

        /// <summary>
        /// Hàm upload ảnh theo loại.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/20/2017   created
        [HttpPost]
        [Route("uploadimage")]
        public HttpResponseMessage SaveImage(string type)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            var modelMsg1 = new NotificationViewModel
                            {
                                MsgType = NotificationType.MSG_TYPE_FAIL,
                                Msg = MsgResource.Msg0042
                            };
                            dict.Add("error", modelMsg1.Msg);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {
                            var modelMsg2 = new NotificationViewModel
                            {
                                MsgType = NotificationType.MSG_TYPE_FAIL,
                                Msg = MsgResource.Msg0043
                            };
                            dict.Add("error", modelMsg2.Msg);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string directory = string.Empty;
                            if (type == "avatar")
                            {
                                directory = "/UploadedFiles/Avatars/";
                            }
                            else if (type == "room")
                            {
                                directory = "/UploadedFiles/Rooms/";
                            }
                            else if (type == "news")
                            {
                                directory = "/UploadedFiles/News/";
                            }
                            else
                            {
                                directory = "/UploadedFiles/";
                            }
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
                            }

                            string path = Path.Combine(HttpContext.Current.Server.MapPath(directory), postedFile.FileName);
                            //Userimage myfolder name where i want to save my image
                            //string pathLogo = Path.Combine(HttpContext.Current.Server.MapPath("/UploadedFiles/Rooms/logo.png"), postedFile.FileName);
                            //ImageHelper.Images(pathLogo, path, 50, 50, postedFile.FileName);
                            postedFile.SaveAs(path);
                            return Request.CreateResponse(HttpStatusCode.OK, Path.Combine(directory, postedFile.FileName));
                        }
                    }
                    var modelMsg3 = new NotificationViewModel
                    {
                        MsgType = NotificationType.MSG_TYPE_SUCCESS,
                        Msg = MsgResource.Msg0044
                    };
                    return Request.CreateErrorResponse(HttpStatusCode.Created, modelMsg3.Msg); ;
                }

                var modelMsg4 = new NotificationViewModel
                {
                    MsgType = NotificationType.MSG_TYPE_FAIL,
                    Msg = MsgResource.Msg0045
                };
                dict.Add("error", modelMsg4.Msg);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);

            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); ;

            }
        }


        /// <summary>
        /// Hàm upload ảnh.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/20/2017   created
        [Route("uploadsingeimage")]
        [HttpPost]
        public async Task UploadSingleFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, new NotSupportedException("Media type not supported"));
            }
            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Rooms");
            var dataFolder = HttpContext.Current.Server.MapPath("~/UploadedFiles/Rooms");
            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            string fileName = string.Empty;
            foreach (MultipartFileData fileData in provider.FileData)
            {
                fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = result.FormData["model"] + "_"
                              + Path.GetFileName(fileName);
                }
                if (File.Exists(Path.Combine(dataFolder, fileName)))
                    File.Delete(Path.Combine(dataFolder, fileName));
                File.Move(fileData.LocalFileName, Path.Combine(dataFolder, fileName));
                File.Delete(fileData.LocalFileName);
            }

            Request.CreateResponse(HttpStatusCode.OK, new { fileName = fileName });
        }
    }
}
