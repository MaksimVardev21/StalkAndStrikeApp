namespace StalkAndStrikeApp.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int HunterId { get; set; }
        public Hunter Hunter { get; set; }
    }
}