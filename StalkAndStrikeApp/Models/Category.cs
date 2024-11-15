namespace StalkAndStrikeApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public ICollection<Gun> Guns { get; set; }
        public string Name { get; set; } // Category name, e.g. "Rifles", "Shotguns"
    }
}
