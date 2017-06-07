using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public int Id { set; get; }
        [Required]
        [MaxLength(100)]
        public string CountryCode { get; set; }

        [MaxLength(100)]
        public string CommonName { get; set; }

        [MaxLength(100)]
        public string FormalName { get; set; }

        [MaxLength(100)]
        public string CountryType { get; set; }

        [MaxLength(100)]
        public string CountrySubType { get; set; }

        [MaxLength(100)]
        public string Sovereignty { get; set; }

        [MaxLength(100)]
        public string Capital { get; set; }

        [MaxLength(100)]
        public string CurrencyCode { get; set; }

        [MaxLength(100)]
        public string CurrencyName { get; set; }
        [MaxLength(100)]
        public string TelephoneCode { get; set; }

        [MaxLength(100)]
        public string CountryCode3 { get; set; }

        [MaxLength(100)]
        public string CountryNumber { get; set; }

        [MaxLength(100)]
        public string InternetCountryCode { get; set; }

        public int? SortOrder { get; set; }

        public bool? IsPublished { get; set; }

        [MaxLength(50)]
        public string Flags { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
