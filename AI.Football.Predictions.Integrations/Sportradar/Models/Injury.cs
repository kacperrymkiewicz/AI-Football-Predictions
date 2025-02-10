namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class Injury
    {
        public int CategoryId { get; set; }
        public string Reason { get; set; }
        public string ExpectedReturn { get; set; }
        public string ImageId { get; set; }
        public int ImageVersion { get; set; }
    }

}