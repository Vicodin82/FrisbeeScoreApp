using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrisbeeScoreApp.Models
{
    public class RoundDisplayItem
    {
        public int RoundId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public DateTime DatePlayed { get; set; }
        public int TotalThrows { get; set; }
        public int TotalVsPar { get; set; }
    }
}
