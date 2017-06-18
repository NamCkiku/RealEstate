using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    [Table("Room")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string RoomName { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [Required]
        public int RoomTypeID { set; get; }

        public int? WardID { get; set; }

        public int? DistrictID { get; set; }

        [Required]
        public int ProvinceID { get; set; }

        public int? VipID { get; set; }

        public int? MoreInfomationID { get; set; }

        public int? PaymentID { get; set; }

        [Required]
        [MaxLength(256)]
        public string ThumbnailImage { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Required]
        public double? Acreage { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(256)]
        public string Address { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        public string UserID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }

        public int? DisplayOrder { get; set; }

        public int? ViewCount { set; get; }

        [StringLength(400)]
        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        [StringLength(400)]
        public string MetaTitle { get; set; }

        public string Tags { set; get; }

        public bool Published { get; set; }
        public DateTime? CreatedDate { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(256)]
        public string UpdatedBy { get; set; }

        public bool isDelete { get; set; }

        public bool Status { get; set; }
    }
}
