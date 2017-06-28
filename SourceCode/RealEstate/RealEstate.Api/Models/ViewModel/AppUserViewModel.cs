using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class AppUserViewModel
    {
        public string Id { set; get; }
        public string FullName { set; get; }

        public string Address { set; get; }

        public string Avatar { get; set; }

        public DateTime? BirthDay { set; get; }

        public bool? Status { get; set; }

        public bool? Gender { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}