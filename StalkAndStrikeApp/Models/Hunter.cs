namespace StalkAndStrikeApp.Models
{
  using System.ComponentModel.DataAnnotations;

  public class Hunter
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "License Number is required")]
    public string LicenseNumber { get; set; } = string.Empty;  // Ensure it's not NULL

    public string? Name { get; set; }
    public int SquadId { get; set; }
    public Squad? Squad { get; set; }
    public ICollection<Dog> Dogs { get; set; } = new List<Dog>();
  }

}
