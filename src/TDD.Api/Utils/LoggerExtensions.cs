using System.Text.Json;

namespace TDD.Api.Utils
{
    public static class LoggerExtensions
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public static void LogJson<T>(this ILogger logger, string message, T obj)
    {
        var json = JsonSerializer.Serialize(obj, _jsonOptions);
        logger.LogInformation($"{message}: {json}");
    }
    }
}