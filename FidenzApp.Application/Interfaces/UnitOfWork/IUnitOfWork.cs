using FidenzApp.Application.Interfaces.Repositories;

namespace FidenzApp.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
    }
}
