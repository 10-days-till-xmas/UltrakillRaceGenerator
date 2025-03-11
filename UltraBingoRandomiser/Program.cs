using UltrakillRaceGenerator.ConsoleAppCore;
using UltrakillRaceGenerator.RaceGenerators;

namespace UltrakillRaceGenerator;
internal class Program
{

    private static void Main(string[] args) // TODO: Add command line arguments to automatically select an option in the menu
    {
        MainMenu.Instance.optionFuncs.Add("Generate Bingo Board", PrintNewBingoBoard);
        MainMenu.Instance.LoadMenu();
    }

    private static void PrintNewBingoBoard()
    {
        string[] easy;
        string[] medium;
        string[] hard;
        (easy, medium, hard) = UltraBingo.GetBingoBoardOptions();
        string bingo = UltraBingo.GenerateBingoBoard(easy, medium, hard);
        Console.WriteLine(bingo);
        ClipboardUtility.SetText(bingo);
    }
}