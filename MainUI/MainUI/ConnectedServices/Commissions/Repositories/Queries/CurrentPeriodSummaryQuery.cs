using MainUI.ConnectedServices.Commissions.Interfaces;
using MainUI.ConnectedServices.Commissions.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories.Queries
{
    public static class CurrentPeriodSummaryQuery
    {
        public static async Task<CurrentPeriodDetail> GetCurrentPeriodSummary(this IQueryClient client)
        {
            var v1 = await client.PostQuery<CurrentCommissionPeriodQueryResult>(@"
    {
        compensationPlans
        {
            id,
            periods(date: """ + System.DateTime.UtcNow.ToShortDateString() + @""")
            {
                id
            }
        }
    }");

            return v1.CompensationPlans.Select(x =>
            {
                return new CurrentPeriodDetail
                {
                    TemplateId = x.Id,
                    PeriodId = x.Periods.Select(y => y.Id).FirstOrDefault()
                };
            }).FirstOrDefault();
        }
    }

    public class CurrentCommissionPeriodQueryResult
    {
        public CompensationPlan[] CompensationPlans { get; set; }
    }
}
