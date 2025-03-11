namespace UltrakillRaceGenerator.ExtensionClasses;
public static class RandomExtensions
{
    public static T Choice<T>(this Random random, IEnumerable<T> choices)
    {
        return random.GetItems([.. choices], 1)[0];
    }
}
