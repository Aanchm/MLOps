using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model_test
{
    public partial class MLModelPredictor
    {
        private static string _modelId;
        internal MLModelPredictor(string modelId)
        {
            _modelId = modelId;
        }

        private static string GetModelPath(string modelId)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string parentPath = assemblyFolderPath.Remove(assemblyFolderPath.IndexOf("model_test"));
            string modelTrainPath = $"/model_train/MLModels/mw_model_{modelId}";

            string modelPath = Path.Combine(parentPath, modelTrainPath);
            return modelPath;
        }

        private static string MLNetModelPath = GetModelPath(_modelId);

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
