using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectCSharp.Helpers;
using ProjectCSharp.Models;
using ProjectCSharp.Repository.Interfaces;

namespace ProjectCSharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployees();
                if (employees is null) return NotFound();
                var chartData = ChartHelper.GetPieChartData(employees);

                ViewBag.PieChartDatasets = chartData.Datasets;
                ViewBag.PieChartLabels = chartData.Labels;

                return View(employees);               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
