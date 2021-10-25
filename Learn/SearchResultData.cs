using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace AetherScripts
{
    public class SearchResultData
    {
        [LoadColumn(0)]
        [ColumnName("GroupId")]
        public string GroupId { get; set; }
        //        [LoadColumn(0)]
        //        [ColumnName("Query")]
        //        public string Query { get; set; }
        //        [LoadColumn(1)]
        //        public float URL { get; set; }
        //        [LoadColumn(2)]
        //        public float Market { get; set; }
        //        [LoadColumn(3)]

        [LoadColumn(3)]
        [ColumnName("Label")]
        public uint Label { get; set; }
        [LoadColumn(4)]
        [ColumnName("QLF_Similarity")]
        public float QLF_Similarity { get; set; }
        [LoadColumn(5)]
        [ColumnName("L1Score")]
        public float L1Score { get; set; }
        [LoadColumn(6)]
        [ColumnName("AdditionalFoundMinutes")]
        public float AdditionalFoundMinutes { get; set; }
        [LoadColumn(7)]
        [ColumnName("AdvancedPreferSimulation")]
        public float AdvancedPreferSimulation { get; set; }
        [LoadColumn(8)]
        [ColumnName("AnchorMostFrequent")]
        public float AnchorMostFrequent { get; set; }
        [LoadColumn(9)]
        [ColumnName("AnchorStreamLength")]
        public float AnchorStreamLength { get; set; }
        [LoadColumn(10)]
        [ColumnName("AnchorStreamLengthBase")]
        public float AnchorStreamLengthBase { get; set; }
        [LoadColumn(11)]
        [ColumnName("AnchorStreamLengthExistence")]
        public float AnchorStreamLengthExistence { get; set; }
        [LoadColumn(12)]
        [ColumnName("AnchorUniqueTermCount")]
        public float AnchorUniqueTermCount { get; set; }
        [LoadColumn(13)]
        [ColumnName("BM25FProxy_0URL")]
        public float BM25FProxy_0URL { get; set; }
        [LoadColumn(14)]
        [ColumnName("BM25FProxy_1Title")]
        public float BM25FProxy_1Title { get; set; }
        [LoadColumn(15)]
        [ColumnName("BM25FProxy_SRAnchor")]
        public float BM25FProxy_SRAnchor { get; set; }
        [LoadColumn(16)]
        [ColumnName("BM25F_SRA_Log65K15")]
        public float BM25F_SRA_Log65K15 { get; set; }
        [LoadColumn(17)]
        [ColumnName("BM25F_SRAnchor")]
        public float BM25F_SRAnchor { get; set; }
        [LoadColumn(18)]
        [ColumnName("BigramAUT_0")]
        public float BigramAUT_0 { get; set; }
        [LoadColumn(19)]
        [ColumnName("BigramAUT_1")]
        public float BigramAUT_1 { get; set; }
        [LoadColumn(20)]
        [ColumnName("BigramAUT_2")]
        public float BigramAUT_2 { get; set; }
        [LoadColumn(21)]
        [ColumnName("BigramAUT_3")]
        public float BigramAUT_3 { get; set; }
        [LoadColumn(22)]
        [ColumnName("BigramAUT_4")]
        public float BigramAUT_4 { get; set; }
        [LoadColumn(23)]
        [ColumnName("BigramAUT_5")]
        public float BigramAUT_5 { get; set; }
        [LoadColumn(24)]
        [ColumnName("BigramAUT_6")]
        public float BigramAUT_6 { get; set; }
        [LoadColumn(25)]
        [ColumnName("BigramAUT_7")]
        public float BigramAUT_7 { get; set; }
        [LoadColumn(26)]
        [ColumnName("BigramAUT_8")]
        public float BigramAUT_8 { get; set; }
        [LoadColumn(27)]
        [ColumnName("BigramCount")]
        public float BigramCount { get; set; }
        [LoadColumn(28)]
        [ColumnName("BigramCountInverted")]
        public float BigramCountInverted { get; set; }
        [LoadColumn(29)]
        [ColumnName("BodyMostFrequent")]
        public float BodyMostFrequent { get; set; }
        [LoadColumn(30)]
        [ColumnName("BodyStreamLength")]
        public float BodyStreamLength { get; set; }
        [LoadColumn(31)]
        [ColumnName("BodyStreamLengthBase")]
        public float BodyStreamLengthBase { get; set; }
        [LoadColumn(32)]
        [ColumnName("BodyStreamLengthExistence")]
        public float BodyStreamLengthExistence { get; set; }
        [LoadColumn(33)]
        [ColumnName("BodyUniqueTermCount")]
        public float BodyUniqueTermCount { get; set; }
        [LoadColumn(34)]
        [ColumnName("CPDv3")]
        public float CPDv3 { get; set; }
        [LoadColumn(35)]
        [ColumnName("CPDv3AgeInDays")]
        public float CPDv3AgeInDays { get; set; }
        [LoadColumn(36)]
        [ColumnName("CPDv3AgeInDays2")]
        public float CPDv3AgeInDays2 { get; set; }
        [LoadColumn(37)]
        [ColumnName("CPDv4")]
        public float CPDv4 { get; set; }
        [LoadColumn(38)]
        [ColumnName("ClickCount")]
        public float ClickCount { get; set; }
        [LoadColumn(39)]
        [ColumnName("ClickCountInverted")]
        public float ClickCountInverted { get; set; }
        [LoadColumn(40)]
        [ColumnName("Click_0")]
        public float Click_0 { get; set; }
        [LoadColumn(41)]
        [ColumnName("Click_1")]
        public float Click_1 { get; set; }
        [LoadColumn(42)]
        [ColumnName("Click_2")]
        public float Click_2 { get; set; }
        [LoadColumn(43)]
        [ColumnName("Click_3")]
        public float Click_3 { get; set; }
        [LoadColumn(44)]
        [ColumnName("Click_4")]
        public float Click_4 { get; set; }
        [LoadColumn(45)]
        [ColumnName("Click_5")]
        public float Click_5 { get; set; }
        [LoadColumn(46)]
        [ColumnName("Click_6")]
        public float Click_6 { get; set; }
        [LoadColumn(47)]
        [ColumnName("Click_7")]
        public float Click_7 { get; set; }
        [LoadColumn(48)]
        [ColumnName("Click_8")]
        public float Click_8 { get; set; }
        [LoadColumn(49)]
        [ColumnName("Click_9")]
        public float Click_9 { get; set; }
        [LoadColumn(50)]
        [ColumnName("Click_All")]
        public float Click_All { get; set; }
        [LoadColumn(51)]
        [ColumnName("Click_BingClickUJ")]
        public float Click_BingClickUJ { get; set; }
        [LoadColumn(52)]
        [ColumnName("Click_IESessionUA")]
        public float Click_IESessionUA { get; set; }
        [LoadColumn(53)]
        [ColumnName("Click_IETBEnusA")]
        public float Click_IETBEnusA { get; set; }
        [LoadColumn(54)]
        [ColumnName("Click_IETBUA")]
        public float Click_IETBUA { get; set; }
        [LoadColumn(55)]
        [ColumnName("DiscoveredAgeInDays")]
        public float DiscoveredAgeInDays { get; set; }
        [LoadColumn(56)]
        [ColumnName("DiscoveredAgeInDaysTaza")]
        public float DiscoveredAgeInDaysTaza { get; set; }
        [LoadColumn(57)]
        [ColumnName("DiscoveredAgeInMinutesTaza")]
        public float DiscoveredAgeInMinutesTaza { get; set; }
        [LoadColumn(58)]
        [ColumnName("DiscoveryTimeTicks")]
        public float DiscoveryTimeTicks { get; set; }
        //        [LoadColumn(59)]
        //        public float DocumentLanguageHash { get; set; }
        //        [LoadColumn(60)]
        //        public float DocumentLocationHash { get; set; }
        [LoadColumn(61)]
        [ColumnName("DocumentShard")]
        public float DocumentShard { get; set; }
        [LoadColumn(62)]
        [ColumnName("DocumentShardBSL")]
        public float DocumentShardBSL { get; set; }
        [LoadColumn(63)]
        [ColumnName("DocumentShardDummy0")]
        public float DocumentShardDummy0 { get; set; }
        [LoadColumn(64)]
        [ColumnName("DomainRank")]
        public float DomainRank { get; set; }
        [LoadColumn(65)]
        [ColumnName("DomainRankBW_Div255")]
        public float DomainRankBW_Div255 { get; set; }
        [LoadColumn(66)]
        [ColumnName("FoundDateInDays")]
        public float FoundDateInDays { get; set; }
        [LoadColumn(67)]
        [ColumnName("FreshIndexTimeAgeBucket1")]
        public float FreshIndexTimeAgeBucket1 { get; set; }
        [LoadColumn(68)]
        [ColumnName("FreshIndexTimeAgeInMin")]
        public float FreshIndexTimeAgeInMin { get; set; }
        [LoadColumn(69)]
        [ColumnName("FreshIndexTimeInMin")]
        public float FreshIndexTimeInMin { get; set; }
        [LoadColumn(70)]
        [ColumnName("FreshSuperTail")]
        public float FreshSuperTail { get; set; }
        [LoadColumn(71)]
        [ColumnName("FreshSuperTailServed")]
        public float FreshSuperTailServed { get; set; }
        [LoadColumn(72)]
        [ColumnName("FreshTier")]
        public float FreshTier { get; set; }
        [LoadColumn(73)]
        [ColumnName("FreshTierServed")]
        public float FreshTierServed { get; set; }
        [LoadColumn(74)]
        [ColumnName("IdfX10_0")]
        public float IdfX10_0 { get; set; }
        [LoadColumn(75)]
        [ColumnName("IdfX10_1")]
        public float IdfX10_1 { get; set; }
        [LoadColumn(76)]
        [ColumnName("IdfX10_2")]
        public float IdfX10_2 { get; set; }
        [LoadColumn(77)]
        [ColumnName("IdfX10_3")]
        public float IdfX10_3 { get; set; }
        [LoadColumn(78)]
        [ColumnName("IdfX10_4")]
        public float IdfX10_4 { get; set; }
        [LoadColumn(79)]
        [ColumnName("IdfX10_5")]
        public float IdfX10_5 { get; set; }
        [LoadColumn(80)]
        [ColumnName("IdfX10_6")]
        public float IdfX10_6 { get; set; }
        [LoadColumn(81)]
        [ColumnName("IdfX10_7")]
        public float IdfX10_7 { get; set; }
        [LoadColumn(82)]
        [ColumnName("IdfX10_8")]
        public float IdfX10_8 { get; set; }
        [LoadColumn(83)]
        [ColumnName("IdfX10_9")]
        public float IdfX10_9 { get; set; }
        [LoadColumn(84)]
        [ColumnName("IsNews")]
        public float IsNews { get; set; }
        [LoadColumn(85)]
        [ColumnName("JunkPage")]
        public float JunkPage { get; set; }
        [LoadColumn(86)]
        [ColumnName("LUDv1")]
        public float LUDv1 { get; set; }
        [LoadColumn(87)]
        [ColumnName("LUDv1AgeInDays")]
        public float LUDv1AgeInDays { get; set; }
        [LoadColumn(88)]
        [ColumnName("LUDv1AgeInDays2")]
        public float LUDv1AgeInDays2 { get; set; }
        [LoadColumn(89)]
        [ColumnName("Language")]
        public float Language { get; set; }
        [LoadColumn(90)]
        [ColumnName("Location")]
        public float Location { get; set; }
        [LoadColumn(91)]
        [ColumnName("NumberOfPerfectMatches_MultiInstanceTitle")]
        public float NumberOfPerfectMatches_MultiInstanceTitle { get; set; }
        [LoadColumn(92)]
        [ColumnName("NumberOfPerfectMatches_MultiInstanceUrlV3")]
        public float NumberOfPerfectMatches_MultiInstanceUrlV3 { get; set; }
        [LoadColumn(93)]
        [ColumnName("NumberOfPerfectMatches_SRAnchor")]
        public float NumberOfPerfectMatches_SRAnchor { get; set; }
        [LoadColumn(94)]
        [ColumnName("NumberOfPositionsInQuery")]
        public float NumberOfPositionsInQuery { get; set; }
        [LoadColumn(95)]
        [ColumnName("OdpClassifierV2")]
        public float OdpClassifierV2 { get; set; }
        [LoadColumn(96)]
        [ColumnName("PackedFrequenciesNewAnchor_0")]
        public float PackedFrequenciesNewAnchor_0 { get; set; }
        [LoadColumn(97)]
        [ColumnName("PackedFrequenciesNewAnchor_1")]
        public float PackedFrequenciesNewAnchor_1 { get; set; }
        [LoadColumn(98)]
        [ColumnName("PackedFrequenciesNewAnchor_2")]
        public float PackedFrequenciesNewAnchor_2 { get; set; }
        [LoadColumn(99)]
        [ColumnName("PackedFrequenciesNewAnchor_3")]
        public float PackedFrequenciesNewAnchor_3 { get; set; }
        [LoadColumn(100)]
        [ColumnName("PackedFrequenciesNewAnchor_4")]
        public float PackedFrequenciesNewAnchor_4 { get; set; }
        [LoadColumn(101)]
        [ColumnName("PackedFrequenciesNewAnchor_5")]
        public float PackedFrequenciesNewAnchor_5 { get; set; }
        [LoadColumn(102)]
        [ColumnName("PackedFrequenciesNewAnchor_6")]
        public float PackedFrequenciesNewAnchor_6 { get; set; }
        [LoadColumn(103)]
        [ColumnName("PackedFrequenciesNewAnchor_7")]
        public float PackedFrequenciesNewAnchor_7 { get; set; }
        [LoadColumn(104)]
        [ColumnName("PackedFrequenciesNewAnchor_8")]
        public float PackedFrequenciesNewAnchor_8 { get; set; }
        [LoadColumn(105)]
        [ColumnName("PackedFrequenciesNewAnchor_9")]
        public float PackedFrequenciesNewAnchor_9 { get; set; }
        [LoadColumn(106)]
        [ColumnName("PublicationDateDays")]
        public float PublicationDateDays { get; set; }
        [LoadColumn(107)]
        [ColumnName("QueryHasClickPhrase")]
        public float QueryHasClickPhrase { get; set; }
        [LoadColumn(108)]
        [ColumnName("RankingWordDiscoveredAgeInDays")]
        public float RankingWordDiscoveredAgeInDays { get; set; }
        [LoadColumn(109)]
        [ColumnName("RankingWordDiscoveryDate")]
        public float RankingWordDiscoveryDate { get; set; }
        [LoadColumn(110)]
        [ColumnName("RankingWordSegment")]
        public float RankingWordSegment { get; set; }
        [LoadColumn(111)]
        [ColumnName("RankingWordSegmentAuthority")]
        public float RankingWordSegmentAuthority { get; set; }
        [LoadColumn(112)]
        [ColumnName("RelaxCount")]
        public float RelaxCount { get; set; }
        [LoadColumn(113)]
        [ColumnName("RelaxCountInverted")]
        public float RelaxCountInverted { get; set; }
        [LoadColumn(114)]
        [ColumnName("SpamPage")]
        public float SpamPage { get; set; }
        [LoadColumn(115)]
        [ColumnName("StaticRank")]
        public float StaticRank { get; set; }
        [LoadColumn(116)]
        [ColumnName("StreamFrequency_0URL_0")]
        public float StreamFrequency_0URL_0 { get; set; }
        [LoadColumn(117)]
        [ColumnName("StreamFrequency_0URL_1")]
        public float StreamFrequency_0URL_1 { get; set; }
        [LoadColumn(118)]
        [ColumnName("StreamFrequency_0URL_2")]
        public float StreamFrequency_0URL_2 { get; set; }
        [LoadColumn(119)]
        [ColumnName("StreamFrequency_0URL_3")]
        public float StreamFrequency_0URL_3 { get; set; }
        [LoadColumn(120)]
        [ColumnName("StreamFrequency_0URL_4")]
        public float StreamFrequency_0URL_4 { get; set; }
        [LoadColumn(121)]
        [ColumnName("StreamFrequency_0URL_5")]
        public float StreamFrequency_0URL_5 { get; set; }
        [LoadColumn(122)]
        [ColumnName("StreamFrequency_0URL_6")]
        public float StreamFrequency_0URL_6 { get; set; }
        [LoadColumn(123)]
        [ColumnName("StreamFrequency_0URL_7")]
        public float StreamFrequency_0URL_7 { get; set; }
        [LoadColumn(124)]
        [ColumnName("StreamFrequency_0URL_8")]
        public float StreamFrequency_0URL_8 { get; set; }
        [LoadColumn(125)]
        [ColumnName("StreamFrequency_0URL_9")]
        public float StreamFrequency_0URL_9 { get; set; }
        [LoadColumn(126)]
        [ColumnName("StreamFrequency_1Title_0")]
        public float StreamFrequency_1Title_0 { get; set; }
        [LoadColumn(127)]
        [ColumnName("StreamFrequency_1Title_1")]
        public float StreamFrequency_1Title_1 { get; set; }
        [LoadColumn(128)]
        [ColumnName("StreamFrequency_1Title_2")]
        public float StreamFrequency_1Title_2 { get; set; }
        [LoadColumn(129)]
        [ColumnName("StreamFrequency_1Title_3")]
        public float StreamFrequency_1Title_3 { get; set; }
        [LoadColumn(130)]
        [ColumnName("StreamFrequency_1Title_4")]
        public float StreamFrequency_1Title_4 { get; set; }
        [LoadColumn(131)]
        [ColumnName("StreamFrequency_1Title_5")]
        public float StreamFrequency_1Title_5 { get; set; }
        [LoadColumn(132)]
        [ColumnName("StreamFrequency_1Title_6")]
        public float StreamFrequency_1Title_6 { get; set; }
        [LoadColumn(133)]
        [ColumnName("StreamFrequency_1Title_7")]
        public float StreamFrequency_1Title_7 { get; set; }
        [LoadColumn(134)]
        [ColumnName("StreamFrequency_1Title_8")]
        public float StreamFrequency_1Title_8 { get; set; }
        [LoadColumn(135)]
        [ColumnName("StreamFrequency_1Title_9")]
        public float StreamFrequency_1Title_9 { get; set; }
        [LoadColumn(136)]
        [ColumnName("StreamFrequency_4AnchorNew_0")]
        public float StreamFrequency_4AnchorNew_0 { get; set; }
        [LoadColumn(137)]
        [ColumnName("StreamFrequency_4AnchorNew_1")]
        public float StreamFrequency_4AnchorNew_1 { get; set; }
        [LoadColumn(138)]
        [ColumnName("StreamFrequency_4AnchorNew_2")]
        public float StreamFrequency_4AnchorNew_2 { get; set; }
        [LoadColumn(139)]
        [ColumnName("StreamFrequency_4AnchorNew_3")]
        public float StreamFrequency_4AnchorNew_3 { get; set; }
        [LoadColumn(140)]
        [ColumnName("StreamFrequency_4AnchorNew_4")]
        public float StreamFrequency_4AnchorNew_4 { get; set; }
        [LoadColumn(141)]
        [ColumnName("StreamFrequency_4AnchorNew_5")]
        public float StreamFrequency_4AnchorNew_5 { get; set; }
        [LoadColumn(142)]
        [ColumnName("StreamFrequency_4AnchorNew_6")]
        public float StreamFrequency_4AnchorNew_6 { get; set; }
        [LoadColumn(143)]
        [ColumnName("StreamFrequency_4AnchorNew_7")]
        public float StreamFrequency_4AnchorNew_7 { get; set; }
        [LoadColumn(144)]
        [ColumnName("StreamFrequency_4AnchorNew_8")]
        public float StreamFrequency_4AnchorNew_8 { get; set; }
        [LoadColumn(145)]
        [ColumnName("StreamFrequency_4AnchorNew_9")]
        public float StreamFrequency_4AnchorNew_9 { get; set; }
        [LoadColumn(146)]
        [ColumnName("StreamFrequency_2Body_0")]
        public float StreamFrequency_2Body_0 { get; set; }
        [LoadColumn(147)]
        [ColumnName("StreamFrequency_2Body_1")]
        public float StreamFrequency_2Body_1 { get; set; }
        [LoadColumn(148)]
        [ColumnName("StreamFrequency_2Body_2")]
        public float StreamFrequency_2Body_2 { get; set; }
        [LoadColumn(149)]
        [ColumnName("StreamFrequency_2Body_3")]
        public float StreamFrequency_2Body_3 { get; set; }
        [LoadColumn(150)]
        [ColumnName("StreamFrequency_2Body_4")]
        public float StreamFrequency_2Body_4 { get; set; }
        [LoadColumn(151)]
        [ColumnName("StreamFrequency_2Body_5")]
        public float StreamFrequency_2Body_5 { get; set; }
        [LoadColumn(152)]
        [ColumnName("StreamFrequency_2Body_6")]
        public float StreamFrequency_2Body_6 { get; set; }
        [LoadColumn(153)]
        [ColumnName("StreamFrequency_2Body_7")]
        public float StreamFrequency_2Body_7 { get; set; }
        [LoadColumn(154)]
        [ColumnName("StreamFrequency_2Body_8")]
        public float StreamFrequency_2Body_8 { get; set; }
        [LoadColumn(155)]
        [ColumnName("StreamFrequency_2Body_9")]
        public float StreamFrequency_2Body_9 { get; set; }
        [LoadColumn(156)]
        [ColumnName("TimeInMinutesSinceJanuary2001")]
        public float TimeInMinutesSinceJanuary2001 { get; set; }
        [LoadColumn(157)]
        [ColumnName("TitleStreamLength")]
        public float TitleStreamLength { get; set; }
        [LoadColumn(158)]
        [ColumnName("TitleStreamLengthBase")]
        public float TitleStreamLengthBase { get; set; }
        [LoadColumn(159)]
        [ColumnName("TopLevelDomain")]
        public float TopLevelDomain { get; set; }
        [LoadColumn(160)]
        [ColumnName("TotalAnchor")]
        public float TotalAnchor { get; set; }
        [LoadColumn(161)]
        [ColumnName("UrlStreamLength")]
        public float UrlStreamLength { get; set; }
        [LoadColumn(162)]
        [ColumnName("UrlStreamLengthBase")]
        public float UrlStreamLengthBase { get; set; }
        [LoadColumn(163)]
        [ColumnName("WordsFound_0URL")]
        public float WordsFound_0URL { get; set; }
        [LoadColumn(164)]
        [ColumnName("WordsFound_1Title")]
        public float WordsFound_1Title { get; set; }
        [LoadColumn(165)]
        [ColumnName("WordsFound_4AnchorNew")]
        public float WordsFound_4AnchorNew { get; set; }
        [LoadColumn(166)]
        [ColumnName("WordsFound_2Body")]
        public float WordsFound_2Body { get; set; }
        [LoadColumn(167)]
        [ColumnName("WordsInPath")]
        public float WordsInPath { get; set; }
        [LoadColumn(168)]
        [ColumnName("WordsInPathBase")]
        public float WordsInPathBase { get; set; }
        //        [LoadColumn(169)]
        //        public float L1ScoreDiv2000 { get; set; }
        //        [LoadColumn(170)]
        //        public float L1ScoreDiv1500 { get; set; }
        //        [LoadColumn(171)]
        //        public float L1ScoreDiv1000 { get; set; }
        //        [LoadColumn(172)]
        //        public float L1ScoreDiv500 { get; set; }
        //        [LoadColumn(173)]
        //        public float L1ScoreDiv100 { get; set; }
        //        [LoadColumn(174)]
        //        public float QLF_SimilarityDiv20 { get; set; }
        //        [LoadColumn(175)]
        //        public float QLF_SimilarityDiv50 { get; set; }
        //        [LoadColumn(176)]
        //        public float QLF_SimilarityDiv100 { get; set; }
        //        [LoadColumn(177)]
        //        public float QLF_SimilarityDiv200 { get; set; }
        [LoadColumn(178)]
        [ColumnName("BM25F_URL_Log65K15")]
        public float BM25F_URL_Log65K15 { get; set; }
        [LoadColumn(179)]
        [ColumnName("BM25F_Title_Log65K15")]
        public float BM25F_Title_Log65K15 { get; set; }
    };
}
