using System.Diagnostics.Metrics;

namespace StalkAndStrikeApp.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }

        // Foreign key
        public int HunterId { get; set; }
        public Hunter Hunter { get; set; }
    }

}
