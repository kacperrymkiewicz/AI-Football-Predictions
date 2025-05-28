using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.API.Models
{
    public class PredictionAccuracy
    {
        public int TotalPredictions { get; set; }
        public int CorrectPredictions { get; set; }

        public int HomeWinPredictions { get; set; }
        public int HomeWinCorrect { get; set; }

        public int DrawPredictions { get; set; }
        public int DrawCorrect { get; set; }

        public int AwayWinPredictions { get; set; }
        public int AwayWinCorrect { get; set; }

        public int ScoreCorrectPredictions { get; set; }
        public int ScoreClosePredictions { get; set; }

        public float OverallAccuracy => TotalPredictions == 0 ? 0 : (float)CorrectPredictions / TotalPredictions;
        public float HomeWinAccuracy => HomeWinPredictions == 0 ? 0 : (float)HomeWinCorrect / HomeWinPredictions;
        public float DrawAccuracy => DrawPredictions == 0 ? 0 : (float)DrawCorrect / DrawPredictions;
        public float AwayWinAccuracy => AwayWinPredictions == 0 ? 0 : (float)AwayWinCorrect / AwayWinPredictions;
        public float ScoreAccuracy => ScoreCorrectPredictions == 0 ? 0 : (float)ScoreCorrectPredictions / TotalPredictions;
        public float ScoreCloseAccuracy => ScoreClosePredictions == 0 ? 0 : (float)ScoreClosePredictions / TotalPredictions;
    }
}