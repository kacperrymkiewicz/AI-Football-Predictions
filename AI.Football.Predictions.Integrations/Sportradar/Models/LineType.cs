using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class LineType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int InternalOptionType { get; set; }
    }

}