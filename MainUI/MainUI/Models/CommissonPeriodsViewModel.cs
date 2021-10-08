using MainUI.ConnectedServices.Commissions.Models;
using System;
using System.Collections.Generic;

namespace MainUI.Models
{
    public class CommissonPeriodsViewModel
    {
        public DateTime Prev;
        public DateTime Next;
        public List<Period> Periods { get; set; }
    }
}
