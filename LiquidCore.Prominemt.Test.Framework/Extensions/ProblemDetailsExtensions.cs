using LiquidCore.Prominemt.Model.Domain.Errors;
using LiquidCore.Prominemt.Model.Helpers;

namespace LiquidCore.Prominemt.Test.Framework.Extensions;

/// <summary>
/// Problem details extensions
/// </summary>
public static class ProblemDetailsExtensions
{
    /// <summary>
    /// Parse errors from extensions
    /// </summary>
    /// <param name="extensions">Problem details extensions. Instance <see cref="IDictionary{TKey,TValue}"/></param>
    /// <returns>Array of <see cref="Error"/></returns>
    public static Error[] ParseErrors(this IDictionary<string, object?> extensions)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        options.Converters.Add(new ArrayJsonConverter<Error>());

        return JsonSerializer.Deserialize<IReadOnlyCollection<Error>>(extensions["errors"]?.ToString() ?? string.Empty, options)?.ToArray() ?? [];
    }
}