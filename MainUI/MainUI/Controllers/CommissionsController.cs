using MainUI.Authentication;
using MainUI.ConnectedServices.Commissions.Interfaces;
using MainUI.ConnectedServices.Commissions.Models;
using MainUI.ConnectedServices.Customers.Interfaces;
using MainUI.ConnectedServices.Customers.Models;
using MainUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainUI.Controllers
{
    [Authorize]
    public class CommissionsController : Controller
    {
        private readonly ICommissionPeriodRepository _commissionPeriodRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICommissionDetailRepository _commissionDetailRepository;

        public CommissionsController(ICommissionPeriodRepository commissionPeriodRepository,
            ICustomerRepository customerRepository,
            ICommissionDetailRepository commissionDetailRepository)
        {
            _commissionPeriodRepository = commissionPeriodRepository;
            _customerRepository = customerRepository;
            _commissionDetailRepository = commissionDetailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CommissionPeriods(DateTime? id)
        {
            var pagesize = 10;
            var date = id.HasValue ? id.Value.AddDays(1) : DateTime.UtcNow;
            var periodArray = await _commissionPeriodRepository.GetPeriods(date, pagesize);

            var model = new CommissonPeriodsViewModel
            {
                Next = date.AddMonths(pagesize),
                Prev = date.AddMonths(-pagesize),
                Periods = periodArray.ToList()
            };

            model.Periods.Sort((x, y) => x.Begin.CompareTo(y.Begin));

            return View(model);
        }

        private decimal CalcTrend(IEnumerable<int> data)
        {
            return CalcTrend(data.Select(x => (decimal)x));
        }

        private decimal CalcTrend(IEnumerable<decimal> data)
        {
            using (var enumerator = data.GetEnumerator())
            {
                var lastFinal = 0m;

                //Average the remaining values.
                var starting = 0m;
                var count = 0;
                while (enumerator.MoveNext())
                {
                    starting += lastFinal;
                    lastFinal = enumerator.Current;
                    count++;
                }
                if (count > 1)
                {
                    starting = starting / (count - 1);

                    var final = lastFinal;
                    return GetPercentIncrease(starting, final);
                }

                return 0;
            }
        }

        private decimal GetPercentIncrease(decimal starting, decimal final)
        {
            //returning 100% here is technically incorrect.
            if (starting < 1) return final < 1 ? 0m : 100m;

            var res = final - starting;
            res = res / Math.Abs(starting);
            res = res * 100;

            return Math.Round(res, 1);
        }

        public async Task<IActionResult> CommissionPeriodDetail(long? templateId, long? periodId)
        {
            if (!templateId.HasValue || !periodId.HasValue)
            {
                var defaultVals = await _commissionPeriodRepository.GetCurrentPeriodSummary();
                templateId = templateId.HasValue ? templateId.Value : defaultVals.TemplateId;
                periodId = periodId.HasValue ? periodId.Value : defaultVals?.PeriodId;
            }

            var qryResult = await _commissionPeriodRepository.GetPeriodDetail(templateId.Value, periodId.Value);
            //qryResult.Periods = qryResult.Periods.Reverse().ToArray();
            var curPeriod = qryResult.Periods.Where(x => x.Begin == qryResult.Period.Begin).First();

            var model = new CommissionPeriodDetailViewModel
            {
                Period = new ConnectedServices.Commissions.Models.Period
                {
                    Begin = qryResult.Period.Begin,
                    End = qryResult.Period.End,
                    Id = qryResult.Period.Id,
                    CompensationPlanId = qryResult.Period.CompensationPlanId
                },
                BonusesByTitle = qryResult.Period.TitleBonus,
                BonusesByRank = qryResult.Period.RankBonus,

                TotalCommissions = curPeriod.TotalCommissions,
                TotalCommissionTrend = CalcTrend(qryResult.Periods.Select(x => x.TotalCommissions)),
                CommissionHistory = new ChartData { Title = "Commissions" },

                TotalCustomers =  curPeriod.TotalCustomers,
                CustomersPaid = curPeriod.TotalCustomersPaid,
                CustomersPaidTrend = CalcTrend(qryResult.Periods.Select(x => x.TotalCustomersPaid)),

                CustomerPaid12 = 0,
                CustomerPaid6 = 0,
                CustomerPaidTM = curPeriod.TotalCustomersPaid,

                TotalFtVolume = qryResult.FirstTime[1].FirstTimeBonus.Sum(x => x.TotalVolume),
                TotalFtVolumeTrend = CalcTrend(qryResult.FirstTime.Select(x => x.FirstTimeBonus.Sum(y => y.TotalVolume))),
                TotalFtVolumeHistory1 = new ChartData { Title = "This Period" },
                TotalFtVolumeHistory2 = new ChartData { Title = "Last Period" },

                TotalVolume = curPeriod.TotalVolume,
                TotalVolumeTrend = CalcTrend(qryResult.Periods.Select(x => x.TotalVolume)),
                TotalVolumeHistory = new ChartData { Title = "Volume" },

                DailyVolumeHistoryW = new ChartData { Title = "Total Daily Volume" },
                DailyVolumeHistoryR = new ChartData { Title = "R" },
                DailyVolumeHistoryV = new ChartData { Title = "V" },

                DailyCommissionTotal = new ChartData { Title = "Total Daily Commissions" },
                TodayCommissionTotal = qryResult.Period.DateTypeBonus.Where(x => x.Description == DateTime.UtcNow.ToShortDateString()).FirstOrDefault()?.PaidAmount ?? 0m,
                YesterdayCommissionTotal = qryResult.Period.DateTypeBonus.Where(x => x.Description == DateTime.UtcNow.AddDays(-1).ToShortDateString()).FirstOrDefault()?.PaidAmount ?? 0m
            };

            model.TodayCommissionTotalIncPercent = GetPercentIncrease(model.YesterdayCommissionTotal, model.TodayCommissionTotal);

            var paidToList = qryResult.Period.TopEarners.Select(x => x.Description);
            var customerList = await _customerRepository.GetCustomers(paidToList);
            var dict = customerList?.ToDictionary(x => x.Id, y => y);

            model.TopEarners = qryResult.Period.TopEarners.Select(x =>
            {
                var bonus = new Bonus
                {
                    Amount = x.PaidAmount,
                    NodeId = x.Description,
                    Rank = "1",
                    Volume = x.TotalVolume
                };

                dict.TryGetValue(bonus.NodeId, out Customer ptCustomer);
                bonus.Customer = ptCustomer ?? new Customer { Id = bonus.NodeId, FullName = bonus.NodeId, ProfileImage = "1.jpg" };

                return bonus;

            }).ToArray();


            model.CommissionHistory.Data.AddRange(qryResult.Periods.Select(x =>
            {
                return new ChartDataRow
                {
                    Title = x.Begin.ToShortDateString(),
                    Value = x.TotalCommissions
                };
            }));


            model.TotalVolumeHistory.Data.AddRange(qryResult.Periods.Select(x =>
            {
                return new ChartDataRow
                {
                    Title = x.Begin.ToShortDateString(),
                    Value = x.TotalVolume
                };
            }));

            var thisPeriod = qryResult.FirstTime.Where(x => x.Begin == qryResult.Period.Begin).FirstOrDefault().FirstTimeBonus.ToDictionary(x => x.Description);
            var lastPeriod = qryResult.FirstTime.Where(x => x.Begin != qryResult.Period.Begin).FirstOrDefault().FirstTimeBonus.ToDictionary(x => x.Description);
            var lp = qryResult.FirstTime.Where(x => x.Begin != qryResult.Period.Begin).FirstOrDefault();

            var daylyCommission = qryResult.Period.DateTypeBonus.ToDictionary(x => x.Description);
            var daylyVolume = qryResult.Period.VolumeSummary.ToDictionary(x => x.Description);

            for (int i = 0; i < DateTime.DaysInMonth(qryResult.Period.Begin.Year, qryResult.Period.Begin.Month); i++)
            {
                var lpDateKey = ToKeyString(lp.Begin, i + 1);
                var lpValue = lastPeriod.ContainsKey(lpDateKey) ? lastPeriod[lpDateKey].TotalVolume : 0;
                model.TotalFtVolumeHistory2.Data.Add(new ChartDataRow { Title = lpDateKey, Value = lpValue });

                var dateKey = ToKeyString(qryResult.Period.Begin, i + 1);

                var tpValue = thisPeriod.ContainsKey(dateKey) ? thisPeriod[dateKey].TotalVolume : 0;
                model.TotalFtVolumeHistory1.Data.Add(new ChartDataRow { Title = dateKey, Value = tpValue });

                var dvWValue = daylyVolume.ContainsKey(dateKey) ? daylyVolume[dateKey].TotalVolume : 0;
                model.DailyVolumeHistoryW.Data.Add(new ChartDataRow { Title = dateKey, Value = dvWValue });

                //var dvRValue = daylyCommission.ContainsKey(dateKey) ? daylyCommission[dateKey].TotalVolume : 0;
                //model.DailyVolumeHistoryR.Data.Add(new ChartDataRow { Title = dateKey, Value = dvRValue });

                //var dvVValue = daylyCommission.ContainsKey(dateKey) ? daylyCommission[dateKey].TotalVolume : 0;
                //model.DailyVolumeHistoryV.Data.Add(new ChartDataRow { Title = dateKey, Value = dvVValue });

                var dcValue = daylyCommission.ContainsKey(dateKey) ? daylyCommission[dateKey].PaidAmount : 0;
                model.DailyCommissionTotal.Data.Add(new ChartDataRow { Title = dateKey, Value = dcValue });
            }

            return View(model);
        }

        private string ToKeyString(DateTime date, int day)
        {
            var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            if (day > lastDay) return string.Empty;

            var lpKeyDate = new DateTime(date.Year, date.Month, day);
            return lpKeyDate.ToString("yyyy-MM-dd");
        }

        public async Task<IActionResult> CommssionBonusSummary(string templateId, string periodId, string group, string groupValue)
        {
            var period = await _commissionDetailRepository.GetBonuses(templateId, periodId, group, groupValue);

            var bonuses = period.Bonuses;

            var paidToList = bonuses.Select(x => x.NodeId);
            var customerList = await _customerRepository.GetCustomers(paidToList);
            var dict = customerList?.ToDictionary(x => x.Id, y => y);

            foreach (var bonus in bonuses)
            {
                dict.TryGetValue(bonus.NodeId, out Customer ptCustomer);
                bonus.Customer = ptCustomer ?? new Customer { Id = bonus.NodeId, FullName = bonus.NodeId, ProfileImage = "1.jpg" };
            }

            return View(new BonusSummaryViewModel { BonusGroupValue = groupValue, Bonuses = bonuses, TemplateId = templateId, Period = period });
        }

        public IActionResult Unreleased()
        {
            return View();
        }
    }
}