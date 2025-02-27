namespace StalkAndStrikeApp.Models
{
  public class HunterManagementViewModel
  {
    public List<Hunter> Hunters { get; set; } = new List<Hunter>();
    public List<Squad> Squads { get; set; } = new List<Squad>();
    public List<Dog> Dogs { get; set; } = new List<Dog>();
  }
}
