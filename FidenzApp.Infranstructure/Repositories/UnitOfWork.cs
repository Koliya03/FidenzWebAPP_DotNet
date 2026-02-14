using FidenzApp.Application.Interfaces.Repositories;
using FidenzApp.Application.Interfaces.UnitOfWork;
using FidenzApp.Domain.Data;

namespace FidenzApp.Infranstructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICustomerRepository CustomerRepository{ get; set; }
        
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CustomerRepository = new CustomerRepository(db);
        }
       
    }
}
