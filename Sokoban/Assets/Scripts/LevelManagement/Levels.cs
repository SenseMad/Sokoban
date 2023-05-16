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
    /// Получить путь до хранения уровней
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public static string GetPathToStorageLevels(Location parLocation)
    {
      return $"Locations/{parLocation}";
    }

    /// <summary>
    /// Получить путь до хранения уровней
    /// </summary>
    /// <param name="parLocation">Локация</param>
    /// <param name="parNumLevel">Номер уровня</param>
    public static string GetPathToStorageLevels(Location parLocation, int parNumLevel)
    {
      return $"Locations/{parLocation}/{parLocation}_{parNumLevel}";
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

      LevelData[] levelData = Resources.LoadAll<LevelData>(path);

      if (!levelTable.ContainsKey(parLocation))
        levelTable[parLocation] = 0;

      levelTable[parLocation] = levelData.Length;
    }

    /// <summary>
    /// Получить все количество уровней на каждой локации
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
    /// Получить уровень
    /// </summary>
    /// <param name="parLocation">Локация</param>
    /// <param name="parNumLevel">Номер уровня</param>
    public static LevelData GetLevelData(Location parLocation, int parNumLevel)
    {
      string path = GetPathToStorageLevels(parLocation, parNumLevel);

      LevelData levelData = Resources.Load<LevelData>(path);

      if (levelData == null)
      {
        Debug.LogError($"Локация {parLocation} с номером {parNumLevel} не найдена!");
        return null;
      }

      return levelData;
    }

    /// <summary>
    /// Получить данные следующего уровня
    /// </summary>
    /// <param name="parCurrentLocation">Текущая локация</param>
    /// <param name="parCurrentLevel">Текущий уровень</param>
    public static LevelData GetNextLevelData(Location parCurrentLocation, int parCurrentLevel)
    {
      if (parCurrentLevel < GetNumberLevelsLocation(parCurrentLocation))
        return GetLevelData(parCurrentLocation, parCurrentLevel + 1); // Вернуть текущую локацию и новый уровень

      if ((int)parCurrentLocation + 1 <= GetLocation.GetNamesAllLocation().Length - 1)
        return GetLevelData(parCurrentLocation + 1, 1); // Вернуть новую локацию и первый уровень

      return GetLevelData(parCurrentLocation, parCurrentLevel); // Вернуть текущую локацию и текущий уровень
    }

    /// <summary>
    /// Получить список локаций
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
    /// Получить список данных об уровнях
    /// </summary>
    /// <param name="parLocation">Локация</param>
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