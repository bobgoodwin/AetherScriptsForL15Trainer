using System.IO;
using System.Collections.Generic;
using System;

namespace AetherScripts
{
    class Program
    {
        static void Main(string[] args)
        {
            string src = args[0];
            string mapstring = args[1];
            string dst = args[2];
            if (!Directory.Exists(dst))
            {
                Directory.CreateDirectory(dst);
            }

            Dictionary<string, string> map = new Dictionary<string, string>();
            string[] parts = mapstring.Split(',');
            int col = int.Parse(parts[0]);
            for (int i = 1; i < parts.Length; i++)
            {
                string[] subparts = parts[i].Split('=');
                map[subparts[0]] = subparts[1];
            }

            foreach (var fn in Directory.EnumerateFiles(src))
            {
                using (TextReader tr = new StreamReader(fn))
                {
                    string ofn = Path.Combine(dst, Path.GetFileName(fn));
                    using (TextWriter tw = new StreamWriter(ofn))
                    {
                        string line;
                        while (null != (line = tr.ReadLine()))
                        {
                            parts = line.Split('\t');
                            if (parts.Length > col)
                            {
                                if (map.TryGetValue(parts[col], out string replacement))
                                {
                                    parts[col] = replacement;
                                    line = string.Join('\t', parts);
                                }

                            }

                            tw.WriteLine(line);
                        }
                    }
                }
            }

        }
    }
}
