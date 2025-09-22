using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FidenzApp.Application.Interfaces.Repositories;
using FidenzApp.Domain.Data;
using FidenzApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FidenzApp.Infranstructure.Repositories
{
    public class CustomerRepository : Repository<Customers>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Customers entity)
        {
            _db.Customers_Tb.Update(entity);  
            await _db.SaveChangesAsync();
        }

        public async Task seedDataFromJsonAsync(List<Customers> JsonCustomers)
        {

            foreach (var c in JsonCustomers)
            {
                if (!await _db.Customers_Tb.AnyAsync(x => x._id == c._id))
                {
                    await _db.AddAsync(c);
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}

