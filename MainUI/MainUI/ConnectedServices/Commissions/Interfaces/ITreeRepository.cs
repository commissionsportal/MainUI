using MainUI.ConnectedServices.Commissions.Models;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Interfaces
{
    public interface ITreeRepository
    {
        Task<Tree[]> GetTree(string nodeId);
    }
}
