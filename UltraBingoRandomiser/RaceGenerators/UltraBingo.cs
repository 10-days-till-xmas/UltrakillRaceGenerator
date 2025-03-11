using System.Text.Json;

namespace UltrakillRaceGenerator.RaceGenerators;

internal class UltraBingo
{
    private record BingoTile(string name);

    private static readonly JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };

    internal static (string[] Easy, string[] Medium, string[] Hard) GetBingoBoardOptions()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "BingoBoardOptions.json");
        try
        {
            string rawBingoBoardOptions = File.ReadAllText(filePath);
            Dictionary<string, List<string>>? data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(rawBingoBoardOptions);
            return (data?["Easy"]?.ToArray() ?? [], data?["Medium"]?.ToArray() ?? [], data?["Hard"]?.ToArray() ?? []);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read BingoBoardOptions.json: {ex.Message}");
            return ([], [], []);
        }
    }

    internal static string GenerateBingoBoard(string[] easy, string[] medium, string[] hard)
    {
        var Easy = new Queue<string>(Random.Shared.GetItems(easy, 12));
        var Medium = new Queue<string>(Random.Shared.GetItems(medium, 12));
        var Hard = new Queue<string>(Random.Shared.GetItems(hard, 1));

        string[] Layout = [
            "E","E","M","E","E",
            "E","M","M","M","E",
            "M","M","H","M","M",
            "E","M","M","M","E",
            "E","E","M","E","E"];

        BingoTile[] Board = Layout.Select<string, BingoTile>(difficulty => difficulty switch
        {
            "E" => new(Easy.Dequeue()),
            "M" => new(Medium.Dequeue()),
            "H" => new(Hard.Dequeue()),
            _ => throw new Exception($"{difficulty} is not a valid difficulty")
        }).ToArray();

        string json = JsonSerializer.Serialize(Board, jsonSerializerOptions);

        return json;
    }
}
