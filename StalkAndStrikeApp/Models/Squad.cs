using System.ComponentModel.DataAnnotations;

namespace StalkAndStrikeApp.Models
{
    public class Squad
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Hunter> Hunters { get; set; } = new List<Hunter>();
    }
}
