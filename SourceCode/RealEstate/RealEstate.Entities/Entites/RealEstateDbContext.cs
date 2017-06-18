using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    public class RealEstateDbContext : IdentityDbContext<AppUser>
    {
        public RealEstateDbContext() : base("RealEstateConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<AppRole> AppRoles { set; get; }
        public DbSet<IdentityUserRole> UserRoles { set; get; }
        public DbSet<Country> Countrys { set; get; }
        public DbSet<Province> Provinces { set; get; }
        public DbSet<District> Districts { set; get; }
        public DbSet<Ward> Wards { set; get; }
        public DbSet<RoomType> RoomTypes { set; get; }
        public DbSet<Room> Room { set; get; }
        public DbSet<MoreInfomation> MoreInfomations { set; get; }
        public DbSet<Error> Errors { set; get; }
        public static RealEstateDbContext Create()
        {
            return new RealEstateDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasKey<string>(r => r.Id).ToTable("AppRoles");
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("AppUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("AppUserLogins");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("AppUserClaims");
        }
    }
}
