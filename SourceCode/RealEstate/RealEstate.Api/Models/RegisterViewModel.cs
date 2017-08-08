using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 0, ErrorMessage = "Họ và tên phải lớn hơn 10 và nhỏ hơn 20 ký tự")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Họ và tên phải lớn hơn 10 và nhỏ hơn 20 ký tự")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Số điện thoại phải lớn hơn 8 và nhỏ hơn 14 ký tự")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}