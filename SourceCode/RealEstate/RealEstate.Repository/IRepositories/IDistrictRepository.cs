﻿using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository.IRepositories
{
    public interface IDistrictRepository : IRepository<District>
    {
        IEnumerable<DistrictEntity> GetAllDistrict();
    }
}
