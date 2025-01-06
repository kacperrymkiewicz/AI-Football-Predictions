using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class ActualPlayTime
    {
        public string Title { get; set; }
        public ActualTime ActualTime { get; set; }
        public TotalTime TotalTime { get; set; }
    }
}