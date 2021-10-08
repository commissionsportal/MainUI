using MainUI.ConnectedServices.Commissions.Models;
using System.Collections.Generic;
using System.Linq;

namespace MainUI.Models
{
    public class CommissionPeriodDetailViewModel
    {
        public Period Period { get; set; }
        public BonusSummary[] BonusesByTitle { get; set; }
        public BonusSummary[] BonusesByRank { get; set; }
        public decimal TotalCommissions { get; set; }
        public decimal TotalCommissionTrend { get; set; }
        public TrendHelper TotalCommissionTrendH { get { return new TrendHelper(TotalCommissionTrend); } }
        public ChartData CommissionHistory { get; set; }

        public int TotalCustomers { get; set; }
        public int CustomersPaid { get; set; }
        public decimal CustomersPaidPercent { get { return TotalCustomers > 0 ? ((decimal)CustomersPaid / TotalCustomers) * 100m : 0m; } }
        public decimal CustomersPaidTrend { get; set; }
        public TrendHelper CustomersPaidTrendH { get { return new TrendHelper(CustomersPaidTrend); } }
        public decimal CustomerUnPaid { get { return TotalCustomers - CustomersPaid; } }
        public int CustomerPaidTM { get; set; }
        public decimal CustomerPaidTMP { get { return TotalCustomers > 0 ? ((decimal)CustomerPaidTM / TotalCustomers) * 100m : 0m; } }
        public int CustomerPaid6 { get; set; }
        public decimal CustomerPaid6P { get { return TotalCustomers > 0 ? ((decimal)CustomerPaid6 / TotalCustomers) * 100m : 0m; } }
        public int CustomerPaid12 { get; set; }
        public decimal CustomerPaid12P { get { return TotalCustomers > 0 ? ((decimal)CustomerPaid12 / TotalCustomers) * 100m : 0m; } }

        public decimal TotalFtVolume { get; set; }
        public decimal TotalFtVolumeTrend { get; set; }
        public TrendHelper TotalFtVolumeTrendH { get { return new TrendHelper(TotalFtVolumeTrend); } }
        public ChartData TotalFtVolumeHistory1 { get; set; }
        public ChartData TotalFtVolumeHistory2 { get; set; }

        public decimal TotalVolume { get; set; }
        public decimal TotalVolumeTrend { get; set; }
        public TrendHelper TotalVolumeTrendH { get { return new TrendHelper(TotalVolumeTrend); } }
        public ChartData TotalVolumeHistory { get; set; }

        public ChartData DailyVolumeHistoryW { get; set; }
        public ChartData DailyVolumeHistoryR { get; set; }
        public ChartData DailyVolumeHistoryV { get; set; }

        public ChartData DailyCommissionTotal { get; set; }
        public decimal TodayCommissionTotal { get; set; }
        public decimal YesterdayCommissionTotal { get; set; }
        public decimal TodayCommissionTotalIncPercent { get; set; }
        public string TodayCommissionTotalIncPercentString { get { return TodayCommissionTotalIncPercent < 0 ? $"{TodayCommissionTotalIncPercent}% less" : $"+{TodayCommissionTotalIncPercent}% more"; } }
        public TrendHelper TodayCommissionTotalIncTendH { get { return new TrendHelper(TodayCommissionTotalIncPercent); } }

        public Bonus[] TopEarners { get; set; }
    }
}
