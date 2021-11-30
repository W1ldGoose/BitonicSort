
// Последовательная версия алгоритма bitonic sort

using System;
using System.Collections.Generic;

namespace BitonicSort
{
    public static partial class BitonicSort
    {
        private const bool Inc = true;
        private const bool Dec = false;
        private static void Swap<T>(ref T i, ref T j)
        {
            T tmp = i;
            i = j;
            j = tmp;
        }
        // Метод компаратор для сортировки
        private static void Compare<T>(T[] items, int i, int j, bool dir) where T: IComparable<T>
        {
            var result = items[i].CompareTo(items[j]) >= 0;
            if (dir == result)
            {
                Swap(ref items[i], ref items[j]);
            }
        }

        private static void BitonicMerge<T>(T[] items, int low, int n, bool dir) where T : IComparable<T>
        {
            if (n > 1)
            {
                int m = n / 2;
                for (int i = low; i < low + m; ++i)
                {
                    Compare(items, i, i + m, dir);
                }
                BitonicMerge(items,low,m,dir);
                BitonicMerge(items,low+m,m,dir);
            }
        }

        static void SerialBitonicSort<T>(T[] items, int low, int n, bool dir) where T : IComparable<T>
        {
            if (n > 1)
            {
                int m = n / 2;
                SerialBitonicSort(items, low,m,Inc );
                SerialBitonicSort(items,low+m,m,Dec);
                
                BitonicMerge(items,low,n,dir);
            }
        }

        public static void SerialBitonicSort<T>(T[] items, int size) where T : IComparable<T>
        {
            SerialBitonicSort<T>(items,0,size,Inc);
        }
    }
}