using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.ModelView
{
    public class UserTransactionHistoryEntity
    {
        public int WalletTransactionID { get; set; }

        public int WalletID { get; set; }

        public int WalletTransactionTypeID { get; set; }

        public string UserID { get; set; }

        public DateTime DateTransaction { get; set; }

        public float AmountTransaction { get; set; }

        public float AmountCurrent { get; set; }

        public float BeginBalance { get; set; }

        public string Note { get; set; }

        public bool? IsVerified { get; set; }

        public string VerifiedByUser { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public string ReasonOfDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedByUser { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
