using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FidenzApp.Application.DTO;
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

        public async Task<IEnumerable<ZipGroupDto>> GetAllByZipAsync()
        {
            return await _db.Customers_Tb
                .GroupBy(c => c.address.zipcode)
                .OrderBy(g => g.Key)
                .Select(group => new ZipGroupDto
                {
                    ZipCode = group.Key,
                    Customers = group
                        .Select(c => new CustomerDto
                        {
                            _id = c._id,
                            index = c.index,
                            age = c.age,
                            eyeColor = c.eyeColor,
                            name = c.name,
                            gender = c.gender,
                            company = c.company,
                            email = c.email,
                            phone = c.phone,
                            about = c.about,
                            registered = c.registered,
                            latitude = c.latitude,
                            longitude = c.longitude,
                            tags = c.tags ?? new List<string>(),
                            address = new AddressDto
                            {
                                number = c.address.number,
                                street = c.address.street,
                                city = c.address.city,
                                state = c.address.state,
                                zipcode = c.address.zipcode
                            }
                        })
                        .ToList()
                })
                .ToListAsync();
        }

    }
}

