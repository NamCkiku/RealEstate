using Dropbox.Api;
using Dropbox.Api.Files;
using RealEstate.Common.Helper.Dropbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.Service
{
    public class ImagesService
    {
        private const string DropboxImagesFolderName = "/images";
        private readonly DropboxClient dropboxClient;
        public ImagesService(DropboxClient dropboxClient)
        {
            this.dropboxClient = dropboxClient;
        }

        private async Task UploadImageForUser(string name, string extension, string username, byte[] image)
        {
            using (MemoryStream memStream = new MemoryStream(image))
            {
                await this.dropboxClient.Files.UploadAsync(
                    string.Format("{0}/{1}/{2}.{3}", DropboxImagesFolderName, username, name, extension),
                    WriteMode.Overwrite.Instance,
                    body: memStream);
            }
        }

        private async Task<IList<DropBoxImageModel>> DownloadImagesForUser(string username)
        {
            ListFolderResult fileList = await this.dropboxClient.Files.ListFolderAsync(string.Format("{0}/{1}", DropboxImagesFolderName, username));

            if (fileList.Entries.Count == 0)
            {
                throw new InvalidOperationException("Invalid user specified or user has no images.");
            }

            var result = new List<DropBoxImageModel>();
            foreach (var imageFile in fileList.Entries)
            {
                using (var response = await this.dropboxClient.Files.DownloadAsync(string.Format("{0}/{1}/{2}", DropboxImagesFolderName, username, imageFile.Name)))
                {
                    byte[] data = await response.GetContentAsByteArrayAsync();
                    var currentImage = new DropBoxImageModel()
                    {
                        Data = data,
                        ImageName = imageFile.Name,
                        UserName = username
                    };

                    result.Add(currentImage);
                }
            }

            return result;
        }


        //public async Task Delete(int id)
        //{
        //    var image = this.images
        //        .All()
        //        .FirstOrDefault(i => i.Id == id);

        //    if (image == null)
        //    {
        //        throw new ArgumentOutOfRangeException("Invalid image id.");
        //    }

        //    await this.dropboxClient.Files.DeleteAsync(string.Format("{0}/{1}/{2}.{3}", DropboxImagesFolderName, image.User.UserName, image.Name, image.Extension));

        //    this.images.Delete(image);
        //    this.images.SaveChanges();
        //}
    }
}
