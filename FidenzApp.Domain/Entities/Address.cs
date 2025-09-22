using Microsoft.EntityFrameworkCore;
namespace FidenzApp.Domain.Entities
{
    [Owned]
    public class Address
    {
        public int number { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zipcode { get; set; }
    }
}
