using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameForURL { get; set; }
        public List<int> SportTypes { get; set; }
        public int ImageVersion { get; set; }
        public bool IsInternational { get; set; }
    }

}