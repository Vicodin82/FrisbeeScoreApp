using SQLite;

namespace FrisbeeScoreApp.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int HoleCount { get; set; }
    }
}