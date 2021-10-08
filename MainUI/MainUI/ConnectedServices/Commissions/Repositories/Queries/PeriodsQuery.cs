using MainUI.ConnectedServices.Commissions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories.Queries
{
    public static class PeriodsQuery
    {
        public static async Task<Period[]> GetPeriods(this IQueryClient client, DateTime date, int pageSize)
        {
            var result = await client.PostQuery<PeriodsResult>(@"
            {
              compensationPlans {
                name
                periods(date: """ + date.ToUniversalTime().ToShortDateString() + @""", previous: " + pageSize + @") {
                  begin
                  end
                  id
                  compensationPlanId
                  totalCommissions
                  totalVolume
                  totalCustomersPaid
                  status
                  snapshots {
                    id
                    compensationPlanId
                    periodId
                  }
                }
              }
            }");


            var model = new List<Period>();

            foreach (var template in result.CompensationPlans)
            {
                var periods = template.Periods;
                periods.ToList().ForEach(x => x.CompensationPlanName = template.Name);
                model.AddRange(periods);
            }

            return model.ToArray();
        }
    }

    public class PeriodsResult
    {
        public CompensationPlansResult[] CompensationPlans { get; set; }
    }

    public class CompensationPlansResult
    {
        public string Name { get; }
        public Period[] Periods { get; set; }
    }
}
