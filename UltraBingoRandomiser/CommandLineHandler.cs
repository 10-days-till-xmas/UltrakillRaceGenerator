using System.CommandLine;
using UltrakillRaceGenerator.RaceGenerators;

namespace UltrakillRaceGenerator;

internal class CommandLineHandler
{
    RootCommand rootCommand = new("Ultrasaturday race generator");

    Option<bool> copyOption = new(
        aliases: ["-c", "--copy"],
        description: "Copy the output to the clipboard",
        getDefaultValue: () => true);


    public RootCommand HandleCommandLine()
    {
        rootCommand.AddGlobalOption(copyOption);

        AddBingoCommand(rootCommand);
        AddStandardRaceCommand(rootCommand);

        return rootCommand;
    }


    private Command AddBingoCommand(RootCommand rootCommand)
    {
        var listPrintOption = new Option<bool>("--list", "Print the bingo as a list instead of a json");

        var bingoBoardCommand = new Command("bingo", "Generate a new bingo board");
        bingoBoardCommand.AddOption(listPrintOption);
        bingoBoardCommand.SetHandler(
            (bool list, bool copy) =>
            {
                PrintNewBingoBoard(copy, !list);
            },
            listPrintOption, copyOption
            );
        rootCommand.AddCommand(bingoBoardCommand);
        return bingoBoardCommand;
    }
    private Command AddStandardRaceCommand(RootCommand rootCommand)
    {
        var mlCountOption = new Option<int>(
            aliases: ["-mlc", "--multilevelcount"],
            description: "The number of multilevel runs to be generated",
            getDefaultValue: () => 4);
        var ilCountOption = new Option<int>(
            aliases: ["-ilc", "--ilcount"],
            description: "The number of IL runs to be generated",
            getDefaultValue: () => 5);

        var standardRaceCommand = new Command("standard", "Generate a list of randomised standard races");
        standardRaceCommand.AddOption(mlCountOption);
        standardRaceCommand.AddOption(ilCountOption);

        standardRaceCommand.SetHandler(
            PrintNewStandardRace,
            mlCountOption, ilCountOption, copyOption
            );

        rootCommand.AddCommand(standardRaceCommand);
        return standardRaceCommand;
    }

    private static void PrintNewBingoBoard(bool copy = true, bool asJson = true)
    {
        string bingo = UltraBingo.GenerateBingoBoardAsString(convertToJson: asJson);
        Console.WriteLine(bingo);
        if (copy)
        {
            string bingoJson = UltraBingo.GenerateBingoBoardAsJsonString();
            ClipboardUtility.SetText(bingoJson);
        }
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
            ClipboardUtility.SetText(string.Join('\n', strings));
        }
    }
}
