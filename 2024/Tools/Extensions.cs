namespace AdventOfCode2024.Tools;

static class Extensions
{
    public static IEnumerable<(T, T)> Pairs<T>(this IEnumerable<T> values)
    {
        if (values.Count() < 2) yield break;

        HashSet<T> seen = [];

        foreach (T valueA in values)
        {
            seen.Add(valueA);

            foreach (T valueB in values)
            {
                // To avoid (y, x) if (x, y) already seen
                if (seen.Contains(valueB)) continue;

                yield return (valueA, valueB);
            }
        }
    }

    public static string Join<T>(this IEnumerable<T> values, char c) =>
        string.Join(c, values);

    public static bool IsInBounds<T>(this List<List<T>> values, Point point) =>
        point.X >= 0 && point.X < values[0].Count && point.Y >= 0 && point.Y < values.Count;

    public static bool EqualsAt<T>(this List<List<T>> values, Point point, T t) where T : notnull =>
        values.IsInBounds(point) && values.At(point).Equals(t);

    public static bool EqualsAt<T>(this List<List<T>> values, Point point, IEnumerable<T> ts) where T : notnull =>
        values.IsInBounds(point) && ts.Any(t => values.At(point).Equals(t));

    public static T At<T>(this List<List<T>> values, Point point) =>
        values[point.Y][point.X];

    public static void ReplaceAt<T>(this List<List<T>> values, Point point, T value) =>
        values[point.Y][point.X] = value;
}