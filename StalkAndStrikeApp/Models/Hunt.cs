namespace StalkAndStrikeApp.Models
{
    public class Hunt
    {
        public int HuntId { get; set; }
        public int UserId { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string GameTracked { get; set; } // What game was hunted (e.g., deer, ducks)

    }
}
