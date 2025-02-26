using AI.Football.Predictions.ML.Models;
using AI.Football.Predictions.ML.Services;
using Microsoft.ML;
using System;
using System.Reflection;

public class Program
{
    static void Main(string[] args)
    {
        var service = new MatchPredictionService();
        string dataPath = "Data/matches.csv";

        if (File.Exists(dataPath))
        {
            //service.Train(dataPath);
            Console.WriteLine("Model trained successfully!");
        }
        else
        {
            Console.WriteLine("Training data not found!");
            return;
        }

        var sampleMatch = new MatchData
        {
            HomeGoalsAvg = 1.8f,
            AwayGoalsAvg = 3.2f,
            HomePossessionAvg = 25f,
            AwayPossessionAvg = 75f,
            HomeShotsAvg = 3f,
            AwayShotsAvg = 9f,
            HomeWinRate = 0.25f,
            AwayWinRate = 0.75f,
            H2HHomeWins = 2,
            H2HAwayWins = 7,
            H2HDraws = 2,
            MatchResult = 0
        };

        var prediction = service.Predict(sampleMatch);
        Console.WriteLine($"Predicted Match Result: {prediction.PredictedResult}");
        //Console.WriteLine($"Confidence Scores: HomeWin={prediction.Score[0]:F2}, Draw={prediction.Score[1]:F2}, AwayWin={prediction.Score[2]:F2}");
    }
}
