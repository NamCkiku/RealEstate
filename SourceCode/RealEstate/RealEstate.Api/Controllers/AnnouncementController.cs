using AutoMapper;
using Microsoft.AspNet.Identity;
using RealEstate.Api.Models.Common;
using RealEstate.Api.SignalR;
using RealEstate.Common.Core;
using RealEstate.Entities.Entites;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RealEstate.Api.Controllers
{
    [RoutePrefix("api/Announcement")]
    public class AnnouncementController : ApiControllerBase
    {
        private IAnnouncementService _announcementService;

        public AnnouncementController(IErrorService errorService,
            IAnnouncementService announcementService)
            : base(errorService)
        {
            _announcementService = announcementService;
        }
        [Route("getTopMyAnnouncement")]
        [HttpGet]
        public HttpResponseMessage GetTopMyAnnouncement(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                List<Announcement> model = _announcementService.ListAllUnread(User.Identity.GetUserId(), 1, 10, out totalRow);
                IEnumerable<AnnouncementViewModel> modelVm = Mapper.Map<List<Announcement>, List<AnnouncementViewModel>>(model);

                PaginationSet<AnnouncementViewModel> pagedSet = new PaginationSet<AnnouncementViewModel>()
                {
                    Items = modelVm,
                    Page = 1,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / 1)
                };
                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int pageIndex, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _announcementService.GetListAll(pageIndex, pageSize, out totalRow);

                IEnumerable<AnnouncementViewModel> modelVm = Mapper.Map<List<Announcement>, List<AnnouncementViewModel>>(model);
                PaginationSet<AnnouncementViewModel> pagedSet = new PaginationSet<AnnouncementViewModel>()
                {
                    Items = modelVm,
                    Page = pageIndex,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("detail/{id}")]
        [HttpGet]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            if (id == 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }
            var announcement = _announcementService.GetDetail(id);
            if (announcement == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No data");
            }
            var modelVm = Mapper.Map<Announcement, AnnouncementViewModel>(announcement);

            return request.CreateResponse(HttpStatusCode.OK, modelVm);
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, AnnouncementViewModel announcementVm)
        {
            if (ModelState.IsValid)
            {
                var newAnnoun = new Announcement();
                try
                {
                    newAnnoun.Content = announcementVm.Content;
                    newAnnoun.Status = announcementVm.Status;
                    newAnnoun.Title = announcementVm.Title;
                    newAnnoun.CreatedDate = DateTime.Now;
                    newAnnoun.UserId = User.Identity.GetUserId();
                    foreach (var user in announcementVm.AnnouncementUsers)
                    {
                        newAnnoun.AnnouncementUsers.Add(new AnnouncementUser()
                        {
                            UserId = user.UserId,
                            HasRead = false
                        });
                    }
                    _announcementService.Create(newAnnoun);
                    _announcementService.SaveChanges();

                    var announ = _announcementService.GetDetail(newAnnoun.ID);
                    //push notification
                    RealEstateHub.PushToAllUsers(Mapper.Map<Announcement, AnnouncementViewModel>(announ), null);

                    return request.CreateResponse(HttpStatusCode.OK, announcementVm);

                }
                catch (Exception dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpGet]
        [Route("markAsRead")]
        public HttpResponseMessage MarkAsRead(HttpRequestMessage request, int announId)
        {
            try
            {
                _announcementService.MarkAsRead(User.Identity.GetUserId(), announId);
                _announcementService.SaveChanges();
                return request.CreateResponse(HttpStatusCode.OK, announId);

            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            _announcementService.DeleteAnnouncement(id);
            _announcementService.SaveChanges();

            return request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}
