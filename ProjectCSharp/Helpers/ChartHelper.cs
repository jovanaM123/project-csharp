using ProjectCSharp.Models;
using ProjectCSharp.Response;
using System.Linq;

namespace ProjectCSharp.Helpers
{
    public static class ChartHelper
    {
        public static ChartData GetPieChartData(List<EmployeeModel> employees)
        {
            var totalWorkedTime = employees.Select(e => e.TotalWorkedHours).ToList();
            var totalTime = totalWorkedTime.Sum();
            var percentageData = totalWorkedTime.Select(time => (time / (double)totalTime) * 100).ToList();

            var pieChartDatasets = new List<object> { new { data = percentageData } };
            var pieChartLabels = employees.Select(e => string.IsNullOrEmpty(e.EmployeeName) ? "Unknown" : e.EmployeeName).ToList();

            return new ChartData { Datasets = pieChartDatasets, Labels = pieChartLabels };
        }
    }
}
