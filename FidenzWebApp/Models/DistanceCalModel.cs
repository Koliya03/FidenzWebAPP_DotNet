using System.ComponentModel.DataAnnotations;

namespace FidenzWebApp.Models
{
    public class DistanceCalModel
    {
        [Required] public string Id { get; set; }
        [Required] public string Name { get; set; }
        [Required(ErrorMessage = "Latitude is required.")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double? Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required.")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double? Longitude { get; set; }
        public double? DistanceKm { get; set; }
        public string Error { get; set; }

        public DistanceCalModel() { }
        public DistanceCalModel(string id,string name) { 
            Id = id;
            Name = name;
        }

    }
}
