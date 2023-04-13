using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Sokoban.LevelManagement
{
  /// <summary>
  /// Класс, который хранит уровни
  /// </summary>
  [System.Serializable]
  public sealed class Levels
  {
    /// <summary>
    /// Таблица количества уровней на локации
    /// </summary>
    private readonly static Dictionary<Location, int> levelTable = new Dictionary<Location, int>();

    //======================================

    /// <summary>
    /// Текущие данные выбранного уровня
    /// </summary>
    public static LevelData CurrentSelectedLevelData;

    //======================================

    /// <summary>
    /// Получить путь до хранения локаций
    /// </summary>
    public static string GetPathToStarageLocations()
    {
      return $"{Application.dataPath}/Resources/Locations";
    }

    /// <summary>
    /// Получить путь до хранения уровней
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public static string GetPathToStorageLevels(Location parLocation)
    {
      return $"Assets/Resources/Locations/{parLocation}";
    }

    /// <summary>
    /// Получить путь до хранения уровней
    /// </summary>
    /// <param name="parLocation">Локация</param>
    /// <param name="parNumLevel">Номер уровня</param>
    public static string GetPathToStorageLevels(Location parLocation, int parNumLevel)
    {
      return $"Assets/Resources/Locations/{parLocation}/{parLocation}_{parNumLevel}.asset";
    }

    //======================================

    /// <summary>
    /// Получить количество уровней на локации
    /// </summary>
    public static int GetNumberLevelsLocation(Location parLocation)
    {
      if (!levelTable.ContainsKey(parLocation))
      {
        Debug.LogError($"Локация {parLocation} не найдена!");
        return 0;
      }

      return levelTable[parLocation];
    }

    //======================================

    /// <summary>
    /// Определить количество уровней на локации при старте игры
    /// </summary>
    /// <param name="parLocation">Локация</param>
    private static void DetermineNumberLevelsLocation(Location parLocation)
    {
      string path = GetPathToStorageLevels(parLocation);

      string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { path });

      if (!levelTable.ContainsKey(parLocation))
        levelTable[parLocation] = 0;

      levelTable[parLocation] = guids.Length;
    }

    /// <summary>
    /// Получить все количество уровней на каждой локации
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
    /// Получить уровень по локации и номеру
    /// </summary>
    /// <param name="parLocation">Локация</param>
    /// <param name="parNumLevel">Номер уровня</param>
    public static LevelData GetLevelData(Location parLocation, int parNumLevel)
    {
      string path = GetPathToStorageLevels(parLocation, parNumLevel);

      LevelData levelData = AssetDatabase.LoadAssetAtPath<LevelData>(path);

      if (levelData == null)
      {
        Debug.LogError($"Локация {parLocation} с номером {parNumLevel} не найдена!");
        return null;
      }

      return levelData;
    }

    /// <summary>
    /// Получить список локаций
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
    /// Получить список данных об уровнях
    /// </summary>
    /// <param name="parLocation">Локация</param>
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