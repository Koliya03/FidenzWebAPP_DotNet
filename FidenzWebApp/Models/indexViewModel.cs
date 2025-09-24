
using FidenzApp.Application.DTO;

namespace FidenzWebApp.Models
{
    public class indexViewModel
    {
        public List<CustomerDto> customerList = new List<CustomerDto>();
        public bool isGroupedByZip = false;
    }
}
