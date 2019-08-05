using System;
using System.Collections.Generic;

namespace Fwuan.Compiler
{
    public static class ReadOnlySpanExtensions
    {
        public static int Count<T>(this ReadOnlySpan<T> sequence, T item, EqualityComparer<T> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;

            int count = 0;
            foreach (T t in sequence)
                if (comparer.Equals(t, item))
                    count++;

            return count;
        }
        
        public static int Count<T>(this ReadOnlySpan<T> sequence, Func<T, bool> equality)
        {
            int count = 0;
            foreach (T item in sequence)
                if (equality(item))
                    count++;

            return count;
        }

        public static string ToString(this ReadOnlySpan<char> sequence, int start, int length) => sequence.Slice(start, length).ToString();
    }
}