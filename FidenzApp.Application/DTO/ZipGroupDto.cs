using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Application.DTO
{
    public class ZipGroupDto
    {
        public int ZipCode { get; set; }
        public List<CustomerDto> Customers { get; set; } = new(); 
    }
}
