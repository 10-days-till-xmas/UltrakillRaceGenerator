using UltrakillRaceGenerator.ExtensionClasses;

namespace UltrakillRaceGenerator.RaceGenerators;

internal static class StandardRaces
{
    private static readonly string[] baseList = ["Fullgame", "Act1", "Act2"];
    private static readonly string[] specialsList = ["Fullgame All Bosses", "Fullgame All levels", "Newgame Prelude", "All Encores"];

    private static readonly string[][] categories = [["Any"], ["P%"], ["Nomo", "Nomow"], ["Inbounds Any%", "Inbounds P%"], ["Weaponless"], ["Check%"]];

    internal static string[] allRaces = [..from catGroup in categories
                                        from cat in catGroup
                                        from len in baseList
                                        select $"{len} {cat}"];
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


    internal static Random random = new();
    internal static string GetRandomLevel()
    {
        string[] choices = [.. baseLevels.Values.Select(layer => random.Choice(layer))];
        return random.Choice(choices);
    }

    internal static string[] GetRandomStandardRaces()
    {
        // Race1
        string[] Slist = ["Fullgame All Bosses", "Fullgame All levels", "Newgame Prelude", "All Encores"];

        string Any = random.Choice(baseList) + " Any%";
        string P = random.Choice(baseList) + " P%";
        string Nomo = random.Choice([.. baseList.Select(category => $"{category} Nomo%"), .. baseList.Select(category => $"{category} Nomow%")]);
        string Weaponless = random.Choice(baseList) + "Weaponless%";
        string Inbounds = random.Choice([.. baseList.Select(cat => $"{cat} Inbounds Any%"), .. baseList.Select(cat => $"{cat} Inbounds P%")]);
        string Check = random.Choice(baseList) + " Check%";
        string Special = random.Choice(Slist);
        string[] Racelist1 = [Any, P, Nomo, Weaponless, Inbounds, Special, Check];

        string[] Races1 = random.GetItems(Racelist1, 4);
        var Race1 = Races1[0];
        var Race2 = Races1[1];
        var Race3 = Races1[2];
        var Race4 = Races1[3];

        // Race Levels

        string any = GetRandomLevel() + " Any%";
        string p = GetRandomLevel() + " P%";
        string nomo = GetRandomLevel() + " Nomo%";
        string weaponless = GetRandomLevel() + "Weaponless%";
        string inbounds = GetRandomLevel() + "Inbounds Any%";
        string[] specialList = [
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
        string special = random.Choice(specialList);
        string check = GetRandomLevel() + " Check%";

        string[] RaceList2 = [any, p, nomo, weaponless, inbounds, special, check];
        string[] Races2 = random.GetItems(RaceList2, 5);

        string Race5 = Races2[0];
        string Race6 = Races2[1];
        string Race7 = Races2[2];
        string Race8 = Races2[3];
        string Race9 = Races2[4];

        return [
            Race1,
            Race2,
            Race3,
            Race4,
            "-----",
            Race5,
            Race6,
            Race7,
            Race8,
            Race9
            ];
    }
}
