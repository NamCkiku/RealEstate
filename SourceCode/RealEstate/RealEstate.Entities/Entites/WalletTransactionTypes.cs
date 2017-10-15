using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.Entites
{
    [Table("WalletTransactionTypes")]
    public class WalletTransactionTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WalletTransactionTypeID { get; set; }

        public string Name { get; set; }

        public bool? TypeTransaction { get; set; }

        public string Description { get; set; }

        public bool? IsWeb { get; set; }

        public bool? IsAuto { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? UpdatedByUser { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
