using SQLite;

namespace FrisbeeScoreApp.Models
{
    public class RoundScore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int RoundId { get; set; }

        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Throws { get; set; }
    }
}