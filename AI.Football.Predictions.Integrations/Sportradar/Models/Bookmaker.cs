using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Bookmaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string NameForURL { get; set; }
        public ActionButton ActionButton { get; set; }
        public string Color { get; set; }
        public int ImageVersion { get; set; }
    }

}