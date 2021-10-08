using MainUI.ConnectedServices.Commissions.Models;

namespace MainUI.Models
{
    public class BonusSummaryViewModel
    {
        public string TemplateId { get; set; }
        public Period Period { get; set; }
        public string BonusGroupValue { get; set; }
        public Bonus[] Bonuses { get; set; }
    }
}
