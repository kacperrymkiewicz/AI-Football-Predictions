namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class Official
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public int CountryId { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string NameForURL { get; set; }
    }

}