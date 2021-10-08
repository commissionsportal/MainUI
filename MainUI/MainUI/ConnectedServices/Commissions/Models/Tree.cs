using System.Collections.Generic;

namespace MainUI.ConnectedServices.Commissions.Models
{
    public class Tree
    {
        public string Name { get; set; }
        public Node Node { get; set; }

        public string[] GetIds()
        {
            List<string> ids = new List<string>();

            if (Node != null)
            {
                ids.AddRange(Node.GetIds());
            }

            return ids.ToArray();
        }
    }
}
