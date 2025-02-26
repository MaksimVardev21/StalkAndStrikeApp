using System.ComponentModel.DataAnnotations;

namespace StalkAndStrikeApp.Models
{
    public class HuntedPlaceViewModel
    {
        [Required]
        public IFormFile ImageFile { get; set; } // File upload

        [Required]
        public double Latitude { get; set; } // Google Maps latitude

        [Required]
        public double Longitude { get; set; } // Google Maps longitude
    }
}
