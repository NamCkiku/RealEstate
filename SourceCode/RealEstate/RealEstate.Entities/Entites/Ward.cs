using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    [Table("Ward")]
    public class Ward
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [MaxLength(50)]
        public string LatiLongTude { get; set; }

        public int DistrictID { get; set; }

        public int SortOrder { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsDeleted { get; set; }

        [ForeignKey("DistrictID")]
        public virtual District District { set; get; }
    }
}
