using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.Linq;

using System.Reflection;
using System.Threading.Tasks;

namespace AetherScripts
{
    class Prediction
    {
        [ColumnName("Score")]
        public float score { get; set; }
    }


    class Program
    {
        static string ProcessLine(string line, Dictionary<string, int> hdr, PredictionEngine<SearchResultData, Prediction> predEngine)
        {
            var parts = line.Split('\t');
            SearchResultData mi = new SearchResultData();
            foreach (PropertyInfo prop in typeof(SearchResultData).GetProperties())
            {
                if (hdr.TryGetValue(prop.Name, out int col))
                {
                    prop.SetValue(mi, float.Parse(parts[col]));
                }
            }

            string query = parts[hdr["Query"]];
            string url = parts[hdr["URL"]];
            string market = parts[hdr["Market"]];
            string judgement = parts[hdr["Judgement"]];
            float l15Score = predEngine.Predict(mi).score;
            return $"{url}\t{query}\t{market}\t{judgement}\t{l15Score}";
        }
        static void Main(string[] args)
        {
            string catalogFilename = args[0];
            string modelFilename = args[1];
            string outputFilename = args[2];
            //string trainingInputFilename = @"D:\model\outputDirectory\training_input_0.tsv.out.tsv";// args[2];
            var mlContext = new MLContext();
            Stream stream = File.Open(modelFilename, FileMode.Open);
            var trainedModel = mlContext.Model.Load(stream, out var inputSchema);
            PredictionEngine<SearchResultData, Prediction> predEngine = mlContext.Model.CreatePredictionEngine<SearchResultData, Prediction>(trainedModel);


            string dir = System.IO.Path.GetDirectoryName(catalogFilename);
            int cnt = 0;
            using (TextReader cat = new StreamReader(catalogFilename))
            {
                using (TextWriter tw = new StreamWriter(outputFilename))
                {
                    System.IO.Directory.SetCurrentDirectory(dir);
                    tw.WriteLine("URL\tQuery\tQueryMarket\tHumanRating\tModelSetScore");
                    string catLine;
                    while (null != (catLine = cat.ReadLine()))
                    {
                        using (TextReader tr = new StreamReader(catLine))
                        {
                            Dictionary<string, int> hdr = new Dictionary<string, int>();
                            string line = tr.ReadLine();
                            string[] parts = line.Split('\t');
                            for (int i = 0; i < parts.Length; i++)
                            {
                                hdr[parts[i]] = i;
                            }

                            List<Task<string>> tasks = new List<Task<string>>();
                            while (null != (line = tr.ReadLine()))
                            {
//                                if (tasks.Count() > 8)
//                                {
//                                    int n = Task.WaitAny(tasks.ToArray());
//                                    tw.WriteLine(tasks[n].Result);
//                                    tasks.RemoveAt(n);
//                                }
//
//                                tasks.Add(Task.Run(() => ProcessLine(line, hdr, predEngine)));
                                                                string result = ProcessLine(line, hdr, predEngine);
                                                                tw.WriteLine(result);
                                if (++cnt % 100000 == 0)
                                {
                                    Console.WriteLine(cnt);
                                }
                            }

//                            foreach (var n in tasks)
//                            {
//                                n.Wait();
//                                tw.WriteLine(n.Result);
//                            }
                        }
                    }
                }
            }
        }
    }
}
