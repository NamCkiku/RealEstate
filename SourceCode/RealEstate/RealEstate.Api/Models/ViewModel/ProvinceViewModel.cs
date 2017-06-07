using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class ProvinceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int? TelephoneCode { get; set; }

        public string ZipCode { get; set; }

        public int CountryId { get; set; }

        public string CountryCode { get; set; }

        public int? SortOrder { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsDeleted { get; set; }
        public virtual CountryViewModel Country { set; get; }

    }
}