using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.Service
{
    public class ErrorService : BaseService<Error>, IErrorService
    {
        public ErrorService(IRepository<Error> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
