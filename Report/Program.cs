using System.IO;
using System;
using System.Collections.Generic;
namespace AetherScripts
{
    class Program
    {
        static Dictionary<string, float> fidelity = new Dictionary<string, float>();
        static float baseline = 0;
        static void Main(string[] args)
        {
            ReadTailFidelity(args[0]);
            ReadSaaSFidelity(args[1]);
            string hdr;
            using (TextReader tr = new StreamReader(args[2]))
            {
                hdr = tr.ReadLine();
            }
            using (TextWriter tw = new StreamWriter(args[3]))
            {
                tw.WriteLine(hdr);
                Console.WriteLine(hdr);
                foreach (var n in fidelity)
                {
                    tw.WriteLine($"{n.Key}\t{n.Value - baseline}");
                    Console.WriteLine($"{n.Key}\t{(n.Value - baseline):0.00}");
                }
            }
        }

        static void ReadTailFidelity(string path)
        {
            using (TextReader tr = new StreamReader(path))
            {
                string line;
                while (null != (line = tr.ReadLine()))
                {
                    line = line.Trim();
                    string[] parts = line.Split('\t');
                    if (parts.Length > 3)
                    {
                        fidelity[parts[1]] = float.Parse(parts[3]);
                    }
                }

            }

            //Tail    15KTo2500 FullSet 79.32   83.04   87.05   81.12   75.51   69.61
            //Tail    10KTo2500 FullSet 79.27   83.06   87.05   81.08   75.42   69.44
            //Tail    5KTo2500 FullSet 78.61   82.47   86.39   80.45   74.62   68.56

        }
        static void ReadSaaSFidelity(string path)
        {
            //            SaaS    2500    FullSet 76.77   81.17   85.62   78.62   72.58   65.98
            using (TextReader tr = new StreamReader(path))
            {
                string line;
                while (null != (line = tr.ReadLine()))
                {
                    line = line.Trim();
                    string[] parts = line.Split('\t');
                    if (parts.Length > 3)
                    {
                        baseline = float.Parse(parts[3]);
                    }
                }

            }
        }
    }
}
