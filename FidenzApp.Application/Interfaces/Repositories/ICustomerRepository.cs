using FidenzApp.Application.DTO;
using FidenzApp.Domain.Entities;

namespace FidenzApp.Application.Interfaces.Repositories
{
    public  interface ICustomerRepository : IRepository<Customers>
    {
        Task UpdateAsync(Customers entity);

        Task<IEnumerable<ZipGroupDto>> GetAllByZipAsync();
        Task seedDataFromJsonAsync(List<Customers> JsonCustomers);

    }
}
