using System.Text.Json;
using System.Text.Json.Serialization;

namespace IoTPlatform.Infrastructure.Data.Seeders;

public static class JsonSeedLoader
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static async Task<List<T>> LoadAsync<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                $"Seed data file was not found: {filePath}");
        }

        await using var stream = File.OpenRead(filePath);

        var records = await JsonSerializer.DeserializeAsync<List<T>>(
            stream,
            JsonOptions);

        return records ?? new List<T>();
    }
}