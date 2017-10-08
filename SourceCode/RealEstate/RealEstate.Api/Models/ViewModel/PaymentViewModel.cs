using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class PaymentViewModel
    {
        public string PaymentMethod { get; set; }
        public string BankCode { get; set; }

        public double TotalAmount { get; set; }

        public int OrderCode { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }


    }
    public class PaymentCashViewModel
    {
        [Required]
        public string CardType { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Họ và tên phải lớn hơn 5 và nhỏ hơn 50 ký tự")]
        public string Pincard { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Họ và tên phải lớn hơn 5 và nhỏ hơn 50 ký tự")]
        public string SerialCard { get; set; }
    }
}