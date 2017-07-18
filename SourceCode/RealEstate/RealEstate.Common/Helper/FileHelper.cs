using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Helper
{
    /// <summary>
    /// All references to File IO should go this class
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  19/07/2017   created
    /// </Modified>
    public class FileHelper
    {
        /// <summary>
        /// Kiểm tra thư mục có tồn tại khôngs s
        /// </summary>
        /// <param name="DricroryPath">đường dẫn lưu file</param>
        /// <returns>true : tồn tại; false không tồn tại</returns>
        public static bool IsExsitDirectory(string DirectoryPath)
        {
            try
            {

                return new DirectoryInfo(DirectoryPath).Exists;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Kiểm tra một file có tồn tại không
        /// </summary>
        /// <param name="FilePath"> đường dẫn đầy đủ tới file</param>
        /// <returns>true : file đã tồn tại ; false : không tồn tại</returns>
        public static bool IsExsitFile(string FilePath)
        {
            try
            {
                FileInfo objFileInfo = new FileInfo(FilePath);

                return objFileInfo.Exists;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Copy một file tới một nơi khác
        /// </summary>
        /// <param name="SourceFile">file nguồn </param>
        /// <param name="DestinationFile">file đích</param>
        /// <returns>true : copy thành công ; false : lỗi copy</returns>
        public static bool CopyFileTo(string SourceFile, string DestinationFile)
        {
            try
            {
                FileInfo objFileInfo = new FileInfo(SourceFile);
                objFileInfo.CopyTo(DestinationFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa một file
        /// </summary>
        /// <param name="Filename"></param>
        /// <returns>true : xóa thành công ; false : lỗi xóa file</returns>
        public static bool DeleteFile(string Filename)
        {
            try
            {
                FileInfo objFileInfo = new FileInfo(Filename);
                objFileInfo.Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get tên của file trong đường dẫn tới file
        /// </summary>
        /// <param name="FilePath">full paths và file name</param>
        /// <returns>filename + Extension</returns>
        public static string GetFileName(string FilePath)
        {
            return Path.GetFileName(FilePath);
        }

        /// <summary>
        /// laays  phần mở rộng của file
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns>trả về phần mở rộng của file</returns>
        public static string GetExtensionFile(string FilePath)
        {
            return Path.GetExtension(FilePath);
        }

        /// <summary>
        /// Tao thu muc tu duong dan
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateDirectory(string path)
        {
            bool ret = false;
            try
            {
                DirectoryInfo dirInfo = null;

                if (!Directory.Exists(path))
                {
                    dirInfo = new DirectoryInfo(path);
                    dirInfo.Create();
                }
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
    }
}
