using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.Service
{
    public class AnnouncementService : BaseService<Announcement>, IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IAnnouncementUserRepository _announcementUserRepository;

        public AnnouncementService(IRepository<Announcement> repository, IAnnouncementRepository announcementRepository,
            IAnnouncementUserRepository announcementUserRepository,
            IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _announcementRepository = announcementRepository;
            _announcementUserRepository = announcementUserRepository;
        }

        public Announcement Create(Announcement announcement)
        {
            return _announcementRepository.Add(announcement);
        }

        public void DeleteAnnouncement(int notificationId)
        {
            _announcementRepository.Delete(notificationId);
        }

        public List<Announcement> GetListAll(int pageIndex, int pageSize, out int totalRow)
        {
            var query = _announcementRepository.GetAll(new string[] { "AppUser" });
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).ToList();
        }

        public List<Announcement> GetListByUserId(string userId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = _announcementRepository.GetMulti(x => x.UserId == userId);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate)
                .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
        }

        public List<Announcement> GetListByUserId(string userId, int top)
        {
            return _announcementRepository.GetMulti(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .Take(top).ToList();
        }

        public Announcement GetDetail(int id)
        {
            return _announcementRepository.GetSingleByCondition(x => x.ID == id, new string[] { "AppUser" });
        }

        public List<Announcement> ListAllUnread(string userId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = _announcementRepository.GetAllUnread(userId);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public void MarkAsRead(string userId, int notificationId)
        {
            var announ = _announcementUserRepository.GetSingleByCondition(x => x.AnnouncementId == notificationId && x.UserId == userId);
            if (announ == null)
            {
                _announcementUserRepository.Add(new AnnouncementUser()
                {
                    AnnouncementId = notificationId,
                    UserId = userId,
                    HasRead = true
                });
            }
            else
            {
                announ.HasRead = true;
            }
        }
    }
}
