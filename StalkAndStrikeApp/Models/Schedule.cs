namespace StalkAndStrikeApp.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime HuntDate { get; set; }
        public int LocationId { get; set; }
        public string Notes { get; set; }
        public HuntingLocation Location { get; set; }
    }
}
