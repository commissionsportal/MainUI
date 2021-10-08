using MainUI.ConnectedServices.Commissions.Models;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories.Queries
{
    public static class BonusesSummaryQuery
    {
        public static async Task<Period> GetBonuses(this IQueryClient client, string templateId, string periodId, string group, string groupValue)
        {
            var v1 = await client.PostQuery<BonusesSummaryQueryResult>(@"
    {
      compensationPlan(id: """ + templateId + @""") {
        period(id: """ + periodId + @""") {
          id          
          begin,
          end,
          bonuses(group: " + group + @", groupValue: """ + groupValue + @""") {
            amount
            bonusTitle
            bonusId
            description
            level
            nodeId
            volume
            rank
            commissionDate
            percent
            released
          }
        }
      }
    }");

            return v1.CompensationPlan.Period;
        }

        private class BonusesSummaryQueryResult
        {
            public BSQR_CompensationPlan CompensationPlan { get; set; }

        }

        private class BSQR_CompensationPlan
        {
            public Period Period { get; set; }
        }
    }
}
