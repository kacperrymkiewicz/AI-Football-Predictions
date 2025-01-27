using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Option
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public Rate Rate { get; set; }
        public int BookmakerId { get; set; }
        public PrematchRate PrematchRate { get; set; }
        public string Link { get; set; }
        public int Trend { get; set; }
        public OldRate OldRate { get; set; }
    }

}