using System.Collections.Generic;

namespace MainUI.ConnectedServices.Commissions.Models
{
    public class Node
    {
        public string NodeId { get; set; }
        public string UplineId { get; set; }
        public Node[] Nodes { get; set; }

        public List<string> GetIds()
        {
            var ids = new List<string>();

            ids.Add(NodeId);
            if (!string.IsNullOrWhiteSpace(UplineId)) ids.Add(UplineId);
            if (Nodes != null)
            {
                foreach (var node in Nodes)
                {
                    ids.AddRange(node.GetIds());
                }
            }

            return ids;
        }
    }
}
