using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RealEstate.Api.NganLuongAPI
{
    #region Entity RequestInfo
    public class RequestInfo
    {
        private String _Funtion = "CardCharge";

        public String Funtion
        {
            get { return _Funtion; }
        }
        private String _Version = "2.0";

        public String Version
        {
            get { return _Version; }
        }
        private String _Merchant_id = String.Empty;

        public String Merchant_id
        {
            get { return _Merchant_id; }
            set { _Merchant_id = value; }
        }
        private String _Merchant_acount = String.Empty;

        public String Merchant_acount
        {
            get { return _Merchant_acount; }
            set { _Merchant_acount = value; }
        }
        private String _Merchant_password = String.Empty;

        public String Merchant_password
        {
            get { return _Merchant_password; }
            set { _Merchant_password = value; }
        }
        private String _Pincard = String.Empty;

        public String Pincard
        {
            get { return _Pincard; }
            set { _Pincard = value; }
        }
        private String _SerialCard = String.Empty;

        public String SerialCard
        {
            get { return _SerialCard; }
            set { _SerialCard = value; }
        }
        private String _CardType = String.Empty;

        public String CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }
        private String _Refcode = String.Empty;

        public String Refcode
        {
            get { return _Refcode; }
            set { _Refcode = value; }
        }
        private String _Client_fullname = String.Empty;

        public String Client_fullname
        {
            get { return _Client_fullname; }
            set { _Client_fullname = value; }
        }
        private String _Client_email = String.Empty;

        public String Client_email
        {
            get { return _Client_email; }
            set { _Client_email = value; }
        }
        private String _Client_mobile = String.Empty;

        public String Client_mobile
        {
            get { return _Client_mobile; }
            set { _Client_mobile = value; }
        }
    }

    public class ResponseCashInfo
    {
        private String _Errorcode = string.Empty;

        public String Errorcode
        {
            get { return _Errorcode; }
            set { _Errorcode = value; }
        }
        private String _TransactionID = String.Empty;

        public String TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        private String _Message = String.Empty;

        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private String _Card_amount = "0";

        public String Card_amount
        {
            get { return _Card_amount; }
            set { _Card_amount = value; }
        }
        private String _Transaction_amount = "0";

        public String Transaction_amount
        {
            get { return _Transaction_amount; }
            set { _Transaction_amount = value; }
        }
        private String _Refcode = String.Empty;

        public String Refcode
        {
            get { return _Refcode; }
            set { _Refcode = value; }
        }
    }
    #endregion
    public class NLCardLib
    {
        private String Merchant_Pass = "";
        private NLCardLib() { }
        public static ResponseCashInfo CardChage(RequestInfo info)
        {
            String result = HttpPost(info);
            return ParserResult(result);
        }
        #region Function  Lib
        private static ResponseCashInfo ParserResult(string output)
        {
            ResponseCashInfo info = new ResponseCashInfo();
            String[] arr = output.Split('|');
            if (arr.Length == 13)
            {
                info.Errorcode = arr[0];
                info.Message = GetErrorMessage(info.Errorcode);
                info.Card_amount = arr[10];
                info.Refcode = arr[6];
                info.Transaction_amount = arr[11];
                info.TransactionID = arr[12];
            }
            else
            {
                info.Errorcode = "99";
                info.Message = "Lỗi không xác định.";
            }
            return info;
        }
        private static String GetErrorMessage(string _ErrorCode)
        {
            String _Message = "";
            switch (_ErrorCode)
            {
                case "00":
                    _Message = "Giao dịch thành công";
                    break;
                case "01":
                    _Message = "Lỗi, địa chỉ IP truy cập API của NgânLượng.vn bị từ chối";
                    break;
                case "02":
                    _Message = "Lỗi, tham số gửi từ merchant tới NgânLượng.vn chưa chính xác.";
                    break;
                case "03":
                    _Message = "Lỗi, mã merchant không tồn tại hoặc merchant đang bị khóa kết nối tới NgânLượng.vn";
                    break;
                case "04":
                    _Message = "Lỗi, mã checksum không chính xác";
                    break;
                case "05":
                    _Message = "Tài khoản nhận tiền nạp của merchant không tồn tại";
                    break;
                case "06":
                    _Message = "Tài khoản nhận tiền nạp của  merchant đang bị khóa hoặc bị phong tỏa, không thể thực hiện được giao dịch nạp tiền";
                    break;
                case "07":
                    _Message = "Thẻ đã được sử dụng";
                    break;
                case "08":
                    _Message = "Thẻ bị khóa";
                    break;
                case "09":
                    _Message = "Thẻ hết hạn sử dụng";
                    break;
                case "10":
                    _Message = "Thẻ chưa được kích hoạt hoặc không tồn tại";
                    break;
                case "11":
                    _Message = "Mã thẻ sai định dạng";
                    break;
                case "12":
                    _Message = "Sai số serial của thẻ";
                    break;
                case "13":
                    _Message = "Mã thẻ và số serial không khớp";
                    break;
                case "14":
                    _Message = "Thẻ không tồn tại";
                    break;
                case "15":
                    _Message = "Thẻ không sử dụng được";
                    break;
                case "16":
                    _Message = "Số lần tưử của thẻ vượt quá giới hạn cho phép";
                    break;
                case "17":
                    _Message = "Hệ thống Telco bị lỗi hoặc quá tải, thẻ chưa bị trừ";
                    break;
                case "18":
                    _Message = "Hệ thống Telco  bị lỗi hoặc quá tải, thẻ có thể bị trừ, cần phối hợp với NgânLượng để đối soát";
                    break;
                case "19":
                    _Message = "Kết nối NgânLượng với Telco bị lỗi, thẻ chưa bị trừ.";
                    break;
                case "20":
                    _Message = "Kết nối tới Telco thành công, thẻ bị trừ nhưng chưa cộng tiền trên NgânLượng.vn";
                    break;
                case "99":
                    _Message = "Lỗi tuy nhiên lỗi chưa được định nghĩa hoặc chưa xác định được nguyên nhân";
                    break;
            }
            return _Message;
        }
        private static string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }
        private static String GetParamPost(RequestInfo info)
        {

            String request = "";

            request += "func=" + info.Funtion;
            request += "&version=" + info.Version;
            request += "&merchant_id=" + info.Merchant_id;
            request += "&merchant_account=" + info.Merchant_acount;
            request += "&merchant_password=" + CreateMD5Hash(info.Merchant_id + "|" + info.Merchant_password);
            request += "&pin_card=" + info.Pincard;
            request += "&card_serial=" + info.SerialCard;
            request += "&type_card=" + info.CardType;
            request += "&ref_code=" + info.Refcode;
            request += "&client_fullname=" + info.Client_fullname;
            request += "&client_email=" + info.Client_email;
            request += "&client_mobile=" + info.Client_mobile;

            return request;
        }


        private static String HttpPost(RequestInfo request)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = GetParamPost(request);
            byte[] data = encoding.GetBytes(postData);

            // Prepare web request...
            HttpWebRequest myRequest =
              (HttpWebRequest)WebRequest.Create("https://www.nganluong.vn/mobile_card.api.post.v2.php");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.

            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string output = reader.ReadToEnd();
            response.Close();
            return output;
        }
        #endregion End Funtion Lib
    }
}