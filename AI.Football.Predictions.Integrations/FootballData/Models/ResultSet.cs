using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.FootballData.Models
{
    public class ResultSet
    {
        public int Count { get; set; }
        public string Competitions { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public int Played { get; set; }
    }
}