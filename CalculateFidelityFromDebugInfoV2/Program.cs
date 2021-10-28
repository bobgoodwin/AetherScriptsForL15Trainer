// CalculateFidelityFromDebugInfoV2.Program
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

internal class Program
{
	private class QueryMatadata
	{
		public string market;

		public int topIndex;

		public int[] idealSet;

		public int[] recallSet;

		public float maxcg;

		public int topg;

		public string top;

		public List<string> docList;

		public QueryMatadata()
		{
			idealSet = new int[5];
			recallSet = new int[5];
			topIndex = 4;
		}
	}

	public struct FidelityStats
	{
		public int idealP;

		public int idealE;

		public int idealG;

		public int idealF;

		public int recallP;

		public int recallE;

		public int recallG;

		public int recallF;

		public double fidP;

		public double fidE;

		public double fidG;

		public double fidF;

		public double fidT;

		public double NCG;

		public int countP;

		public int countE;

		public int countG;

		public int countF;

		public int countNCG;

		public int[] countT;

		public int[] recallT;

		public int[] idealT;

		public int[] hrsT;

		public int count;

		public void Initializer()
		{
			countT = new int[4];
			recallT = new int[4];
			idealT = new int[4];
			hrsT = new int[4];
			for (int i = 0; i < 4; i++)
			{
				countT[i] = 0;
				recallT[i] = 0;
				idealT[i] = 0;
				hrsT[i] = 0;
			}
		}

		public void UpdateIdealSet(int[] idealset, int topindex)
		{
			idealP += idealset[0];
			idealE += idealset[1];
			idealG += idealset[2];
			idealF += idealset[3];
			if (idealset[0] > 0)
			{
				countP++;
			}
			if (idealset[1] > 0)
			{
				countE++;
			}
			if (idealset[2] > 0)
			{
				countG++;
			}
			if (idealset[3] > 0)
			{
				countF++;
			}
			if (idealset[0] > 0 || idealset[1] > 0 || idealset[2] > 0 || idealset[3] > 0)
			{
				countNCG++;
			}
			bool flag = false;
			for (int i = 0; i + topindex < 4; i++)
			{
				idealT[i] += idealset[topindex + i];
				if (idealset[topindex + i] > 0 && !flag)
				{
					countT[i]++;
					flag = true;
				}
			}
		}

		public void UpdateRecallSet(int[] idealset, int[] recallset, int topindex)
		{
			double num = 0.0;
			double num2 = 0.0;
			recallP += recallset[0];
			recallE += recallset[1];
			recallG += recallset[2];
			recallF += recallset[3];
			for (int i = 0; i + topindex < 4; i++)
			{
				recallT[i] += recallset[topindex + i];
			}
			if (idealset[0] > 0)
			{
				fidP += (double)recallset[0] / (double)idealset[0];
			}
			if (idealset[1] > 0)
			{
				fidE += (double)recallset[1] / (double)idealset[1];
			}
			if (idealset[2] > 0)
			{
				fidG += (double)recallset[2] / (double)idealset[2];
			}
			if (idealset[3] > 0)
			{
				fidF += (double)recallset[3] / (double)idealset[3];
			}
			if (idealset[0] > 0 || idealset[1] > 0 || idealset[2] > 0 || idealset[3] > 0)
			{
				num2 = GetTopFidelity(idealset, recallset, topindex);
				fidT += num2;
				num = GetNCG(idealset, recallset);
				NCG += num;
			}
		}

		public void UpdateHRS(int topindex)
		{
			count++;
			hrsT[topindex]++;
		}

		public static double GetTopFidelity(int[] idealset, int[] recallset, int topindex)
		{
			return (idealset[topindex] > 0) ? ((double)recallset[topindex] / (double)idealset[topindex]) : 0.0;
		}

		public static double GetNCG(int[] idealset, int[] recallset)
		{
			return (double)(31 * recallset[0] + 15 * recallset[1] + 7 * recallset[2] + 3 * recallset[3]) / (double)(31 * idealset[0] + 15 * idealset[1] + 7 * idealset[2] + 3 * idealset[3]);
		}
	}

	private enum ComparisionMethod
	{
		invalid,
		equal,
		jaccard
	}

	private struct ComparisionMethodEx
	{
		public ComparisionMethod method;

		public float threshold;

		public ComparisionMethodEx(ComparisionMethod method, float thread)
		{
			this.method = method;
			threshold = thread;
		}

		public override string ToString()
		{
			if (method == ComparisionMethod.jaccard)
			{
				return method.ToString() + " with threshold " + threshold;
			}
			return method.ToString();
		}
	}

