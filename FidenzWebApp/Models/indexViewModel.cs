
using FidenzApp.Domain.Entities;

namespace FidenzWebApp.Models
{
    public class indexViewModel
    {
        public List<Customers> customerList = new List<Customers>();
        public bool isGroupedByZip = false;
    }
}
