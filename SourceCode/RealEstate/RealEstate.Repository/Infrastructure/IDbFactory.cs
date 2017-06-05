using RealEstate.Entities.Entites;
using System;

namespace RealEstate.Repository.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        RealEstateDbContext Init();
    }
}