	private const int maxRateCount = 4;

	private const string fullSet = "FullSet";

	private static int[] gains = new int[4] { 100, 75, 50, 25 };

	private static string[] labels = new string[4] { "Perfect", "Excellent", "Good", "Fair" };

	private static Dictionary<string, QueryMatadata> query2idealset;

	private static Dictionary<string, int> querydoc2gainindex;

	private static Dictionary<string, int> marketQuerySet;

	private static Dictionary<string, int> marketFidelity;

	private static FidelityStats[] marketFidelityStats;

	private static ComparisionMethodEx method;

	private static int MapRate(string rate)
	{
		string text = rate.ToLower();
		int result = -1;
		int num = 0;
		string[] array = labels;
		foreach (string text2 in array)
		{
			if (text == text2.ToLower())
			{
				result = num;
				break;
			}
			num++;
		}
		return result;
	}

	private static ComparisionMethodEx ValidComparasionMethod(string comparisionMethod)
	{
		ComparisionMethodEx result = new ComparisionMethodEx(ComparisionMethod.invalid, 0.8f);
		if (comparisionMethod.ToLower().IndexOf("equal") == 0)
		{
			result.method = ComparisionMethod.equal;
		}
		if (comparisionMethod.ToLower().IndexOf("jaccard") == 0)
		{
			string[] array = comparisionMethod.Split('_');
			result.method = ComparisionMethod.jaccard;
			if (array.Length > 1 && !float.TryParse(array[1], out result.threshold))
			{
				result.method = ComparisionMethod.invalid;
			}
		}
		return result;
	}

	private static void Main(string[] args)
	{
		if (args.Length < 12)
		{
			Console.WriteLine("CalculateFidelityFromDebugInfo idealFile queryIndex_IdealFile docIndex_IdealFile rateIndex_IdealFile recallFile  queryIndex_RecallFile docIndex_RecallFile outputFile hrsFile perQueryFidelity perfDebugFiel comparasionMethod(equal, jaccard)");
			return;
		}
		method = ValidComparasionMethod(args[11]);
		if (method.method == ComparisionMethod.invalid)
		{
			Console.WriteLine("The input comparision method {0} is invlid.", args[11]);
			Console.WriteLine("The comparision can be equal or jaccard with optional threashold. For example, equal, jaccard, jaccard_0.8");
			return;
		}
		Console.WriteLine("ComparisionMethod = " + method.ToString());
		query2idealset = new Dictionary<string, QueryMatadata>();
		querydoc2gainindex = new Dictionary<string, int>();
		marketQuerySet = new Dictionary<string, int>();
		string file = args[0];
		uint index_qkey = uint.Parse(args[1]);
		uint index_dkey = uint.Parse(args[2]);
		uint index_rate = uint.Parse(args[3]);
		ReadIdealSet(file, querydoc2gainindex, query2idealset, index_qkey, index_dkey, index_rate);
		Console.WriteLine("Loaded Ideal file. Query Count = " + query2idealset.Keys.Count);
		string hrsFile = args[8];
		ReadHrs(hrsFile, query2idealset, marketQuerySet);
		Console.WriteLine("Loaded HRS file. Query Count = " + query2idealset.Keys.Count);
		string recallFile = args[4];
		string recall_qkey = args[5];
		string recall_dkey = args[6];
		string outputFile = args[7];
		string debugFile = args[10];
		ReadRecallSet(recallFile, debugFile, querydoc2gainindex, query2idealset, recall_qkey, recall_dkey);
		marketFidelity = new Dictionary<string, int>();
		marketFidelityStats = new FidelityStats[marketQuerySet.Keys.Count + 1];
		int num = 0;
		marketFidelityStats[num].Initializer();
		marketFidelity.Add("FullSet", num);
		num++;
		foreach (string key in marketQuerySet.Keys)
		{
			marketFidelityStats[num].Initializer();
			marketFidelity.Add(key, num);
			num++;
		}
		StreamWriter streamWriter = new StreamWriter(args[9]);
		streamWriter.WriteLine("Qid\tNcg\tTop");
		foreach (string key2 in query2idealset.Keys)
		{
			if (!query2idealset.ContainsKey(key2))
			{
				Console.WriteLine("Invalid query id: " + key2);
				continue;
			}
			QueryMatadata queryMatadata = query2idealset[key2];
			string market = queryMatadata.market;
			if (string.IsNullOrEmpty(market) || queryMatadata.topIndex >= 4)
			{
				Console.WriteLine("Invalid query id: " + key2);
				continue;
			}
			int[] idealSet = queryMatadata.idealSet;
			int topIndex = queryMatadata.topIndex;
			marketFidelityStats[marketFidelity["FullSet"]].UpdateIdealSet(idealSet, topIndex);
			marketFidelityStats[marketFidelity[market]].UpdateIdealSet(idealSet, topIndex);
			double num2 = 0.0;
			double num3 = 0.0;
			int[] recallSet = queryMatadata.recallSet;
			marketFidelityStats[marketFidelity["FullSet"]].UpdateRecallSet(idealSet, recallSet, topIndex);
			marketFidelityStats[marketFidelity[market]].UpdateRecallSet(idealSet, recallSet, topIndex);
			num3 = FidelityStats.GetTopFidelity(idealSet, recallSet, topIndex);
			num2 = FidelityStats.GetNCG(idealSet, recallSet);
			streamWriter.WriteLine(key2 + "\t" + num2 * 100.0 + "\t" + num3 * 100.0);
			marketFidelityStats[marketFidelity["FullSet"]].UpdateHRS(topIndex);
			marketFidelityStats[marketFidelity[market]].UpdateHRS(topIndex);
		}
		streamWriter.Close();
		OutputFidelity(outputFile);
	}

