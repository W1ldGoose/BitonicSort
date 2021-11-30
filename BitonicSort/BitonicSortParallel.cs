using System;
using System.Threading;
using System.Threading.Tasks;

namespace BitonicSort
{
    public static partial class BitonicSort
    {
        private static void ParallelBitonicMerge<T>(T[] items, int low, int n, bool dir) where T : IComparable<T>
        {
            // мержим последовательности параллельно только если достаточно работы
            if (n > 700)
            {
                int m = n / 2;
                for (int i = low; i < low + m; ++i)
                {
                    Compare(items,i,i+m,dir);
                }
                // вариант через лямбда выражения
                Parallel.Invoke(
                    ()=>
                    {
                        //Console.WriteLine("Выполняется ParallelBitonicMerge m={0}, low={1}",m,low);
                        ParallelBitonicMerge<T>(items, low, m, dir);
                    },
                    ()=>
                    {
                     //   Console.WriteLine("Выполняется ParallelBitonicMerge m={0}, low+m={1}",m,low+m);
                        ParallelBitonicMerge<T>(items, low + m, m, dir);
                    });
            }
            else if (n>1)
            {
                BitonicMerge(items,low,n,dir);
            }
        }

        private static void ParallelBitonicSort<T>(T[] items, int low, int n, bool dir) where T : IComparable<T>
        {
            if (n > 1)
            {
                int m = n / 2;
                
                Parallel.Invoke(
                    () =>
                    {
                        ParallelBitonicSort(items, low, m, Inc);
                    },
                    () =>
                    {
                        ParallelBitonicSort(items, low + m, m, Dec);
                    });
                ParallelBitonicMerge(items,low,n,dir);
            }
        }

        public static void ParallelBitonicSort<T>(T[] items, int size) where T : IComparable<T>
        {
            ParallelBitonicSort(items,0,size,Inc);
        }
        
    }
}