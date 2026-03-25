using SQLite;

namespace FrisbeeScoreApp.Models
{
    public class Hole
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CourseId { get; set; }      // Mihin rataan väylä kuuluu
        public int HoleNumber { get; set; }    // Väylän numero, esim 1, 2, 3...
        public int Par { get; set; }           // Ihannetulos
    }
}