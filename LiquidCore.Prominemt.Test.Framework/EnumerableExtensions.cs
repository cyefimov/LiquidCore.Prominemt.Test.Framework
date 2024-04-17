namespace LiquidCore.Prominemt.Test.Framework;

/// <summary>
/// IEnumerable extensions
/// </summary>
public static class EnumerableExtensions
{   
    private static readonly Random Rand = new();
    private static T RandomElement<T>(this IReadOnlyList<T> array) => array[Rand.Next(0, array.Count - 1)];

    /// <summary>
    /// Get random element from list
    /// </summary>
    /// <typeparam name="T">Type of list elements</typeparam>
    /// <param name="list">List of elements. Instance of <see cref="List{T}"/></param>
    /// <returns>Element. Instance of <typeparamref name="T"/>. Null if list is empty</returns>
    public static T RandomElement<T>(this IEnumerable<T> list) => list.ToArray().RandomElement();
}