﻿using RealEstate.Entities.Entites;
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
    public class DistrictService : BaseService<District>, IDistrictService
    {
        public DistrictService(IRepository<District> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}