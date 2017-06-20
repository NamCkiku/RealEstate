using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Resource
{
    public class MsgResource
    {
        // Required
        public const string Msg0001 = "Email không được để trống.";
        public const string Msg0002 = "Email không đúng định dạng.";
        public const string Msg0015 = "Email đã tồn tại, nhập email khác.";
        public const string Msg0021 = "Số chứng minh thư đã tồn tại, hãy nhập lại.";

        // Authen
        public const string Msg0003 = "Tài khoản chưa kích hoạt.";
        public const string Msg0004 = "Tài khoản bị khóa.";
        public const string Msg0005 = "Nhập email không đúng.";
        public const string Msg0006 = "Mật khẩu không được để trống.";
        public const string Msg0007 = "Nhập mật khẩu không đúng.";
        public const string Msg0008 = "Hệ thống bận, vui lòng thử lại sau.";
        public const string Msg0024 = "Bạn không có quyền với thao tác này, vui lòng liên hệ quản trị hệ thống.";


        // Change password
        public const string Msg0009 = "Mật khẩu cũ không được để trống.";
        public const string Msg0010 = "Mật khẩu cũ không đúng.";
        public const string Msg0011 = "Mật khẩu mới không được để trống.";
        public const string Msg0012 = "Nhập lại mật khẩu mới.";
        public const string Msg0013 = "Mật khẩu mới không đúng.";
        public const string Msg0014 = "Đổi mật khẩu thàng công.";

        // Access screen
        public const string Msg0016 = "Bạn không có quyền vào trang này.";


        // Not found
        public const string Msg0017 = "Không tìm thấy dữ liệu.";
        public const string Msg0018 = "Bạn không có quyền thao tác này.";
        public const string Msg0019 = "Thêm mới thành công.";
        public const string Msg0020 = "Cập nhật thành công.";
        public const string Msg0029 = "Hủy thành công.";
        public const string Msg0030 = "Lưu thành công.";
        public const string Msg0031 = "Xóa thành công.";

        // News
        public const string Msg0022 = "Đã có bài viết cho danh mục này, không thể thêm mới.";
        public const string Msg0023 = "Đã có bài viết cho danh mục này, không thể cập nhật.";

        // Add orrder
        public const string Msg0025 = "Hệ thống chưa hỗ trợ gói dịch vụ: Giao tối -> Ngoại thành.";
        public const string Msg00251 = "“Gói cước này chỉ áp dụng lấy và giao nội thành.";
        // 
        public const string Msg0026 = "Quý khách đã nhập sai đường dẫn, vui lòng thử lại.";
        public const string Msg0027 = "Không tìm thấy đơn hàng, hoặc không thể thao tác.";
        public const string Msg0028 = "Nhân viên hệ thống đã cập nhật đơn hàng, vui lòng thử lại.";
        public const string Msg0039 = "Đơn hàng đã hủy, không thể thao tác.";
        public const string Msg0040 = "Mã vạch đã được sử dụng, hãy nhập mã khác.";
        public const string Msg0041 = "Cập nhật mã vạch thành công.";

        public const string Msg0033 = "Thêm đơn hàng và gửi yêu cầu lấy hàng thành công.";

        public const string Msg0034 = "Thêm mới và gửi yêu cầu lấy hàng {0}/{1} đơn hàng thành công.";

        public const string Msg0032 = "Không có đơn hàng trong danh sách tạm, không thể gửi yêu cầu.";

        // COD Khoi luong
        public const string Msg0035 = "Giá trị đơn hàng (COD) không hợp lệ (tối đa 100.000.000 đ).";
        public const string Msg0036 = "Khối lượng kiện hàng không hợp lệ (từ 0 - 100.000 gram).";

        // Excxel
        public const string Msg0037 = "Chọn tệp tin không hợp lệ, đuôi mở rộng .xlsx hoặc .xls và <= 2MB.";
        public const string Msg0038 = "Phiên bản tệp tin mẫu không đúng, hãy tải tệp tin template mới nhất.";


        // Upload
        public const string Msg0042 = "Chọn tệp tin không hợp lệ, đuôi mở rộng .jpg,.gif,.png và <= 2MB.";
        public const string Msg0043 = "Chọn tệp tin không hợp lệ phải <= 1MB.";
        public const string Msg0044 = "Tải lên tập tin thành công";
        public const string Msg0045 = "Vui lòng tải lên 1 tập tin";
    }
}
