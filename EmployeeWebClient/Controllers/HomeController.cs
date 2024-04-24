using EmployeeWebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        // to get all employees
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("EmployeeService");
            var response = await client.GetAsync("api/Employee");

            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
                return View(employees);
            }
            else
            {
                // Handle the error
                return View("Error");
            }
        }


        // to create a new employee 
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            var client = _clientFactory.CreateClient("EmployeeService");
            var response = await client.PostAsJsonAsync("api/Employee", employee);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the error
                return View("Error");
            }
        }


        // to get an employee by id
        public async Task<IActionResult> GetEmployee(int id)
        {
            var client = _clientFactory.CreateClient("EmployeeService");
            var response = await client.GetAsync($"api/Employee/{id}");

            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadFromJsonAsync<Employee>();
                return View(employee);
            }
            else
            {
                // Handle the error
                return View("Error");
            }
        }


        // to update an employee
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var client = _clientFactory.CreateClient("EmployeeService");
            var response = await client.GetAsync($"api/Employee/{id}");

            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadFromJsonAsync<Employee>();
                return View(employee);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Employee/UpdateEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployee(int id, [Bind("Id, Name, Position, Office, Salary")] Employee employee)
        {
            var client = _clientFactory.CreateClient("EmployeeService");
            var response = await client.PutAsJsonAsync($"api/Employee/{id}", employee);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error
                return View("Error");
            }
        }


        // to delete an employee

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var client = _clientFactory.CreateClient("EmployeeService");
            var response = await client.DeleteAsync($"api/Employee/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error
                return View("Error");
            }
        }




    }
}
