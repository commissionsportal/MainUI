using System;
using System.Linq;

namespace MainUI.ConnectedServices.Commissions.Models
{
    public class Period
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public int CompensationPlanId { get; set; }
        public string CompensationPlanName { get; set; }
        public string Status { get; set; }
        public int TotalCustomersPaid { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal TotalCommissions { get; set; }

        public BonusSummary[] BonusSummary { get; set; }
        public Bonus[] Bonuses { get; set; }
        public Source[] Sources { get; set; }
        public Snapshot[] Snapshots { get; set; }
        public CommissionValue[] CommissionValues { get; set; }
        public RankAdvance[] RankAdvance { get; set; }

        public string GetCommissionValue(string key)
        {
            return CommissionValues.Where(x => x.ValueId == key).FirstOrDefault()?.Value ?? string.Empty;
        }

        public string BeginString
        {
            get
            {
                return Begin.ToString("MMMM d, yyyy");
            }
        }

        public string EndString
        {
            get
            {
                return End.ToString("MMMM d, yyyy");
            }
        }

        public string PaidCountFormatted
        {
            get
            {
                return TotalCustomersPaid.ToString();
            }
        }

        public string TotalVolumeFormatted
        {
            get
            {
                return string.Format("{0:n}", TotalVolume);
            }
        }

        public string TotalPaidFormatted
        {
            get
            {
                return string.Format("{0:C}", TotalCommissions);
            }
        }

        public decimal PercentValue
        {
            get
            {
                var percent = TotalVolume > 0 ? Math.Round(TotalCommissions / TotalVolume, 2) : 0m;
                return percent;
            }
        }

        public decimal PercentDisplay
        {
            get
            {
                return Math.Round(PercentValue * 100);
            }
        }

        public bool ShowCommit
        {
            get
            {
                return this.Status != "Committed";
            }
        }

        public string StatusClass
        {
            get
            {
                if (this.Status == "Pending") return "warning";
                if (this.Status == "Committed") return "success";
                return "secondary";
            }
        }
    }
}
