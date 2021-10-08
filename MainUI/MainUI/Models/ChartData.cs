using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainUI.Models
{
    public class ChartData
    {
        public List<ChartDataRow> Data = new List<ChartDataRow>();
        public string Title { get; set; }

        public string ToValueString()
        {
            return string.Join(',', Data.Select(x => x.Value).ToArray());
        }

        public string ToTitleString()
        {
            return @"""" + string.Join(@""",""", Data.Select(x => x.Title).ToArray()) + @"""";
        }
    }
}
