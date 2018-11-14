using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

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
        public DbSet<Client> Clients { set; get; }
        public DbSet<RefreshToken> RefreshTokens { set; get; }

        public DbSet<Tag> Tags { set; get; }
        public DbSet<RoomTag> RoomTags { set; get; }
        public DbSet<AuditLog> AuditLogs { set; get; }

        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

        public DbSet<UserWallet> UserWallets { set; get; }
        public DbSet<WalletTransactionTypes> WalletTransactionTypes { set; get; }
        public DbSet<UserTransactionHistory> UserTransactionHistory { set; get; }

        public DbSet<SystemSetting> SystemSettings { get; set; }

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
