using Microsoft.ML;
using System;
using System.Linq;

namespace AetherScripts
{
    internal class Program
    {
        private static string trainDatasetPath;
        private static string ModelPath;

        private static void Main(string[] args)
        {
            trainDatasetPath = args[0];
            ModelPath = args[1];
            MLContext mlContext = new MLContext(seed: 0);

            IDataView trainData = mlContext.Data.LoadFromTextFile<SearchResultData>(trainDatasetPath, separatorChar: '\t', hasHeader: true);
            IEstimator<ITransformer> pipeline = CreatePipeline(mlContext, trainData);

            // Train the model on the training dataset. To perform training you need to call the Fit() method.
            Console.WriteLine("===== Train the model on the training dataset =====\n");
            ITransformer model = pipeline.Fit(trainData);
        }

        private static IEstimator<ITransformer> CreatePipeline(MLContext mlContext, IDataView dataView)
        {
            const string FeaturesVectorName = "Features";

            Console.WriteLine("===== Set up the trainer =====\n");

            // Specify the columns to include in the feature input data.
            var featureCols = dataView.Schema.AsQueryable()
                .Select(s => s.Name)
                .Where(c =>
                    c != nameof(SearchResultData.Label) &&
                    c != "GroupId")
                 .ToArray();

            // Create an Estimator and transform the data:
            // 1. Concatenate the feature columns into a single Features vector.
            // 2. Create a key type for the label input data by using the value to key transform.
            // 3. Create a key type for the group input data by using a hash transform.
            IEstimator<ITransformer> dataPipeline = mlContext.Transforms.Concatenate(FeaturesVectorName, featureCols)
                .Append(mlContext.Transforms.Conversion.MapValueToKey(nameof(SearchResultData.Label)))
                .Append(mlContext.Transforms.Conversion.Hash(nameof(SearchResultData.GroupId), nameof(SearchResultData.GroupId), numberOfBits: 20));

            // Set the LightGBM LambdaRank trainer.
            IEstimator<ITransformer> trainer = mlContext.Ranking.Trainers.LightGbm(labelColumnName: nameof(SearchResultData.Label), featureColumnName: FeaturesVectorName, rowGroupColumnName: nameof(SearchResultData.GroupId));
            IEstimator<ITransformer> trainerPipeline = dataPipeline.Append(trainer);

            return trainerPipeline;
        }
    }
}