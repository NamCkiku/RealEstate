using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.ModelView
{
    public class AppUserEntity
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public DateTime? BirthDay { get; set; }

        public bool? Status { get; set; }

        public bool? Gender { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
