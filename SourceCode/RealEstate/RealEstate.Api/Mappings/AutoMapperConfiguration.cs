using AutoMapper;
using RealEstate.Api.Models.ViewModel;
using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Mappings
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



                cfg.CreateMap<WardEntity, WardViewModel>();
                cfg.CreateMap<DistrictEntity, DistrictViewModel>();
                cfg.CreateMap<ProvinceEntity, ProvinceViewModel>();
                cfg.CreateMap<RoomEntity, RoomListViewModel>();
                cfg.CreateMap<Room, RoomListViewModel>();
                cfg.CreateMap<RoomListEntity, ListRoomViewModel>();
                cfg.CreateMap<AuditlogEntity, AuditlogViewModel>();
                cfg.CreateMap<AppUserEntity, AppUserViewModel>();
                cfg.CreateMap<UserTransactionHistoryEntity, UserTransactionHistoryViewModel>();
            });
        }
    }
}