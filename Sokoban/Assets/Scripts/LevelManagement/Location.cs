using System;

public class GetLocation
{
  /// <summary>
  /// �������� �������� ���� �������
  /// </summary>
  public static Location[] GetNamesAllLocation()
  {
    return (Location[])Enum.GetValues(typeof(Location));
  }
}

/// <summary>
/// �������
/// </summary>
[Serializable]
public enum Location
{
  Summer,
  Winter
}