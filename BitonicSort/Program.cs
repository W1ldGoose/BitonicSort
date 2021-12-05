using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace BitonicSort
{
    /*Для сортировки массива произвольного размера N, 
    исходный массив должен быть разделён на подмассивы 
    размера степени двойки. Каждый подмассив сортируется 
    битонической сортировкой, а затем производится слияние
    уже отсортированных подмассивов в итоговый отсортированный массив*/
    public class Program
    {
        [MemoryDiagnoser()]
        [Orderer(SummaryOrderPolicy.FastestToSlowest)]
        [RankColumn()]
        [RPlotExporter]
        [SimpleJob(RunStrategy.Throughput)]
        public class BenchmarkTest
        {
            public IEnumerable<int> ValuesForN => new int[]
            {
                (int) Math.Pow(2, 15),
                (int) Math.Pow(2, 17),
                (int) Math.Pow(2, 20)
            };

            [ParamsSource(nameof(ValuesForN))] public int N = 1024;

            public double[] unsortedArray;

            [GlobalSetup]
            public void Setup()
            {
                unsortedArray = new double[N];
                FillArray(unsortedArray);
            }

            [Benchmark]
            public void ParallelTest()
            {
                // unsortedArray = new double[N];
                // FillArray(unsortedArray);
                BitonicSort.ParallelBitonicSort(unsortedArray, N);
                File.AppendAllText("test.txt", IsSorted(unsortedArray).ToString());
            }

            [Benchmark]
            public void SerialTest() => BitonicSort.SerialBitonicSort(unsortedArray, N);
        }

        public static void FillArray(double[] unsortedArray)
        {
            int minValue = 0;
            int maxValue = 100;
            Random rand = new Random();
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                unsortedArray[i] = rand.NextDouble() * (maxValue - minValue) + minValue;
            }
        }

        public static bool IsSorted(double[] a)
        {
            bool sorted = true;
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i - 1] > a[i])
                {
                    sorted = false;
                    break;
                }
            }

            return sorted;
        }

        public static double[] testArr = new double[4096];

        static void Main(string[] args)
        {
            /* FillArray(testArr);
             BitonicSort.SerialBitonicSort(testArr, testArr.Length);
             FillArray(testArr);
             BitonicSort.ParallelBitonicSort(testArr, testArr.Length);
            */

            var summary = BenchmarkRunner.Run<BenchmarkTest>();
        }
    }
}