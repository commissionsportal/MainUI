using MainUI.Authentication;
using MainUI.ConnectedServices.Commissions.Interfaces;
using MainUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MainUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICommissionPeriodRepository _commissionPeriodRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ICommissionPeriodRepository commissionPeriodRepository, ILogger<HomeController> logger)
        {
            _logger = logger;
            _commissionPeriodRepository = commissionPeriodRepository;
        }

        public async Task<IActionResult> Index()
        {
            var defaultVals = await _commissionPeriodRepository.GetCurrentPeriodSummary();
            return Redirect($"/Commissions/CommissionPeriodDetail?templateId={defaultVals.TemplateId}&periodId={defaultVals.PeriodId}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