	private static bool IsMatch(string qid, string did, out string goldendoc, Dictionary<string, QueryMatadata> query2idealset, Dictionary<string, int> querydoc2gainindex)
	{
		bool result = false;
		goldendoc = did;
		switch (method.method)
		{
			case ComparisionMethod.equal:
				{
					string key = qid + "\t" + did;
					result = querydoc2gainindex.ContainsKey(key);
					break;
				}
			case ComparisionMethod.jaccard:
				{
					JaccardSimilarity jaccardSimilarity = new JaccardSimilarity(method.threshold);
					if (!query2idealset.ContainsKey(qid) || query2idealset[qid].docList == null)
					{
						break;
					}
					List<string> docList = query2idealset[qid].docList;
					double num = 0.0;
					foreach (string item in docList)
					{
						double jaccardSimilarity2 = 0.0;
						bool flag = jaccardSimilarity.IsMatch(did, item, out jaccardSimilarity2);
						if (flag && jaccardSimilarity2 > num)
						{
							result = flag;
							goldendoc = item;
							num = jaccardSimilarity2;
						}
					}
					break;
				}
		}
		return result;
	}

	private static void ReadRecallSet(string recallFile, string debugFile, Dictionary<string, int> querydoc2gainindex, Dictionary<string, QueryMatadata> query2idealset, string recall_qkey, string recall_dkey)
	{
		StreamReader streamReader = new StreamReader(recallFile, Encoding.UTF8);
		uint num = uint.MaxValue;
		uint num2 = uint.MaxValue;
		string text;
		if (recall_qkey.StartsWith("m:") || recall_dkey.StartsWith("m:"))
		{
			text = streamReader.ReadLine();
			string[] array = text.Split('\t');
			uint num3 = 0u;
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				if (text2.ToLower() == recall_qkey.ToLower())
				{
					num = num3;
				}
				if (text2.ToLower() == recall_dkey.ToLower())
				{
					num2 = num3;
				}
				num3++;
			}
		}
		else
		{
			num = uint.Parse(recall_qkey);
			num2 = uint.Parse(recall_dkey);
		}
		StreamWriter streamWriter = new StreamWriter(debugFile, append: false, Encoding.UTF8);
		while ((text = streamReader.ReadLine()) != null)
		{
			string[] array3 = text.Split('\t');
			if (array3.Length < 3)
			{
				continue;
			}
			string text3 = array3[num];
			string text4 = array3[num2];
			string goldendoc = text4;
			if (!IsMatch(text3, text4, out goldendoc, query2idealset, querydoc2gainindex))
			{
				continue;
			}
			string key = text3 + "\t" + goldendoc;
			if (querydoc2gainindex.ContainsKey(key))
			{
				int num4 = querydoc2gainindex[key];
				querydoc2gainindex.Remove(key);
				QueryMatadata queryMatadata = new QueryMatadata();
				if (query2idealset.ContainsKey(text3))
				{
					queryMatadata = query2idealset[text3];
					query2idealset.Remove(text3);
				}
				queryMatadata.recallSet[num4]++;
				query2idealset.Add(text3, queryMatadata);
				streamWriter.WriteLine(text3 + "\t" + goldendoc + "\t" + labels[num4] + "\t" + text4);
			}
		}
		streamReader.Close();
		streamWriter.Close();
	}

	private static void ReadHrs(string hrsFile, Dictionary<string, QueryMatadata> query2idealset, Dictionary<string, int> marketQuerySet)
	{
		StreamReader streamReader = new StreamReader(hrsFile);
		string text;
		while ((text = streamReader.ReadLine()) != null)
		{
			string[] array = text.Split('\t');
			if (array.Length < 5)
			{
				continue;
			}
			string text2 = array[0];
			string key = array[1];
			int num = MapRate(array[3]);
			if (num >= 0)
			{
				if (!marketQuerySet.ContainsKey(text2) && !string.IsNullOrEmpty(text2))
				{
					marketQuerySet[text2] = 1;
				}
				QueryMatadata queryMatadata = new QueryMatadata();
				if (query2idealset.ContainsKey(key))
				{
					queryMatadata = query2idealset[key];
					query2idealset.Remove(key);
				}
				queryMatadata.market = text2;
				queryMatadata.topIndex = Math.Min(queryMatadata.topIndex, num);
				query2idealset.Add(key, queryMatadata);
			}
		}
		streamReader.Close();
	}

	private static void ReadIdealSet(string file, Dictionary<string, int> querydoc2gainindex, Dictionary<string, QueryMatadata> query2idealset, uint index_qkey, uint index_dkey, uint index_rate)
	{
		string text = "";
		StreamReader streamReader = new StreamReader(file);
		while ((text = streamReader.ReadLine()) != null)
		{
			string[] array = text.Split('\t');
			if (array.Length < 3)
			{
				continue;
			}
			string text2 = array[index_qkey];
			string text3 = array[index_dkey];
			int num = MapRate(array[index_rate]);
			QueryMatadata queryMatadata = new QueryMatadata();
			if (num >= 0 && !querydoc2gainindex.ContainsKey(text2 + "\t" + text3))
			{
				querydoc2gainindex[text2 + "\t" + text3] = num;
				if (query2idealset.ContainsKey(text2))
				{
					queryMatadata = query2idealset[text2];
					query2idealset.Remove(text2);
				}
				else
				{
					queryMatadata.docList = new List<string>();
				}
				queryMatadata.idealSet[num]++;
				queryMatadata.docList.Add(text3);
				query2idealset.Add(text2, queryMatadata);
			}
		}
		streamReader.Close();
	}

	private static void OutputFidelity(string outputFile)
	{
		StringBuilder stringBuilder = new StringBuilder();
		List<string> list = new List<string>();
		list.Add("FullSet");
		if (marketQuerySet.Keys.Count > 1)
		{
			foreach (string key in marketQuerySet.Keys)
			{
				list.Add(key);
			}
		}
		stringBuilder.AppendLine("query basis fidelity:");
		stringBuilder.AppendLine("\tNCG@INF\tTop\tPerfect\tExcellent\tGood\tFair");
		foreach (string item in list)
		{
			FidelityStats fidelityStats = marketFidelityStats[marketFidelity[item]];
			stringBuilder.AppendFormat("{6}\t{0:F2}\t{1:F2}\t{2:F2}\t{3:F2}\t{4:F2}\t{5:F2}\r\n", 100.0 * fidelityStats.NCG / (double)fidelityStats.countNCG, 100.0 * fidelityStats.fidT / (double)fidelityStats.countT[0], 100.0 * fidelityStats.fidP / (double)fidelityStats.countP, 100.0 * fidelityStats.fidE / (double)fidelityStats.countE, 100.0 * fidelityStats.fidG / (double)fidelityStats.countG, 100.0 * fidelityStats.fidF / (double)fidelityStats.countF, item);
		}
		stringBuilder.AppendLine("\r\nurl basis fidelity:");
		stringBuilder.AppendLine("\tNCG@INF\tTop\tPerfect\tExcellent\tGood\tFair");
		foreach (string item2 in list)
		{
			FidelityStats fidelityStats2 = marketFidelityStats[marketFidelity[item2]];
			stringBuilder.AppendFormat("{6}\t{0:F2}\t{1:F2}\t{2:F2}\t{3:F2}\t{4:F2}\t{5:F2}\r\n", 100.0 * (double)(31 * fidelityStats2.recallP + 15 * fidelityStats2.recallE + 7 * fidelityStats2.recallG + 3 * fidelityStats2.recallF) / (double)(31 * fidelityStats2.idealP + 15 * fidelityStats2.idealE + 7 * fidelityStats2.idealG + 3 * fidelityStats2.idealF), 100.0 * (double)fidelityStats2.recallT[0] / (double)fidelityStats2.idealT[0], 100.0 * (double)fidelityStats2.recallP / (double)fidelityStats2.idealP, 100.0 * (double)fidelityStats2.recallE / (double)fidelityStats2.idealE, 100.0 * (double)fidelityStats2.recallG / (double)fidelityStats2.idealG, 100.0 * (double)fidelityStats2.recallF / (double)fidelityStats2.idealF, item2);
		}
		stringBuilder.AppendLine("\r\nRecallSet Url-Rating distribution:");
		stringBuilder.AppendLine("\tNCG@INF\tTop\tPerfect\tExcellent\tGood\tFair");
		foreach (string item3 in list)
		{
			FidelityStats fidelityStats3 = marketFidelityStats[marketFidelity[item3]];
			stringBuilder.AppendFormat("{6}\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", fidelityStats3.recallP + fidelityStats3.recallE + fidelityStats3.recallG + fidelityStats3.recallF, fidelityStats3.recallT[0], fidelityStats3.recallP, fidelityStats3.recallE, fidelityStats3.recallG, fidelityStats3.recallF, item3);
		}
		stringBuilder.AppendLine("\r\nIdealSet Url-Rating distribution:");
		stringBuilder.AppendLine("\tNCG@INF\tTop\tPerfect\tExcellent\tGood\tFair");
		foreach (string item4 in list)
		{
			FidelityStats fidelityStats4 = marketFidelityStats[marketFidelity[item4]];
			stringBuilder.AppendFormat("{6}\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", fidelityStats4.idealP + fidelityStats4.idealE + fidelityStats4.idealG + fidelityStats4.idealF, fidelityStats4.idealT[0], fidelityStats4.idealP, fidelityStats4.idealE, fidelityStats4.idealG, fidelityStats4.idealF, item4);
		}
		stringBuilder.AppendLine("\r\nIdealSet Query-Rating distribution:");
		stringBuilder.AppendLine("\tNCG@INF\tTop\tPerfect\tExcellent\tGood\tFair");
		foreach (string item5 in list)
		{
			FidelityStats fidelityStats5 = marketFidelityStats[marketFidelity[item5]];
			stringBuilder.AppendFormat("{6}\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", fidelityStats5.countNCG, fidelityStats5.countT[0], fidelityStats5.countP, fidelityStats5.countE, fidelityStats5.countG, fidelityStats5.countF, item5);
		}
		stringBuilder.AppendLine("\r\nHRS Query-TopRate distribution:");
		stringBuilder.AppendLine("\tNCG@INF\tTop\tPerfect\tExcellent\tGood\tFair");
		foreach (string item6 in list)
		{
			FidelityStats fidelityStats6 = marketFidelityStats[marketFidelity[item6]];
			stringBuilder.AppendFormat("{6}\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", fidelityStats6.count, fidelityStats6.count, fidelityStats6.hrsT[0], fidelityStats6.hrsT[1], fidelityStats6.hrsT[2], fidelityStats6.hrsT[3], item6);
		}
		int[] countT = marketFidelityStats[marketFidelity["FullSet"]].countT;
		int[] recallT = marketFidelityStats[marketFidelity["FullSet"]].recallT;
		int[] idealT = marketFidelityStats[marketFidelity["FullSet"]].idealT;
		for (int i = 1; i < 4; i++)
		{
			countT[i] += countT[i - 1];
			recallT[i] += recallT[i - 1];
			idealT[i] += idealT[i - 1];
		}
		stringBuilder.AppendLine("\r\nRecallSet Url-TopRate distribution:");
		stringBuilder.AppendLine("\tTop-1\tTop-2\tTop-3\tTop-4");
		stringBuilder.AppendFormat("{4}\t{0}\t{1}\t{2}\t{3}", recallT[0], recallT[1], recallT[2], recallT[3], "FullSet");
		stringBuilder.AppendLine("\r\nIdealSet Url-TopRate distribution:");
		stringBuilder.AppendLine("\tTop-1\tTop-2\tTop-3\tTop-4");
		stringBuilder.AppendFormat("{4}\t{0}\t{1}\t{2}\t{3}", idealT[0], idealT[1], idealT[2], idealT[3], "FullSet");
		stringBuilder.AppendLine("\r\nIdealSet Query-TopRate distribution:");
		stringBuilder.AppendLine("\tTop-1\tTop-2\tTop-3\tTop-4");
		stringBuilder.AppendFormat("{4}\t{0}\t{1}\t{2}\t{3}", countT[0], countT[1], countT[2], countT[3], "FullSet");
		StreamWriter streamWriter = new StreamWriter(outputFile);
		streamWriter.WriteLine(stringBuilder.ToString());
		streamWriter.Close();
		Console.WriteLine(stringBuilder.ToString());
	}
}
