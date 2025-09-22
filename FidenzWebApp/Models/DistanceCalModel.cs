using System.ComponentModel.DataAnnotations;

namespace FidenzWebApp.Models
{
    public class DistanceCalModel
    {
        [Required] public string Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public double? Latitude { get; set; }
        [Required] public double? Longitude { get; set; }

        public double? DistanceKm { get; set; }
        public string Error { get; set; }

        public DistanceCalModel() { }
        public DistanceCalModel(string id,string name) { 
            Id = id;
            Name = name;
        }

    }
}
