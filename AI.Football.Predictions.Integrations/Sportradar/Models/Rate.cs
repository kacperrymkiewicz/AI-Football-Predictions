using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Rate
    {
        public double Decimal { get; set; }
        public string Fractional { get; set; }
        public string American { get; set; }
    }

}