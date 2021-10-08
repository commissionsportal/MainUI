using MainUI.ConnectedServices.Commissions.Models;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories.Queries
{
    public static class CustomerCommissionDetailQuery
    {
        public static async Task<Template> GetCustomerCommissionDetail(this IQueryClient client, string customerId, long templateId, long periodId)
        {
            var result = await client.PostQuery<CustomerCommissionDetailQueryResult>(@"{
  compensationPlan(id: " + templateId + @")
  {
    id
    name
    description
    period(id: " + periodId + @") {
      id
      begin
      end
      commissionValues(nodeId: """ + customerId + @""") {
        nodeId
        valueId
        value
      }
      sources(nodeId: """ + customerId + @"""){
        id
        nodeId
        date
        externalId
        sourceGroupId
        value
      },
      bonuses(nodeId: """ + customerId + @""")
      {
        bonusId
        bonusTitle
        amount
        commissionDate
        description
        level
        percent
        rank
        released
        volume
      }
      rankAdvance(nodeId: """ + customerId + @""") {
        nodeId
        rankId
        rankName
        rankAdvanceValues {
                valueId
                required
          value
        }
      }
    }
  }
}");

            return result.CompensationPlan;
        }
    }

    internal class CustomerCommissionDetailQueryResult
    {
        public Template CompensationPlan { get; set; }
    }
}
