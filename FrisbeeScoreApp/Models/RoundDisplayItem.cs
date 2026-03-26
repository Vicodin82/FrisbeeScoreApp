namespace FrisbeeScoreApp.Models
{
    public class RoundDisplayItem
    {
        public int RoundId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public DateTime DatePlayed { get; set; }
        public int TotalThrows { get; set; }
        public int TotalVsPar { get; set; }
        public string Weather { get; set; } = string.Empty; // Sääsymboli + teksti
    }
}