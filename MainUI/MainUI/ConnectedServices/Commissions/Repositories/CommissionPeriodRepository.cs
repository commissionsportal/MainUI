using MainUI.ConnectedServices.Commissions.Interfaces;
using MainUI.ConnectedServices.Commissions.Models;
using MainUI.ConnectedServices.Commissions.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories
{
    public class CommissionPeriodRepository : ICommissionPeriodRepository
    {
        private readonly IQueryClient _client;

        public CommissionPeriodRepository(IQueryClient client)
        {
            _client = client;
        }

        public async Task<CurrentPeriodDetail> GetCurrentPeriodSummary()
        {
            return await _client.GetCurrentPeriodSummary();
        }

        public async Task<CompensationPlan> GetPeriodDetail(string templateId, string periodId)
        {
            return await _client.GetPeriodDetail(templateId, periodId);
        }

        public async Task<IEnumerable<Models.Period>> GetPeriods(DateTime date, int pageSize)
        {
            return await _client.GetPeriods(date, pageSize);
        }
    }
}
