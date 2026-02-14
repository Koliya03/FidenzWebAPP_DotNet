using FidenzApp.Application.DTO;
using Microsoft.AspNetCore.Mvc;

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
