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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

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

                        int MaxContentLength = 1024 * 1024 * 2; //Size = 1 MB

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
                            string rename = ReNameImage(type, postedFile.FileName);
                            string path = Path.Combine(HttpContext.Current.Server.MapPath(directory), rename);
                            var statusUp = AddLogoToImage(postedFile);
                            statusUp.Save(path);
                            return Request.CreateResponse(HttpStatusCode.OK, Path.Combine(directory, rename));
                            //postedFile.SaveAs(path);
                            //return Request.CreateResponse(HttpStatusCode.OK, Path.Combine(directory, postedFile.FileName));
                            //var pathResult = Path.Combine(directory, postedFile.FileName);
                            //return Request.CreateResponse(HttpStatusCode.OK, pathResult);
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
                AddLogoToImageMulti(dataFolder, fileName);
            }

            Request.CreateResponse(HttpStatusCode.OK, new { fileName = fileName });
        }

        /// <summary>
        /// Gắn logo vào hình ảnh
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="directory">Đường dẫn thư mục</param>
        /// <param name="fileName">Tên file</param>
        /// ThaoNV 9/9/2017 created
        public Image AddLogoToImage(HttpPostedFile file)
        {
            try
            {
                int width, height;
                // Đường dẫn file Logo cần chèn
                string logo = HttpContext.Current.Server.MapPath("~/Content/logo/Logo_bizland.vn-01.png");
                string logoCenter = HttpContext.Current.Server.MapPath("~/Content/logo/Logo_bizland.vn-02.png");
                Image img = Image.FromFile(logo);
                Image imgCenter = Image.FromFile(logoCenter);
                // Tạo đối tượng Bitmap truyền vào đường dẫn File ảnh
                System.IO.Stream fileStream = file.InputStream;
                fileStream.Position = 0;
                byte[] fileContents = new byte[file.ContentLength];
                fileStream.Read(fileContents, 0, file.ContentLength);
                System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(fileContents));
                width = (int)(image.Width * 0.125);
                height = (int)((img.Height * width) / img.Width);
                // Tạo đối tượng Graphic từ Bitmap
                Graphics myGraphics = Graphics.FromImage(image);
                // Vẽ lại hình ảnh, chèn nội dung mới vào.
                Bitmap myBitmapLogo = new Bitmap(width, height);
                Bitmap myBitmapLogoCenter = new Bitmap(width, height);
             //   myBitmapLogo.RotateFlip(RotateFlipType.Rotate270FlipNone);
                Graphics g1 = Graphics.FromImage((Image)myBitmapLogo);
                g1.InterpolationMode = InterpolationMode.High;
                g1.DrawImage(img, 0, 0, width, height);
                Graphics g2 = Graphics.FromImage((Image)myBitmapLogoCenter);
                g2.InterpolationMode = InterpolationMode.High;
                g2.DrawImage(imgCenter, 0, 0, width, height);

                myGraphics.DrawImage(myBitmapLogo, image.Width - 20 - myBitmapLogo.Width, image.Height - 20 - myBitmapLogo.Height, myBitmapLogo.Width, myBitmapLogo.Height);
                myGraphics.DrawImage(myBitmapLogoCenter, ((image.Width - myBitmapLogoCenter.Width) / 2), ((image.Height - myBitmapLogoCenter.Height) / 2), myBitmapLogoCenter.Width, myBitmapLogoCenter.Height);
                // Xuất hình ảnh mới
                //   HttpContext.Current.Response.ContentType = "image/jpeg";
                //  image.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);

                // Dùng code này nếu lưu ảnh
                // image.Save(HttpContext.Current.Server.MapPath(directory + "/" + fileName));
                return image;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string AddLogoToImageMulti(string dataFolder, string fileName)
        {
            try
            {
             
                // Đường dẫn file ảnh. 
                string imageFile = Path.Combine(dataFolder, fileName);
                // Đường dẫn file Logo cần chèn
                string logo = HttpContext.Current.Server.MapPath("~/Content/logo/Logo_bizland.vn-01.png");
                Image img = Image.FromFile(logo);
                // Tạo đối tượng Bitmap truyền vào đường dẫn File ảnh
                Bitmap myBitmap = new Bitmap(imageFile);
                // Tạo đối tượng Graphic từ Bitmap
                Graphics myGraphics = Graphics.FromImage(myBitmap);
                // Vẽ lại hình ảnh, chèn nội dung mới vào.
                Bitmap myBitmapLogo = new Bitmap(logo);
                myBitmapLogo.RotateFlip(RotateFlipType.Rotate270FlipNone);
                Graphics g1 = Graphics.FromImage((Image)myBitmapLogo);
                g1.InterpolationMode = InterpolationMode.High;
                g1.DrawImage(img, 0, 0, (int)(myBitmap.Width * 0.6) , (int)(myBitmap.Height * 0.6));
                myGraphics.DrawImage(myBitmapLogo, myBitmap.Width - 20 - myBitmapLogo.Width, myBitmap.Height - 20 - myBitmapLogo.Height, myBitmapLogo.Width, myBitmapLogo.Height);

                // Xuất hình ảnh mới
                // HttpContext.Current.Response.ContentType = "image/jpeg";
                // myBitmap.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);

                // Dùng code này nếu lưu ảnh
                File.Delete(imageFile);
                myBitmap.Save(HttpContext.Current.Server.MapPath(dataFolder + "/" + fileName));
                return "";
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public string ReNameImage(string type, string name)
        {
            string[] format = name.Split('.');
            var g = Guid.NewGuid();
            return type + "-" + g + "-" + DateTime.Now.ToString("yyyyMMddmmhhss") + "." + format[1].ToString();
        }
    }
}
