using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Administrator.Models.ViewModel
{
    public class RoomTypeViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string RoomTypeName { get; set; }

        [Required]
        [StringLength(256)]
        public string Alias { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(256)]
        public string ImageIcon { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(256)]
        public string UpdatedBy { get; set; }

        [StringLength(256)]
        public string MetaKeyword { get; set; }

        [StringLength(256)]
        public string MetaDescription { get; set; }

        public bool isDelete { get; set; }

        public bool Status { get; set; }
    }
}