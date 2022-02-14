using MainUI.ConnectedServices.Commissions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Interfaces
{
    public interface ICommissionPeriodRepository
    {
        Task<IEnumerable<Models.Period>> GetPeriods(DateTime dateTime, int pageSize);
        Task<CurrentPeriodDetail> GetCurrentPeriodSummary();
        Task<CompensationPlan> GetPeriodDetail(long templateId, long periodId);
    }

    public class CompensationPlan
    {
        public long Id { get; set; }
        public Period Period { get; set; }
        public PeriodSummary[] Periods { get; set; }
        public FirstTimePeriod[] FirstTime { get; set; }
    }

    public class Period
    {
        public int Id { get; set; }
        public int CompensationPlanId { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public BonusSummary[] RankBonus { get; set; }
        public BonusSummary[] TitleBonus { get; set; }
        public BonusSummary[] DateTypeBonus { get; set; }
        public BonusSummary[] TotalVolume { get; set; }
        public BonusSummary[] TopEarners { get; set; }
        public BonusSummary[] VolumeSummary { get; set; }
    }

    public class PeriodSummary
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string Status { get; set; }
        public decimal TotalCommissions { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalCustomersPaid { get; set; }
        public decimal TotalVolume { get; set; }
    }

    public class FirstTimePeriod
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public BonusSummary[] FirstTimeBonus { get; set; }
    }
}
