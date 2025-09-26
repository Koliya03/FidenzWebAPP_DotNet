using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.Services;
using FidenzApp.Application.Interfaces.UnitOfWork;
using FidenzApp.Domain.Entities;
using FidenzApp.Infranstructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FidenzWebApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
           
            var customersList =await customerService.GetAllAsync();
            return Ok(customersList);

        }


        [HttpGet]
        [Route("customerID/{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Id can not be empty ");
            }
            var customer =await customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return BadRequest("Can not find the Customer");
            }
            return Ok(customer);
        }


        [HttpGet]
        [Route("GroupByZipCode")]
        public async Task<IActionResult> GetCustomerByZipCode()
        {
            var customersByZip = await customerService.GetAllByZipAsync();
            return Ok(customersByZip);
           
        }


        [HttpGet]
        [Route("byCustomerSearch/{search}")]
        public async Task<IActionResult> GetCustomerByEarch(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return BadRequest("search text is required.");

            var customers =await customerService.GetAllBySearchAsync(search);
            return Ok(customers);
        }

        [HttpPatch]
        [Route("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, updateCustomerDto updateCustomer)
        {
            var customer =await customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return BadRequest("can not find Customer");
            }
            await customerService.UpdateCustomer(id,updateCustomer);
            return Ok(customer);
        }

        [HttpGet]
        [Route("Distance/{id}/Latitude/{latitude:double}/longitude/{longitude:double}")]
        public async Task<IActionResult> GetDistance(string id, double latitude, double longitude)
        {
          
           var Response = await customerService.getDistance(id, latitude, longitude);
            if (Response == null)
            {
               return BadRequest("Can not find the Customer");
            }
            else
            {
                double km = Response.Value;
                return Ok(km);
            }
               

        }
  
    }
}


    
