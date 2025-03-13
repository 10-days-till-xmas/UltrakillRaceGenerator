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
