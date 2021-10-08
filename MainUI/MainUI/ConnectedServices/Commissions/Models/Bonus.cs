using MainUI.ConnectedServices.Customers.Models;
using System;

namespace MainUI.ConnectedServices.Commissions.Models
{
    public class Bonus
    {
        public string BonusId { get; set; }
        public string BonusTitle { get; set; }
        public string NodeId { get; set; }
        public long PeriodId { get; set; }
        public long CompensationPlanId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Volume { get; set; }
        public decimal Percent { get; set; }
        public decimal Released { get; set; }
        public int Level { get; set; }
        public DateTime CommissionDate { get; set; }
        public bool IsFirstTimeBonus { get; set; }
        public string Rank { get; set; }
        public string CustomerType { get; set; }
        public Customer Customer { get; set; }
    }
}
