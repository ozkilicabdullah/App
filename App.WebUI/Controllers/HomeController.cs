using App.Core.Dto;
using App.WebUI.Models;
using App.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserApiService _userApiService;
        public HomeController(ILogger<HomeController> logger, UserApiService userApiService)
        {
            _logger = logger;
            _userApiService = userApiService;
        }

        public async Task<IActionResult> Index(DateTime? BeginDate, DateTime? EndDate)
        {
            if (BeginDate == null)
                BeginDate = DateTime.Now.AddDays(-2);
            if (EndDate == null)
                EndDate = DateTime.Now.AddDays(1);

            ViewBag.BeginDate = BeginDate;
            ViewBag.EndDate = EndDate;

            var reports = await _userApiService.UserRegisterationReports(new UserRegisterationReportRequestDto
            {
                BeginDate = BeginDate.GetValueOrDefault().ToString(),
                EndDate = EndDate.GetValueOrDefault().ToString()
            });

            return View(reports);
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