//#define Invoke

using System;
using System.Threading;
using System.Threading.Tasks;

namespace BitonicSort
{
    public static partial class BitonicSort
    {
        public static int ThreadsCount = 4;
        private static int length;

        private static void ParallelBitonicMerge<T>(T[] items, int low, int n, bool dir) where T : IComparable<T>
        {
            if(n<=1)
                return;
            // мержим последовательности параллельно только если достаточно работы
            int half = n / 2;
            for (int i = low; i < low + half; ++i)
            {
                Compare(items, i, i + half, dir);
            }
            if (n > length)
            {
#if Invoke
                Parallel.Invoke(
                    ()=>
                    {
                        ParallelBitonicMerge<T>(items, low, m, dir);
                    },
                    ()=>
                    {
                        ParallelBitonicMerge<T>(items, low + m, m, dir);
                    });
#else
                Thread lowThread = new Thread(() => { ParallelBitonicMerge<T>(items, low, half, dir); });
                Thread highThread = new Thread(() => { ParallelBitonicMerge<T>(items, low + half, half, dir); });

                lowThread.Start();
                highThread.Start();

                lowThread.Join();
                highThread.Join();
#endif
            }
            else
            {
                if (half > 1)
                {
                    BitonicMerge(items, low, n, dir);
                }
            }
        }

        private static void ParallelBitonicSort<T>(T[] items, int low, int n, bool dir) where T : IComparable<T>
        {
            if (n <= 1)
                return;
            int half = n / 2;
            if (n > length)
            {
#if Invoke
                Parallel.Invoke(
                    () =>
                    {
                        ParallelBitonicSort(items, low, m, Inc);
                    },
                    () =>
                    {
                        ParallelBitonicSort(items, low + m, m, Dec);
                    });
#else
                Thread lowThread = new Thread(() => { ParallelBitonicSort(items, low, half, Inc); });
                Thread highThread = new Thread(() => { ParallelBitonicSort(items, low + half, half, Dec); });

                lowThread.Start();
                highThread.Start();

                lowThread.Join();
                highThread.Join();
#endif
            }
            else
            {
                if (half > 1)
                {
                    SerialBitonicSort(items, low, half, Inc);
                    SerialBitonicSort(items, low + half, half, Dec);
                }
            }

            ParallelBitonicMerge(items, low, n, dir);
        }

        public static void ParallelBitonicSort<T>(T[] items, int size) where T : IComparable<T>
        {
            length = size / ThreadsCount;
            ParallelBitonicSort(items, 0, size, Inc);
        }
    }
}