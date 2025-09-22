using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Infranstructure.Seed
{
    public interface ISeeder
    {
        Task SeedUserAsync();
        Task SeedCustomerAsync();
    }
}
