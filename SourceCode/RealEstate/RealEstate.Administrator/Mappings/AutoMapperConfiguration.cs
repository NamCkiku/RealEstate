using AutoMapper;
using RealEstate.Administrator.Models.ViewModel;
using RealEstate.Entities.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Administrator.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Country, CountryViewModel>();
                cfg.CreateMap<Province, ProvinceViewModel>();
                cfg.CreateMap<District, DistrictViewModel>();
                cfg.CreateMap<Ward, WardViewModel>();
                cfg.CreateMap<RoomType, RoomTypeViewModel>();
                cfg.CreateMap<Room, RoomViewModel>();
            });
        }
    }
}