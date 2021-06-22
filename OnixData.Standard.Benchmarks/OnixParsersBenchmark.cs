using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using OnixData.Version3;

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
        
        [Params("Sample-1-Products", "Sample-5-Products", "Sample-50-Products", "Sample-500-Products")]
        public string FileName;

        [GlobalSetup]
        public void Setup()
        {
            fileName = FileName;
        }
        
        private void ParseSampleFileUsingV3Parser(string sampleFilename)
        {
            var filepath = $"Samples/{sampleFilename}.xml";

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
        
        private void ParseSampleFileUsingLegacyParser(string sampleFilename)
        {
            var filepath = $"Samples/{sampleFilename}.xml";

            var fileInfo = new FileInfo(filepath);
            using var legacyParser = new OnixLegacyParser(fileInfo, true);
            
            foreach (OnixProduct tmpProduct in legacyParser)
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
        public void LegacyParser() => ParseSampleFileUsingLegacyParser(fileName);
    }
    
    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<OnixParsersBenchmark>();
        }
    }
}
