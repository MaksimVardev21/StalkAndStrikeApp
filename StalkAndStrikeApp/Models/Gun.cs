namespace StalkAndStrikeApp.Models
{
    public class Gun
    {
        public int Id { get; set; }
        public string Name { get; set; } // Name of the gun
        public string Description { get; set; } // Description of the gun
        public int CategoryId { get; set; } // Foreign key for Category
        public Category Category { get; set; } // Navigation property for Category
    }
}
