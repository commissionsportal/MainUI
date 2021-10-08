using Newtonsoft.Json.Linq;

namespace MainUI.ConnectedServices
{
    public class QLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
