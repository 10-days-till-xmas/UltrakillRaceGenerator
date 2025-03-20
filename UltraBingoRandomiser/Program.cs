using System.CommandLine;
using UltrakillRaceGenerator.ConsoleAppCore;
using UltrakillRaceGenerator.RaceGenerators;

namespace UltrakillRaceGenerator;
internal class Program
{
    private static async Task<int> Main(string[] args)
    {


        if (args.Length == 0)
        {
            LoadDefaultMainMenu();
            return 0;
        }
        var commandLineHandler = new CommandLineHandler();
        var rootCommand = commandLineHandler.HandleCommandLine();

        return await rootCommand.InvokeAsync(args);
    }
   

    private static void LoadDefaultMainMenu()
    {
        MainMenu.Instance.optionFuncs.Add("Generate Bingo Board", PrintNewBingoBoard);
        MainMenu.Instance.optionFuncs.Add("Generate Standard Race Options", () => PrintNewStandardRace());
        MainMenu.Instance.LoadMenu();
    }

    private static void PrintNewBingoBoard()
    {
        string bingo = UltraBingo.GenerateBingoBoardAsString();
        Console.WriteLine(bingo);
        ClipboardUtility.SetText(bingo);
    }

    private static void PrintNewStandardRace(int mlCount = 4, int ilCount = 5, bool copy = false)
    {
        string[] strings = StandardRaces.GetRandomStandardRaces(mlCount, ilCount);
        foreach (string s in strings)
        {
            Console.WriteLine(s);
        }
        if (copy)
        {
            ClipboardUtility.SetText(string.Join("\n", strings));
        }
    }
}