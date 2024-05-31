using Newtonsoft.Json;

namespace Shipwrecked.Domain;

/// <summary>
/// Utility class of helper methods
/// </summary>
public static class Util
{
    /// <summary>
    /// Makes a deep clone of an object. Useful for reducers
    /// </summary>
    public static T Clone<T>(T obj) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj))!;
}