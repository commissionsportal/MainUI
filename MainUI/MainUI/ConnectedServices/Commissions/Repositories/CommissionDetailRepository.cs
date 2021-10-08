using MainUI.ConnectedServices.Commissions.Interfaces;
using MainUI.ConnectedServices.Commissions.Models;
using MainUI.ConnectedServices.Commissions.Repositories.Queries;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories
{
    public class CommissionDetailRepository : ICommissionDetailRepository
    {
        private readonly IQueryClient _client;

        public CommissionDetailRepository(IQueryClient client)
        {
            _client = client;
        }

        public async Task<Models.Period> GetBonuses(string templateId, string periodId, string group, string groupValue)
        {
            return await _client.GetBonuses(templateId, periodId, group, groupValue);
        }

        public async Task<Template> GetCustomerCommissionDetail(string customerId, long templateId, long periodId)
        {
            return await _client.GetCustomerCommissionDetail(customerId, templateId, periodId);
        }
    }
}
