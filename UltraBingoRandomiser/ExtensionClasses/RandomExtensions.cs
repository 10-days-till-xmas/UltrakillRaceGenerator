using System.Runtime.CompilerServices;

namespace UltrakillRaceGenerator.ExtensionClasses;
public static class RandomExtensions
{
    public static T Choice<T>(this Random random, IEnumerable<T> choices)
    {
        return random.GetItems([.. choices], 1)[0];
    }

    public static IEnumerable<T> GetUniqueItems<T>(this Random random, T[] choices, int count)
    {
        switch(count)
        {
            case < 0:
                throw new ArgumentException("Count must be greater than or equal to 0");
            case 0:
                return [];
            case 1:
                return [random.Choice(choices)];
        }

        int length = choices.Length;
        
        if (length < count)
        {
            throw new ArgumentException("Not enough choices to select from");
        }
        else if (length == count)
        {
            return choices;
        }

        int[] indexRange = Enumerable.Range(0, length).ToArray();
        random.Shuffle(indexRange);
        var selectedIndices = indexRange[0..count];
        return choices.GetItemsWithIndices(selectedIndices);

    }
    private static IEnumerable<T> GetItemsWithIndices<T>(this T[] array, IEnumerable<int> indices)
    {
        foreach (var index in indices)
        {
            yield return array[index];
        }
    }
}
