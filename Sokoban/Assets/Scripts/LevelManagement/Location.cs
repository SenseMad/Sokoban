using System;

public class GetLocation
{
  /// <summary>
  /// Получить названия всех локаций
  /// </summary>
  public static Location[] GetNamesAllLocation()
  {
    return (Location[])Enum.GetValues(typeof(Location));
  }
}

/// <summary>
/// Локации
/// </summary>
[Serializable]
public enum Location
{
  Summer,
  Winter
}