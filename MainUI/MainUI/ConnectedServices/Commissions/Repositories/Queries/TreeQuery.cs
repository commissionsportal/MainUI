using MainUI.ConnectedServices.Commissions.Models;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Commissions.Repositories.Queries
{
    public static class TreeQuery
    {
        public static async Task<Tree[]> GetTree(this IQueryClient client, string nodeId)
        {
            var result = await client.PostQuery<TreeQueryResult>(@"{
  trees {
    name
    node(id: """ + nodeId + @""") {
      nodeId
      uplineId
      nodes {
        nodeId
      }
    }
  }
}
");

            return result.Trees;
        }
    }

    internal class TreeQueryResult
    {
        public Tree[] Trees { get; set; }
    }

}
