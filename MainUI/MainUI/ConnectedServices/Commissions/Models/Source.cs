using System;

namespace MainUI.ConnectedServices.Commissions.Models
{
    public class Source
    {
        public long Id { get; set; }
        public string NodeId { get; set; }
        public DateTime Date { get; set; }
        public string SourceGroupId { get; set; }
        public string Value { get; set; }
        public string ExternalId { get; set; }
    }
}
