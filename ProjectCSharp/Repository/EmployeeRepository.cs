using Newtonsoft.Json;
using ProjectCSharp.Models;
using ProjectCSharp.Repository.Interfaces;
using ProjectCSharp.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectCSharp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string? ApiEndpoint;
        public EmployeeRepository(IConfiguration configuration)
        {
            ApiEndpoint = configuration.GetValue<string>("ApiEndpoint");
        }

        public async Task<List<EmployeeModel>?> GetEmployees()
        {
            if (ApiEndpoint is null) throw new Exception("Error happened");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = await client.GetFromJsonAsync<List<Employee>>(ApiEndpoint);

                    if (data is not null)
                    {
                        var employees = new List<EmployeeModel>();

                        foreach (var employee in data.Where(e => e.DeletedOn == null))
                        {
                            var existingEmployee = employees.FirstOrDefault(x => x.EmployeeName == employee.EmployeeName);
                            var totalWorkedHoursThatDay = CalculateTotalHours(employee);

                            if (existingEmployee != null)
                            {
                                existingEmployee.TotalWorkedHours += totalWorkedHoursThatDay;
                            }
                            else
                            {
                                var newEmployee = new EmployeeModel
                                {
                                    EmployeeName = employee.EmployeeName,
                                    TotalWorkedHours = totalWorkedHoursThatDay
                                };

                                employees.Add(newEmployee);
                            }
                        }

                        var sortedEmployees = employees.OrderByDescending(e => e.TotalWorkedHours).ToList();

                        return sortedEmployees;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error happened when getting data: {ex.Message}");
                }

                return null;
            }
        }

        private double CalculateTotalHours(Employee employee)
        {
            DateTime startTime = DateTime.Parse(employee.StarTimeUtc);
            DateTime endTime = DateTime.Parse(employee.EndTimeUtc);

            TimeSpan duration = endTime - startTime;

            return duration.TotalHours;
        }

    }
}
