using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameForURL { get; set; }
        public bool DrawSupport { get; set; }
        public int ImageVersion { get; set; }
    }

}