using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    public static class Iterators
    {
        public static IEnumerable<T> Flatten<T>(IEnumerable<IEnumerable<T>> items) => items.SelectMany(i => i ?? new T[1]);
        public static IEnumerable<T> Filter<T>(IEnumerable<T> items, Predicate<T> predicate) => items.Where(item => predicate(item));
    }
}