using FidenzApp.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
    }
}
