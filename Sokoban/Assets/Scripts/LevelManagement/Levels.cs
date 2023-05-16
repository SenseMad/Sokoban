using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Sokoban.LevelManagement
{
  /// <summary>
  /// �����, ������� ������ ������
  /// </summary>
  [System.Serializable]
  public sealed class Levels
  {
    /// <summary>
    /// ������� ���������� ������� �� �������
    /// </summary>
    private readonly static Dictionary<Location, int> levelTable = new Dictionary<Location, int>();

    //======================================

    /// <summary>
    /// ������� ������ ���������� ������
    /// </summary>
    public static LevelData CurrentSelectedLevelData;

    //======================================

    /// <summary>
    /// �������� ���� �� �������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    public static string GetPathToStorageLevels(Location parLocation)
    {
      return $"Locations/{parLocation}";
    }

    /// <summary>
    /// �������� ���� �� �������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    /// <param name="parNumLevel">����� ������</param>
    public static string GetPathToStorageLevels(Location parLocation, int parNumLevel)
    {
      return $"Locations/{parLocation}/{parLocation}_{parNumLevel}";
    }

    //======================================

    /// <summary>
    /// �������� ���������� ������� �� �������
    /// </summary>
    public static int GetNumberLevelsLocation(Location parLocation)
    {
      if (!levelTable.ContainsKey(parLocation))
      {
        Debug.LogError($"������� {parLocation} �� �������!");
        return 0;
      }

      return levelTable[parLocation];
    }

    //======================================

    /// <summary>
    /// ���������� ���������� ������� �� ������� ��� ������ ����
    /// </summary>
    /// <param name="parLocation">�������</param>
    private static void DetermineNumberLevelsLocation(Location parLocation)
    {
      string path = GetPathToStorageLevels(parLocation);

      LevelData[] levelData = Resources.LoadAll<LevelData>(path);

      if (!levelTable.ContainsKey(parLocation))
        levelTable[parLocation] = 0;

      levelTable[parLocation] = levelData.Length;
    }

    /// <summary>
    /// �������� ��� ���������� ������� �� ������ �������
    /// </summary>
    public static void GetFullNumberLevelsLocation()
    {
      for (int i = 0; i < GetLocation.GetNamesAllLocation().Length; i++)
      {
        DetermineNumberLevelsLocation((Location)i);
      }
    }

    //======================================

    /// <summary>
    /// �������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    /// <param name="parNumLevel">����� ������</param>
    public static LevelData GetLevelData(Location parLocation, int parNumLevel)
    {
      string path = GetPathToStorageLevels(parLocation, parNumLevel);

      LevelData levelData = Resources.Load<LevelData>(path);

      if (levelData == null)
      {
        Debug.LogError($"������� {parLocation} � ������� {parNumLevel} �� �������!");
        return null;
      }

      return levelData;
    }

    /// <summary>
    /// �������� ������ ���������� ������
    /// </summary>
    /// <param name="parCurrentLocation">������� �������</param>
    /// <param name="parCurrentLevel">������� �������</param>
    public static LevelData GetNextLevelData(Location parCurrentLocation, int parCurrentLevel)
    {
      if (parCurrentLevel < GetNumberLevelsLocation(parCurrentLocation))
        return GetLevelData(parCurrentLocation, parCurrentLevel + 1); // ������� ������� ������� � ����� �������

      if ((int)parCurrentLocation + 1 <= GetLocation.GetNamesAllLocation().Length - 1)
        return GetLevelData(parCurrentLocation + 1, 1); // ������� ����� ������� � ������ �������

      return GetLevelData(parCurrentLocation, parCurrentLevel); // ������� ������� ������� � ������� �������
    }

    /// <summary>
    /// �������� ������ �������
    /// </summary>
    public static List<Location> GetListLocation()
    {
      List<Location> listLocations = new List<Location>();

      foreach (var location in GetLocation.GetNamesAllLocation())
      {
        listLocations.Add(location);
      }

      return listLocations;
    }

    /// <summary>
    /// �������� ������ ������ �� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    public static List<LevelData> GetListLevelData(Location parLocation)
    {
      List<LevelData> levelData = new List<LevelData>();

      LevelData[] tempLevelData = Resources.LoadAll<LevelData>(GetPathToStorageLevels(parLocation));

      for (int i = 0; i < tempLevelData.Length; i++)
      {
        levelData.Add(tempLevelData[i]);
      }

      return levelData;
    }

    //======================================
  }
}