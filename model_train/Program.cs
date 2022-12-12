using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.AutoML;
using Common;
using model_train;

string trainingPath = args[0];

if (!File.Exists(trainingPath))
{
    Console.WriteLine($"Incorrect filepath: {trainingPath}");
    return;
}

if (!UInt32.TryParse(args[1], out uint ExperimentTime))
    ExperimentTime = 7200;

string BaseModelsRelativePath = @"../../../../MLModels";
string ModelRelativePath = $"{BaseModelsRelativePath}/mw_model.zip";
string OnnxModelRelativePath = $"{BaseModelsRelativePath}/mw_onnx_model.onnx";
string ModelPath = GetAbsolutePath(ModelRelativePath);
string OnnxModelPath = GetAbsolutePath(OnnxModelRelativePath);

string LabelColumnName = "FareAmount";

MLContext mlContext = new MLContext();
BuildTrainEvaluateAndSaveModel(mlContext);


ITransformer BuildTrainEvaluateAndSaveModel(MLContext mlContext)
{
    IDataView trainingDataView = mlContext.Data.LoadFromTextFile<InputData>(trainingPath, hasHeader: true, separatorChar: ',');

    var progressHandler = new RegressionExperimentProgressHandler();
    ConsoleHelper.ConsoleWriteHeader("=============== Training the model ===============");
    Console.WriteLine($"Running AutoML regression experiment for {ExperimentTime} seconds...");
    ExperimentResult<RegressionMetrics> experimentResult = mlContext.Auto()
        .CreateRegressionExperiment(ExperimentTime)
        .Execute(trainingDataView, LabelColumnName, progressHandler: progressHandler);

    // Evaluate the model and print metrics
    ConsoleHelper.ConsoleWriteHeader("===== Evaluating model's accuracy with test data =====");
    RunDetail<RegressionMetrics> best = experimentResult.BestRun;
    ITransformer trainedModel = best.Model;
    Console.WriteLine($"Best Model {best.TrainerName}");

    //Save/persist the trained model to a .ZIP file
    mlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath);

    using (var stream = File.Create(OnnxModelPath))
        mlContext.Model.ConvertToOnnx(trainedModel, trainingDataView, stream);

    Console.WriteLine("The model is saved to {0}", ModelPath);

    return trainedModel;
}


string GetAbsolutePath(string relativePath)
{
    FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
    string assemblyFolderPath = _dataRoot.Directory.FullName;
    string fullPath = Path.Combine(assemblyFolderPath, relativePath);
    return fullPath;
}
