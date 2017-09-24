using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class AppUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Họ và tên phải lớn hơn 10 và nhỏ hơn 20 ký tự")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Địa chỉ phải lớn hơn 10 và nhỏ hơn 20 ký tự")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public string Avatar { get; set; }

        public DateTime? BirthDay { get; set; }

        public bool? Status { get; set; }

        public bool? Gender { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        [Display(Name = "PhoneNumber")]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Số điện thoại phải lớn hơn 8 và nhỏ hơn 14 ký tự")]
        public string PhoneNumber { get; set; }

        public int Coin { get; set; }

        public int RewardPoint { get; set; }

        public int RankStar { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}