using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository.Repositories
{
    public class AuditLogRepository : RepositoryBase<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
