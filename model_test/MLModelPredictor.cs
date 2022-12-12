using Microsoft.ML;

namespace model_test
{
    public partial class MLModelPredictor
    {
        private static string MLNetModelPath;
        internal MLModelPredictor()
        {
            MLNetModelPath = GetModelPath();
        }

        private static string GetModelPath()
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string parentPath = assemblyFolderPath.Remove(assemblyFolderPath.IndexOf("model_test"));
            string modelTrainPath = @$"model_train\MLModels\mw_model.zip";

            string modelPath = parentPath + modelTrainPath;
            Console.WriteLine($"Model Path: {modelPath}");

            return modelPath;
        }

        internal static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        internal ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
