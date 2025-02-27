using System.ComponentModel.DataAnnotations;

namespace StalkAndStrikeApp.Models
{
  public class HuntedPlace
  {
    public int Id { get; set; }

    [Required]
    public string ImagePath { get; set; }

    [Required]
    public double Latitude { get; set; }

    [Required]
    public double Longitude { get; set; }
  }
}
