// CalculateFidelityFromDebugInfoV2.JaccardSimilarity
using System;
using System.Collections.Generic;

public class JaccardSimilarity
{
	public static readonly double DefaultThreshold = 0.95;

	public static readonly char[] Seperators = new char[15]
	{
		'"', '\'', ',', ';', '?', '\t', '.', ' ', '(', ')',
		'\ufffd', '“', '”', '—', '–'
	};

	public readonly double _threshold;

	public JaccardSimilarity(double threshold)
	{
		_threshold = threshold;
	}

	public bool IsMatch(string source, string target, out double jaccardSimilarity)
	{
		if (source == null || target == null)
		{
			jaccardSimilarity = 0.0;
			return false;
		}
		if (string.Compare(source, target) == 0)
		{
			jaccardSimilarity = 100.0;
			return true;
		}
		string[] collection = source.Split(Seperators, StringSplitOptions.RemoveEmptyEntries);
		IEnumerable<string> collection2 = target.Split(Seperators, StringSplitOptions.RemoveEmptyEntries);
		HashSet<string> sourceSet = new HashSet<string>(collection);
		HashSet<string> targetSet = new HashSet<string>(collection2);
		jaccardSimilarity = Compare(sourceSet, targetSet);
		return jaccardSimilarity > _threshold;
	}

	private double Compare(HashSet<string> sourceSet, HashSet<string> targetSet)
	{
		if (sourceSet.Count == 0 && targetSet.Count == 0)
		{
			return 1.0;
		}
		int num = sourceSet.Count;
		int num2 = 0;
		foreach (string item in targetSet)
		{
			if (sourceSet.Contains(item))
			{
				num2++;
			}
			else
			{
				num++;
			}
		}
		return (double)num2 / (double)num;
	}
}
