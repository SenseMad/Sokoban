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
    public static string GetPathToStarageLocations()
    {
      return $"{Application.dataPath}/Resources/Locations";
    }

    /// <summary>
    /// �������� ���� �� �������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    public static string GetPathToStorageLevels(Location parLocation)
    {
      return $"Assets/Resources/Locations/{parLocation}";
    }

    /// <summary>
    /// �������� ���� �� �������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    /// <param name="parNumLevel">����� ������</param>
    public static string GetPathToStorageLevels(Location parLocation, int parNumLevel)
    {
      return $"Assets/Resources/Locations/{parLocation}/{parLocation}_{parNumLevel}.asset";
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

      string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { path });

      if (!levelTable.ContainsKey(parLocation))
        levelTable[parLocation] = 0;

      levelTable[parLocation] = guids.Length;
    }

    /// <summary>
    /// �������� ��� ���������� ������� �� ������ �������
    /// </summary>
    public static void GetFullNumberLevelsLocation()
    {
      for (int i = 0; i < System.Enum.GetValues(typeof(Location)).Length; i++)
      {
        DetermineNumberLevelsLocation((Location)i);
      }
    }

    //======================================

    /// <summary>
    /// �������� ������� �� ������� � ������
    /// </summary>
    /// <param name="parLocation">�������</param>
    /// <param name="parNumLevel">����� ������</param>
    public static LevelData GetLevelData(Location parLocation, int parNumLevel)
    {
      string path = GetPathToStorageLevels(parLocation, parNumLevel);

      LevelData levelData = AssetDatabase.LoadAssetAtPath<LevelData>(path);

      if (levelData == null)
      {
        Debug.LogError($"������� {parLocation} � ������� {parNumLevel} �� �������!");
        return null;
      }

      return levelData;
    }

    /// <summary>
    /// �������� ������ �������
    /// </summary>
    public static List<Location> GetListLocation()
    {
      List<Location> listLocations = new List<Location>();

      string path = GetPathToStarageLocations();
      string[] folderNames = Directory.GetDirectories(path);

      foreach (var location in System.Enum.GetValues(typeof(Location)))
      {
        foreach (string folderName in folderNames)
        {
          if (location.ToString() != Path.GetFileName(folderName))
            continue;

          listLocations.Add((Location)location);
        }
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
      string path = GetPathToStorageLevels(parLocation);

      string[] assetGuids = AssetDatabase.FindAssets("t:LevelData", new string[] { path });

      foreach (var assetGuid in assetGuids)
      {
        string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);

        LevelData tempLevelData = AssetDatabase.LoadAssetAtPath<LevelData>(assetPath);

        levelData.Add(tempLevelData);
      }

      return levelData;
    }

    //======================================
  }
}