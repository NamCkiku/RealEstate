﻿using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.IService
{
    public interface IProvinceService : IBaseService<Province>
    {
        IEnumerable<ProvinceEntity> GetAllProvince();
    }
}
