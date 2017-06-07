using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    [Table("District")]
    public class District
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [MaxLength(50)]
        public string LatiLongTude { get; set; }

        public int ProvinceId { get; set; }

        public int? SortOrder { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsDeleted { get; set; }


        [ForeignKey("ProvinceId")]
        public virtual Province Province { set; get; }

    }
}
