using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.FootballData.Models
{
    public class Filters
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Permission { get; set; }
    }
}