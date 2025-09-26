using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.UnitOfWork;
using FidenzApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetCustomerByIdAsync(string id);
        Task<IEnumerable<ZipGroupDto>> GetAllByZipAsync();
        Task<IEnumerable<CustomerDto>> GetAllBySearchAsync(string search);
        Task UpdateCustomer(string id, updateCustomerDto updateCustomer);
        Task <ActionResult<double>> getDistance(string id, double latitude, double longitude);

    }
}
