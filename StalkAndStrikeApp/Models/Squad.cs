namespace StalkAndStrikeApp.Models
{
    public class Squad
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<Hunter> Hunters { get; set; }
    }
}
