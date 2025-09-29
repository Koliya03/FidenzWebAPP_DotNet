namespace FidenzApp.Application.DTO
{
    public class ZipGroupDto
    {
        public int ZipCode { get; set; }
        public List<CustomerDto> Customers { get; set; } = new(); 
    }
}
