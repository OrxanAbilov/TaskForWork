using Microsoft.AspNetCore.Mvc;
using TaskForWork.Models.DTOs;
using TaskForWork.Repositories;

namespace TaskForWork.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportRepository reportRepository;
        public ReportsController(IReportRepository _reportRepository)
        {
            reportRepository= _reportRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetSaleReport()
        {
            List<SaleReportDto> report = reportRepository.GetSaleReport();
            return Json(report);
        }
    }
}
