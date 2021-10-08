namespace MainUI.ConnectedServices.Commissions.Models
{
    public class RankAdvance
    {
        public string NodeId { get; set; }
        public int RankId { get; set; }
        public string RankName { get; set; }
        public RankAdvanceValue[] RankAdvanceValues { get; set; }
    }
}
