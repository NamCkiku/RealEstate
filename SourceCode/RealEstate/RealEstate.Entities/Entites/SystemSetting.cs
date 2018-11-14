using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Entities.Entites
{
    [Table("SystemSetting")]
    public partial class SystemSetting
    {
        [Key]
        [StringLength(250)]
        public string Field { get; set; }

        [StringLength(250)]
        public string Value { get; set; }

        [StringLength(250)]
        public string ValDefault { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(256)]
        public string UpdatedBy { get; set; }
    }
}
