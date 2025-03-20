using UltrakillRaceGenerator.ExtensionClasses;
using System.Text.Json;


namespace UltrakillRaceGenerator.RaceGenerators;

// TODO: enable the ability to change the json file and read it again
internal static class StandardRaces
{
    private static string[] baseList = [];
    private static string[] specialsList = [];
    private static Dictionary<string, List<string>> baseLevels = [];
    private static string[] specialLevels = [];

    private static bool hasJsonBeenChecked = false;

    private static readonly Dictionary<CategoryDefaults, Category[]> Categories = new()
    {
        { CategoryDefaults.Any, [Category.Any] },
        { CategoryDefaults.P, [Category.P] },
        { CategoryDefaults.Nomo, [Category.Nomo, Category.Nomow] },
        { CategoryDefaults.Inbounds, [Category.InboundsAny, Category.InboundsNomo, Category.InboundsP] },
        { CategoryDefaults.Weaponless, [Category.Weaponless] },
        { CategoryDefaults.Check, [Category.Check] }
    };

    internal static Random random = new();
    internal static string GetRandomLevel()
    {
        CheckIfLevelListsAreInstantiated();
        // Selects a random layer, and then a random multiLevel, so each layer has an equal chance of being selected
        // However, not every multiLevel has an equal chance of being selected as a result

        string[] choices = [.. baseLevels.Values.Select(layer => random.Choice(layer))];
        return random.Choice(choices);
    }

    internal static string[] GetRandomMultilevelRace(int count, CategoryDefaults categories)
    {
        CheckIfLevelListsAreInstantiated();

        IEnumerable<Category[]> selectedCategories = Categories.Where(pair => categories.HasFlag(pair.Key)).Select(pair => pair.Value);
        IEnumerable<string> selectedRaceList2 = selectedCategories.Select(catGroup => random.Choice(baseList) + " " + random.Choice(catGroup));
        
        IEnumerable<string> selectedRaceList = from catGroup in selectedCategories
                                               from category in catGroup
                                               from multiLevel in baseList
                                               select multiLevel + " " + category;

        if (categories.HasFlag(CategoryDefaults.Special))
        {
            selectedRaceList2 = selectedRaceList2.Append(random.Choice(specialsList));
        }

        string[] Races1 = random.GetUniqueItems(selectedRaceList2.ToArray(), count).ToArray();
        return Races1;
    }
    internal static string[] GetRandomILRaces(int count, CategoryDefaults categories)
    {
        CheckIfLevelListsAreInstantiated();

        IEnumerable<string> selectedRaceList =
            Categories
            .Where(pair => categories.HasFlag(pair.Key))
            .Select(pair => pair.Value)
            .Select(catGroup => GetRandomLevel() + " " + random.Choice(catGroup))
            .Concat(categories.HasFlag(CategoryDefaults.Special) ? [random.Choice(specialLevels)] : []);

        string[] Races2 = random.GetUniqueItems(selectedRaceList.ToArray(), count).ToArray();
        return Races2;
    }

    internal static string[] GetRandomStandardRaces(int mlCount = 4, int ilCount = 5)
    {
        // Multilevel Race

        string[] MultilevelRaces = GetRandomMultilevelRace(mlCount, CategoryDefaults.All & ~CategoryDefaults.Check);

        // IL Race

        string[] ILRaces = GetRandomILRaces(ilCount,
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

    private static void CheckIfLevelListsAreInstantiated()
    {
        if (!hasJsonBeenChecked)
        {
            GetLevelListsFromJson();
            hasJsonBeenChecked = true;
        }
    }

    internal static bool GetLevelListsFromJson()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "StandardRaceOptions.json");
        try
        {
            string rawStandardRaceOptions = File.ReadAllText(filePath);
            using JsonDocument doc = JsonDocument.Parse(rawStandardRaceOptions);
            JsonElement root = doc.RootElement;
            baseList = JsonSerializer.Deserialize<string[]>(root.GetProperty("Base Multilevel").GetRawText()) ?? [];
            specialsList = JsonSerializer.Deserialize<string[]>(root.GetProperty("Special Multilevel").GetRawText()) ?? [];
            baseLevels = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(root.GetProperty("Base Levels").GetRawText()) ?? [];
            specialLevels = JsonSerializer.Deserialize<string[]>(root.GetProperty("Special Levels").GetRawText()) ?? [];
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read BingoBoardOptions.json: {ex.Message}");
            return false;
        }
    }
}
