using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RealEstate.Common.Helper
{
    public static class ImageHelper
    {
        //Ghép 2 ảnh theo tọa độ
        public static void Images(string imagelogo, string imgTarget, float X, float Y, string id)
        {
            Image img = Image.FromFile(imagelogo);
            Image img2 = Image.FromFile(imgTarget);
            int width = img.Width;
            int height = img.Height;
            Bitmap img3 = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img3);
            g.Clear(Color.Transparent);
            g.DrawImage(img, new Point(0, 0));
            g.DrawImage(img2, new PointF(X, Y));
            g.Dispose();
            img.Dispose();
            img3.Save(HttpContext.Current.Server.MapPath("~/UploadedFiles/Rooms/" + id + "0.jpg"));
            img2.Dispose();
        }


        //Ghép chữ vào ảnh
        public static void AddWatermark(FileStream fs, string watermarkText, Stream outputStream)
        {
            Image img = Image.FromStream(fs);
            Font font = new Font("Verdana", 16, FontStyle.Bold, GraphicsUnit.Pixel);
            //Adds a transparent watermark with an 100 alpha value.
            Color color = Color.FromArgb(100, 0, 0, 0);
            //The position where to draw the watermark on the image
            Point pt = new Point(10, 30);
            SolidBrush sbrush = new SolidBrush(color);

            Graphics gr = null;
            try
            {
                gr = Graphics.FromImage(img);
            }
            catch
            {
                Image img1 = img;
                img = new Bitmap(img.Width, img.Height);
                gr = Graphics.FromImage(img);
                gr.DrawImage(img1, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                img1.Dispose();
            }

            gr.DrawString(watermarkText, font, sbrush, pt);
            gr.Dispose();

            img.Save(outputStream, ImageFormat.Jpeg);
        }


        //Ghép chữ vào ảnh
        public static void Text()
        {
            string firstText = "Hello";
            string secondText = "WorldWorld World World World World World World World World dđ";

            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 50f);

            string imageFilePath = @"G:\ASP.Net MVC\WindowsFormsApp1\img\img.jpg";
            Bitmap bitmap = (Bitmap)Image.FromFile(imageFilePath);//load the image file

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Arial", 20))
                {
                    graphics.DrawString(firstText, arialFont, Brushes.Blue, firstLocation);
                    graphics.DrawString(secondText, arialFont, Brushes.Red, secondLocation);
                }
            }
            Image i = (Image)bitmap;
            i.Save("sss.png");
        }


        //Chỉnh sửa width height của ảnh
        public static Image SetWitdhHeight(string filename, int width, int height)
        {
            Image img = Image.FromFile(filename);
            Bitmap new_image = new Bitmap(width, height);
            Graphics g1 = Graphics.FromImage((Image)new_image);
            g1.InterpolationMode = InterpolationMode.High;
            g1.DrawImage(img, 0, 0, width, height);
            return new_image;
        }

        //Dowload ảnh
        public static void DowloadImage(string filename, string id)
        {
            string filepath = filename;

            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(filepath, HttpContext.Current.Server.MapPath("~/Asset/img/" + id + ".jpg"));
                }
                catch
                {
                    client.Dispose();
                }


            }
        }
    }
}
