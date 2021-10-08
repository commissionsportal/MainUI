using MainUI.ConnectedServices.Commissions.Models;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Interfaces
{
    public interface ICommissionDetailRepository
    {
        Task<Template> GetCustomerCommissionDetail(string customerId, long templateId, long periodId);

        Task<Models.Period> GetBonuses(string templateId, string periodId, string group, string groupValue);

    }
}
