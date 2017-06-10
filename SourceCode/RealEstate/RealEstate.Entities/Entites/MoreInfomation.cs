using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    [Table("MoreInfomation")]
    public class MoreInfomation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MoreInfomationID { get; set; }

        public string FloorNumber { get; set; }

        public string ToiletNumber { get; set; }

        public string BedroomNumber { get; set; }

        public string Compass { get; set; }

        public decimal? ElectricPrice { get; set; }

        public decimal? WaterPrice { get; set; }

        [Column(TypeName = "xml")]
        public string Convenient { get; set; }
    }
}
