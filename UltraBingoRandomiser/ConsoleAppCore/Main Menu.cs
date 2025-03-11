namespace UltrakillRaceGenerator.ConsoleAppCore;

internal class MainMenu : Singleton<MainMenu>
{
    public Dictionary<string, Action> optionFuncs = [];

    public KeyValuePair<string, Action>[] defaultOptions = [
        new KeyValuePair<string, Action>( "Exit", () => Environment.Exit(0) )
    ];

    private Dictionary<string, Action> Options
    {
        get => new([.. optionFuncs, .. defaultOptions]);
    }

    internal void LoadMenu(int? index = null)
    {
        if (index.HasValue)
        {
            if (Options.ElementAtOrDefault(index.Value) is KeyValuePair<string, Action> option)
            {
                option.Value();
            }
            else
            {
                throw new Exception("Unknown option selected. Very unexpected...");
            }
        }

        Console.Clear();
        Console.WriteLine("Select one of the following options to proceed:");

        OptionSelector selector = new([.. Options.Select(kvp => kvp.Key)]);
        selector.PrintOptionSelector(true, out string selectedOption);

        Console.Clear();
        if (Options.TryGetValue(selectedOption, out Action? selectedAction))
        {
            selectedAction();
        }
        else
        {
            throw new Exception("Unknown option selected. Very unexpected...");
        }

        Console.WriteLine("Press any key to return to the main menu");
        Console.ReadKey(true);
        LoadMenu();
    }
}
