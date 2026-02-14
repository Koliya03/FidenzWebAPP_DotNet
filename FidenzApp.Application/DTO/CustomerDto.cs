using System.ComponentModel.DataAnnotations;

namespace FidenzApp.Application.DTO
{
    public class CustomerDto
    {
        
        [Key]
        public string _id { get; set; }
        public int index { get; set; }
        public int age { get; set; }
        public string eyeColor { get; set; }

        public string name { get; set; }
        public string gender { get; set; }
        public string company { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public AddressDto address { get; set; }
        public string about { get; set; }

        public string registered { get; set; }
        public double latitude { get; set; }

        public double longitude { get; set; }

        public List<string> tags { get; set; }
    }
}

