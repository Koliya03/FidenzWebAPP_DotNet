
using Azure;
using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.Services.IServices;
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
    [Authorize]
    public class HomeController : Controller
    {
        private ICustomerService _customerService;
        private readonly IAuthService _authService;

        public HomeController(IAuthService authService, ICustomerService customerService)
        {
            _authService = authService;
            _customerService = customerService;
        }

        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            return View("Login");         
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto userdto)
        {
            if (userdto == null) {
                return View();
            }
            var token = await _authService.LoginAsync(userdto);
            if (token == null)
            {
                ViewBag.Message = "Incorrect UserId or Password!";
                return View();
            }
            else
            {
                Response.Cookies.Append("Auth", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });
                return RedirectToAction(nameof(Index));
            }   
        }

       
        public IActionResult Logout()
        {
             Response.Cookies.Delete("Auth");
            return RedirectToAction("LoginPage");
        }       
        public async Task<IActionResult> Index(string Search)
        {

            var indexVM = new indexViewModel();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                var customersBySearch = await _customerService.GetAllBySearchAsync(Search);
                indexVM.customerList = customersBySearch.ToList();
            }
            else
            {
               var customersByAll = await _customerService.GetAllAsync();
                indexVM.customerList = customersByAll.ToList();
            }

            indexVM.isGroupedByZip = false;
            return View(indexVM);
        }

        public async Task<IActionResult> Update(string id)
        {

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }


        public async Task<IActionResult> UpdateSave(Customers obj)
        {

            var cutomerModel = new updateCustomerDto(obj.name, obj.email, obj.phone);

            if (!TryValidateModel(cutomerModel))
                return View("Update", obj);

            await _customerService.UpdateCustomer(obj._id, cutomerModel);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ViewInfo(string id)
        {

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }


        public async Task<IActionResult> Distance(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            var vm = new DistanceCalModel(customer._id, customer.name);
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DistanceCalc(DistanceCalModel vm)
        {

            try
            {
                vm.DistanceKm = await _customerService.getDistance(
                    vm.Id,
                    vm.Latitude.Value,
                    vm.Longitude.Value
                );
            }
            catch
            {
                vm.Error = "Customer not found or distance calculation failed.";
            }

            return View("Distance", vm);
        }


        public async Task<IActionResult> ZipSearch()
        {
            var list = await _customerService.GetAllByZipAsync();
            var indexVM = new indexViewModel
            {
                customerList = list?.ToList() ?? new List<CustomerDto>(),
                isGroupedByZip = true
            };
            
            return View("Index", indexVM);
        }       
    }

}
