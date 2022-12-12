using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.ML.Data;
using Microsoft.ML;
using model_test;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;


var modelId = args[0];
var predictor = new MLModelPredictor(modelId);


double PredictValue(InputData data)
{
    //Load sample data
    var sampleData = new ModelInput()
    {
        Carriage_Vel = (float)data.Carriage_Vel,
        Iq_Act = (float)data.Iq_Act,
        Id_Act = (float)data.Id_Act,
        Vd_Act = (float)data.Vd_Act,
        Vq_Act = (float)data.Vq_Act,
        PhaseAngle_SP = (float)data.PhaseAngle_SP,
        Torque_SP = (float)data.Torque_SP,
        Resistance = (float)data.Resistance,
        Inductance = (float)data.Inductance,
    };

    //Load model and predict output
    var result = predictor.Predict(sampleData);
    return (double)result.Score;

}

var fp = args[1];

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
        double predictedVel = PredictValue(record);
        outputDataset.Add(new(Timestamp : record.Timestamp, ActualVel: record.Barcode_Vel, PredictedVel: predictedVel));
    }
}

// dump outputs to file.
var outputFilePathRoot = Path.GetPathRoot(fileName);
var outputFileName = fileName.Replace(".csv", "") + $"_predictions_{modelId}.csv";

using var outputFile = File.Create(outputFileName);

using var writer = new StreamWriter(outputFile);
using var outputCsv = new CsvWriter(writer, CultureInfo.InvariantCulture);
outputCsv.WriteRecords(outputDataset);

Console.WriteLine(outputFileName);

public class InputData
{
    public double Carriage_Center_Pos { get; set; }
    public double Carriage_Vel { get; set; }
    public double Barcode_Pos { get; set; }
    public double Barcode_Vel { get; set; }
    public double Id_Act { get; set; }
    public double Iq_Act { get; set; }
    public double Vd_Act { get; set; }
    public double Vq_Act { get; set; }
    public double PhaseAngle_SP { get; set; }
    public double Torque_SP { get; set; }
    public double Timestamp { get; set; }
    public double Inductance { get; set; }
    public double Resistance { get;set; }
}

public record OutputData(double Timestamp, double ActualVel, double PredictedVel);