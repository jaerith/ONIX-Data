using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using OnixData.Standard.Legacy;
using OnixData.Standard.Version3;

namespace OnixData.Standard.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net462, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [RPlotExporter]
    public class OnixParsersBenchmark
    {
        private string fileName;
        
        [Params("Sample-1-Product", "Sample-5-Products", "Sample-50-Products", "Sample-500-Products")]
        public string FileName;

        [GlobalSetup]
        public void Setup()
        {
            fileName = FileName;
        }
        
        private void ParseSampleFileUsingV3Parser(string sampleFilename)
        {
            var filepath = $"Samples/V3/{sampleFilename}.xml";

            var fileInfo = new FileInfo(filepath);
            using var v3Parser = new OnixParser(fileInfo, true);
            
            foreach (OnixProduct tmpProduct in v3Parser)
            {
                if (!tmpProduct.IsValid())
                {
                    Console.WriteLine(tmpProduct.GetParsingError()?.Message);
                }
            }
        }

        private void ParseSampleFileUsingV3ParserNoPreprocess(string sampleFilename)
        {
            var filepath = $"Samples/V3/{sampleFilename}.xml";

            var fileInfo = new FileInfo(filepath);

            // NOTE: If you know your source is providing ONIX files without problematic data
            //       (no special ONIX encodings, lack of bad characters, etc.), then you can gain
            //       a significant performance boost by turning off preprocessing.
            //       But be sure of that data quality beforehand.  And then count yourself very lucky...
            using var v3Parser = 
                new OnixParser(File.ReadAllText(fileInfo.FullName), false, false);

            foreach (OnixProduct tmpProduct in v3Parser)
            {
                if (!tmpProduct.IsValid())
                {
                    Console.WriteLine(tmpProduct.GetParsingError()?.Message);
                }
            }
        }

        private void ParseSampleFileUsingLegacyParser(string sampleFilename)
        {
            var filepath = $"Samples/Legacy/{sampleFilename}.xml";

            var fileInfo = new FileInfo(filepath);
            using var legacyParser = new OnixLegacyParser(fileInfo, true);
            
            foreach (OnixLegacyProduct tmpProduct in legacyParser)
            {
                if (!tmpProduct.IsValid())
                {
                    Console.WriteLine(tmpProduct.GetParsingError()?.Message);
                }
            }
        }

        private void ParseSampleFileUsingLegacyParserNoPreprocess(string sampleFilename)
        {
            var filepath = $"Samples/Legacy/{sampleFilename}.xml";

            var fileInfo = new FileInfo(filepath);

            // NOTE: If you know your source is providing ONIX files without problematic data
            //       (no special ONIX encodings, lack of bad characters, etc.), then you can gain
            //       a significant performance boost by turning off preprocessing.
            //       But be sure of that data quality beforehand.  And then count yourself very lucky...
            using var legacyParser = 
                new OnixLegacyParser(File.ReadAllText(fileInfo.FullName), false, false);

            foreach (OnixLegacyProduct tmpProduct in legacyParser)
            {
                if (!tmpProduct.IsValid())
                {
                    Console.WriteLine(tmpProduct.GetParsingError()?.Message);
                }
            }
        }

        [Benchmark]
        public void V3Parser() => ParseSampleFileUsingV3Parser(fileName);

        [Benchmark]
        public void V3ParserNoPreprocessing() => ParseSampleFileUsingV3ParserNoPreprocess(fileName);

        [Benchmark]
        public void LegacyParser() => ParseSampleFileUsingLegacyParser(fileName);

        [Benchmark]
        public void LegacyParserNoPreprocessing() => ParseSampleFileUsingLegacyParserNoPreprocess(fileName);
    }

    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<OnixParsersBenchmark>();
        }
    }
}
