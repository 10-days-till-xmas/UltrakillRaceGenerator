namespace UltrakillRaceGenerator;
internal static class ConsoleUtility
{
    internal static void ClearLine(int lineNumber)
    {
        (int oldLeft, int oldTop) = Console.GetCursorPosition();
        Console.SetCursorPosition(0, lineNumber);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(oldLeft, oldTop);
    }
}
