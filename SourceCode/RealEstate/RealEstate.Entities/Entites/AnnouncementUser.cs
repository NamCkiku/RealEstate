﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    [Table("AnnouncementUsers")]
    public class AnnouncementUser
    {
        [Key]
        [Column(Order = 1)]
        public int AnnouncementId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }

        public bool HasRead { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("AnnouncementId")]
        public virtual Announcement Announcement { get; set; }
    }
}
