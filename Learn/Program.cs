using Microsoft.ML;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace AetherScripts
{
    internal class Program
    {
        private static string trainDatasetPath;
        private static string ModelPath;
        private static string SetupPath;
        private static HashSet<string> skiplist = new HashSet<string>();

        private static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            trainDatasetPath = args[0];
            trainDatasetPath = Path.Combine(Path.GetDirectoryName(args[0]), "training_input_*.tsv.out.tsv");
            SetupPath = args[1];
            ModelPath = args[2];
            Console.WriteLine($"{trainDatasetPath} {SetupPath} {ModelPath}");
            ReadSetup();
            MLContext mlContext = new MLContext(seed: 0);

            IDataView trainData = mlContext.Data.LoadFromTextFile<SearchResultData>(trainDatasetPath, separatorChar: '\t', hasHeader: true);
            IEstimator<ITransformer> pipeline = CreatePipeline(mlContext, trainData);

            // Train the model on the training dataset. To perform training you need to call the Fit() method.
            Console.WriteLine("===== Train the model on the training dataset =====\n");
            ITransformer trainedModel = pipeline.Fit(trainData);
            mlContext.Model.Save(trainedModel, trainData.Schema, ModelPath);
            DateTime end = DateTime.Now;
            Console.WriteLine($"minutes taken to build model {(end - start).TotalMinutes:0.00}");
        }

        private static void ReadSetup()
        {
            skiplist.Add("Label");
            skiplist.Add("GroupId");
            using (TextReader tr = new StreamReader(SetupPath))
            {
                string line;
                while (null!=(line=tr.ReadLine()))
                {
                    string[] parts = line.Trim().Split(' ');
                    switch (parts[0])
                    {
                        case "skip:":
                            skiplist.Add(parts[1]);
                            break;
                        default:
                            Console.WriteLine($"unknown setup command {line}");
                            throw new Exception($"unknown setup command {line}");
                            break;
                    }
                }
            }
        }

        private static IEstimator<ITransformer> CreatePipeline(MLContext mlContext, IDataView dataView)
        {
            const string FeaturesVectorName = "Features";

            Console.WriteLine("===== Set up the trainer =====\n");

            // Specify the columns to include in the feature input data.
            var featureCols = dataView.Schema.AsQueryable()
                .Select(s => s.Name)
                .Where(c =>
                    !skiplist.Contains(c))
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