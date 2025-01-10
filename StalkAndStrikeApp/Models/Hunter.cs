namespace StalkAndStrikeApp.Models
{
    public class Hunter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LicenseNumber { get; set; }
        public int SquadId { get; set; }
        public Squad? Squad { get; set; }
        public ICollection<Dog> Dogs { get; set; } = new List<Dog>();
    }
}