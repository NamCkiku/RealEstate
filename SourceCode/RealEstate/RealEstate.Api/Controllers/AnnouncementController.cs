using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RealEstate.Api.Controllers
{
    public class AnnouncementController : ApiControllerBase
    {
        public AnnouncementController(IErrorService errorService)
            : base(errorService)
        {
        }
    }
}
