using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmployeeApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:7137/");

                while (true)
                {
                    Console.WriteLine("\n========= Employee Management Menu =========");
                    Console.WriteLine("1. Get All Employees");
                    Console.WriteLine("2. Get Employee By ID");
                    Console.WriteLine("3. Get Employees By Department");
                    Console.WriteLine("4. Add Employee");
                    Console.WriteLine("5. Update Employee");
                    Console.WriteLine("6. Delete Employee");
                    Console.WriteLine("7. Update Employee Email");
                    Console.WriteLine("8. Exit");
                    Console.WriteLine("=================================");
                    Console.Write("Enter your choice: ");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await GetAllEmployees(client);
                            break;
                        case "2":
                            await GetEmployeeById(client);
                            break;
                        case "3":
                            await GetEmployeesByDept(client);
                            break;
                        case "4":
                            await AddEmployee(client);
                            break;
                        case "5":
                            await UpdateEmployee(client);
                            break;
                        case "6":
                            await DeleteEmployee(client);
                            break;
                        case "7":
                            await UpdateEmployeeEmail(client);
                            break;
                        case "8":
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
            }
        }

        static async Task GetAllEmployees(HttpClient client)
        {
            var response = await client.GetAsync("api/employees");
            Console.WriteLine("\nAll Employees:");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        static async Task GetEmployeeById(HttpClient client)
        {
            Console.Write("Enter Employee ID: ");
            string id = Console.ReadLine();
            var response = await client.GetAsync($"api/employees/{id}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        static async Task GetEmployeesByDept(HttpClient client)
        {
            Console.Write("Enter Department Name: ");
            string dept = Console.ReadLine();
            var response = await client.GetAsync($"api/employees/bydept?department={dept}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        static async Task AddEmployee(HttpClient client)
        {
            Console.Write("Enter Employee ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Department: ");
            string department = Console.ReadLine();

            Console.Write("Enter Mobile No (10 digits): ");
            string mobileNo = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            var newEmployee = new
            {
                Id = id,
                Name = name,
                Department = department,
                MobileNo = mobileNo,
                Email = email
            };

            var content = new StringContent(JsonConvert.SerializeObject(newEmployee), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/employees", content);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        static async Task UpdateEmployee(HttpClient client)
        {
            Console.Write("Enter Employee ID to Update: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Updated Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Updated Department: ");
            string department = Console.ReadLine();

            Console.Write("Enter Updated Mobile No: ");
            string mobileNo = Console.ReadLine();

            Console.Write("Enter Updated Email: ");
            string email = Console.ReadLine();

            var updatedEmployee = new
            {
                Id = id,
                Name = name,
                Department = department,
                MobileNo = mobileNo,
                Email = email
            };

            var content = new StringContent(JsonConvert.SerializeObject(updatedEmployee), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/employees/{id}", content);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        static async Task DeleteEmployee(HttpClient client)
        {
            Console.Write("Enter Employee ID to Delete: ");
            string id = Console.ReadLine();
            var response = await client.DeleteAsync($"api/employees/{id}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        static async Task UpdateEmployeeEmail(HttpClient client)
        {
            Console.Write("Enter Employee ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter New Email: ");
            string newEmail = Console.ReadLine();

            var content = new StringContent(JsonConvert.SerializeObject(new { Email = newEmail }), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/employees/{id}/email")
            {
                Content = content
            };
            var response = await client.SendAsync(request);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
