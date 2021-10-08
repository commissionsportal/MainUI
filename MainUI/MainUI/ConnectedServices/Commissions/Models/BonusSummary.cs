using System;

namespace MainUI.ConnectedServices.Commissions.Models
{
    public class BonusSummary
    {
        public string Group { get; set; }
        public string Description { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PaidCount { get; set; }
        public decimal TotalVolume { get; set; }

        public decimal PercentValue(decimal totalPaid)
        {
                var percent = PaidAmount > 0 ? Math.Round(PaidAmount / totalPaid, 2) : 0;
                return percent;
        }

        public decimal PercentDisplay(decimal totalPaid)
        {
            return Math.Round(PercentValue(totalPaid) * 100);
        }
    }
}
