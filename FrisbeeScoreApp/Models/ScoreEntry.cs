using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrisbeeScoreApp.Models
{
    public class ScoreEntry
    {
        public int HoleNumber { get; set; }   // Väylän numero
        public int Par { get; set; }          // Väylän par
        public int Throws { get; set; }       // Käyttäjän heitot
    }
}
