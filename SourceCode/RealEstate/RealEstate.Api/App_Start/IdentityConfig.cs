﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RealEstate.Entities.Entites;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System;

namespace RealEstate.Api
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserStore : UserStore<AppUser>
    {
        public ApplicationUserStore(RealEstateDbContext context)
            : base(context)
        {
        }
        public class ApplicationRoleManager : RoleManager<AppRole>
        {
            public ApplicationRoleManager(IRoleStore<AppRole, string> roleStore)
                : base(roleStore)
            {
            }

            public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
            {
                return new ApplicationRoleManager(new RoleStore<AppRole>(context.Get<RealEstateDbContext>()));
            }
        }
        public class ApplicationSignInManager : SignInManager<AppUser, string>
        {
            public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
                : base(userManager, authenticationManager)
            {
            }

            public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
            {
                return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
            }

            public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
            {
                return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
            }
        }
        public class ApplicationUserManager : UserManager<AppUser>
        {
            public ApplicationUserManager(IUserStore<AppUser> store)
                : base(store)
            {
            }

            public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
            {
                var manager = new ApplicationUserManager(new UserStore<AppUser>(context.Get<RealEstateDbContext>()));
                // Configure validation logic for usernames
                manager.UserValidator = new UserValidator<AppUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Configure validation logic for passwords
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };

                // Configure user lockout defaults
                manager.UserLockoutEnabledByDefault = true;
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);

                manager.MaxFailedAccessAttemptsBeforeLockout = 5;

                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
                }
                return manager;
            }
        }
    }
}
