using FidenzApp.Application.DTO;
using FidenzApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace FidenzApp.Application.Interfaces.Repositories
{
    public  interface ICustomerRepository : IRepository<Customers>
    {
        Task UpdateAsync(Customers entity);

        Task<IEnumerable<ZipGroupDto>> GetAllByZipAsync();
        Task seedDataFromJsonAsync(List<Customers> JsonCustomers);

    }
}
