using SQLite;

namespace FrisbeeScoreApp.Models
{
    public class Round
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CourseId { get; set; }     // Mihin rataan kuuluu
        public DateTime DatePlayed { get; set; }

        public int TotalThrows { get; set; }  // Kokonaisheitot
        public int TotalVsPar { get; set; }   // Suhde pariin
    }
}