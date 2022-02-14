using MainUI.Authentication;
using MainUI.ConnectedServices.Commissions.Interfaces;
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
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICommissionDetailRepository _commissionDetailRepository;
        private readonly ICommissionPeriodRepository _commissionPeriodRepository;
        private readonly ITreeRepository _treeRepository;

        public CustomersController(ICustomerRepository customerRepository,
            ICommissionDetailRepository commissionDetailRepository,
            ICommissionPeriodRepository commissionPeriodRepository,
            ITreeRepository treeRepository)
        {
            _customerRepository = customerRepository;
            _commissionDetailRepository = commissionDetailRepository;
            _commissionPeriodRepository = commissionPeriodRepository;
            _treeRepository = treeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CustomerList(int id = 1)
        {
            var PageSize = 15;

            var custsWithCount = await _customerRepository.GetCustomers(PageSize, id);
            var lastPage = (int)Math.Ceiling((double)custsWithCount.TotalCustomers / PageSize);

            var pageRange = new List<int>();
            pageRange.Add(1);
            if (lastPage > 1)
            {
                pageRange.AddRange(Ext.Each(Math.Max(2, id - 2), Math.Min(lastPage, Math.Max(7, id + 3))));
                pageRange.Add(lastPage);
            }

            var model = new CustomerListViewModel
            {
                Customers = custsWithCount.Customers,
                TotalCustomers = custsWithCount.TotalCustomers,
                PageSize = PageSize,
                CurrentPage = id,
                NextPage = id + 1,
                PrevPage = id - 1,
                LastPage = lastPage,
                DisplayPages = pageRange.ToArray()
            };

            return View(model);
        }

        public static class Ext
        {
            public static IEnumerable<int> Each(int start, int end)
            {
                return Enumerable.Range(start, end - start);
            }
        }

        public async Task<IActionResult> CustomerDetail(string id, long templateId, long periodId)
        {
            if (id == "favicon.ico") return null;

            if (templateId == 0 || periodId == 0)
            {
                var defaultVals = await _commissionPeriodRepository.GetCurrentPeriodSummary();
                templateId = defaultVals.TemplateId;
                periodId = defaultVals.PeriodId;
            }

            var comPlanValues = await _commissionDetailRepository.GetCustomerCommissionDetail(id, templateId, periodId);

            var model = new CustomerDetailViewModel
            {
                Period = comPlanValues.Period,
                Customer = await _customerRepository.GetCustomer(id),
                Trees = await _treeRepository.GetTree(id),
                Customers = new Dictionary<string, Customer>(),
                Values = comPlanValues
            };

            var treeIds = new string[0];
            foreach (var tree in model.Trees)
            {
                treeIds = treeIds.Union(tree.GetIds()).ToArray();
            }

            if (treeIds.Length > 0)
            {
                foreach (var customer in await _customerRepository.GetCustomers(treeIds))
                {
                    model.Customers.Add(customer.Id, customer);
                }
            }

            foreach(var tId in treeIds)
            {
                if (!model.Customers.ContainsKey(tId))
                {
                    model.Customers.Add(tId, new Customer
                    {
                        Id = tId,
                        FullName = "Deleted"
                    });
                }
            }

            return View(model);
        }

        public IActionResult CorporateTrees()
        {
            return View();
        }
    }
}