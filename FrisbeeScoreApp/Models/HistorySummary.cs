using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrisbeeScoreApp.Models
{
    public class HistorySummary
    {
        public int RoundCount { get; set; }     // Kierrosten määrä
        public int BestVsPar { get; set; }      // Paras tulos pariin
        public double AverageVsPar { get; set; } // Keskiarvo pariin
    }
}
