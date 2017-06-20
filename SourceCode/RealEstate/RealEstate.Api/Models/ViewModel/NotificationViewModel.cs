using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class NotificationViewModel
    {
        public byte MsgType { get; set; }
        public string Msg { get; set; }
    }

    public class NotificationType
    {
        public const int MSG_TYPE_SUCCESS = 1;
        public const int MSG_TYPE_FAIL = 2;
    }
}