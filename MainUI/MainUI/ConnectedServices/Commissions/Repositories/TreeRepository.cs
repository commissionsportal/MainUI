using MainUI.ConnectedServices.Commissions.Interfaces;
using MainUI.ConnectedServices.Commissions.Models;
using MainUI.ConnectedServices.Commissions.Repositories.Queries;
using System;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories
{
    public class TreeRepository : ITreeRepository
    {
        private readonly IQueryClient _client;

        public TreeRepository(IQueryClient client)
        {
            _client = client;
        }

        public Task<Tree[]> GetTree(string nodeId)
        {
            return _client.GetTree(nodeId);
        }
    }
}
