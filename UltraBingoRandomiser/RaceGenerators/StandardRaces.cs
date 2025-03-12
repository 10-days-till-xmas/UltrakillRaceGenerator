using UltrakillRaceGenerator.ExtensionClasses;

namespace UltrakillRaceGenerator.RaceGenerators;

[Flags]
internal enum CategoryDefaults
{
    None = 0,
    Any = 1,
    P = 2,
    Nomo = 4,
    Nomow = 8,
    InboundsAny = 16,
    InboundsP = 32,
    InboundsNomo = 64,
    Inbounds = InboundsAny | InboundsP,
    Weaponless = 128,
    Check = 256,
    Special = 512,
    All = Any | P | Nomo | Nomow | InboundsAny | InboundsP | InboundsNomo | Weaponless | Check | Special
}

// TODO: clean this up, make it read from a json file and add it as a menu option
internal static class StandardRaces
{
    private static readonly string[] baseList = ["Fullgame", "Act1", "Act2"];
    private static readonly string[] specialsList = [
        "Fullgame All Bosses", 
        "Fullgame All levels", 
        "Newgame Prelude", 
        "All Encores"
        ];

    private static readonly Dictionary<CategoryDefaults, Category[]> Categories = new()
    {
        { CategoryDefaults.Any, [Category.Any] },
        { CategoryDefaults.P, [Category.P] },
        { CategoryDefaults.Nomo, [Category.Nomo, Category.Nomow] },
        { CategoryDefaults.Inbounds, [Category.InboundsAny, Category.InboundsNomo, Category.InboundsP] },
        { CategoryDefaults.Weaponless, [Category.Weaponless] },
        { CategoryDefaults.Check, [Category.Check] }
    };

    private static readonly Dictionary<string, string[]> baseLevels = new()
    {
        {"Prelude", ["0-1", "0-2", "0-3", "0-4", "0-5"]},
        {"Limbo", ["1-1", "1-2", "1-3", "1-4"]},
        {"Lust", ["2-1", "2-2", "2-3", "2-4"]},
        {"Gluttony", ["3-1", "3-2"]},
        {"Greed", ["4-1", "4-2", "4-3", "4-4"]},
        {"Wrath", ["5-1", "5-2", "5-3", "5-4"]},
        {"Heresy", ["6-1", "6-2"]},
        {"Violence", ["7-1", "7-2", "7-3", "7-4"]},
        {"Sanctum", ["P-1", "P-2"]},
        {"Encore", ["0-E", "1-E"]}
    };
    private static readonly string[] specialLevels = [
        "0-S",
        "1-S",
        "2-S",
        "4-S",
        "5-S",
        "0-2 Secret Exit",
        "1-1 Secret Exit",
        "2-3 Secret Exit",
        "3-1 Secret Exit",
        "4-2 Secret Exit",
        "5-1 Secret Exit",
        "6-2 Secret Exit",
        "1-2 Rodent%",
        "0-2 Secret Encounter",
        "1-3 Agony and Tundra",
        "1-4 Hank%",
        "1-4 All Skulls",
        "2-3 Break All power boxes",
        "3-2 Drop Gaberiel in pit",
        "4-2 Sandcastle%",
        "4-3 Druid Knight",
        "5-2 Sacrifice Florp",
        "5-3 Make Hank Jr",
        "5-4 No jumping",
        "6-1 Angry and Rude",
        "6-2 Drop Gabriel in the pit"];

    internal static Random random = new();
    internal static string GetRandomLevel()
    {
        // Selects a random layer, and then a random level, so each layer has an equal chance of being selected
        // However, not every level has an equal chance of being selected as a result
        string[] choices = [.. baseLevels.Values.Select(layer => random.Choice(layer))];
        return random.Choice(choices);
    }

    internal static string[] GetRandomMultilevelRace(int count, CategoryDefaults categories)
    {
        IEnumerable<Category[]> selectedCategories = Categories.Where(pair => categories.HasFlag(pair.Key)).Select(pair => pair.Value);
        IEnumerable<string> selectedRaceList = selectedCategories.Select(catGroup => random.Choice(baseList) + random.Choice(catGroup));

        if (categories.HasFlag(CategoryDefaults.Special))
        {
            selectedRaceList = selectedRaceList.Append(random.Choice(specialsList));
        }

        string[] Races1 = random.GetItems(selectedRaceList.ToArray(), count);
        return Races1;
    }
    internal static string[] GetRandomILRaces(int count, CategoryDefaults categories)
    {
        IEnumerable<string> selectedRaceList =
            Categories
            .Where(pair => categories.HasFlag(pair.Key))
            .Select(pair => pair.Value)
            .Select(catGroup => GetRandomLevel() + " " + random.Choice(catGroup))
            .Concat(categories.HasFlag(CategoryDefaults.Special) ? [random.Choice(specialLevels)] : []);

        string[] Races2 = random.GetItems(selectedRaceList.ToArray(), count);
        return Races2;
    }

    internal static string[] GetRandomStandardRaces()
    {
        // Multilevel Race

        string[] MultilevelRaces = GetRandomMultilevelRace(4, CategoryDefaults.All & ~CategoryDefaults.Check);

        // IL Race

        string[] ILRaces = GetRandomILRaces(5,
            CategoryDefaults.Any | 
            CategoryDefaults.P | 
            CategoryDefaults.Nomo | 
            CategoryDefaults.Weaponless |
            CategoryDefaults.InboundsAny |
            CategoryDefaults.Check |
            CategoryDefaults.Special
            );

        return [
            ..MultilevelRaces,
            "-----",
            ..ILRaces
            ];
    }
}
