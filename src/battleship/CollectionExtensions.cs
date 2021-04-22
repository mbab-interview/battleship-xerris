using System.Collections.Generic;
using System.Linq;

namespace battleship
{
    public static class CollectionExtensions
    {
        // This is not performant, but since we are working with tiny lists, the readability outweights the performance
        public static bool AllIdentical<T>(this IEnumerable<T> collection)
        {
            return collection.Distinct().Count() == 1;
        }

        // This is not performant, but since we are working with tiny lists, the readability outweights the performance
        public static bool AllDifferent<T>(this IEnumerable<T> collection)
        {
            return collection.Distinct().Count() == collection.Count();
        }

        // Clever trick inspired from SO https://stackoverflow.com/a/13359693
        public static bool AreConsecutive(this IEnumerable<int> ints)
        {
            //Is empty consecutive?
            if (!ints.Any())
                return true;

            return !ints.OrderBy(x => x).Select((i, j) => i - j).Distinct().Skip(1).Any();
        }

        // Clever trick inspired from SO https://stackoverflow.com/a/13359693
        public static bool AreConsecutive(this IEnumerable<char> chars)
        {
            //Is empty consecutive?
            if (!chars.Any())
                return true;

            return !chars.OrderBy(x => x).Select((i, j) => i - j).Distinct().Skip(1).Any();
        }
    }
}
