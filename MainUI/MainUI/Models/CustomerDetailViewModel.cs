using MainUI.ConnectedServices.Commissions.Models;
using MainUI.ConnectedServices.Customers.Models;
using System.Collections.Generic;
using System.Linq;

namespace MainUI.Models
{
    public class CustomerDetailViewModel
    {
        public Period Period { get; set; }
        public Customer Customer { get; set; }
        public Tree[] Trees { get; set; }
        public Dictionary<string, Customer> Customers { get; set; }
        public Template Values { get; set; }


        public int EnrollmentCount
        {
            get
            {
                return Trees[0].Node?.Nodes.Count() ?? 0;
            }
        }

        public decimal DownlineSalesVolume
        {
            get
            {
                var dsv = Period.CommissionValues.Where(x => x.ValueId == "DSV").FirstOrDefault();

                if (decimal.TryParse(dsv?.Value, out decimal result))
                {
                    return result;
                }

                return 0m;
            }
        }

        public int TotalWholesale
        {
            get
            {
                return EnrollmentCount - TotalRetail;
            }
        }

        public Customer Upline
        {
            get
            {
                if (Customers != null)
                {
                    var uplineId = Trees[0].Node?.UplineId;
                    if (uplineId != null)
                    {
                        return Customers[uplineId];
                    }
                }

                return new Customer { };
            }
        }

        public bool HasUpline()
        {
            return Trees[0].Node?.UplineId != null;
        }

        public Customer[] WholesaleCustomers
        {
            get 
            {
                var result = new List<Customer>();
                if (Trees[0].Node != null)
                {
                    foreach (var node in Trees[0].Node.Nodes)
                    {
                        result.Add(Customers[node.NodeId]);
                    }
                }

                return result.Take(15).ToArray();
            }
        }

        public decimal TotalCommissions
        {
            get
            {
                return Period.Bonuses.Sum(x => x.Amount);
            }
        }

        public decimal Unreleased
        {
            get
            {
                return TotalCommissions - Period.Bonuses.Sum(x => x.Released);
            }
        }

        public int TotalRetail
        {
            get
            {
                int res = 0;
                foreach (var tree in Trees)
                {
                    if (tree.Node != null)
                    {
                        int treeCount = 0;
                        foreach (var node in tree?.Node?.Nodes)
                        {
                            if (Customers[node.NodeId].CustomerType?.Trim() != "1") treeCount++;
                        }
                        if (treeCount > res) res = treeCount;
                    }
                }

                return res;
            }
        }

        public RankAdvance CurrentRank
        {
            get
            {
                var curRank = CurrentRankId;

                return Period.RankAdvance.Where(x => x.RankId == curRank).FirstOrDefault() 
                    ?? new RankAdvance { RankName = "Customer", RankId = 0, NodeId = Customer.Id, RankAdvanceValues = new RankAdvanceValue[0] };
            }
        }

        public int CurrentRankId
        {
            get
            {
                var curRank = 0;

                if (int.TryParse(Period.GetCommissionValue("Rank"), out int result))
                {
                    curRank = result;
                }

                return curRank > 0 ? curRank : 10;
            }
        }

        public bool IsCurrent(int rankId)
        {
            return CurrentRankId == rankId;
        }

        public bool HasLast(int rankId)
        {
            return rankId > 10;
        }

        public bool HasNext(int rankId)
        {
            return rankId < 100;
        }

        public string RankActiveText(int rankId)
        {
            return rankId == CurrentRankId ? "active show" : string.Empty;
        }

        public int GetPercent(decimal x, decimal y)
        {
            var percent = (x / y) * 100;

            if (percent > 100) percent = 100;

            var result = System.Math.Round(percent, 0);
            return System.Convert.ToInt32(result);
        }

        public string GetValueText(string valueId)
        {
            if (valueId == "PSVc") return "Personal Sales Volume";
            if (valueId == "DSV") return "Downline Sales Volume";
            if (valueId == "EL") return "Executive Legs";
            
            return valueId;
        }
    }
}
