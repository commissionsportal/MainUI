using MainUI.ConnectedServices.Commissions.Interfaces;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories.Queries
{
    public static class PeriodDetailQuery
    {
        public static async Task<CompensationPlan> GetPeriodDetail(this IQueryClient client, string templateId, string periodId)
        {
            var qryRes = await client.PostQuery<CommissionPeriodDetailResult>(@"
     {
      compensationPlan(id: """ + templateId + @""") {
        period(id: """ + periodId + @""") {
          id
          begin
          end
          compensationPlanId
          RankBonus: bonusSummary(group: RANK) {
            group
            description
            paidAmount
            paidCount
            totalVolume
          }
          TitleBonus: bonusSummary(group: BONUS_TITLE) {
            group
            description
            paidAmount
            paidCount
            totalVolume
          }
          TopEarners: bonusSummary(group: NODE_ID, first: 10) {
            group
            description
            paidAmount
            paidCount
            totalVolume
          }
          DateTypeBonus: bonusSummary(group: [Commission_Date]) {
            group
            description
            paidAmount
            paidCount
            totalVolume
          }
          volumeSummary(group: [Volume_Date]) {
            group
            description
            totalVolume
          }
        }
        FirstTime: periods(at: """ + periodId + @""", previous: 2) {
          id
          begin
          end
          FirstTimeBonus: bonusSummary(group: [Commission_Date, First_Time_Bonus]) {
            group
            description
            paidAmount
            paidCount
            totalVolume
          }
        }
        periods(at: """ + periodId + @""", previous: 6) {
          id
          begin
          end
          status
          totalCommissions
          totalCustomers
          totalCustomersPaid
          totalVolume
        }
      }
    }
    ");

            return qryRes.CompensationPlan;
        }
    }

    public class CommissionPeriodDetailResult
    {
        public CompensationPlan CompensationPlan { get; set; }
    }
}
