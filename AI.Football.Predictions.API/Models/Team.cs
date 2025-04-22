using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.API.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsWinner { get; set; }
        public TeamStatistics Statistics { get; set; }
    }
}