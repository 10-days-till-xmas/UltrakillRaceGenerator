namespace UltrakillRaceGenerator.RaceGenerators;

internal enum BaseRaceCategory
{
    Any,
    Nomo,
    P
}

[Flags]
internal enum CategoryModifiers
{
    None = 0,
    Inbounds = 1,
    Weaponless = 2,
    Check = 4 // This generally isn't combined with P%
}

internal record Category(string Name, BaseRaceCategory BaseCategory = BaseRaceCategory.Any, CategoryModifiers Modifiers = CategoryModifiers.None)
{
    public override string ToString()
    {
        return Name;
    }
    
    public static Category Any = new("Any");
    public static Category P = new("P%", BaseRaceCategory.P);
    public static Category Nomo = new("Nomo%", BaseRaceCategory.Nomo);
    public static Category Nomow = new("Nomow%", BaseRaceCategory.Nomo, CategoryModifiers.Weaponless);
    public static Category InboundsAny = new("Inbounds Any%", Modifiers:CategoryModifiers.Inbounds);
    public static Category InboundsP = new("Inbounds P%", BaseRaceCategory.P, CategoryModifiers.Inbounds);
    public static Category InboundsNomo = new("Inbounds Nomo%", BaseRaceCategory.Nomo, CategoryModifiers.Inbounds);
    public static Category Weaponless = new("Weaponless", Modifiers: CategoryModifiers.Weaponless);
    public static Category Check = new("Check%", Modifiers: CategoryModifiers.Check);
}
