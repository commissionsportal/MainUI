using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainUI.Models
{
    public class TrendHelper //class="icon icon-inline text-green"
    {
        //Download SVG icon from http://tabler-icons.io/i/trending-up
        //Download SVG icon from http://tabler-icons.io/i/trending-down

        private readonly string _trendUpSvg = @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon ms-1 icon-inline text-green"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round""><path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none"" /><polyline points=""3 17 9 11 13 15 21 7"" /><polyline points=""14 7 21 7 21 14"" /></svg>";
        private readonly string _trendDownSvg = @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon ms-1 icon-inline text-red"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round""><path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path><polyline points=""3 7 9 13 13 9 21 17""></polyline><polyline points=""21 10 21 17 14 17""></polyline></svg>";
        private readonly string _trendNone = @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon ms-1 icon-inline text-yellow"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round""><path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none"" /><line x1=""5"" y1=""12"" x2=""19"" y2=""12"" /></svg>";

        public TrendHelper(decimal trend)
        {
            Trend = trend;
        }

        public decimal Trend { get; }

        public string TextColor
        {
            get
            {
                return Trend > 0 ? "green" :
                  Trend < 0 ? "red" : "yellow";
            }
        }

        public string TrendSvg
        {
            get
            {
                return Trend > 0 ? _trendUpSvg :
                  Trend < 0 ? _trendDownSvg : _trendNone;
            }
        }
    }
}
