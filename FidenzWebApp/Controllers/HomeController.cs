
using Azure;
using FidenzApp.Application.DTO;
using FidenzApp.Domain.Entities;
using FidenzWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace FidenzWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;   
        public HomeController(IWebHostEnvironment env)
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:7299/api/") };
        }
       
        [HttpGet]
        [AllowAnonymous]          
        public IActionResult Login()
        {
            return View();         
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto userdto)
        {
            var response =await _client.PostAsJsonAsync($"Auth/login", userdto);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Incorrect UserId or Password!";
                return View();
            }
            string token =await response.Content.ReadAsStringAsync();

            Response.Cookies.Append("Auth", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            });



            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Auth");
            return RedirectToAction("Login");
        }

        private void AttachBearerFromCookie()
        {
            if (Request.Cookies.TryGetValue("Auth", out var token) && !string.IsNullOrWhiteSpace(token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _client.DefaultRequestHeaders.Authorization = null;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index(string Search)
        {
            AttachBearerFromCookie();

            var indexVM = new indexViewModel();
            HttpResponseMessage response;

            if (!string.IsNullOrWhiteSpace(Search))
            {
                response = await _client.GetAsync($"Customer/byCustomerSearch/{Search}");
            }
            else
            {
                response = await _client.GetAsync("Customer");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return RedirectToAction(nameof(Login));
            }
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                indexVM.customerList = JsonConvert.DeserializeObject<List<Customers>>(data);
                indexVM.isGroupedByZip = false;
            }

            return View(indexVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            AttachBearerFromCookie();
            var response =await _client.GetAsync($"Customer/customerID/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }

            if (response.IsSuccessStatusCode)
            {
                string data =await response.Content.ReadAsStringAsync();
                var customerbyID = JsonConvert.DeserializeObject<Customers>(data);
                return View(customerbyID);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Customers obj)
        {

            AttachBearerFromCookie();

            var cutomerModel = new updateCustomerDto(obj.name, obj.email, obj.phone);
            if (!TryValidateModel(cutomerModel))       
                return View(obj);
            var response =await _client.PutAsJsonAsync($"Customer/{obj._id}", cutomerModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
          
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> ViewInfo(string id)
        {
            AttachBearerFromCookie();

            var response =await _client.GetAsync($"Customer/customerID/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }

            if (response.IsSuccessStatusCode)
            {
                string data =await  response.Content.ReadAsStringAsync();
                var customerbyID = JsonConvert.DeserializeObject<Customers>(data);
                return View(customerbyID);
            }
            return NotFound();
        }

        [HttpGet()]
        public async Task<IActionResult> Distance(string id)
        {
            AttachBearerFromCookie();

            var response =await  _client.GetAsync($"Customer/customerID/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }
            if (response.IsSuccessStatusCode)
            {
                string data =await response.Content.ReadAsStringAsync();
                var customerbyID = JsonConvert.DeserializeObject<Customers>(data);
                var vm = new DistanceCalModel(customerbyID._id, customerbyID.name);
                return View(vm);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Distance(DistanceCalModel vm)
        {
            AttachBearerFromCookie();
            var response = await _client.GetAsync($"Customer/Distance/{vm.Id}/Latitude/{vm.Latitude}/longitude/{vm.Longitude}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                vm.DistanceKm = JsonConvert.DeserializeObject<double>(data);
            }
            else
            {
                vm.Error = "Customer not found";
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ZipSearch()
        {
            AttachBearerFromCookie();

            var response =await _client.GetAsync("Customer/GroupByZipCode");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }
            var indexVM = new indexViewModel();
            if (response.IsSuccessStatusCode)
            {
                var json =await  response.Content.ReadAsStringAsync();
                indexVM.customerList = JsonConvert.DeserializeObject<List<Customers>>(json);
                indexVM.isGroupedByZip = true;
            }
            return View("Index", indexVM);
        }

       
       
    }

}
