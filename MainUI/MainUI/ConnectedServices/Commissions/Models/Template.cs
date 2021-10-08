using System;
using System.Linq;

namespace MainUI.ConnectedServices.Commissions.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public Period Period { get; set; }
        public Period[] Periods { get; set; }

        public string SalesAsString()
        {
            return string.Join(", ", Periods.Reverse().Select(p => Math.Round(p.TotalCommissions)));
        }

        public string VolumeAsString()
        {
            return string.Join(", ", Periods.Reverse().Select(p => Math.Round(p.TotalVolume)));
        }
    }
}
