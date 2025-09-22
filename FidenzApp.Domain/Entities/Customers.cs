using FidenzApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Domain.Entities
{
    public class Customers
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

        public Address address { get; set; }
        public string about { get; set; }

        public string registered { get; set; }
        public double latitude { get; set; }

        public double longitude { get; set; }

        public List<string> tags { get; set; }
    }
}
