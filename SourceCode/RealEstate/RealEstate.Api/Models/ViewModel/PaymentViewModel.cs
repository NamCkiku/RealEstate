using System;
using System.Collections.Generic;
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

        public string CardType { get; set; }


        public string Pincard { get; set; }

        public string SerialCard { get; set; }
    }
}