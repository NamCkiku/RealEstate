using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.ModelView
{
    public class UserWalletEntity
    {
        public int WalletID { get; set; }

        public string UserID { get; set; }

        public string WalletName { get; set; }

        public Double? Amount { get; set; }

        public Double? PromotionAmount { get; set; }

        public DateTime? PromotionExprireDate { get; set; }

        public bool? IsLocked { get; set; }

        public string ReasonLocked { get; set; }

        public bool? IsDeleted { get; set; }

        public string ReasonDeleted { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedByUser { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
