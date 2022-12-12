using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.ML.Data;
using Microsoft.ML;
using model_test;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

var predictor = new MLModelPredictor();

double PredictValue(InputData data)
{
    //Load sample data
    var sampleData = new ModelInput()
    {
        AvgAreaIncome = (float)data.AvgAreaIncome,
        AvgAreaHouseAge = (float)data.AvgAreaHouseAge,
        AvgAreaNumberRooms = (float)data.AvgAreaNumberBedrooms,
        AvgAreaNumberBedrooms = (float)data.AvgAreaNumberBedrooms,
        AreaPopulation = (float)data.AreaPopulation,
    };

    //Load model and predict output
    var result = predictor.Predict(sampleData);
    return (double)result.Score;
}

var fp = args[0];

if (!File.Exists(fp))
{
    Console.WriteLine($"Incorrect filepath: {fp}");
    return;
}

var fileName = fp;
var outputDataset = new List<OutputData>();

using (var reader = new StreamReader(fileName))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csv.GetRecords<InputData>();

    foreach (var record in records)
    {
        double predictedPrice= PredictValue(record);
        outputDataset.Add(new(Address: record.Address, ActualPrice : record.Price, PredictedPrice: predictedPrice));
    }
}

// dump outputs to file.
var outputFileName = fileName.Replace(".csv", "") + $"_predictions.csv";

using var outputFile = File.Create(outputFileName);

using var writer = new StreamWriter(outputFile);
using var outputCsv = new CsvWriter(writer, CultureInfo.InvariantCulture);
outputCsv.WriteRecords(outputDataset);

Console.WriteLine(outputFileName);

public class InputData
{
    public float AvgAreaIncome { get; set; }

    public float AvgAreaHouseAge { get; set; }

    public float AvgAreaNumberRooms { get; set; }

    public float AvgAreaNumberBedrooms { get; set; }

    public float AreaPopulation { get; set; }

    public float Price { get; set; }
    
    public string Address { get; set; }
}


public record OutputData(string Address, double ActualPrice, double PredictedPrice);