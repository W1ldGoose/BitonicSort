using System;
using System.Collections.Generic;
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
               (int) Math.Pow(2, 21),
               (int) Math.Pow(2, 22)
            };
           
            [ParamsSource(nameof(ValuesForN))] 
            public int N = 1000;

            public int[] unsortedArray;

            [GlobalSetup]
            public void Setup()
            {
                unsortedArray = new int[N];
                FillArray(unsortedArray);
            }

            [Benchmark]
            public void ParallelTest() => BitonicSort.ParallelBitonicSort(unsortedArray, N);

            [Benchmark]
            public void SerialTest() => BitonicSort.SerialBitonicSort(unsortedArray, N);
        }
        
        public static void FillArray(int[] unsortedArray)
        {
            Random rand = new Random();
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                unsortedArray[i] = rand.Next(0, 1000000);
            }
        }

        public static int[] testArr = new int[1024];
        static void Main(string[] args)
        {
          
            
            /*
            FillArray(testArr);
           // BitonicSort.SerialSort(testArr,testArr.Length);
           BitonicSort.ParallelBitonicSort(testArr, testArr.Length);
           foreach (var i in testArr)
           {
               Console.Write(i+" ");
           }
           */
            
            var summary = BenchmarkRunner.Run<BenchmarkTest>();
            
        }
    }
}