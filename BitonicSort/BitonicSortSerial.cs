
// Последовательная версия алгоритма bitonic sort

using System;
using System.Collections.Generic;

namespace BitonicSort
{
    public partial class BitonicSort
    {
        private const bool INC = true;
        private const bool DEC = false;
        private void Swap<T>(ref T i, ref T j)
        {
            T tmp = i;
            i = j;
            j = tmp;
        }
        // Метод компаратор для сортировки
        private void Compare<T>(T[] items, int i, int j, bool dir) where T: IComparable<T>
        {
            var result = items[i].CompareTo(items[j]) > 0;
            if (dir == result)
            {
                Swap(ref items[i], ref items[j]);
            }
        }

        private void BitonicMerge<T>(T[] items, int lo, int n, bool dir) where T : IComparable<T>
        {
            if (n > 1)
            {
                int m = n / 2;
                for (int i = lo; i < lo + m; ++i)
                {
                    Compare<T>(items, i, i + m, dir);
                }
                BitonicMerge<T>(items,lo,m,dir);
                BitonicMerge<T>(items,lo+m,m,dir);
            }
        }

        void SerialSort<T>(T[] items, int lo, int n, bool dir) where T : IComparable<T>
        {
            if (n > 1)
            {
                int m = n / 2;
                SerialSort(items, lo,m,INC );
                SerialSort(items,lo+m,m,DEC);
                
                BitonicMerge(items,lo,n,dir);
            }
        }
        void SerialSort<T>(T[] items, int size) where T : IComparable<T>
        {
            SerialSort<T>(items,0,size,INC);
        }
    }
}