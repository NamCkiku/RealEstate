using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;

namespace RealEstate.Repository.Repositories
{ 
    public class AnnouncementUserRepository : RepositoryBase<AnnouncementUser>, IAnnouncementUserRepository
    {
        public AnnouncementUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}