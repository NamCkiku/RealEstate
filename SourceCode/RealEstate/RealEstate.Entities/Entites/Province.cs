using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        public int? TelephoneCode { get; set; }

        [MaxLength(20)]
        public string ZipCode { get; set; }

        public int CountryId { get; set; }

        [MaxLength(2)]
        public string CountryCode { get; set; }

        public int? SortOrder { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsDeleted { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { set; get; }

    }
}
