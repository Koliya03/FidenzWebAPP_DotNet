using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.UnitOfWork;
using FidenzApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Application.Interfaces.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customers>> GetAllAsync();
        Task<Customers> GetCustomerByIdAsync(string id);
        Task<IEnumerable<Customers>> GetAllByZipAsync();
        Task<IEnumerable<Customers>> GetAllBySearchAsync(string search);
        Task UpdateCustomer(string id, updateCustomerDto updateCustomer);
        Task <double> getDistance(string id, double latitude, double longitude);
    }
}
