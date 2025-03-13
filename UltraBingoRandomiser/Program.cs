using UltrakillRaceGenerator.ConsoleAppCore;
using UltrakillRaceGenerator.RaceGenerators;

namespace UltrakillRaceGenerator;
internal class Program
{

    private static void Main(string[] args) // TODO: Add command line arguments to automatically select an option in the menu
    {
        MainMenu.Instance.optionFuncs.Add("Generate Bingo Board", PrintNewBingoBoard);
        MainMenu.Instance.optionFuncs.Add("Generate Standard Race Options", PrintNewStandardRace);
        MainMenu.Instance.LoadMenu();
    }

    private static void PrintNewBingoBoard()
    {
        string bingo = UltraBingo.GenerateBingoBoardAsJsonString();
        Console.WriteLine(bingo);
        ClipboardUtility.SetText(bingo);
    }

    private static void PrintNewStandardRace()
    {
        string[] strings = StandardRaces.GetRandomStandardRaces();
        foreach (string s in strings)
        {
            Console.WriteLine(s);
        }
    }
}